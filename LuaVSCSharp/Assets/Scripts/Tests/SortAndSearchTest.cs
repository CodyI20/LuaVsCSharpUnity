public class SortAndSearchTest : TestRunner
{
    protected override void LuaTestLogic(int iterations = 1000)
    {
        luaScript.Call(luaScript.Globals["sort_search_test"], iterations);
    }

    protected override void CSharpTestLogic(int iterations = 1000)
    {
        System.Random random = new System.Random();
        float result = 0f;

        for (int i = 0; i < iterations; i++)
        {
            // Generate an array of 500 random integers
            int[] numbers = new int[500];
            for (int j = 0; j < numbers.Length; j++)
            {
                numbers[j] = random.Next(1, 1000);
            }
            
            System.Array.Sort(numbers);

            // Perform an aggregation
            int sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            
            result += sum;
        }

        _uiText.text = $"The C# result is: {result.ToString()}";
    }
}