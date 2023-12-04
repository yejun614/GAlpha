using System.Diagnostics;
using freakcode.Cryptography;
using GAlpha.Base;

namespace GAlpha.GA;

public class Model {
    public List<GeneArray> Specimen { get; set; }
    public Evaluator GeneEvaluator { get; set; }
    public Scissor GeneScissor { get; set; }
    public int Generation { get; }

    public Model(List<GeneArray> specimen, Evaluator geneEvaluator, Scissor geneScissor) {
        Specimen = specimen;
        GeneEvaluator = geneEvaluator;
        GeneScissor = geneScissor;
        Generation = 0;
    }

    public Model(int geneArrCount, Evaluator geneEvaluator, Scissor geneScissor) {
        Specimen = new(geneArrCount);
        GeneEvaluator = geneEvaluator;
        GeneScissor = geneScissor;
        Generation = 0;
    }

    public void SortGeneByFitness() {
        Specimen.Sort((A, B) => GeneEvaluator.Evaluate(A).CompareTo(GeneEvaluator.Evaluate(B)));
    }

    public void Crossover(double start, double end) {
        int startIndex = Specimen.Count - (int)Math.Round(Specimen.Count * start);
        int endIndex = startIndex + (int)Math.Round((Specimen.Count - startIndex) * end);
        Debug.Assert(startIndex <= endIndex);

        for (int index = 0; index < endIndex - startIndex; ++index) {
            GeneScissor.Crossover(Specimen[startIndex + index], Specimen[endIndex - index]);
        }
    }

    public void Mutation(double start, double end, double probability = 0.5, double innerProbability = 0.5) {
        int startIndex = Specimen.Count - (int)Math.Round(Specimen.Count * start);
        int endIndex = startIndex + (int)Math.Round((Specimen.Count - startIndex) * end);
        Debug.Assert(startIndex <= endIndex);

        for (int index = startIndex; index < endIndex; ++index) {
            if (CryptoRandom.Instance.NextDouble() >= probability) {
                Specimen[index].Mutation(innerProbability);
            }
        }
    }

    public double NextGeneration() {
        // 교차
        Crossover(0.8, 1.0);
        // 돌연변이
        Mutation(0.8, 1.0, 0.5, 0.5);
        // 적응도 내림차순으로 정렬
        SortGeneByFitness();
        // 적응도 확인
        return GeneEvaluator.Evaluate(Specimen[0]);
    }

    public Tuple<GeneArray, double> BestGeneAndFitness() {
        int bestGene = 0;
        double bestFitness = 0;

        for (int i = 0; i < Specimen.Count; ++i) {
            double current = GeneEvaluator.Evaluate(Specimen[i]);
            if (current > bestFitness) {
                bestGene = i;
                bestFitness = current;
            }
        }

        return new Tuple<GeneArray, double>(Specimen[bestGene], bestFitness);
    }
}
