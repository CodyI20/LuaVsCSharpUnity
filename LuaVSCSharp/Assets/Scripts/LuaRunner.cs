using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class LuaRunner : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    public TextMeshProUGUI uiText;

    private Script luaScript;

    void Start()
    {
        luaScript = new Script();

        luaScript.Globals["set_text"] = (System.Action<string>)((newText) => {
            uiText.text = newText;
        });

        luaScript.Globals["set_color"] = (System.Action<string>)((colorName) =>
        {
            if (colorName.ToLower() == "red") healthBar.color = Color.red;
            else if (colorName.ToLower() == "green") healthBar.color = Color.green;
        });

        luaScript.Globals["set_fill"] = (System.Action<float>)((fillAmount) =>
        {
            healthBar.fillAmount = fillAmount;
        });

        // Load Lua file from StreamingAssets
        string luaPath = System.IO.Path.Combine(Application.streamingAssetsPath, "update_health.lua");
        string luaCode = System.IO.File.ReadAllText(luaPath);

        luaScript.DoString(luaCode); // Executes and registers the Lua functions
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
    
    public void RunCSharpHealthUpdate()
    {
        System.Random rng = new System.Random(42);
        float health = 100;

        var stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            int damage = rng.Next(5, 21);
            int heal = rng.Next(1, 11);

            health -= damage;
            health = Mathf.Min(100, health + heal);

            if (health < 30)
                healthBar.color = Color.red;
            else
                healthBar.color = Color.green;

            healthBar.fillAmount = health / 100f;
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log("C# Health Update Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }
    
    public void RunLuaHealthUpdate()
    {
        var stopwatch = Stopwatch.StartNew();
        luaScript.Call(luaScript.Globals["update_health"]);
        stopwatch.Stop();
        UnityEngine.Debug.Log("Lua Health Update Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }


}
