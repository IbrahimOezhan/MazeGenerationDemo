using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class KruskalsMaze : MazeGenerator
{
    public List<List<Cell>> sets = new();

    [SerializeField] private Material notSelected;
    [SerializeField] private Material selected;

    public async void Generate()
    {
        Init();
        await KruskalGeneration();
    }

    private async Task KruskalGeneration()
    {
        foreach (var cell in cells)
        {
            sets.Add(new() { cell });
        }

        List<Wall> _walls = new List<Wall>();
        _walls.AddRange(walls);

        for (int i = 0; i < _walls.Count; i++)
        {
            int rdm = Random.Range(0, _walls.Count);
            (_walls[i], _walls[rdm]) = (_walls[rdm], _walls[i]);
        }

        while (_walls.Count > 0)
        {

            foreach(Wall _wall in walls)
            {
                _wall.SetMaterial(notSelected);
            }

            Wall wall = _walls[Random.Range(0, _walls.Count)];

            wall.SetMaterial(selected);

            await Task.Delay(500);



            (Cell cellLeft, Cell cellRight) = wall.GetSplittingCells(cells);

            if (cellLeft != null && cellRight != null)
            {
                List<Cell> set1 = sets.Find(x => x.Contains(cellLeft));
                List<Cell> set2 = sets.Find(x => x.Contains(cellRight));

                if (set1 != set2)
                {
                    List<Cell> newSet = new();
                    newSet.AddRange(set1);
                    newSet.AddRange(set2);
                    sets.Remove(set1);
                    sets.Remove(set2);
                    sets.Add(newSet);
                    wall.Open();
                }
            }

            _walls.Remove(wall);
        }
    }
}
