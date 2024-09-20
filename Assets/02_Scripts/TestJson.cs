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
        [SerializeField] private Dictionary<A, SaveData> saveDataDictionary;

        private void Start()
        {
            saveDataDictionary = new Dictionary<A, SaveData>()
            {
                {A.a, new SaveData {name = "a", level = 1, speed = 1.0f}},
                {A.aa, new SaveData {name = "aa", level = 2, speed = 2.0f}}
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset1();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                EasyToJsonEncrypt.ToJson(saveData, "SaveData");
                EasyToJsonEncrypt.ListToJson(saveDataList, "SaveDataList");
                EasyToJsonEncrypt.DictionaryToJson(saveDataDictionary, "SaveDataDictionary");
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                saveData = EasyToJsonEncrypt.FromJson<SaveData>("SaveData");
                saveDataList = EasyToJsonEncrypt.ListFromJson<SaveData>("SaveDataList");
                saveDataDictionary = EasyToJsonEncrypt.DictionaryFromJson<A, SaveData>("SaveDataDictionary");

                foreach (var VARIABLE in saveDataDictionary)
                {
                    Debug.Log(VARIABLE.Key);
                    Debug.Log(VARIABLE.Value.name);
                    Debug.Log(VARIABLE.Value.level);
                    Debug.Log(VARIABLE.Value.speed);
                }
            }
        }

        private void Reset1()
        {
            saveData = new SaveData {name = "", level = 0, speed = 0.0f};
            saveDataList = new List<SaveData>
            {
            };
            saveDataDictionary = new Dictionary<A, SaveData>()
            {
            };
            
            foreach (var VARIABLE in saveDataDictionary)
            {
                Debug.Log(VARIABLE.Key);
                Debug.Log(VARIABLE.Value.name);
                Debug.Log(VARIABLE.Value.level);
                Debug.Log(VARIABLE.Value.speed);
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