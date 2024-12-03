using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGame;

    private bool inEndGame = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool InEndGame
    { 
        get { return inEndGame; } 
        set { inEndGame = value; }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GroundPlayer"))
        {
            inEndGame = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            endGame.SetActive(true);
        }
    }
}
