using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace EasySave.Json
{
    public static class EasyToJsonEncrypt
    {
        /**
         * <summary>
         * Json 파일로 암호화해 저장
         * </summary>
         * <param name="obj">Json으로 저장할 객체</param>
         * <param name="jsonFileName">Json 파일 이름</param>
        */
        public static void ToJson<T>(T obj, string jsonFileName)
        {
            EasyToJson.CreateJsonFolder();
            string path = EasyToJson.GetFilePath(jsonFileName);
            string json = JsonUtility.ToJson(obj);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            string encryptData = EasyEncrypt.Encrypt(data);
            File.WriteAllText(path, encryptData);
        }
        
        /**
         * <summary>
         * 암호회 된 Json 파일을 읽어서 객체로 반환
         * </summary>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <returns>Json 파일을 읽어서 만든 객체</returns>
         */
        public static T FromJson<T>(string jsonFileName)
        {
            string path = EasyToJson.GetFilePath(jsonFileName);
            if (!File.Exists(path))
            {
                Debug.Log("파일이 존재하지 않습니다.");
                Debug.Log("파일을 생성합니다.");
                T defaultObj = default;
                ToJson(defaultObj, jsonFileName);
                return defaultObj;
            }
            string encryptData = File.ReadAllText(path);
            byte[] data = EasyEncrypt.Decrypt(encryptData);
            string json = System.Text.Encoding.UTF8.GetString(data);
            T obj = JsonUtility.FromJson<T>(json);
            return obj;
        }

        /**
         * <summary>
         * List를 암호화된 Json 파일로 저장
         * </summary>
         * <param name="list">Json으로 저장할 리스트</param>
         * <param name="jsonFileName">Json 파일 이름</param>
         */
        public static void ListToJson<T>(List<T> list, string jsonFileName)
        {
            EasyToJson.CreateJsonFolder();
            string path = EasyToJson.GetFilePath(jsonFileName);
            string json = JsonConvert.SerializeObject(list, Formatting.None);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            string encryptData = EasyEncrypt.Encrypt(data);
            File.WriteAllText(path, encryptData);
            Debug.Log(json);
        }

        /**
         * <summary>
         * 암호화된 Json 파일을 읽어서 리스트로 반환
         * </summary>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <returns>Json 파일을 읽어서 만든 리스트</returns>
         */
        public static List<T> ListFromJson<T>(string jsonFileName)
        {
            string path = EasyToJson.GetFilePath(jsonFileName);
            if (!File.Exists(path))
            {
                Debug.Log("파일이 존재하지 않습니다.");
                Debug.Log("파일을 생성합니다.");
                List<T> defaultList = new List<T>();
                ListToJson(defaultList, jsonFileName);
                return defaultList;
            }
            string encryptData = File.ReadAllText(path);
            byte[] data = EasyEncrypt.Decrypt(encryptData);
            string json = System.Text.Encoding.UTF8.GetString(data);
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }

        /**
         * <summary>
         * Dictionary를 암호화된 Json 파일로 저장
         * </summary>
         * <param name="dictionary">Json으로 저장할 딕셔너리</param>
         * <param name="jsonFileName">Json 파일 이름</param>
         */
        public static void DictionaryToJson<TKey, TValue>(Dictionary<TKey, TValue> dictionary,
            string jsonFileName)
        {
            EasyToJson.CreateJsonFolder();
            string path = EasyToJson.GetFilePath(jsonFileName);
            string json = JsonConvert.SerializeObject(dictionary, Formatting.None);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
            string encryptData = EasyEncrypt.Encrypt(data);
            File.WriteAllText(path, encryptData);
            Debug.Log(json);
        }

        /**
         * <summary>
         * 암호화된 Json 파일을 읽어서 딕셔너리로 반환
         * </summary>
         * <param name="jsonFileName">Json 파일 이름</param>
         * <returns>Json 파일을 읽어서 만든 딕셔너리</returns>
         */
        public static Dictionary<TKey, TValue> DictionaryFromJson<TKey, TValue>(string jsonFileName)
        {
            string path = EasyToJson.GetFilePath(jsonFileName);
            if (!File.Exists(path))
            {
                Debug.Log("파일이 존재하지 않습니다.");
                Debug.Log("파일을 생성합니다.");
                Dictionary<TKey, TValue> defaultDictionary = new Dictionary<TKey, TValue>();
                DictionaryToJson(defaultDictionary, jsonFileName);
                return defaultDictionary;
            }
            string encryptData = File.ReadAllText(path);
            byte[] data = EasyEncrypt.Decrypt(encryptData);
            string json = System.Text.Encoding.UTF8.GetString(data);
            Dictionary<TKey, TValue> dictionary = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(json);
            return dictionary;
        }
        
    }
}