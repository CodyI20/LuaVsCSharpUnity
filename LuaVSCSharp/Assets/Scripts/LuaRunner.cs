using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class LuaRunner : MonoBehaviour
{
    public TextMeshProUGUI uiText;

    private Script luaScript;

    void Start()
    {
        luaScript = new Script();

        // Registering a C# function to be callable from Lua
        luaScript.Globals["set_text"] = (System.Action<string>)((newText) =>
        {
            uiText.text = newText;
        });

        // Run Lua function from script
        string luaCode = @"
            function update_text()
                for i = 1,1000 do
                    set_text('Hello from Lua')
                end
            end
        ";

        luaScript.DoString(luaCode);
    }

    public void RunLuaAndTimeIt()
    {
        var stopwatch = Stopwatch.StartNew();
        luaScript.Call(luaScript.Globals["update_text"]);
        stopwatch.Stop();
        UnityEngine.Debug.Log("Lua Execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    public void RunCSharpAndTimeIt()
    {
        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            uiText.text = "Hello from C#";
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("C# Execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }
}
