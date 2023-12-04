using freakcode.Cryptography;
using GAlpha.Base;

namespace GAlpha.GA;

public class BasicScissor : Scissor {
    public override void Crossover(GeneArray A, GeneArray B) {
        int minCount = Math.Min(A.Count, B.Count);
        int pivot = CryptoRandom.Instance.Next(0, minCount);
        
        B.Genes.AddRange(A.Genes.Skip(pivot));
        A.Genes.AddRange(B.Genes.Take(pivot));

        B.Genes.RemoveRange(0, pivot);
        A.Genes.RemoveRange(pivot + 1, A.Count - pivot);

        // TODO: 테스트 할것
    }
}
