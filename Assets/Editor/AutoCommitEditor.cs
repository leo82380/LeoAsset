using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class AutoCommitEditor : EditorWindow
{
    public static string commitMessage;
    [DllImport("msvcrt.dll", EntryPoint="system")]
    static extern int system(string command);
    
    [MenuItem("Tools/Auto Commit")]
    public static void AutoCommit()
    {
        GetWindow<AutoCommitEditor>("Auto Commit");
    }
    
    private void OnGUI()
    {
        commitMessage = EditorGUILayout.TextField("Commit Message", commitMessage);
        if (GUILayout.Button("Commit"))
        {
            system($"git add . && git commit -m \"{commitMessage}\" && git push");
        }
    }
}
