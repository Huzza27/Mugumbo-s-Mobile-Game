using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static ArrayList activeSpawners;

    Transform spawnPoint;
    static int enemyCount;
    int maxEnemyCount = 10;
    private int spawnerIndex;


    //Temporary Single Goblin Prefab
    [SerializeField] GameObject goblin;


    /*<summary>
     * This selects a spawner at random from activeSpawners arraylist. Then runs the spawn command
     * </summary>
     * <params>
     * This method currently takes no parameters, but that will change as I add monster progression
     * </param>
     * <returns>
     * void
     * </returns>
     */
    public void SelectSpawnerAndSpawnEnemy()
    {
        spawnerIndex = Random.Range(0, activeSpawners.Count); //Get a random transform from 'spawnerIndex' arraylist

        EnemySpawner spawner = (EnemySpawner)activeSpawners[spawnerIndex];   //Get the EnemySpawner instance      
        spawner.SpawnEnemy(); //Run the spawn method
    }

    private void Update()
    {
        while (enemyCount < maxEnemyCount) //And the player is in the dungeon
        {
            SelectSpawnerAndSpawnEnemy();
        }
    }

}
