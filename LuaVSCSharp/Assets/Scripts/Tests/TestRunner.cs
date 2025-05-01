using MoonSharp.Interpreter;
using UnityEngine;
using System.Diagnostics;

public abstract class TestRunner : MonoBehaviour
{
    protected Script luaScript;

    protected virtual void Start()
    {
        luaScript = new Script(); //MoonSharp interpreter

        // Load Lua file from StreamingAssets
        string luaPath = System.IO.Path.Combine(Application.streamingAssetsPath, "LUAFunctions.lua");
        string luaCode = System.IO.File.ReadAllText(luaPath);

        luaScript.DoString(luaCode); // Executes and registers the Lua functions
    }

    protected abstract void LuaTestLogic();
    protected abstract void CSharpTestLogic();

    public void RunLuaAndTimeIt()
    {
        var stopwatch = Stopwatch.StartNew();
        LuaTestLogic();
        stopwatch.Stop();
        ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - LUA");
        UnityEngine.Debug.Log("Lua Execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    public void RunCSharpAndTimeIt()
    {
        var stopwatch = Stopwatch.StartNew();
        CSharpTestLogic();
        stopwatch.Stop();
        ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - C#");
        UnityEngine.Debug.Log("C# Execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }
    
}