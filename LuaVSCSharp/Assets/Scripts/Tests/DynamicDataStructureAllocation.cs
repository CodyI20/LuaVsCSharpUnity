using System;

public class DynamicDataStructureAllocation : TestRunner
{
    protected override void LuaTestLogic(int iterations = 1000)
    {
        luaScript.Call(luaScript.Globals["allocate_and_discard_test"], iterations);
    }

    protected override void CSharpTestLogic(int iterations = 1000)
    {
        float result = 0f;

        for (int i = 0; i < iterations; i++)
        {
            // Allocate a new list with a capacity of 1000
            var list = new System.Collections.Generic.List<int>(1000);

            // Fill the list with 1000 integers
            for (int j = 0; j < 1000; j++)
            {
                list.Add(j);
            }

            // Optionally clear the list (or let it go out of scope)
            list.Clear();

            // Accumulate a dummy result to prevent optimization
            result += list.Count;
        }

        _uiText.text = $"The C# result is: {result.ToString()}"; 
    }
}