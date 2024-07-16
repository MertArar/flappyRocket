using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyUniqueNamespace
{
    public class SceneGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;
        [SerializeField] private GameObject[] sceneObjects;
        [SerializeField] private GameObject launchPadPrefab;
        [SerializeField] private GameObject landPadPrefab;
        [SerializeField] private GameObject rocket;

        void Start()
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("This script should be run in Play mode.");
                return;
            }
            /*
            // Sahne verilerini tanımla: [Arkaplan Numarası, Başlangıç X, Başlangıç Y, (obje numarası)]
            List<int[]> sceneData = new List<int[]>
            {
                new int[] { 00, 00, 00 },  // Arkaplan ve LaunchPad pozisyonu
                new int[] { 05, 05, 01 },  // Diğer prefab pozisyonları
                new int[] { 10, 10, 02 },// Diğer prefab pozisyonları
                new int[] { 10, 20, 03 },
                new int[] { 15, 15, 03 } // Bitiş ve LandPad pozisyonu
            };
            */
            List<int[]> sceneData = new List<int[]>
            {
                new int[] { 00, 10, 00 },  // Arkaplan ve LaunchPad pozisyonu
                new int[] { 05, 10, 01 },  // Diğer prefab pozisyonları
                new int[] { 00, 15, 02 },// Diğer prefab pozisyonları
                new int[] { 05, 00, 03 },
                new int[] { 00, 00, 04 } // Bitiş ve LandPad pozisyonu
            };

            List<int[]> backData = new List<int[]>
            {
                new int[] { 00, 00, 00 },
                new int[] { 05, 05, 01 }
            };

            // Sahneyi oluştur
            GenerateObjects(sceneData);
            GenerateScene(backData);

        }
        public void GenerateScene(List<int[]> backData)
        {
            for (int i = 0; i < backData.Count; i++)
            {
                int x = backData[i][0];
                int y = backData[i][1];
                int ObjIndex = backData[i][2];
                Debug.Log(ObjIndex);
                if (ObjIndex >= 0 && ObjIndex < prefabs.Length)
                {
                    Debug.Log("Oluşturuldu");
                    GameObject obj = Instantiate(sceneObjects[ObjIndex], new Vector3(x, y, 0), Quaternion.identity);
                    obj.name = "SceneObj_" + i;
                }
                else
                {
                    Debug.LogError("Invalid prefab index.");
                    return;
                }
            }

            Debug.Log("SA");

        }
        public void GenerateObjects(List<int[]> sceneData)
        {

            // Create and position LaunchPad
            Vector2 launchPadPosition = new Vector2(sceneData[0][0], sceneData[0][1]);
            GameObject launchPad = Instantiate(launchPadPrefab, launchPadPosition, Quaternion.identity);
            launchPad.name = "LaunchPad";
            
            Vector2 rocketPosition = new Vector2(launchPadPosition.x, launchPadPosition.y + 1f);
            GameObject rocketPlace = Instantiate(rocket, rocketPosition, Quaternion.identity);
            rocketPlace.name = "Rocket";

            // Create and position LandPad
            Vector2 landPadPosition = new Vector2(sceneData[sceneData.Count - 1][0], sceneData[sceneData.Count - 1][1]);
            GameObject landPad = Instantiate(landPadPrefab, landPadPosition, Quaternion.identity);
            landPad.name = "LandPad";

            // Create and position other prefabs
            for (int i = 1; i < sceneData.Count - 1; i++)
            {
                int x = sceneData[i][0];
                int y = sceneData[i][1];
                int prefabIndex = sceneData[i][2];

                if (prefabIndex >= 0 && prefabIndex < prefabs.Length)
                {
                    GameObject obj = Instantiate(prefabs[prefabIndex], new Vector3(x, y, 0), Quaternion.identity);
                    obj.name = "Prefab_" + i;
                }
                else
                {
                    Debug.LogError("Invalid prefab index.");
                    return;
                }
            }
        }
    }
}