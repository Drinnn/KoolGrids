using UnityEngine;
using CodeMonkey.Utils;

public class GridTesting : MonoBehaviour {
    private Grid _grid;

    private void Start() {
        _grid = new Grid(4, 2, 10f, new Vector3(0, 0));
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            _grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log(_grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
