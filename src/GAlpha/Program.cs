using GAlpha.Examples.MaxBaggages;

var target = 50;
var model = new MaxBaggagesModel(target);
model.Fit(0.99, 1000);

Console.WriteLine($"Target: {target}");
Console.WriteLine($"Total Generation: {model.Generation}");
Console.WriteLine($"Best Fitness: {model.BestGeneAndFitness()}");
