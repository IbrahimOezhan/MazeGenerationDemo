using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 size;
    [SerializeField] private Cell cell;

    [SerializeField] protected List<Cell> cells = new();
    [SerializeField] protected List<Wall> walls = new();

    protected void Init()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Cell _cell = Instantiate(cell, new Vector3(x, 0, y), Quaternion.identity);
                cells.Add(_cell);
                _cell.Init(walls);
            }
        }
    }
}
