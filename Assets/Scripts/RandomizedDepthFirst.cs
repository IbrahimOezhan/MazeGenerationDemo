using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RandomizedDepthFirst : MazeGenerator
{
    private List<Cell> stack = new();

    [SerializeField] private Material visited;
    [SerializeField] private Material selected;

    public async void Generate()
    {
        Init();

        await Search(cells[Random.Range(0, cells.Count)]);
    }

    private async Task Search(Cell cell)
    {
        await Task.Delay(500);

        cell.visited = true;

        List<Cell> unvisitedNeighbours = cell.GetAdjecentCells(cells).FindAll(x => x.visited == false);

        if (unvisitedNeighbours.Count > 0)
        {
            Cell rdmCell = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

            Vector3 offset = rdmCell.transform.position - cell.transform.position;

            Wall wall = walls.Find(x => x.transform.position == cell.transform.position + (offset * 0.5f));

            wall.Open();

            stack.Add(cell);

            cell.SetMaterial(visited);
            rdmCell.SetMaterial(selected);

            await Search(rdmCell);
        }
        else
        {
            cell.SetMaterial(visited);
            stack[^1].SetMaterial(selected);

            await Backtrack(stack[^1]);
        }
    }

    private async Task Backtrack(Cell cell)
    {
        await Task.Delay(500);

        stack.Remove(cell);

        List<Cell> unvisitedNeighbours = cell.GetAdjecentCells(cells).FindAll(x => x.visited == false);

        if (unvisitedNeighbours.Count > 0)
        {
            cell.SetMaterial(visited);

            await Search(cell);
        }
        else
        {
            cell.SetMaterial(visited);

            if (stack.Count > 0)
            {
                stack[^1].SetMaterial(selected);
                await Backtrack(stack[^1]);
            }
            else
            {
                Debug.Log("Finished");
            }
        }
    }
}
