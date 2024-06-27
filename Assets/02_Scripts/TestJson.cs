using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasySave.Json;
using EasySave.Xml;

public enum A
{
    a,
    aa
}
namespace _02_Scripts
{
    public class TestJson : MonoBehaviour
    {
        [SerializeField] private SaveData saveData;
        [SerializeField] private List<SaveData> saveDataList;
        [SerializeField] private SerializableDictionary<A, SaveData> saveDataDictionary;

        private void Start()
        {
            saveDataDictionary = new SerializableDictionary<A, SaveData>()
            {
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                EasyToJson.ToJson(saveData, "SaveData", true);
                EasyToJson.ListToJson(saveDataList, "SaveDataList", true);
                //EasyToJson.DictionaryToJson(saveDataDictionary, "SaveDataDictionary", true);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                saveData = EasyToJson.FromJson<SaveData>("SaveData");
                saveDataList = EasyToJson.ListFromJson<SaveData>("SaveDataList");
                //saveDataDictionary = EasyToJson.DictionaryFromJson<string, SaveData>("SaveDataDictionary");
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