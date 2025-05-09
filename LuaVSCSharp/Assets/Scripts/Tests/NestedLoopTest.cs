﻿using UnityEngine;
using TMPro;

public class NestedLoopTest : TestRunner
{
    protected override void LuaTestLogic(int iterations)
    {
        luaScript.Call(luaScript.Globals["nested_loop_test"], iterations);
    }

    protected override void CSharpTestLogic(int iterations)
    {
        float result = 0f;
        for (int i = 0; i < iterations; i++)
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
        _uiText.text = $"The C# result is: {result.ToString()}";
    }
}