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

bool IsWall(string c)
{
    return c == "█";
}
List<Point> Neighbours(string[,] map, Point p)
{
    List<Point> result = new List<Point>();

    int x = p.Column;
    int y = p.Row;

    if (y++ < map.GetLength(1) && y++ >= 0 && x < map.GetLength(0) && x >= 0 && !IsWall(map[x, y++]))
    {
        result.Add(new Point(x,y++));
    }
    if (y-- < map.GetLength(1) && y-- >= 0 && x < map.GetLength(0) && x >= 0 && !IsWall(map[x, y++]))
    {
        result.Add(new Point(x,y--));
        
    }if (y < map.GetLength(1) && y >= 0 && x++ < map.GetLength(0) && x++ >= 0 && !IsWall(map[x, y++]))
    {
        result.Add(new Point(x++,y));
    }if (y < map.GetLength(0) && y >= 0 && x-- < map.GetLength(0) && x-- >= 0 && !IsWall(map[x, y++]))
    {
        result.Add(new Point(x--,y));
    }

    return result;
}

List<Point> SearchBFS(string[,] map,Point start, Point end)
{
    var frontier = new Queue<Point>();
    Dictionary<Point, Point?> cameFrom = new Dictionary<Point, Point?>();
    
    cameFrom.Add(start, null);
    frontier.Enqueue(start);

    while (frontier.Count > 0)
    {
        var cur = frontier.Dequeue();
        
        if (IsEqual(cur, end))
        {
            break;
        }

        foreach (Point neighbour in Neighbours(map, cur))
        {
            if (!cameFrom.TryGetValue(neighbour, out _))
            {
                cameFrom.Add(neighbour, cur);
                frontier.Enqueue(neighbour);
            } 
        }
    }

    List<Point> path = new List<Point>();

    Point? current = end;
    while (IsEqual(current.Value, start))
    {
        path.Add(current.Value);
        cameFrom.TryGetValue(current.Value, out current);
    }
    path.Add(start);

    path.Reverse();
    return path;

}

;

var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 15,
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



pri