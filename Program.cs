using Kse.Algorithms.Samples;

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 10,
});

string[,] map = generator.Generate();
new MapPrinter().Print(map);