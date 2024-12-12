using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Vector3 leftCellPos;
    [SerializeField] private Vector3 rightCellPos;

    [SerializeField] private MeshRenderer meshRenderer;
    public void SetMaterial(Material mat)
    {
        meshRenderer.material = mat;
    }

    public void Init(Vector3 offset)
    {
        leftCellPos = transform.position + offset;
        rightCellPos = transform.position - offset;
    }

    public (Cell, Cell) GetSplittingCells(List<Cell> cells)
    {
        Cell cellLeft = cells.Find(x => x.transform.position == leftCellPos);
        Cell cellRight = cells.Find(x => x.transform.position == rightCellPos);
        return (cellLeft, cellRight);
    }

    public Cell IsValid(List<Cell> cells)
    {
        (Cell cellLeft, Cell cellRight) = GetSplittingCells(cells);

        if (cellLeft != null && cellRight != null)
        {

            if (cellLeft.visited && !cellRight.visited) return cellRight;
            if (!cellLeft.visited && cellRight.visited) return cellLeft;
        }

        return null;
    }

    public void Open()
    {
        gameObject.SetActive(false);
    }
}
