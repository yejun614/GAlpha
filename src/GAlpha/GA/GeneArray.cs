using freakcode.Cryptography;
using GAlpha.Base;

namespace GAlpha.GA;

public class GeneArray {
    public int MinValue;
    public int MaxValue;
    public List<Gene> Genes { get; }
    public int Count { get => Genes.Count; }

    public GeneArray(List<Gene> genes, int minValue, int maxValue) {
        Genes = new(genes);
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public GeneArray(int geneCount, int minValue, int maxValue) {
        Genes = new();
        MinValue = minValue;
        MaxValue = maxValue;

        for (int index = 0; index < geneCount; ++index) {
            Gene gene = new Gene();
            gene.Mutation(minValue, maxValue);
            Genes.Add(gene);
        }
    }

    public void Mutation(double probability = 0.5) {
        for (int index = 0; index < Genes.Count; ++index) {
            if (CryptoRandom.Instance.NextDouble() > probability) {
                Genes[index].Mutation(MinValue, MaxValue);
            }
        }
    }

    public override string ToString() {
        return $"[{string.Join(", ", Genes.Select(gene => gene.ToString()))}]";
    }
}
