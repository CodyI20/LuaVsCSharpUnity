using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;
using System.Diagnostics;
using TMPro;

public abstract class TestRunner : MonoBehaviour
{
    protected TextMeshProUGUI _uiText;
    protected Script luaScript;
    
    [SerializeField] private string test_name = "DEFAULT_TEST_NAME";
    [SerializeField] private int iterations = 1000;
    [SerializeField] private int runs_per_test = 20;

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

    protected abstract void LuaTestLogic(int iterations = 1000);
    protected abstract void CSharpTestLogic(int iterations = 1000);

   private ExecutionTimeSeverity GetSeverity(float elapsed)
    {
        return elapsed < 100 ? ExecutionTimeSeverity.Low :
               elapsed < 500 ? ExecutionTimeSeverity.Medium :
               ExecutionTimeSeverity.High;
    }
    
    private float RunAndTimeInternal(Action<int> testLogic, string label)
    {
        float start = Time.realtimeSinceStartup;
        testLogic(iterations);
        float elapsed = (Time.realtimeSinceStartup - start) * 1000f; // ms
    
        ExecutionTimeSeverity severity = GetSeverity(elapsed);
        ExecTimeText.Instance.SetText($"{elapsed:F3} ms - {label}", severity);
        UnityEngine.Debug.Log($"{label} Execution Time: {elapsed} ms");
    
        return elapsed;
    }
    
    private void RunAndTime(Action<int> testLogic, string label)
    {
        RunAndTimeInternal(testLogic, label);
    }
    
    private float RunAndTimeF(Action<int> testLogic, string label)
    {
        return RunAndTimeInternal(testLogic, label);
    }
    
    public void RunLuaAndTimeIt()
    {
        RunAndTime(LuaTestLogic, "LUA");
    }
    
    public void RunCSharpAndTimeIt()
    {
        RunAndTime(CSharpTestLogic, "C#");
    }
    
    protected float RunLuaAndTimeItF()
    {
        return RunAndTimeF(LuaTestLogic, "LUA");
    }
    
    protected float RunCSharpAndTimeItF()
    {
        return RunAndTimeF(CSharpTestLogic, "C#");
    }
    
    public void BenchmarkLua()
    {
        BenchmarkManager.Instance.RunBenchmark(test_name,"LUA", runs_per_test, RunLuaAndTimeItF);
    }

    public void BenchmarkCSharp()
    {
        BenchmarkManager.Instance.RunBenchmark(test_name, "C#", runs_per_test, RunCSharpAndTimeItF);
    }
    
    public void BenchmarkBoth()
    {
        BenchmarkManager.Instance.RunBothBenchmarks(test_name, runs_per_test, RunLuaAndTimeItF, RunCSharpAndTimeItF);
    }

}