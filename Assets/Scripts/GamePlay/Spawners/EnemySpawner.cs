using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static bool spawnerEnabled;
    private static EnemySpawner instance;
    public GameObject goblin;
    private bool canSpawn = true;
    public float spawnerDelay;

    public delegate void SpawnerStatusChanged(EnemySpawner spawner, bool enabled);
    public static event SpawnerStatusChanged OnSpawnerStatusChanged;

    private void Start()
    {
        instance = this;
        SetSpawnerEnabled(true, instance); // Initially add to active spawners list
    }

    /*<summary>
     * This method spawns (right now only a goblin) at the location of this object
     * </summary>
     */
    public void SpawnEnemy()
    {
        if(!canSpawn)
        {
            Debug.Log("Spawner is cooling down");
            return;
        }
        Debug.Log("Spawning Enemy");
        Instantiate(goblin, transform.position, Quaternion.identity);
        SpawnController.enemyCount++;
        Debug.Log(SpawnController.enemyCount + " enemies spawned");
        canSpawn = false;
        

    }

    



    /*<summary>
     * Static method to retrieve the instance of this class
     * </summary>
     * <return>
     * returns enemy spawner instance
     * </return>
     */
    public static EnemySpawner GetInstance()
    {
        return instance;
    }

    /*<summary>
    * Retrieves value of spawnerEnabled bool
    * </summary>
    * <return>
    * returns value of spawnerEnabled
    * </return>
    */
    public static bool isEnabled()
    {
        return spawnerEnabled;
    }


    /*<summary>
    * Setter function for spawnerEnabled property
    * </summary>
    */
    public static void SetSpawnerEnabled(bool value, EnemySpawner spawner)
    {
        spawnerEnabled = value;
        OnSpawnerStatusChanged?.Invoke(spawner, value);
    }
}
