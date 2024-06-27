using System.Diagnostics;
using UnityEngine;
using System.Runtime.InteropServices; // Pro and Free!!!
using DG.Tweening;
 
public class FindHwnd : MonoBehaviour {
 
    [DllImport("user32.dll")] static extern int GetForegroundWindow();
    
    [DllImport("user32.dll", EntryPoint="MoveWindow")]  
    static extern int  MoveWindow (int hwnd, int x, int y,int nWidth,int nHeight,int bRepaint );
    
    [DllImport("user32.dll", EntryPoint="SetWindowLongA")]  
    static extern int  SetWindowLong (int hwnd, int nIndex,int dwNewLong);
       
    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(int hWnd, int nCmdShow);
       
    [DllImport("user32.dll", EntryPoint="SetLayeredWindowAttributes")]  
    static extern int  SetLayeredWindowAttributes (int hwnd, int crKey,byte bAlpha, int dwFlags );
    [DllImport("user32.dll", EntryPoint="SetWindowPos")]
    static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("msvcrt.dll", EntryPoint="system")]
    static extern int system(string command);
    
    private int a = 0;
    private int b = 0;
    int handle;
    private int x;
    private int y;

    private void Start()
    {
        
        handle = GetForegroundWindow();
        x = Screen.width / 2;
        y = Screen.height / 2;
        SetWindowPos(handle, 0, x, y, 0, 0, 0x0001 | 0x0004);
    }

    void Update()
    {
        SetWindowPos(handle, 0, 
            (int)(Mathf.Log(Time.time * 100) * 100) + x, 
            (int)(Mathf.Sin(Time.time * 100) * 100) + y, 
            0, 0, 0x0001 | 0x0004);
        
    }
}