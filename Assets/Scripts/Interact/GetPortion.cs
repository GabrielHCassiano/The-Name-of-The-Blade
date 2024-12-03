using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetPortion : MonoBehaviour
{
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GroundPlayer"))
        {
            print("asd");

            if (playerInputs.InputInteract)
            {
                playerInputs.InputInteract = false;
                playerStatus.UpPotion();
                gameObject.SetActive(false);
            }

        }
    }
}
