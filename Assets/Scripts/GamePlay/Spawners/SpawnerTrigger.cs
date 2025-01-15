using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Spawner"))
        {
            EnemySpawner spawner = collision.GetComponent<EnemySpawner>();
            if (spawner != null && EnemySpawner.isEnabled())
            {
                Debug.Log("Removing " + spawner.name + "from list");
                EnemySpawner.SetSpawnerEnabled(false, spawner);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spawner"))
        {
            EnemySpawner spawner = other.GetComponent<EnemySpawner>();
            if (spawner != null && !EnemySpawner.isEnabled())
            {
                Debug.Log("Adding " + spawner.name + "from list");
                EnemySpawner.SetSpawnerEnabled(true, spawner);
            }
        }
    }
}
