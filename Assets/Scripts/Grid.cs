using UnityEngine;

public class Grid
{
    private int _height => _gridArray.GetLength(1);
    private int _width => _gridArray.GetLength(0);
    private float _cellSize;

    private int[,] _gridArray;

    public Grid(int width, int height, float cellSize)
    {
        _cellSize = cellSize;

        _gridArray = new int[width, height];
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x * _cellSize, y * _cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        _gridArray[x, y] = value;
    }
}