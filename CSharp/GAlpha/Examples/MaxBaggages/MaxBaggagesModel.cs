using GAlpha.GA;

namespace GAlpha.Examples.MaxBaggages;

public class MaxBaggagesModel : Model {
    public MaxBaggagesModel(int target)
    : base(
        100,
        10,
        new MaxBaggagesEvaluator(target),
        new BasicScissor(),
        1,
        10
    ) {
    }
}
