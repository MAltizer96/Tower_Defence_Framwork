using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    //[SerializeField]
    //private float nextSpawnWaitTime = 1f;
    [SerializeField]
    private int speed;
    [SerializeField]
    private string enemyName;
    [SerializeField]
    private int reward;
    [SerializeField]
    private float coinCost;
    [Header("UI Elements")]
    [SerializeField]
    private Slider healthBar;
    private List<Transform> pathPoints;
    private Player player;

    private BoxCollider2D boxCollider;


    public int Speed { get => speed; set => speed = value; }
    public string Name { get => enemyName; set => enemyName = value; }
    public int Reward { get => reward; set => reward = value; }
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                healthBar.value = (float)currentHealth / maxHealth;
                if (currentHealth <= 0)
                {
                    // Handle enemy death
                    Debug.Log(enemyName + " has been defeated!");
                    // You can add more logic here, like playing a death animation, dropping loot, etc.
                    Die();
                }
            }
        }
    }

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    //public float NextSpawnWaitTime { get => nextSpawnWaitTime; set => nextSpawnWaitTime = value; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        boxCollider = GetComponent<BoxCollider2D>();
        // Initialize pathPoints with some example points (you can modify this as needed)
        Transform PathGB = GameObject.Find("Path").transform;
        pathPoints = new List<Transform>();
        foreach (Transform point in PathGB)
        {
            pathPoints.Add(point);
        }
        transform.position = pathPoints[0].position; // Start at the first point

        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player component not found in the scene.");
        }
    }

    private void Update()
    {
        MoveAlongPath();
    }
    private void MoveAlongPath()
    {
        if (pathPoints == null || pathPoints.Count == 0) return;
        // Move towards the next point in the path
        Transform targetPoint = pathPoints[0];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        // Check if we've reached the target point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            pathPoints.RemoveAt(0); // Remove the reached point
            if (pathPoints.Count == 0)
            {
                // Reached the end of the path
                Debug.Log(enemyName + " has reached the end of the path!");
                Destroy(gameObject); // Or handle it as needed
            }
        }
    }

    private void Die()
    {
        // Logic to handle enemy death
        //Debug.Log(enemyName + " has been defeated!");
        player.Coin += reward;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            bullet.HitTargetLogic();
            CurrentHealth -= bullet.Damage;
        }
    }
}
