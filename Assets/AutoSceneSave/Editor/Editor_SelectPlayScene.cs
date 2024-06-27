using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

public static class Editor_SelectPlayScene
{
    private static readonly string perfs = "UNITYNOTE_SELECTPLAYSCENE_";
    private static readonly string[] playSceneTable = { "0번 씬부터 재생", "현재 씬부터 재생" };
    private static int selectedIndex = 0;

    [InitializeOnLoadMethod]
    private static void Init()
    {
        Load();
    }

    

    public static void OnEditorGUI(GUIStyle titleStyle)
    {
        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
        EditorGUILayout.LabelField("플레이 씬 선택", titleStyle);
        
        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUILayout.Popup("플레이 씬 선택", selectedIndex, playSceneTable);
        
        if (EditorGUI.EndChangeCheck())
        {
            Save();

            if (selectedIndex == 0)
            {
                StartFromFirstScene();
            }
            else if (selectedIndex == 1)
            {
                StartFromCurrentScene();
            }
        }
    }

    private static void StartFromCurrentScene()
    {
        EditorSceneManager.playModeStartScene = null;
    }

    private static void StartFromFirstScene()
    {
        var pathOffFirstScene = EditorBuildSettings.scenes[0].path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOffFirstScene);
        
        EditorSceneManager.playModeStartScene = sceneAsset;
    }

    private static void Save()
    {
        EditorPrefs.SetInt($"{perfs}{nameof(selectedIndex)}", selectedIndex);
    }
    
    private static void Load()
    {
        selectedIndex = EditorPrefs.GetInt($"{perfs}{nameof(selectedIndex)}");
    }
}
