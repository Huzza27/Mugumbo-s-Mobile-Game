using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySpawner : MonoBehaviour
{
    
    private static bool spawnerEnabled;
    private static EnemySpawner instance;
    public GameObject goblin;


    /*<summary>
     * This method adds or removes the EnemySpawner instance from the activeSpawners list in 'SpawnController'
     * </summary>
     * <params>
     * This method currently takes no parameters, but that will change as I add monster progression
     * </param>
     * <returns>
     * void
     * </returns>
     */
    void AddOrRemoveSpawnerFromActiveList()
    {
        if(spawnerEnabled)
        {
            SpawnController.activeSpawners.Add(instance);
        }
        else
        {
            SpawnController.activeSpawners.Remove(instance);
        }
    }


    /*<summary>
     * This method spawns (right now only a goblin) at the location of this object
     * </summary>
     * <params>
     * This method currently takes no parameters, but that will change as I add monster progression
     * </param>
     * <returns>
     * void
     * </returns>
     */
    public void SpawnEnemy()
    {
        Instantiate(goblin, transform.position, Quaternion.identity);
    }

    private void Start()
    { 
        AddOrRemoveSpawnerFromActiveList();
    }
}
