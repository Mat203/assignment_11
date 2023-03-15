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

    int px = p.Column;
    int py = p.Row;

    if (py + 1 < map.GetLength(0) && py + 1 >= 0 && px < map.GetLength(1) && px >= 0 && !IsWall(map[px, py + 1]))
    {
        result.Add(new Point(px,py+1));
    }
    if (py - 1 < map.GetLength(0) && py - 1 >= 0 && px < map.GetLength(1) && px >= 0 && !IsWall(map[px, py - 1]))
    {
        result.Add(new Point(px,py-1));
    }
    if (py < map.GetLength(0) && py >= 0 && px+1 < map.GetLength(1) && px+1 >= 0 && !IsWall(map[px+1, py]))
    {
        result.Add(new Point(px+1,py));
    }
    
    if (py < map.GetLength(0) && py >= 0 && px-1 < map.GetLength(1) && px-1 >= 0 && !IsWall(map[px-1, py]))
    {
        result.Add(new Point(px-1,py));
    }
    return result;
}

List<Point> SearchDijkstra(string[,] map, Point start, Point end)
{
    PriorityQueue<Point, int> frontier = new PriorityQueue<Point, int>();
    Dictionary<Point, Point?> CameFrom = new Dictionary<Point, Point?>();
    Dictionary<Point, int> Cost_So_Far = new Dictionary<Point, int>();
    CameFrom.Add(start, null);
    Cost_So_Far.Add(start, 0);
    frontier.Enqueue(start, 0);
    double time = 0;

    while (frontier.Count > 0)
    {
        Point cur = frontier.Dequeue();
        

        if (IsEqual(cur, end))
        {
            break;
        }

        foreach (Point neighbour in Neighbours(map, cur))
        {
            int speed_point = Int32.Parse(map[neighbour.Column, neighbour.Row]);
            int speed = 60-(speed_point-1)*6;
            int new_cost = Cost_So_Far[cur] + speed;
            if (!Cost_So_Far.TryGetValue(neighbour, out _) || new_cost < Cost_So_Far[neighbour])
            {
                Cost_So_Far[neighbour] = new_cost;
                time += 10/Convert.ToDouble(speed);
                int priority = new_cost;
                CameFrom.Add(neighbour, cur);
                frontier.Enqueue(neighbour, priority);

            }
        }
    }
    List<Point> path = new List<Point>();


    Point? current = end;
    while (!IsEqual(current.Value, start))
    {
        path.Add(current.Value);
        CameFrom.TryGetValue(current.Value, out current);
    }
    path.Add(start);
    path.Reverse();
    Console.Write("Загальний час - ");
    Console.Write(Math.Round(time));
    Console.WriteLine(" хв");
    return path;
}


var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 20,
    Width = 20,
    Seed = 1,
    AddTraffic = true
});

string[,] map = generator.Generate();

Point start = new Point(0,0);
Point end = new Point(0,4);

List<Point> path = SearchDijkstra(map, start, end);
List<Point> path1 = new List<Point>(new Point[]
{
    new Point(0,0),
    new Point(0,1),
    new Point(0,2),
    new Point(0,3)
});
PrintMap(map,path);


