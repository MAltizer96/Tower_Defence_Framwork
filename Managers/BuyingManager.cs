using UnityEngine;
using UnityEngine.Tilemaps;
public class BuyingManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject basicGhostTurrentPrefab;
    [SerializeField]
    private GameObject BasicTurrentPrefab;

    private GameObject ghostTurrentInstance;
    private Turrent currentTurent;

    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private MouseOverManager mouseOverManager;
    [SerializeField]
    private Transform turrentParent;

    [SerializeField]
    private UIManager uiManager;


    public GameObject GhostTurrentInstance { get => ghostTurrentInstance; set => ghostTurrentInstance = value; }
    public Turrent CurrentTurent { get => currentTurent; set => currentTurent = value; }

    public void BuyTurrent(Turrent turrent)
    {

        if (player.Coin >= turrent.BuyValue)
        {
            // Additional logic for buying the turrent
            StartTurrentPlacement(turrent);
            Debug.Log("placing");
       
        }
        else
        {
            Debug.Log("Not enough coins to buy this turrent!");
            // Optionally, provide feedback to the player
            StartCoroutine(uiManager.DisplayError("Not enough coins to buy this turrent!"));
        }
    }

    public void StartTurrentPlacement(Turrent turrent)
    {
        if (turrent == null)
            return;
            //Destroy(turrent);

        CurrentTurent = turrent;
        GameObject GhostPrefab = Instantiate(turrent.GhostPrefab);
        GhostTurrentInstance = GhostPrefab;
        GhostTurrentInstance.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f); // Initial color
        GameObject turrentRange = GhostTurrentInstance.transform.Find("Range").gameObject;
        turrentRange.SetActive(true); // Disable range indicator for ghost
        turrentRange.transform.localScale = new Vector3(CurrentTurent.Range, CurrentTurent.Range, 1);
        // gonna have to update ghost prefabe to have stats of turrent available to it, or atleast a reference to the turrent
    }

    private void PlaceTurrent(Vector3 position)
    {
        if (CurrentTurent == null)
            return;
        // Instantiate the real turret at position
        GameObject newTurrent = Instantiate(CurrentTurent.TurrentPrefab, mouseOverManager.GetClosestGridVector2(position), Quaternion.identity);
        Turrent newTurrentScript = newTurrent.GetComponent<Turrent>();

        newTurrentScript.RangeGO.SetActive(false); // Disable range indicator for placed turret
        //GameObject turrentRange = GhostTurrentInstance.transform.Find("Range").gameObject;
        //turrentRange.SetActive(false); // Disable range indicator for ghost
        //turrentRange.transform.localScale = new Vector3(newTurrentScript.Range, newTurrentScript.Range, 1);
        newTurrent.transform.parent = turrentParent.transform;
        player.Coin -= newTurrentScript.BuyValue;
        currentTurent = null;
    }

   
    private void Update()
    {
        if (CurrentTurent != null)
        {
            // Get mouse position in world
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // Set z to 0 for 2D
            mouseOverManager.GetTileAtWorldPosition(mouseWorldPos);
            GhostTurrentInstance.transform.position = mouseOverManager.GetClosestGridVector2(mouseWorldPos);

            // Check if placeable (implement your own logic here)
            bool canPlace = mouseOverManager.CheckPlacement(mouseWorldPos);
            //Debug.Log(canPlace);
            // Change color based on placeability
            var sprite = GhostTurrentInstance.GetComponent<SpriteRenderer>();
            if (sprite != null)
                if (canPlace)
                {
                    sprite.color = new Color(0, 1, 0, 0.5f); // Green with transparency
                }
                else
                {
                    sprite.color = new Color(1, 0, 0, 0.5f); // Red with transparency
                }
                 //sprite.color = canPlace ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);

            // Place turret on click
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceTurrent(mouseWorldPos);
                Destroy(GhostTurrentInstance);
            }
            else if (Input.GetMouseButtonDown(0) && !canPlace)
            {
                StartCoroutine(uiManager.DisplayError("Cannot place turret here!"));
            }
        }
    }
}
