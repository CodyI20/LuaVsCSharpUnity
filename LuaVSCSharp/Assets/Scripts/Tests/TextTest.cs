using TMPro;
using UnityEngine;

public class TextTest : TestRunner
{
    protected override void LuaTestLogic()
    {
        luaScript.Call(luaScript.Globals["display_text"]);
    }

    protected override void CSharpTestLogic()
    {
        for (int i = 0; i < 1000; i++)
        {
            _uiText.text = "Hello from C#";
        }
    }
}