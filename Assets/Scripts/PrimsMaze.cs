using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PrimsMaze : MazeGenerator
{
    [SerializeField] private List<Wall> partOfMazeWalls = new();

    [SerializeField] private Material selected;
    [SerializeField] private Material inList;
    [SerializeField] private Material notSelected;
    [SerializeField] private Material visited;

    public async void Generate()
    {
        Init();

        await GeneratePrim();
    }

    public async Task GeneratePrim()
    {
        Cell rdmCell = cells[Random.Range(0, cells.Count)];

        rdmCell.visited = true;
        rdmCell.SetMaterial(visited);

        List<Wall> _walls = new();
        _walls.AddRange(walls);

        partOfMazeWalls.AddRange(rdmCell.GetAdjecentWalls(_walls));

        _walls.RemoveAll(x => partOfMazeWalls.Contains(x));

        while (partOfMazeWalls.Count > 0)
        {
            foreach (Wall wall in walls)
            {
                wall.SetMaterial(notSelected);
            }

            foreach (Wall wall in partOfMazeWalls)
            {
                wall.SetMaterial(inList);
            }

            Wall rdmWall = partOfMazeWalls[Random.Range(0, partOfMazeWalls.Count)];

            rdmWall.SetMaterial(selected);

            await Task.Delay(500);


            Cell nextCell = rdmWall.IsValid(cells);


            if (nextCell != null)
            {
                nextCell.SetMaterial(visited);
                nextCell.visited = true;
                rdmWall.Open();
                partOfMazeWalls.AddRange(nextCell.GetAdjecentWalls(_walls));
            }

            partOfMazeWalls.Remove(rdmWall);
        }
    }
}
