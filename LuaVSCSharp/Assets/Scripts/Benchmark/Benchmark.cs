using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BenchmarkManager : CodyUtilities.Singleton<BenchmarkManager>
{
    public void RunBenchmark(
        string label,
        string extension,
        int runsPerTest,
        Func<float> testFunction
    ) {
        var results = new List<string>();
        Debug.Log($"Running benchmark '{label}'-{extension} for {runsPerTest} runs...");
        
        results.Add($"RUN, {extension}");

        for (int run = 1; run <= runsPerTest; run++) {
            float elapsed = testFunction();
            results.Add($"{run},{elapsed:F4}");
        }

        // float average = results.Average();
        // float max = results.Max();
        // float min = results.Min();

        string fileName = $"{label}{extension}_benchmark.csv";
        string folderPath = Path.Combine(Application.persistentDataPath, "Benchmarks");
        string fullPath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllLines(fullPath, results);//.Select(r => r.ToString("F4")));

        Debug.Log($"✅ Finished benchmark '{label}'-{extension}");
        //Debug.Log($"📊 Avg: {average:F2} ms | Min: {min:F2} ms | Max: {max:F2} ms");
        Debug.Log($"📁 Saved to: {fullPath}");
    }
    
    public void RunBothBenchmarks(
        string label,
        int runsPerTest,
        Func<float> luaFunc,
        Func<float> csharpFunc
    ) {
        var results = new List<string>();
        Debug.Log($"Running dual benchmark '{label}' for {runsPerTest} runs...");
        
        results.Add("RUN,LUA,C#");

        for (int run = 1; run <= runsPerTest; run++) {
            float luaTime = luaFunc();
            float csTime = csharpFunc();
            results.Add($"{run},{luaTime:F4},{csTime:F4}");
        }

        // float average = results.Average();
        // float max = results.Max();
        // float min = results.Min();

        string fileName = $"{label}_dual_benchmark.csv";
        string folderPath = Path.Combine(Application.persistentDataPath, "Benchmarks");
        string fullPath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllLines(fullPath, results);//.Select(r => r.ToString("F4")));

        Debug.Log($"✅ Finished dual benchmark '{label}'");
        //Debug.Log($"📊 Avg: {average:F2} ms | Min: {min:F2} ms | Max: {max:F2} ms");
        Debug.Log($"📁 Saved to: {fullPath}");
    }
}