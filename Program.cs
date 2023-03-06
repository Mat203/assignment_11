using Kse.Algorithms.Samples;

bool IsEqual(Point a, Point b)
{
    return a.Column == b.Column && a.Row == b.Row;
}

void PrintMap(string[,] map, List<Point> path)
{
    Point start = path[0];
    Point end = path[^1];

    foreach (Point p in path)
    {
        if (IsEqual(p, start))
        {
            map[p.Column, p.Row] = "A";
        }
        else if (IsEqual(p, end))
        {
            map[p.Column, p.Row] = "B";
        }
        else
        {
            map[p.Column, p.Row] = ".";
        }
    }
    new MapPrinter().Print(map);
}

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 10,
    Seed = 1,
});

string[,] map = generator.Generate();

List<Point> path = new List<Point>(new Point[]
{
    new Point(0,0),
    new Point(0,1),
    new Point(0,2),
    new Point(0,3)
});

PrintMap(map, path);

