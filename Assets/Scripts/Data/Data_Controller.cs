using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Data_Controller : MonoBehaviour
{
    public class DataController : MonoBehaviour
    {
        // ---½Ì±ÛÅæÀ¸·Î ¼±¾ð--- 
        static GameObject _container;
        static GameObject Container
        {
            get
            {
                return _container;
            }
        }
        static DataController _instance;
        public static DataController Instance
        {
            get
            {
                if (!_instance)
                {
                    _container = new GameObject();
                    _container.name = "DataController";
                    _instance = _container.AddComponent(typeof(DataController)) as DataController;
                    DontDestroyOnLoad(_container);
                }
                return _instance;
            }
        }
        public string GameDataFileName = "StarfishData.json";
        public Scene_Data _gameData;

        public Scene_Data gameData
        {
            get
            {
                if (_gameData == null)
                {
                    LoadGameData();
                    SaveGameData();
                }
                return _gameData;
            }
        }

        private void Start()
        {
            LoadGameData();
            SaveGameData();
        }

        public void LoadGameData()
        {
            string filePath = Application.persistentDataPath + GameDataFileName;

           
            if (File.Exists(filePath))
            {
                string FromJsonData = File.ReadAllText(filePath);
                _gameData = JsonUtility.FromJson<Scene_Data>(FromJsonData);
            }

            else
            {
                _gameData = new Scene_Data();
            }
        }

        public void SaveGameData()
        {
            string ToJsonData = JsonUtility.ToJson(gameData);
            string filePath = Application.persistentDataPath + GameDataFileName;

            File.WriteAllText(filePath, ToJsonData);


        }
        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }
}
