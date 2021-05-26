using UnityEngine;
using CodeMonkey.Utils;

public class GridBuildingSystem : MonoBehaviour {
    [SerializeField] private Transform testTransform;

    private Grid<GridObject> grid;
    private void Awake() {
        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(-50f, -45f), (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y), true);
    }

    public class GridObject {
        private Grid<GridObject> grid;
        private int x;
        private int y;
        private Transform transform;

        public GridObject(Grid<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetTransform(Transform transform) {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearTransform() {
            transform = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public bool CanBuild() {
            return transform == null;
        }

        public override string ToString() {
            return x + ", " + y + "\n" + transform;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            grid.GetXY(UtilsClass.GetMouseWorldPosition(), out int x, out int y);
            GridObject gridObject = grid.GetGridObject(x, y);
            if (gridObject.CanBuild()) {
                Transform buildTransform = Instantiate(testTransform, grid.GetWorldPosition(x, y), Quaternion.identity);
                gridObject.SetTransform(buildTransform);
            } else {
                UtilsClass.CreateWorldTextPopup("Cannot build here!", UtilsClass.GetMouseWorldPosition());
            }
        }
    }

}
