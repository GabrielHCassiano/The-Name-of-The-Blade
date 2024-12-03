using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractChest : MonoBehaviour
{
    [SerializeField] private PlayerInputs playerInputs;

    [SerializeField] private GameObject camPoint;
    [SerializeField] private GameObject intectPoint;

    private bool openChest = false;

    [SerializeField] private GameObject iten;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePoint();
    }

    public void RotatePoint()
    {
        if (camPoint != null)
            intectPoint.transform.rotation = camPoint.transform.rotation;
    }

    public void ItenDrop()
    {
        if (iten != null)
        {
            iten.SetActive(true);
            //iten.transform.parent = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GroundPlayer") && !openChest)
        {
            intectPoint.SetActive(true);

            if(playerInputs.InputInteract)
            {
                openChest = true;
                animator.SetBool("open", true);
                intectPoint.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GroundPlayer"))
        {
            intectPoint.SetActive(false);
        }
    }
}
