using System.Runtime.InteropServices;
using UnityEditor;

public class AutoCommitEditor : Editor
{
    [DllImport("msvcrt.dll", EntryPoint="system")]
    static extern int system(string command);
    
    [MenuItem("Tools/Auto Commit")]
    public static void AutoCommit()
    {
        system("git add .");
        system("git commit -m \"Auto Commit\"");
        system("git push");
    }
}
