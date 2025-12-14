using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turrent : MonoBehaviour, ITurrent
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float range;
    private GameObject rangeGO;
    [SerializeField]
    private int buyValue;
    [SerializeField]
    private int sellValue;
    
    [SerializeField]
    private float fireRate;
    private float fireCountdown = 0f;
    [SerializeField]
    private float bulletSpeed = 10f;

    [SerializeField]
    private List<GameObject> enemiesInRange = new List<GameObject>();
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject ghostPrefab;
    [SerializeField]
    private GameObject turrentPrefab;

    private BoxCollider2D rangeCollider;
    
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform gunBase;

    [SerializeField]
    private int speedUpgradeCost = 50;
    [SerializeField]
    private int damageUpgradeCost = 50;

    private int speedUpgrades;
    private int damageUpgrades;

    public Transform Target { get => target; set => target = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public int BuyValue { get => buyValue; set => buyValue = value; }
    public int SellValue { get => sellValue; set => sellValue = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float FireCountdown { get => fireCountdown; set => fireCountdown = value; }
    public List<GameObject> EnemiesInRange { get => enemiesInRange; set => enemiesInRange = value; }
    public int SpeedUpgrades { get => speedUpgrades; set => speedUpgrades = value; }
    public int DamageUpgrades { get => damageUpgrades; set => damageUpgrades = value; }
    public int SpeedUpgradeCost { get => speedUpgradeCost; set => speedUpgradeCost = value; }
    public int DamageUpgradeCost { get => damageUpgradeCost; set => damageUpgradeCost = value; }
    public GameObject GhostPrefab { get => ghostPrefab; set => ghostPrefab = value; }
    public GameObject RangeGO { get => rangeGO; set => rangeGO = value; }
    public GameObject TurrentPrefab { get => turrentPrefab; }

    public void SetTarget(Transform target)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        rangeCollider = gameObject.GetComponent<BoxCollider2D>();
        RangeGO = transform.Find("Range").gameObject;
        RangeGO.transform.localScale = new Vector3(range, range, 1);

        if (rangeCollider == null)
        {
            rangeCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        rangeCollider.isTrigger = true;
        rangeCollider.size = new Vector2(range, range);
    }
    private void Update()
    {
        Debug.Log("Enemies in Range Count: " + EnemiesInRange.Count);
        if (Target == null)
            SelectNewTarget();
            Debug.Log("Current Target: " + (Target != null ? Target.name : "None"));
        // Rotate turret to face target
        LookAt();

        if (FireCountdown <= 0f && Target != null)
        {
            Shoot();
            FireCountdown = 1f / FireRate;
        }
        FireCountdown -= Time.deltaTime;
    }

    private void SelectNewTarget()
    {
        if (EnemiesInRange.Count > 0)
        {
            Target = EnemiesInRange[0].transform;
        }
        else
        {
            Target = null;
        }
    }
    public void LookAt()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // If you want the "right" side to face the target:
            gunBase.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // If you want the "up" side to face the target, use:
            // transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        }
    }
    public void Shoot()
    {
        // Implement shooting logic here
        //Debug.Log("Shooting at " + Target.name);
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, gunBase.position, gunBase.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Fire(Target, Damage, bulletSpeed); // Assuming a speed of 10 for the bullet
        }
    }

    private void SpawnBullet()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enemy entered range: " + other.name);
        if (other.CompareTag("Enemy"))
        {
            EnemiesInRange.Add(other.gameObject);
            //if (target == null && EnemiesInRange.Count == 1)
            //{
            //    target = other.transform;
            //}
            //else
            //{
            //    target = EnemiesInRange[0].transform;
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            //if (other.transform == target)
            //{
            //    target = null;
            //    if (enemiesInRange.Count > 0)
            //    {
            //        // Set new target to the first enemy in range
            //        // Note: This is a simple approach; you might want to implement better target selection logic
            //        GameObject newTarget = enemiesInRange[0];
            //        if (newTarget != null)
            //        {
            //            target = newTarget.transform;
            //        }
            //    }
            //}
        }
    }

   
}
