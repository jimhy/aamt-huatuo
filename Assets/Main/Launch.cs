using System;
using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main
{
    public class Launch : MonoBehaviour
    {
        public Button StartButton;
        public Button DownLoadButton;

        public string RemotePath;
        private string[] abs = { "aotdlls", "hotupdatedlls", "game" };

        void Start()
        {
            StartButton.onClick.AddListener(OnStart);
            DownLoadButton.onClick.AddListener(OnDownLoad);
        }

        private void OnStart()
        {
        }

        private void OnDownLoad()
        {
            StartCoroutine(DownLoadDlls(StartUp));
        }

        IEnumerator DownLoadDlls(Action onDownloadComplete)
        {
            var platform                                                  = "android";
            if (Application.platform != RuntimePlatform.Android) platform = "win";
            var pathRoot                                                  = $"{RemotePath}/{platform}";
            var localRoot                                                 = $"{Application.persistentDataPath}";
            foreach (var ab in abs)
            {
                string dllPath = $"{pathRoot}/{ab}";
                Debug.Log($"start download ab:{dllPath}");
                UnityWebRequest www = UnityWebRequest.Get(dllPath);
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    var abBytes  = www.downloadHandler.data;
                    var filePath = $"{localRoot}/{ab}";
                    if (File.Exists(filePath)) File.Delete(filePath);
                    File.WriteAllBytes(filePath, abBytes);
                    Debug.Log($"Write file:dll:{filePath}  size:{abBytes.Length}");
                }
            }

            onDownloadComplete();
        }

        private void StartUp()
        {
            var         hotupdatedllsPath = $"{Application.persistentDataPath}/hotupdatedlls";
            AssetBundle hotUpdateDllAb    = AssetBundle.LoadFromFile(hotupdatedllsPath);
#if !UNITY_EDITOR
        TextAsset dllBytes = hotUpdateDllAb.LoadAsset<TextAsset>("Game.dll.bytes");
        var gameAss = System.Reflection.Assembly.Load(dllBytes.bytes);
#else
            var gameAss = AppDomain.CurrentDomain.GetAssemblies().First(assembly => assembly.GetName().Name == "Game");
#endif
            var gameAbPath = $"{Application.persistentDataPath}/game";
            var mainScene  = AssetBundle.LoadFromFile(gameAbPath);
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }

        void StartGame()
        {
        }
    }
}