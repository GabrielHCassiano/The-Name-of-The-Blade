using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInputs playerInputs;

    [SerializeField] private AudioSource[] audioSource;

    [SerializeField] private GameObject camPoint;

    [SerializeField] private int level = 0;
    [SerializeField] private int point = 0;

    [SerializeField] private float experienceCurrent;
    [SerializeField] private float experienceMax = 10;

    [SerializeField] private int lifeMax = 100;
    [SerializeField] private int lifeCurrent;

    [SerializeField] private float postureMax = 100;
    [SerializeField] private float postureCurrent;

    private int portionCurrent;
    private int portionMax = 1;

    [SerializeField] private Slider experieceBar;
    [SerializeField] private Slider lifeBar;
    [SerializeField] private Slider lifeBarDown;
    [SerializeField] private Slider postureBar;

    [SerializeField] private TextMeshProUGUI portionText;

    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] private GameObject groundbox;
    [SerializeField] private GameObject hurtbox;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private GameObject defese;

    private int recoveryPower = 20;
    private int force = 5;
    [SerializeField] private int damage = 0;
    [SerializeField] private float speed = 8;
    [SerializeField] private float runSpeed = 16;

    [SerializeField] private bool canMove = true, canCam = true, canAttack = true, canDefese = true, canRecovery = true, canDead = true;
    [SerializeField] private bool inRun, inAttack, inDefese, inBlock, inParry, inHurt, inDead, inRecovery, inStun;

    private int combo = 0;
    private int inCombo = 0;

    #region Methods

    public void BarLogic()
    {
        experieceBar.value = experienceCurrent / experienceMax;

        lifeBar.value = (float) lifeCurrent / lifeMax;

        if(lifeBar.value != lifeBarDown.value)
            lifeBarDown.value = Mathf.Lerp(lifeBarDown.value, lifeBar.value, 0.02f);

        postureBar.value = postureCurrent / postureMax;

        portionText.text = portionCurrent.ToString() + " / " + portionMax.ToString();
    }

    public void ResetStatus()
    {
        rb = GetComponent<Rigidbody>();
        playerInputs = GetComponentInChildren<PlayerInputs>();

        playerInputs.ResetAllInputs();

        lifeCurrent = lifeMax;
        postureCurrent = 0;

        portionCurrent = portionMax;

        experieceBar.value = experienceCurrent / experienceMax;
        lifeBar.value = (float)lifeCurrent / lifeMax;
        lifeBarDown.value = lifeBar.value;
        postureBar.value = postureCurrent / postureMax;

        portionText.text = portionCurrent.ToString() + " / " + portionMax.ToString();

        canMove = true;
        canCam = true;
        canAttack = true;
        canDefese = true;
        canRecovery = true;
        canDead = true;

        inRun = false;
        inAttack = false;
        inDefese = false;
        inBlock = false;
        inParry = false;
        inHurt = false;
        inStun = false;
        inDead = false;
        inRecovery = false;

        groundbox.SetActive(true);
        hurtbox.SetActive(true);
        hitbox.SetActive(false);
        defese.SetActive(false);

        combo = 0;
        inCombo = 0;
    }

    public void LevelLogic()
    {
        if (experienceCurrent >= experienceMax)
        {
            experienceCurrent -= experienceMax;
            experienceMax += 5;
            level += 1;
            point += 1;
        }
    }

    public void PostureLogic()
    {
        if (postureCurrent < 0)
            postureCurrent = 0;

        if (postureCurrent >= postureMax)
        {
            StunLogic();
        }

        if (postureCurrent > 0 && !inDefese)
        {
            postureCurrent -= 0.5f * Time.deltaTime;
        }
        else if (postureCurrent > 0 && inDefese)
        {
            postureCurrent -= 3f * Time.deltaTime;
        }

    }

    public void UpLife()
    {
        if (point > 0 && lifeMax < 200)
        {
            point -= 1;
            lifeMax += 10;
        }
    }

    public void UpPosture()
    {
        if (point > 0 && postureMax < 150)
        {
            point -= 1;
            postureMax += 5;
        }
    }

    public void UpForce()
    {
        if (point > 0 && force < 15)
        {
            point -= 1;
            force += 1;
        }
    }

    public void UpPortionPower()
    {
        if (point > 0 && recoveryPower < 40)
        {
            point -= 1;
            recoveryPower += 2;
        }
    }

    public void UpPotion()
    {
        portionMax += 1;
        portionCurrent += 1;
    }

    public void UserPortion()
    {
        if (portionCurrent > 0)
        {
            portionCurrent -= 1;
            if (lifeCurrent + recoveryPower > lifeMax)
                lifeCurrent = lifeMax;
            else if (lifeCurrent + recoveryPower <= lifeMax)
                lifeCurrent += recoveryPower;
        }
    }

    public void ResetRecovery()
    {
        inRecovery = false;
    }

    public void ResetStatusMenu()
    {
        StartCoroutine(CoolDownReset());
    }

    public IEnumerator CoolDownReset()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yield return new WaitForSeconds(0.2f);
        ResetStatus();
        PauseControl.Instance.CanPause = true;
    }


    public void StunLogic()
    {
        StartCoroutine(CoolDownStun());
    }

    public IEnumerator CoolDownStun()
    {
        inStun = true; 
        postureCurrent = 0;
        yield return new WaitForSeconds(0.2f);
        inStun = false;
        canMove = false;
        canAttack = false;
        canDefese = false;
        canRecovery = false;
        rb.velocity = Vector3.zero;
        hitbox.SetActive(false);
        defese.SetActive(false);
        inAttack = false;
        inDefese = false;
        inRecovery = false;
        inHurt = false;
        inBlock = false;
        combo = 0;
        inCombo = 0;
    }

    public void StartCombo()
    {
        audioSource[0].Play();
        rb.velocity = Vector3.zero;
        combo = 0;
        canAttack = true;
    }

    public void ResetAttack()
    {
        inAttack = false;
        canAttack = true;
        canDefese = true;
        canRecovery = true;
        canMove = true;

        combo = 0;
        inCombo = 0;
    }

    public void SetForce(int value)
    {
        damage = value;
    }

    public void MoveAttack(float speed)
    {
        rb.velocity = transform.forward * speed;
    }

    public void ResetMoveAttack()
    {
        rb.velocity = Vector3.zero;
    }

    public void HurtLogic(int dano)
    {
        StartCoroutine(HurtCooldown());
        canMove = false;
        canAttack = false;
        canDefese = false;
        canRecovery = false;
        rb.velocity = Vector3.zero;
        hitbox.SetActive(false);
        defese.SetActive(false);
        inAttack = false;
        inDefese = false;
        inRecovery = false;
        inHurt = true;
        inBlock = false;
        lifeCurrent -= dano;
        postureCurrent += 5;
        combo = 0;
        inCombo = 0;
    }

    public IEnumerator HurtCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        inHurt = false;
    }

    public void DefeseLogic()
    {
        audioSource[1].Play();
        StartCoroutine(DefeseCooldown());
        rb.velocity = Vector3.zero;
        postureCurrent += 10; 
        combo = 0;
        inCombo = 0;
    }

    public void ParryLogic(EnemyStatus enemyStatus)
    {
        audioSource[2].Play();
        inBlock = false;
        inParry = false;
        rb.velocity = Vector3.zero;
        postureCurrent += 5;
        enemyStatus.PostureCurrent += 10;
        combo = 0;
        inCombo = 0;
    }

    public IEnumerator DefeseCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        inBlock = false;
    }

    public void StartParry()
    {
        inParry = true;
    }

    public void EndParry()
    {
        inParry = false;
    }

    public void EndDefese()
    {
        canAttack = true;
        CanRecovery = true;
        inDefese = false;
    }

    public void EndStun()
    {
        canMove = true;
        canAttack = true;
        canDefese = true;
        canRecovery = true;
        inHurt = false;
    }

    public void Dead()
    {
        if (lifeCurrent <= 0 && canDead)
        {
            StartCoroutine(DeadCooldown());
        }
    }

    public IEnumerator DeadCooldown()
    {
        canDead = false;
        canMove = false;
        canAttack = false;
        lifeCurrent = 0;
        inDead = true;
        groundbox.SetActive(false);
        hurtbox.SetActive(false);
        hitbox.SetActive(false);
        defese.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        inDead = false;
    }

    public void GameOverMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
    }

    #endregion

    #region GetandSet

    public AudioSource[] AudioSource
    {
        get { return audioSource; }
        set { audioSource = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Point
    {
        get { return point; }
        set { point = value; }
    }

    public float ExperienceCurrent
    {
        get { return experienceCurrent; }
        set { experienceCurrent = value; }
    }

    public int LifeCurrent
    {
        get { return lifeCurrent; }
        set { lifeCurrent = value; }
    }

    public int LifeMax
    {
        get { return lifeMax; }
        set { lifeMax = value; }
    }
    public float PostureCurrent
    {
        get { return postureCurrent; }
        set { postureCurrent = value; }
    }

    public float PostureMax
    {
        get { return postureMax; }
        set { postureMax = value; }
    }

    public int PortionCurrent
    {
        get { return portionCurrent; }
        set { portionCurrent = value; }
    }

    public int RecoveryPower
    {
        get { return recoveryPower; }
        set { recoveryPower = value; }
    }

    public int Force
    {
        get { return force; }
        set { force = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float Speed
    { 
        get { return speed; }
        set { speed = value; }
    }
    public float RunSpeed
    {
        get { return runSpeed; }
        set { runSpeed = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool CanCam
    {
        get { return canCam; }
        set { canCam = value; }
    }

    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public bool CanDefese
    {
        get { return canDefese; }
        set { canDefese = value; }
    }

    public bool CanRecovery
    {
        get { return canRecovery; }
        set { canDefese = value; }
    }

    public bool InRun
    {
        get { return inRun; }
        set { inRun = value; }
    }

    public bool InAttack
    {
        get { return inAttack; }
        set { inAttack = value; }
    }

    public bool InDefese
    {
        get { return inDefese; }
        set { inDefese = value; }
    }

    public bool InBlock
    {
        get { return inBlock; }
        set { inBlock = value; }
    }

    public bool InParry
    {
        get { return inParry; }
        set { inParry = value; }
    }

    public bool InHurt
    {
        get { return inHurt; }
        set { inHurt = value; }
    }

    public bool InStun
    {
        get { return inStun; }
        set { inStun = value; }
    }

    public bool InDead
    {
        get { return inDead; }
        set { inDead = value; }
    }

    public bool InRecovery
    {
        get { return inRecovery; }
        set { inRecovery = value; }
    }

    public int Combo
    {
        get { return combo; }
        set { combo = value; }
    }

    public int InCombo
    {
        get { return inCombo; }
        set { inCombo = value; }
    }

    #endregion
}
