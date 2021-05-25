using UnityEngine;
using CodeMonkey.Utils;

public class GridTesting : MonoBehaviour {
    private Grid<bool> _grid;

    private void Start() {
        _grid = new Grid<bool>(4, 2, 10f, Vector3.zero, (Grid<bool> g, int x, int y) => new bool(), true);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            _grid.SetGridObject(UtilsClass.GetMouseWorldPosition(), true);
        }
    }
}
