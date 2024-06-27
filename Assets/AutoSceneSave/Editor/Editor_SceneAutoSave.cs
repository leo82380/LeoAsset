using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public static class Editor_SceneAutoSave
{
    private static readonly string perfs = "UNITYNOTE_SCENEAUTOSAVE_";
    private static readonly string[] timeTable = { "5분", "10분", "30분", "1시간", "Custom" };
    private static readonly double[] timeToSeconds = { 300, 600, 1800, 3600 };
    private static readonly string timeNotation = "[yyyy-MM-dd] HH-mm-ss";
    
    private static bool isActivated = false;
    private static bool isShowLogExpanded = false;
    private static int selectedTimeTableIndex = 0;
    
    private static double saveCycle = 0;
    private static double nextSaveRemainingTime = 0;
    private static DateTime lastSaveTime = DateTime.Now;
    
    [InitializeOnLoadMethod]
    private static void Init()
    {
        Load();
        
        lastSaveTime = DateTime.Now;

        var handlers = EditorApplication.update.GetInvocationList();
        
        bool hasAlready = false;
        foreach (var handler in handlers)
        {
            if (handler.Method.Name.Equals(nameof(UpdateAutoSave)))
            {
                hasAlready = true;
                break;
            }
        }

        if (hasAlready == false)
        {
            EditorApplication.update += UpdateAutoSave;
        }
    }

    public static void OnEditorGUI(GUIStyle titleStyle)
    {
        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
        EditorGUILayout.LabelField("자동 저장", titleStyle);
        
        EditorGUI.BeginChangeCheck();
        
        // 자동저장 On/Off 토글
        string textActive = isActivated ? "On" : "Off";
        isActivated = EditorGUILayout.Toggle("자동 저장", isActivated);
        
        EditorGUI.BeginDisabledGroup(!isActivated);
        
        // 저장 주기 Dropdown
        selectedTimeTableIndex = EditorGUILayout.Popup("저장 주기", selectedTimeTableIndex, timeTable);
        // Custom일 때
        if (selectedTimeTableIndex == timeTable.Length - 1)
        {
            EditorGUI.indentLevel++;
            saveCycle = EditorGUILayout.DoubleField("저장 주기(초)", saveCycle);
            EditorGUI.indentLevel--;

            // 10초 이상
            if (saveCycle < 10)
            {
                saveCycle = 10;
            }
        }
        else
        {
            saveCycle = timeToSeconds[selectedTimeTableIndex];
        }
        
        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight * 0.5f);
        isShowLogExpanded = EditorGUILayout.Foldout(isShowLogExpanded, "자동 저장 로그", true);
        if (isShowLogExpanded)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("마지막 저장 시간: " + lastSaveTime.ToString(timeNotation));
            EditorGUILayout.LabelField("다음 저장까지 남은 시간: " + nextSaveRemainingTime.ToString("00.00") + "초");
            EditorGUI.indentLevel--;
        }
        
        EditorGUI.EndDisabledGroup();
        
        if (EditorGUI.EndChangeCheck())
        {
            Save();
        }
    }

    private static void Load()
    {
        isActivated = EditorPrefs.GetBool($"{perfs}{nameof(isActivated)}", false);
        isShowLogExpanded = EditorPrefs.GetBool($"{perfs}{nameof(isShowLogExpanded)}", false);
        selectedTimeTableIndex = EditorPrefs.GetInt($"{perfs}{nameof(selectedTimeTableIndex)}", 0);
        saveCycle = EditorPrefs.GetFloat($"{perfs}{nameof(saveCycle)}", 300);
    }
    
    private static void Save()
    {
        EditorPrefs.SetBool($"{perfs}{nameof(isActivated)}", isActivated);
        EditorPrefs.SetBool($"{perfs}{nameof(isShowLogExpanded)}", isShowLogExpanded);
        EditorPrefs.SetInt($"{perfs}{nameof(selectedTimeTableIndex)}", selectedTimeTableIndex);
        EditorPrefs.SetFloat($"{perfs}{nameof(saveCycle)}", (float)saveCycle);
    }
    
    private static void UpdateAutoSave()
    {
        DateTime dateTime = DateTime.Now;

        if (isActivated == false || EditorApplication.isPlaying == true)
        {
            lastSaveTime = dateTime;
            nextSaveRemainingTime = saveCycle;
            
            return;
        }

        double diff = dateTime.Subtract(lastSaveTime).TotalSeconds;
        
        nextSaveRemainingTime = saveCycle - diff;
        if (nextSaveRemainingTime < 0)
        {
            nextSaveRemainingTime = 0;
        }

        if (diff > saveCycle)
        {
            EditorSceneManager.SaveOpenScenes();
            lastSaveTime = dateTime;
        }
    }
}
