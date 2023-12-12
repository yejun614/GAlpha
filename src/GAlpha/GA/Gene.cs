using freakcode.Cryptography;

namespace GAlpha.GA;

public class Gene {
    public int Data { get; set; }

    public Gene(int data) {
        Data = data;
    }

    public Gene() {
        Data = 0;
    }

    public void Mutation() {
        Data = CryptoRandom.Instance.Next();
    }

    public void Mutation(int minValue) {
        Data = CryptoRandom.Instance.Next(minValue);
    }

    public void Mutation(int minValue, int maxValue) {
        Data = CryptoRandom.Instance.Next(minValue, maxValue);
    }

    public override string ToString() {
        return $"{Data}";
    }
}
