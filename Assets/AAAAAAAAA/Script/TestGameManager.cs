using System;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    private string _code;
    
    [DllImport("msvcrt.dll", EntryPoint="system")]
    static extern int system(string command);
    public string GetUserInfo()
    {
        // user info store variable define
        string[] userInfo = new string[2];
        userInfo[1] = Environment.MachineName;
        return userInfo[1];
    }

    private void Start()
    {
        _code = GetUserInfo();
        system("msgbox \"sss\" \"sss\"");
    }
}
