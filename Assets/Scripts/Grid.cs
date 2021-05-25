using System;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid {
    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged;
    public class OnGridValueChangedEventArgs : EventArgs {
        public int x;
        public int y;
    }


    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private int[,] _gridArray;
    private TextMesh[,] _debugTextArray;

    public Grid(int width, int height, float cellSize, Vector3 originPosition) {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._originPosition = originPosition;

        _gridArray = new int[width, height];
        _debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < _gridArray.GetLength(0); x++) {
            for (int y = 0; y < _gridArray.GetLength(1); y++) {
                _debugTextArray[x, y] = UtilsClass.CreateWorldText(_gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(_cellSize, _cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    private void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition.x - _originPosition.x) / _cellSize);
        y = Mathf.FloorToInt((worldPosition.y - _originPosition.y) / _cellSize);
    }

    public void SetValue(int x, int y, int value) {
        if (x >= 0 && y >= 0 && x < _width && y < _height) {
            _gridArray[x, y] = value;
            _debugTextArray[x, y].text = _gridArray[x, y].ToString();
            OnGridValueChanged?.Invoke(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetValue(Vector3 worldPosition, int value) {
        int x, y;

        GetXY(worldPosition, out x, out y);

        SetValue(x, y, value);
    }

    public int GetValue(int x, int y) {
        if (x >= 0 && y >= 0 && x < _width && y < _height) {
            return _gridArray[x, y];
        } else {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition) {
        int x, y;

        GetXY(worldPosition, out x, out y);

        return GetValue(x, y);
    }

}
