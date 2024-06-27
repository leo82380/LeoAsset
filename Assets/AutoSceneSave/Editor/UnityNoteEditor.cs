using System;
using UnityEditor;
using UnityEngine;

public class UnityNoteEditor : EditorWindow
{
    private static UnityNoteEditor _window;
    private GUIStyle titleStyle;

    [MenuItem("Window/UnityNote")]
    private static void SetUp()
    {
        _window = GetWindow<UnityNoteEditor>();
        _window.titleContent = new GUIContent("UnityNote");
        _window.minSize = new Vector2(300, 300);
        _window.maxSize = new Vector2(1920, 1080);
    }

    private void Awake()
    {
        titleStyle = new GUIStyle(EditorStyles.label)
        {
            fontStyle = FontStyle.Bold,
            normal =
            {
                textColor = Color.white
            }
        };
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("UnityNote", titleStyle);
        Editor_SelectPlayScene.OnEditorGUI(titleStyle);
        Editor_SceneAutoSave.OnEditorGUI(titleStyle);
    }
}
