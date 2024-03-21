using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasySave.Json;
using EasySave.Xml;

namespace _02_Scripts
{
    public class TestJson : MonoBehaviour
    {
        [SerializeField] private SaveData saveData;
        [SerializeField] private List<SaveData> saveDataList;
        private Dictionary<string, SaveData> saveDataDictionary;

        private void Start()
        {
            saveDataDictionary = new Dictionary<string, SaveData>
            {
                {"Player1", new SaveData {name = "Player1", level = 1, speed = 1.0f}},
                {"Player2", new SaveData {name = "Player2", level = 2, speed = 2.0f}},
                {"Player3", new SaveData {name = "Player3", level = 3, speed = 3.0f}}
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                EasyToJson.ToJson(saveData, "SaveData", true);
                EasyToJson.ListToJson(saveDataList, "SaveDataList", true);
                EasyToJson.DictionaryToJson(saveDataDictionary, "SaveDataDictionary", true);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                saveData = EasyToJson.FromJson<SaveData>("SaveData");
                saveDataList = EasyToJson.ListFromJson<SaveData>("SaveDataList");
                saveDataDictionary = EasyToJson.DictionaryFromJson<string, SaveData>("SaveDataDictionary");
            }
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string name;
    public int level;
    public float speed;
}