using freakcode.Cryptography;

namespace GAlpha.GA;

public class Gene {
    public int Data { get; set; }

    public Gene(int data) {
        Data = data;
    }

    public Gene() {
        Mutation();
    }

    public void Mutation() {
        Data = CryptoRandom.Instance.Next();
    }
}
