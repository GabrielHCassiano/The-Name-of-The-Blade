using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefese : MonoBehaviour
{
    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GetComponentInParent<PlayerStatus>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyHitbox") && !playerStatus.InHurt && !playerStatus.InParry && !playerStatus.InDead)
        {
            print("Enemy-Hit (Defese)");
            playerStatus.InBlock = true;
            playerStatus.DefeseLogic();
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyHitbox")&& !playerStatus.InHurt && playerStatus.InParry && !playerStatus.InDead)
        {
            print("Enemy-Hit (Parry)");
            playerStatus.InBlock = true;
            playerStatus.ParryLogic(other.GetComponentInParent<EnemyStatus>());
        }
    }
}
