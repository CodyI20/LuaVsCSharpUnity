using TMPro;
using UnityEngine;

public class TextTest : TestRunner
{
    protected override void LuaTestLogic(int iterations = 1000)
    {
        luaScript.Call(luaScript.Globals["display_text"],iterations);
    }

    protected override void CSharpTestLogic(int iterations = 1000)
    {
        for (int i = 0; i < iterations; i++)
        {
            _uiText.text = "Hello from C#";
        }
    }
}