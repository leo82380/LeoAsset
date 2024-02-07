using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyJson;
using EasyXml;

namespace _02_Scripts
{
    public class TestJson : MonoBehaviour
    {
        [SerializeField] private SaveData saveData;
        [SerializeField] private List<SaveData> saveDataList;
        private Dictionary<string, List<SaveData>> saveDataDic;
        
        private void Start()
        {
            EasyToXml.ToXml(saveData, "save");
            EasyToXml.ListToXml(saveDataList, "saveList");
            saveDataDic = new Dictionary<string, List<SaveData>>();
            saveDataDic.Add("saveDic", saveDataList);
            saveData = null;
            saveDataList = null;
            StartCoroutine(XMLTest());
        }
        
        IEnumerator XMLTest()
        {
            yield return new WaitForSeconds(1f);
            saveData = EasyToXml.FromXml<SaveData>("save");
            saveDataList = EasyToXml.ListFromXml<SaveData>("saveList");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                saveData = EasyToJson.FromJson<SaveData>("save");
                saveDataList = EasyToJson.ListFromJson<SaveData>("saveList");
                saveDataDic = EasyToJson.DictionaryFromJson<string, List<SaveData>>("saveDic");
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