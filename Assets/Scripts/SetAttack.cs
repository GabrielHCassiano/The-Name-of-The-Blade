using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack()
    {
        playerMovement.StartAttack();
    }

    public void ResetAttack()
    {
        playerMovement.ResetAttack();
    }
}
