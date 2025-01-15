using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AttackRange")
        {
            //Start attack
        }
    }
}
