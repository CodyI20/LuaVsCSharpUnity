using TMPro;
using UnityEngine;

public class TextTest : TestRunner
{
    protected override void LuaTestLogic(int iterations)
    {
        luaScript.Call(luaScript.Globals["display_text"],iterations);
    }

    protected override void CSharpTestLogic(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            _uiText.text = "Hello from C#";
        }
    }
}