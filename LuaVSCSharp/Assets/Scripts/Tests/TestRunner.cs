using MoonSharp.Interpreter;
using UnityEngine;
using System.Diagnostics;
using TMPro;

public abstract class TestRunner : MonoBehaviour
{
    protected TextMeshProUGUI _uiText;
    protected Script luaScript;

    private void Awake()
    {
        _uiText = GameObject.FindGameObjectWithTag("ResultText").GetComponent<TextMeshProUGUI>();
        
        if (_uiText == null){
            UnityEngine.Debug.LogError("TextMeshProUGUI component not found on GameObject with tag 'ResultText'");
        }
        else
        {
            _uiText.text = "Press a button to run a test";
        }
    }

    protected virtual void Start()
    {
        luaScript = new Script(); //MoonSharp interpreter

        // Load Lua file from StreamingAssets
        string luaPath = System.IO.Path.Combine(Application.streamingAssetsPath, "LUAFunctions.lua");
        string luaCode = System.IO.File.ReadAllText(luaPath);
        
        luaScript.Globals["set_text"] = (System.Action<string>)((newText) => {
            _uiText.text = newText;
        });
        
        luaScript.Globals["print_to_Unity_console"] = (System.Action<string>)(UnityEngine.Debug.Log);

        luaScript.DoString(luaCode); // Executes and registers the Lua functions
    }

    protected abstract void LuaTestLogic();
    protected abstract void CSharpTestLogic();

    public void RunLuaAndTimeIt()
    {
        var stopwatch = Stopwatch.StartNew();
        LuaTestLogic();
        stopwatch.Stop();
        switch (stopwatch.ElapsedMilliseconds)
        {
            case < 100:
                ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - LUA", ExecutionTimeSeverity.Low);
                break;
            case < 500:
                ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - LUA", ExecutionTimeSeverity.Medium);
                break;
            case >= 500:
                ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - LUA", ExecutionTimeSeverity.High);
                break;
        }
        UnityEngine.Debug.Log("Lua Execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    public void RunCSharpAndTimeIt()
    {
        var stopwatch = Stopwatch.StartNew();
        CSharpTestLogic();
        stopwatch.Stop();
        switch (stopwatch.ElapsedMilliseconds)
        {
            case < 100:
                ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - C#", ExecutionTimeSeverity.Low);
                break;
            case < 500:
                ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - C#", ExecutionTimeSeverity.Medium);
                break;
            case >= 500:
                ExecTimeText.Instance.SetText($"{stopwatch.ElapsedMilliseconds} ms - C#", ExecutionTimeSeverity.High);
                break;
        }
        UnityEngine.Debug.Log("C# Execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }
}