using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseControl : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private EndGame endGame;

    private static PauseControl instance;

    private bool inPause = false;

    private bool canPause = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        print("Master : " + PlayerPrefs.GetFloat("Master", 0.5f));
        print("Music : " + PlayerPrefs.GetFloat("Music", 0.5f));
        print("Effects : " + PlayerPrefs.GetFloat("Effects", 0.5f));
        PauseLogic();
    }

    public void PauseLogic()
    {

        if (playerInputs.InputPause && canPause && playerStatus.LifeCurrent > 0 && !endGame.InEndGame)
        {
            playerInputs.InputPause = false;
            inPause = !inPause;
            if (inPause && canPause)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                playerStatus.CanMove = false;
                playerStatus.CanCam = false;
                playerStatus.CanAttack = false;
                playerStatus.CanDefese = false;
                pauseMenu.SetActive(true);
            }
            if (!inPause && canPause)
            {
                StartCoroutine(PauseCooldown());
            }
        }
    }

    public void ExitPause()
    {
        StartCoroutine(PauseCooldown());
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public IEnumerator PauseCooldown()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inPause = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        playerStatus.CanMove = true;
        playerStatus.CanCam = true;
        playerStatus.CanAttack = true;
        playerStatus.CanDefese = true;
    }

    public static PauseControl Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    public bool CanPause
    {
        get { return canPause; }
        set { canPause = value; }
    }
}
