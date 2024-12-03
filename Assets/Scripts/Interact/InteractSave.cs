using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractSave : MonoBehaviour
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerInputs playerInputs;

    [SerializeField] private GameObject camPoint;
    [SerializeField] private GameObject intectPoint;

    [SerializeField] private EnemyStatus[] enemys;

    [SerializeField] private GameObject statusMenu;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI postureText;
    [SerializeField] private TextMeshProUGUI recoveryPowerText;
    [SerializeField] private TextMeshProUGUI forceText;

    // Start is called before the first frame update
    void Start()
    {
        enemys = FindObjectsOfType<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMenuStatus();
        RotatePoint();
    }

    public void RotatePoint()
    {
        intectPoint.transform.rotation = camPoint.transform.rotation;
    }

    public void SetMenuStatus()
    {
        if (statusMenu.activeSelf)
        {
            PauseControl.Instance.CanPause = false;

            playerStatus.CanMove = false;
            playerStatus.CanCam = false;
            playerStatus.CanAttack = false;
            playerStatus.CanDefese = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        levelText.text = "Level: " + playerStatus.Level.ToString();
        pointText.text = "Point: " + playerStatus.Point.ToString();
        lifeText.text = "Life Max: " + playerStatus.LifeMax.ToString();
        postureText.text = "Posture Max: " + playerStatus.PostureMax.ToString();
        forceText.text = "Force: " + playerStatus.Force.ToString();
        recoveryPowerText.text = "Recovery: " + playerStatus.RecoveryPower.ToString();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GroundPlayer"))
        {
            intectPoint.SetActive(true);

            if (playerInputs.InputInteract)
            {
                playerInputs.InputInteract = false;
                intectPoint.SetActive(false);

                statusMenu.SetActive(true);

                playerStatus.ResetStatus();
                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].ResetStatus();
                }
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
