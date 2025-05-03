using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BenchmarkManager : CodyUtilities.Singleton<BenchmarkManager>
{
    public void RunBenchmark(
        string label,
        int iterations,
        int runsPerTest,
        Action<int> testFunction
    ) {
        var results = new List<float>();
        Debug.Log($"Running benchmark '{label}' for {runsPerTest} runs...");

        for (int run = 0; run < runsPerTest; run++) {
            float start = Time.realtimeSinceStartup;
            testFunction(iterations);
            float elapsed = (Time.realtimeSinceStartup - start) * 1000f; // ms
            results.Add(elapsed);
        }

        float average = results.Average();
        float max = results.Max();
        float min = results.Min();

        string fileName = $"{label}_benchmark.csv";
        string folderPath = Path.Combine(Application.persistentDataPath, "Benchmarks");
        string fullPath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllLines(fullPath, results.Select(r => r.ToString("F4")));

        Debug.Log($"✅ Finished benchmark '{label}'");
        Debug.Log($"📊 Avg: {average:F2} ms | Min: {min:F2} ms | Max: {max:F2} ms");
        Debug.Log($"📁 Saved to: {fullPath}");
    }
}