using TMPro;
using UnityEngine;

public class TextTest : TestRunner
{
    [SerializeField] private TextMeshProUGUI uiText;
    
    protected override void Start()
    {
        base.Start();
        luaScript.Globals["set_text"] = (System.Action<string>)((newText) => {
            uiText.text = newText;
        });
    }

    protected override void LuaTestLogic()
    {
        luaScript.Call(luaScript.Globals["display_text"]);
    }

    protected override void CSharpTestLogic()
    {
        for (int i = 0; i < 1000; i++)
        {
            uiText.text = "Hello from C#";
        }
    }
}