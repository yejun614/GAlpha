using freakcode.Cryptography;
using GAlpha.Base;

namespace GAlpha.GA;

public class BasicScissor : Scissor {
    public override void Crossover(ref GeneArray A, ref GeneArray B) {
        int minCount = Math.Min(A.Count, B.Count);

        for (int index = 0; index < minCount; ++index) {
            if (CryptoRandom.Instance.NextDouble() > 0.5) {
                (A.Genes[index], B.Genes[index]) = (B.Genes[index], A.Genes[index]);
            }
        }
    }
}
