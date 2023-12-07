using GAlpha.Base;
using GAlpha.GA;

namespace GAlpha.Examples.MaxBaggages;

public class MaxBaggagesEvaluator : Evaluator {
    public int MaxSize { get; set; }

    public MaxBaggagesEvaluator(int maxSize) {
        MaxSize = maxSize;
    }

    public override double Evaluate(GeneArray x) {
        int size = 0;
        foreach (Gene gene in x.Genes) {
            size += gene.Data;
        }

        if (size > MaxSize) {
            return 0;
        } else {
            return size / MaxSize;
        }
    }
}
