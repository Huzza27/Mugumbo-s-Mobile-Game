using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] public static ArrayList activeSpawners;

    Transform spawnPoint;
    public static int enemyCount;
    int maxEnemyCount = 10;
    private int spawnerIndex;

    bool canSpawn = true;
    public float spawnDelay;

    public Material activeMaterial, inActiveMaterial;




    private void Awake()
    {
        activeSpawners = new ArrayList();
    }

    /*<summary>
     * This selects a spawner at random from activeSpawners arraylist. Then runs the spawn command
     * </summary>
     */
    public void SelectSpawnerAndSpawnEnemy()
    {
        spawnerIndex = Random.Range(0, activeSpawners.Count); // Get a random transform from 'spawnerIndex' arraylist
        EnemySpawner spawner = (EnemySpawner)activeSpawners[spawnerIndex]; // Get the EnemySpawner instance      
        spawner.SpawnEnemy(); // Run the spawn method
    }

    private void Update()
    {
        if(enemyCount < maxEnemyCount && canSpawn) // And the player is in the dungeon
        {
            SelectSpawnerAndSpawnEnemy();
            canSpawn = false;
            StartCoroutine("cooldownTimer", spawnDelay);
        }
    }

    IEnumerator cooldownTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        canSpawn = true;
    }

    private void OnEnable()
    {
        EnemySpawner.OnSpawnerStatusChanged += HandleSpawnerStatusChanged; //Add HandleSpawnerStatusChanged to event delagate in EnemySpawner
    }

    private void OnDisable()
    {
        EnemySpawner.OnSpawnerStatusChanged -= HandleSpawnerStatusChanged; //Remove HandleSpawnerStatusChanged from event delagate in EnemySpawner
    }

    /*<summary>
     * Adds or removes a spawner from the actvive spawners list
     * </summary>
     */
    private void HandleSpawnerStatusChanged(EnemySpawner spawner, bool enabled)
    {
        if (enabled)
        {
            activeSpawners.Add(spawner);
            spawner.gameObject.GetComponent<MeshRenderer>().material = activeMaterial;
        }
        else
        {
            activeSpawners.Remove(spawner);
            spawner.gameObject.GetComponent<MeshRenderer>().material = inActiveMaterial;
        }
    }

    public void PrintList()
    {
        string output = "Spawners in list:";
        foreach (EnemySpawner spawner in activeSpawners)
        {
            output += spawner.ToString();
        }
        Debug.Log(output);
    }
}
