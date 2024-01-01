using System.Collections.Generic;
using UnityEngine;

public class Simulation
{
    private PointGrid pointGrid;
    public int[] Rule;

    public Simulation(PointGrid pointGrid, int[] rule)
    {
        this.pointGrid = pointGrid;
        this.Rule = rule;
    }

    public void Simulate()
    {
        List<Vector3> oldVectors = new List<Vector3>();
        List<Vector3> newAliveVectors = new List<Vector3>();
        List<Vector3> newDeadVectors = new List<Vector3>();

        foreach (var kvp in pointGrid.points)
        {
            oldVectors.Add(kvp.Key);
        }

        foreach (var pos in oldVectors)
        {
            for (float y = pos.y - pointGrid.distance; y <= pos.y + pointGrid.distance; y += pointGrid.distance)
            {
                for (float z = pos.z - pointGrid.distance; z <= pos.z + pointGrid.distance; z += pointGrid.distance)
                {
                    for (float x = pos.x - pointGrid.distance; x <= pos.x + pointGrid.distance; x += pointGrid.distance)
                    {
                        int neighbours = CountNeighbours(new Vector3(x, y, z), oldVectors);

                        if (oldVectors.Contains(new Vector3(x, y, z)) && !newDeadVectors.Contains(new Vector3(x, y, z)))
                        {
                            if (!(Rule[0] <= neighbours && neighbours <= Rule[1]))
                            {
                                newDeadVectors.Add(new Vector3(x, y, z));
                            }
                        }
                        else if (Rule[2] <= neighbours && neighbours <= Rule[3] && !newAliveVectors.Contains(new Vector3(x, y, z)))
                        {
                            newAliveVectors.Add(new Vector3(x, y, z));
                        }
                    }
                }
            }
        }

        foreach (var v in newDeadVectors)
        {
            pointGrid.RemovePoint(v);
        }

        foreach (var v in newAliveVectors)
        {
            pointGrid.AddPoint(v);
        }
    }

    public int CountNeighbours(Vector3 pos, List<Vector3> points)
    {
        int count = 0;
        for (float y = pos.y - pointGrid.distance; y <= pos.y + pointGrid.distance; y += pointGrid.distance)
        {
            for (float z = pos.z - pointGrid.distance; z <= pos.z + pointGrid.distance; z += pointGrid.distance)
            {
                for (float x = pos.x - pointGrid.distance; x <= pos.x + pointGrid.distance; x += pointGrid.distance)
                {
                    if (points.Contains(new Vector3(x, y, z)))
                    {
                        count++;
                    }
                }
            }
        }
        if (points.Contains(pos))
        {
            count--;
        }
        return count;
    }
}
