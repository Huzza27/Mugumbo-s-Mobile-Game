using UnityEngine;
using UnityEngine.Rendering;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField] private  float moveSpeed;
    private float distanceToMovePerFrame;
    private float distanceFromEnemyToPlayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //Find player once spanwed
        distanceToMovePerFrame = moveSpeed * Time.deltaTime; // calculate step
    }
    // Update is called once per frame
    void Update()
    {
        HandleEnemyMovementTowardsPlayer();
    }

    void HandleEnemyMovementTowardsPlayer()
    {
        distanceFromEnemyToPlayer = Vector3.Distance(player.position, transform.position);
        if (player != null && distanceFromEnemyToPlayer >= 2f)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.position, distanceToMovePerFrame);

        }
        else
        {
            Debug.Log("No player found!");
        }
    }

}
