using freakcode.Cryptography;

namespace GAlpha.GA;

public class GeneArray {
    public List<Gene> Genes { get; }
    public int Count { get => Genes.Count; }

    public GeneArray(Gene[] genes) {
        Genes = genes.ToList();
    }

    public GeneArray(List<Gene> genes) {
        Genes = new(genes);
    }

    public GeneArray(int geneCount) {
        Genes = new(geneCount);
    }

    public void Mutation(double probability = 0.5) {
        for (int index = 0; index < Genes.Count; ++index) {
            if (CryptoRandom.Instance.NextDouble() > probability) {
                Genes[index].Mutation();
            }
        }
    }
}
