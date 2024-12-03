using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    private Vector3 enemyPosition;
    private Quaternion enemyRotation;

    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    [SerializeField] private PlayerStatus playerStatus;

    [SerializeField] private bool isBoss;
    [SerializeField] private GameObject endGame;

    [SerializeField] private int lifeMax;
    [SerializeField] private int lifeCurrent;

    [SerializeField] private float postureMax;
    [SerializeField] private float postureCurrent;

    [SerializeField] private Slider lifeBar;
    [SerializeField] private Slider lifeBarDown;
    [SerializeField] private Slider postureBar;

    [SerializeField] private int force = 0;
    [SerializeField] private float speed = 8;

    [SerializeField] private GameObject groundbox;
    [SerializeField] private GameObject hurtbox;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private GameObject enemyUI;

    private RaycastHit hit;
    private Vector3 dir;
    [SerializeField] private float fovAngle;
    [SerializeField] private float range;

    private bool inTag;

    private bool canMove = true, canAttack = true, canDead = true;
    private bool inAttack, inHurt, inDead, inStun;

    private int combo = 0;
    private int inCombo = 0;

    [SerializeField] private int combolenght;


    public void BarLogic()
    {
        lifeBar.value = (float)lifeCurrent / lifeMax;

        if (lifeBar.value != lifeBarDown.value)
            lifeBarDown.value = Mathf.Lerp(lifeBarDown.value, lifeBar.value, 0.02f);

        postureBar.value = postureCurrent / postureMax;
    }


    public void ResetStatus()
    {
        if (enemyPosition != Vector3.zero)
        {
            transform.position = enemyPosition;
            transform.rotation = enemyRotation;
        }

        enemyPosition = transform.position;
        enemyRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        animator.Play("Move");

        lifeCurrent = lifeMax;
        postureCurrent = 0;

        lifeBar.value = (float)lifeCurrent / lifeMax;
        lifeBarDown.value = lifeBar.value;
        postureBar.value = (float)postureCurrent / postureMax;

        canMove = true;
        canAttack = true;
        canDead = true;

        inTag = false;
        inAttack = false;
        inHurt = false;
        inDead = false;

        groundbox.SetActive(true);
        hurtbox.SetActive(true);
        hitbox.SetActive(false);
        enemyUI.SetActive(true);

        combo = 0;
        inCombo = 0;
    }

    public void PostureLogic()
    {
        if (postureCurrent < 0)
            postureCurrent = 0;

        if (postureCurrent >= postureMax)
        {
            StunLogic();
        }

        if (postureCurrent > 0)
        {
            postureCurrent -= 0.5f * Time.deltaTime;
        }

    }

    public void RayCast()
    {
        dir = playerStatus.transform.position - transform.position;
        float angle = Vector3.Angle(dir, transform.forward);

        Ray ray = new Ray(transform.position, dir);
        bool rayHit = Physics.Raycast(ray, out hit, range);

        if (angle < fovAngle / 2 && rayHit == true & hit.collider != null)
        {
            inTag = true;
            Debug.DrawRay(transform.position, dir, Color.red);
        }
        else
            inTag = false;
    }

    public void StartCombo()
    {
        combo = 0;
        canAttack = true;
    }

    public void ResetAttack()
    {
        inAttack = false;
        canAttack = true;
        canMove = true;

        combo = 0;
        inCombo = 0;
    }

    public void SetForce(int value)
    {
        force = value;
    }

    public void MoveAttack(float speed)
    {
        int id = Random.Range(1, 11);

        if(id == 1)
            rb.velocity = -transform.forward * speed;
        else if (id > 1 && id < 4)
            rb.velocity = transform.forward * speed;

    }

    public void ResetMoveAttack()
    {
        rb.velocity = Vector3.zero;
        navMeshAgent.velocity = Vector3.zero;
    }

    public void HurtLogic(int dano)
    {
        canMove = false;
        canAttack = false;
        rb.velocity = Vector3.zero;
        navMeshAgent.velocity = Vector3.zero;
        hitbox.SetActive(false);
        inHurt = true;
        lifeCurrent -= dano;
        postureCurrent += 5;
        combo = 0;
        inCombo = 0;
        StartCoroutine(HurtCooldown());
    }

    public IEnumerator HurtCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        inHurt = false;
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
        rb.velocity = Vector3.zero;
        navMeshAgent.velocity = Vector3.zero;
        hitbox.SetActive(false);
        inAttack = false;
        inHurt = true;
        combo = 0;
        inCombo = 0;
    }

    public void EndStun()
    {
        canMove = true;
        canAttack = true;
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
        enemyUI.SetActive(false);

        float exp = Random.Range(2, 3f);
        playerStatus.ExperienceCurrent += exp;

        if (isBoss)
            endGame.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        inDead = false;
    }

    #region GetandSet

    public PlayerStatus PlayerStatus
    {
        get { return playerStatus; }
        set { playerStatus = value; }
    }

    public float PostureCurrent
    {
        get { return postureCurrent; }
        set { postureCurrent = value; }
    }

    public RaycastHit Hit
    { 
        get { return hit; }
        set {  hit = value; }
    }

    public Vector3 Dir
    {
        get { return dir; }
        set { dir = value; }
    }

    public float FovAngle
    {
        get { return fovAngle; }
        set { fovAngle = value; }
    }

    public float Range
    {
        get { return range; }
        set { range = value; }
    }


    public int Force
    {
        get { return force; }
        set { force = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }
    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    public bool InTag
    {
        get { return inTag; }
        set { inTag = value; }
    }

    public bool InStun
    {
        get { return inStun; }
        set { inStun = value; }
    }

    public bool InAttack
    {
        get { return inAttack; }
        set { inAttack = value; }
    }

    public bool InHurt
    {
        get { return inHurt; }
        set { inHurt = value; }
    }

    public bool InDead
    {
        get { return inDead; }
        set { inDead = value; }
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

    public int ComboLenght
    {
        get { return combolenght; }
        set { combolenght = value; }
    }

    #endregion
}
