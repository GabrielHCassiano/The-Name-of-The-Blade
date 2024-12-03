using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    
    private EnemyStatus enemyStatus;

    // Start is called before the first frame update
    void Start()
    {
        enemyStatus = GetComponentInParent<EnemyStatus>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerHitbox") && !enemyStatus.InDead)
        {
            print("Player-Hit-");
            enemyStatus.HurtLogic(other.GetComponentInParent<PlayerStatus>().Damage + other.GetComponentInParent<PlayerStatus>().Force);
        }
    }
}
