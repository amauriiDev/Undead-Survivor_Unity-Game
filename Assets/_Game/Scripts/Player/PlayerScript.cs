using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IDamageable
{


    //constantes
    const int initalHealth = 5;
    const int initalDamage = 1;
    const float initalAttackRange = 3.1f;
    const float initialAttackspeedCooldown = 1.0f;


    // atributos do GameObject
    PlayerController playerController;
    AnimationController animationController;
    AudioController audioController;


    [Header("Atributos Anexos no Inspector")]
    [SerializeField]private GameObject bulletPref;
    [SerializeField]private SpawnBullet spawnBullet;


    //* atributos da classe
    //[SerializeField]
    private int health;
    //[SerializeField]
    private int damage;
    //[SerializeField]
    private float attackSpeedCooldown;


    //[SerializeField]
    private LayerMask enemyLayer;
    //[SerializeField]
    private float attackRange;
    //[SerializeField]
    private bool isAttacking;
    //[SerializeField]
    private bool isMoving;
    //[SerializeField]
    private bool isTargeting;
    //[SerializeField]
    private bool isAlive;

    //"EVENTOS"
    public static event Action OnDead;

    // variaveis de delta time
    //[SerializeField]
    private float currentAttackSpeedCooldown;
    

    //* Getter's e Setter's
    public int Health { get => health;}
    public int Damage { get => damage;}
    public bool IsTargeting { get => isTargeting; set => isTargeting = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }

    private void Awake()
    {
        health = initalHealth;
        damage = initalDamage;
    }


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animationController = GetComponentInChildren<AnimationController>();
        audioController = GetComponent<AudioController>();
        spawnBullet = GetComponentInChildren<SpawnBullet>();
        enemyLayer = LayerMask.GetMask("Enemy");

        attackRange = initalAttackRange;
        attackSpeedCooldown = initialAttackspeedCooldown;
        currentAttackSpeedCooldown = initialAttackspeedCooldown;

        isAlive = true;
        isMoving = false;
        isAttacking = true;
        isTargeting = false;

    }

    private void FixedUpdate()
    {
        Attack();
    }

    void IDamageable.TakeDamage(int damage){
        this.health-= damage;
        audioController.PlayTakeHit();
        if (health <= 0)
        {
            Dead();
        }
    }

    private void Attack()
    {
       currentAttackSpeedCooldown -= Time.fixedDeltaTime;

        RaycastHit2D hit = Physics2D.CircleCast(
            transform.position, attackRange, Vector2.zero, 1, enemyLayer);
        
        if(!hit){
            currentAttackSpeedCooldown = attackSpeedCooldown;
            IsTargeting = false;
            return;
        }

        GameObject bullet = null;
        isTargeting = true;
        isAttacking = true;

        if (currentAttackSpeedCooldown < 0.0f && !isMoving && isAlive)
        {
            currentAttackSpeedCooldown = attackSpeedCooldown;
            // atirar no primeiro inimigo encontrado na area
            float distance = hit.transform.position.x - transform.position.x;
            playerController.setLocalRotation(distance);
            audioController.PlayShoot();
            bullet = Instantiate(bulletPref, spawnBullet.getPosition(), Quaternion.identity);
            bullet.GetComponent<Bullet>().Initi(damage, hit.transform, transform.rotation.y * 180);
        }
    }

    private void Dead()
    {
        OnDead?.Invoke();
        isAlive = false;
        playerController.enabled = false;
        animationController.setIsDead(true);
        animationController.GetComponentInChildren<Weapon>().gameObject.SetActive(false);


    }
    public void IncreaseDamage(){
        this.damage+=1;
    }
    public void IncreaseSpeed(){
        playerController.Speed +=  playerController.Speed* 0.05f;
    }
    public void IncreaseHealth(){
        this.health+=1;
    }

    public void IncreaseAttackSpeed(int currentLevel){
        //nao é a coisa mais esperta a se fazer mas funciona
        float[] speeds = {1.0f, .70f, .50f, .45f, .40f, .30f, .25f, .20f, .15f, .08f};
        this.attackSpeedCooldown = speeds[currentLevel-1];
    }
}
