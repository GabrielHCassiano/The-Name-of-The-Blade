using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyHitbox") && !playerStatus.InBlock)
        {
            print("Enemy-Hit-");
            playerStatus.HurtLogic(other.GetComponentInParent<EnemyStatus>().Force);
        }
    }
}
