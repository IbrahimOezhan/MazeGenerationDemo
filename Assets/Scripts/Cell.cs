using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private Vector3 up = new Vector3(0, 0, 1);
    private Vector3 down = new Vector3(0, 0, -1);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3 right = new Vector3(1, 0, 0);

    [SerializeField] private Wall wall;



    public bool visited;
    [SerializeField] private MeshRenderer meshRenderer;
    public void SetMaterial(Material mat)
    {
        meshRenderer.material = mat;
    }

    public void Init(List<Wall> walls)
    {
        AddWall(up, walls);
        AddWall(down, walls);
        AddWall(left, walls);
        AddWall(right, walls);
    }



    private void AddWall(Vector3 offset, List<Wall> walls)
    {
        offset *= 0.5f;
        Vector3 offsettedPos = transform.position + offset;
        if (walls.Find(x => x.transform.position == offsettedPos) == null)
        {
            Wall _wall = Instantiate(wall, offsettedPos, Quaternion.identity);

            if (offset.x != 0)
            {
                _wall.transform.localScale = new Vector3(0.05f, 1, 1);
            }

            if (offset.z != 0)
            {
                _wall.transform.localScale = new Vector3(1, 1, 0.05f);
            }

            walls.Add(_wall);
            _wall.Init(offset);
        }
    }

    public List<Wall> GetAdjecentWalls(List<Wall> walls)
    {
        List<Wall> adjecent = new();

        Wall wallUp = GetAdjecentWall(walls, up);
        Wall wallDown = GetAdjecentWall(walls, down);
        Wall wallLeft = GetAdjecentWall(walls, left);
        Wall wallRight = GetAdjecentWall(walls, right);

        if (wallUp != null) adjecent.Add(wallUp);
        if (wallDown != null) adjecent.Add(wallDown);
        if (wallLeft != null) adjecent.Add(wallLeft);
        if (wallRight != null) adjecent.Add(wallRight);

        return adjecent;
    }

    private Wall GetAdjecentWall(List<Wall> walls, Vector3 offset)
    {
        Vector3 offsetPos = transform.position + (offset * 0.5f);
        Wall wall = walls.Find(x => x.transform.position == offsetPos);
        return wall;
    }

    public List<Cell> GetAdjecentCells(List<Cell> cells)
    {
        List<Cell> adjecent = new();

        Cell wallUp = GetAdjecentCell(cells, up);
        Cell wallDown = GetAdjecentCell(cells, down);
        Cell wallLeft = GetAdjecentCell(cells, left);
        Cell wallRight = GetAdjecentCell(cells, right);

        if (wallUp != null) adjecent.Add(wallUp);
        if (wallDown != null) adjecent.Add(wallDown);
        if (wallLeft != null) adjecent.Add(wallLeft);
        if (wallRight != null) adjecent.Add(wallRight);

        return adjecent;
    }

    private Cell GetAdjecentCell(List<Cell> cells, Vector3 offset)
    {
        Vector3 offsetPos = transform.position + offset;
        Cell cell = cells.Find(x => x.transform.position == offsetPos);
        return cell;
    }
}
