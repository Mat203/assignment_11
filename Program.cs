﻿using Kse.Algorithms.Samples;

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
List<Point> GetNeighbours(int row, int column, string[,] maze)
{
     var result = new List<Point>();
     TryAddWithOffset(1, 0);
     TryAddWithOffset(-1, 0);
     TryAddWithOffset(0, 1);
     TryAddWithOffset(0, -1);
     return result;

     void TryAddWithOffset(int offsetRow, int offsetColumn)
     {
         var newX = row + offsetRow;
         var newY = column + offsetColumn;
         if (newX >= 0 && newY >= 0 && newX < maze.GetLength(0) && newY < maze.GetLength(1) && maze[newX, newY] != "#")
         {
             result.Add(new Point(newY, newX));
         }
     }
}

List<Point> SearchEvristika(string[,] map, Point start, Point end)
{
    PriorityQueue<Point, int> frontier = new PriorityQueue<Point, int>();
    Dictionary<Point, Point?> CameFrom = new Dictionary<Point, Point?>();
    Dictionary<Point, int> Cost_So_Far = new Dictionary<Point, int>();
    CameFrom.Add(start, null);
    Cost_So_Far.Add(start, 0);
    frontier.Enqueue(start, 0);

    while (frontier.Count > 0)
    {
        Point cur = frontier.Dequeue();
        

        if (IsEqual(cur, end))
        {
            break;
        }

        foreach (Point neighbour in GetNeighbours(cur.Row, cur.Column, map))
        {
            int new_cost = Cost_So_Far[cur] + 1;
            var manhattanDistance = Math.Abs(start.Column - end.Column) + Math.Abs(start.Row - end.Row);
            if (!Cost_So_Far.TryGetValue(neighbour, out _) || new_cost < Cost_So_Far[neighbour])
            {
                Cost_So_Far[neighbour] = new_cost;
                int priority = new_cost + manhattanDistance;
                CameFrom.Add(neighbour, cur);
                frontier.Enqueue(neighbour, priority);

            }
        }
    }
    List<Point> path = new List<Point>();

    foreach (Point p in path)
    {
        Console.WriteLine(p.Column);
        Console.WriteLine(p.Row);
    }

    Point? current = end;
    while (!IsEqual(current.Value, start))
    {
        path.Add(current.Value);
        CameFrom.TryGetValue(current.Value, out current);
    }
    path.Add(start);
    path.Reverse();
    return path;
}


var generator = new MapGenerator(new MapGeneratorOptions()
{
    Height = 10,
    Width = 15,
    Seed = 1
});

string[,] map = generator.Generate();

Point start = new Point(0,0);
Point end = new Point(9,8);

List<Point> path = SearchEvristika(map, start, end);

PrintMap(map, path);


