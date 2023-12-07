using System.Diagnostics;
using freakcode.Cryptography;
using GAlpha.Base;

namespace GAlpha.GA;

public class Model {
    public GeneArray[] Specimen { get; set; }
    public Evaluator GeneEvaluator { get; set; }
    public Scissor GeneScissor { get; set; }
    public int Generation { get; set; }
    public int MinValue;
    public int MaxValue;

    public Model(GeneArray[] specimen, Evaluator geneEvaluator, Scissor geneScissor, int minValue, int maxValue) {
        Specimen = specimen;
        GeneEvaluator = geneEvaluator;
        GeneScissor = geneScissor;
        Generation = 0;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public Model(int specimenCount, int geneArrCount, Evaluator geneEvaluator, Scissor geneScissor, int minValue, int maxValue) {
        Specimen = new GeneArray[specimenCount];
        for (int i = 0; i < specimenCount; ++i) {
            do {
                Specimen[i] = new GeneArray(geneArrCount, minValue, maxValue);
            } while (geneEvaluator.Evaluate(Specimen[i]) < 0.1);
            Console.WriteLine($"init: Specimen[{i}] eval = {geneEvaluator.Evaluate(Specimen[i])}");
        }

        GeneEvaluator = geneEvaluator;
        GeneScissor = geneScissor;
        Generation = 0;
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public void SortGeneByFitness() {
        Array.Sort(Specimen, (A, B) => (-1) * GeneEvaluator.Evaluate(A).CompareTo(GeneEvaluator.Evaluate(B)));
    }

    public void Crossover(double start, double end) {
        int startIndex = Specimen.Length - (int)Math.Round(Specimen.Length * start);
        int endIndex = startIndex + (int)Math.Round((Specimen.Length - startIndex) * end);

        Debug.Assert(startIndex >= 0);
        Debug.Assert(endIndex <= Specimen.Length);
        Debug.Assert(startIndex <= endIndex);

        for (int index = 0; index < endIndex - startIndex; ++index) {
            GeneScissor.Crossover(ref Specimen[startIndex + index], ref Specimen[endIndex - index - 1]);
        }
    }

    public void Mutation(double start, double end, double probability = 0.5, double innerProbability = 0.5) {
        int startIndex = Specimen.Length - (int)Math.Round(Specimen.Length * start);
        int endIndex = startIndex + (int)Math.Round((Specimen.Length - startIndex) * end);
        Debug.Assert(startIndex <= endIndex);

        for (int index = startIndex; index < endIndex; ++index) {
            if (CryptoRandom.Instance.NextDouble() >= probability) {
                Specimen[index].Mutation(innerProbability);
            }
        }
    }

    public double NextGeneration() {
        // 세대 증감
        ++Generation;
        // 교차
        Crossover(0.8, 1.0);
        // 돌연변이
        Mutation(0.8, 1.0, 0.5, 0.5);
        // 적응도 내림차순으로 정렬
        SortGeneByFitness();
        // 적응도 확인
        return GeneEvaluator.Evaluate(Specimen[0]);
    }

    public bool Fit(double goalFitness, int maxGeneration) {
        Generation = 0;
        while (Generation < maxGeneration) {
            if (NextGeneration() >= goalFitness) {
                return true;
            }
        }
        return false;
    }

    public Tuple<GeneArray, double> BestGeneAndFitness() {
        int bestGene = 0;
        double bestFitness = 0;

        for (int i = 0; i < Specimen.Length; ++i) {
            double current = GeneEvaluator.Evaluate(Specimen[i]);
            if (current > bestFitness) {
                bestGene = i;
                bestFitness = current;
            }
        }

        return new Tuple<GeneArray, double>(Specimen[bestGene], bestFitness);
    }
}
