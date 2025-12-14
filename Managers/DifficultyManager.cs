using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private WaveManager waveManager;
    
    public void IncreaseDifficulty()
    {
        // Increase the difficulty multiplier
        // This could be a percentage increase or a fixed amount
        // For example, increase by 20% each wave
        waveManager.DifficultyMultiplier *= 1.2f;
        // Decrease spawn interval to make enemies spawn faster
        waveManager.SpawnInterval = Mathf.Max(0.1f, waveManager.SpawnInterval - 0.1f); // Ensure it doesn't go below 0.1 seconds
        Debug.Log("Difficulty increased! New spawn interval: " + waveManager.SpawnInterval);
    }

}
