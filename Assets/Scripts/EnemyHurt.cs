using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{

    private int life = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyDead();
    }

    public void EnemyDead()
    {
        if (life <= 0)
        {
            life = 0;
            GetComponentInParent<Rigidbody>().gameObject.SetActive(false);
        }
    }

    public void EnemyHurtLogic(int dano)
    {
        life -= dano;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerHitbox"))
        {
            print("-Hit-");
            EnemyHurtLogic(25);

        }
    }
}
