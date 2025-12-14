using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;
public class WaveManager : MonoBehaviour
{
    //[SerializeField]
    // public Enemy[] enemyForWave; // List of waves, each wave is a list of enemies>
    //[SerializeField]
    //private int numberOfWaves;
    //[SerializeField]
    //private int[] enemiesPerWave; // Number of enemies in each wave

    [SerializeField]
    private Enemy[] enemies; // Different types of enemies

    private float difficultyMultiplier = 1f; // Increase difficulty by 20% each wave
   

    private int currentWaveIndex = 0;
    private int waveIndex = 0;

    private bool waveInProgress = false;
    private float spawnInterval = 2.0f; // Time between spawns

    [SerializeField]
    private List<Wave> waves;
    [SerializeField]
    private Transform startButton;
    [SerializeField]
    private TextMeshProUGUI waveNumberText;


    public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
    public float DifficultyMultiplier { get => difficultyMultiplier; set => difficultyMultiplier = value; }

    private void Start()
    {
        Debug.Log(waves[0]);
        //StartNextWave();
    }

    public void StartNextWave()
    {

        //if (startButton.gameObject.activeSelf == false)
        //{
        //    startButton.gameObject.SetActive(true);
        //    while (startButton.gameObject.activeSelf == true)
        //    {
        //        yield return new WaitForSeconds(.5f);
        //    }
        //}
        waveInProgress = true;
        startButton.gameObject.SetActive(false);
        int currentWaveInt = currentWaveIndex + 1;
        waveNumberText.text = currentWaveInt.ToString();
        StartCoroutine(SpawnEnemies(waves[currentWaveIndex]));
        if (waveIndex < waves.Count)
        {

        }
        else
        {
            Debug.Log("All waves completed!");
            waveIndex = 0;
            startButton.gameObject.SetActive(true);
            //StartCoroutine(StartNextWave());
        }
    }
    private IEnumerator SpawnEnemies(Wave wave)
    {
        foreach (EnemyCount enemyCount in wave.enemiesTypes)
        {
            Enemy enemyPrefab = enemyCount.enemyPrefab;
            int count = enemyCount.count;
            for (int i = 0; i < count; i++)
            {
                Debug.Log("Spawn");
                Enemy enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                enemy.MaxHealth = Mathf.RoundToInt(enemy.MaxHealth * DifficultyMultiplier);
                enemy.CurrentHealth = enemy.MaxHealth;



                yield return new WaitForSeconds(spawnInterval);
            }

        }
        if (waveInProgress)
        {
            yield return new WaitForSeconds(1f);
        }
        startButton.gameObject.SetActive(true);


        waveInProgress = false;
        currentWaveIndex++;
        //StartCoroutine(StartNextWave());
    }


}
