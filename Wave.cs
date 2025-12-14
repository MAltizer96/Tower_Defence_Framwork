using System.Collections.Generic;


[System.Serializable]
public class Wave
{
    public List<EnemyCount> enemiesTypes;
}

[System.Serializable]
public class EnemyCount
{
    public Enemy enemyPrefab;
    public int count;
}