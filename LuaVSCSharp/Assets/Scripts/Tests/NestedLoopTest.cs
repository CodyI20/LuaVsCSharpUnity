using UnityEngine;
using TMPro;

public class NestedLoopTest : TestRunner
{
    [SerializeField] private TextMeshProUGUI text;

    protected override void Start()
    {
        base.Start();
        luaScript.Globals["set_text"] = (System.Action<string>)((newText) => {
            text.text = newText;
        });
    }
    protected override void LuaTestLogic()
    {
        luaScript.Call(luaScript.Globals["nested_loop_test"]);
    }

    protected override void CSharpTestLogic()
    {
        float result = 0f;
        for (int i = 0; i < 1000; i++)
        {
            for (int j = 0; j < 200; j++)
            {
                // Basic arithmetic operations
                float addition = i + j;
                float multiplication = i * j;
                float division = (j != 0) ? (float)i / j : 0f;
                float subtraction = i - j;

                // Trigonometric operations
                float sine = Mathf.Sin(i * Mathf.Deg2Rad);
                float cosine = Mathf.Cos(j * Mathf.Deg2Rad);
                float tangent = Mathf.Tan((i + j) * Mathf.Deg2Rad);

                // Exponential and logarithmic operations
                float exponential = Mathf.Exp(i % 10);
                float logarithm = (i + j > 0) ? Mathf.Log(i + j) : 0f;

                // Combine results (example: sum of all operations)
                result += addition + multiplication + division + subtraction + sine + cosine + tangent + exponential + logarithm;
            }
        }
        text.text = $"The C# result is: {result.ToString()}";
    }
}