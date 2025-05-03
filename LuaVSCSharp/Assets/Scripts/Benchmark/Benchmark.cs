using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Benchmark : CodyUtilities.Singleton<Benchmark>
{
    /// <summary>
    /// Runs `runsPerTest` benchmarks of `testFunction(iterations)` and spits out a CSV.
    /// </summary>
    public void RunBenchmark(
        int iterations,
        int runsPerTest,
        Action<int> testFunction,
        string outputFileName
    ) {
        var results = new List<float>(runsPerTest);
        for (int run = 0; run < runsPerTest; run++) {
            float start = Time.realtimeSinceStartup;
            testFunction(iterations);
            float elapsed = (Time.realtimeSinceStartup - start) * 1000f; // ms
            results.Add(elapsed);
        }
        File.WriteAllLines(outputFileName,
            results.Select(r => r.ToString("F4")));
        Debug.Log($"Wrote {runsPerTest} samples to {outputFileName}");
    }
}