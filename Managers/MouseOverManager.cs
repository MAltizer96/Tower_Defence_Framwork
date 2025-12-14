using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MouseOverManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap; // Assign your Tilemap in the Inspector

    [SerializeField]
    private UpgradeManager upgradeManager;

    [SerializeField]
    private BuyingManager buyingManager;
    // Gets the mouse position in world coordinates (for 2D)
    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 0f; // Set to camera distance if using 3D
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    // Gets the closest grid cell center position
    //public Vector3 GetClosestGridPosition(Vector3 worldPosition)
    //{
    //    Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
    //    return tilemap.GetCellCenterWorld(cellPosition);
    //}

    // Checks if a Turrent is at the nearest grid cell
    public Turrent TurrentAtNearestGrid()
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        Vector3 gridPos = GetClosestGridVector2(mouseWorldPos);

        // Find all Turrents in the scene
        GameObject turrentsParent = GameObject.Find("Turrents");

        foreach (Transform turrent in turrentsParent.transform)
        {
            if (Vector2.Distance(turrent.position, gridPos) < 0.1f)
            {
                Debug.Log("Turrent found at grid position: " + gridPos);
                return turrent.GetComponent<Turrent>();
            }
        }
        return null;
    }

    public TileBase GetTileAtWorldPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        //Debug.Log(tilemap.GetTile(cellPosition).name);
        return tilemap.GetTile(cellPosition);
    }
    public Vector2 GetClosestGridVector2(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        Vector3 cellCenterWorld = tilemap.GetCellCenterWorld(cellPosition);
        return new Vector2(cellCenterWorld.x, cellCenterWorld.y);
    }
    public bool CheckPlacement(Vector3 position)
    {
        // Example: check for collisions, grid, etc.
        TileBase currentTile = GetTileAtWorldPosition(position);
        //Debug.Log("CurrentTile: " + currentTile);
        if (currentTile.name == "Grass")
        {
            if(TurrentAtNearestGrid() == null)
                return true;
        }
        return false;

    }
    private void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            if (buyingManager.GhostTurrentInstance != null)
            {
                return;
            }

            Turrent turrent = TurrentAtNearestGrid();
            if (turrent != null)
            {
                Debug.Log("Turrent at grid cell!");
                upgradeManager.OpenTurrentUI(turrent);
                // Additional logic for when a turrent is at the grid cell
            }
            else
            {
                Debug.Log("No turrent at grid cell.");
            }
        }
    }
}