using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{

    private Animator animator;
    private Rigidbody2D rigid2D;
    private AudioController audioController;

    [Header("ATRIBUTOS INDIVIDUAIS")]
    [SerializeField]private float initialSpeed = 1.5f;
    [SerializeField]private int initialDamage = 1;
    [SerializeField]private int initialHealth = 3;
    [SerializeField]private int xpAmount = 1;
    [SerializeField]private GameObject coinPrefab;

    /// atributos da classe
    //[SerializeField]
    private Transform player;
    //[SerializeField]
    private Vector2 movement;
    //[SerializeField]
    private int damage;
    //[SerializeField]
    private int health;
    //[SerializeField]
    private float speed;


    // Eventos
    public static event Action OnHitPlayer;
    public static event Action OnDeath;


    private void OnEnable()
    {
        PlayerScript.OnDead+= UnLinkPlayer;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();
        audioController = GetComponent<AudioController>();
        speed = initialSpeed;
        health = initialHealth;
        damage = initialDamage;
        try{
            setPlayer();
        }
        catch (System.Exception){
            Debug.Log("Jogador nao foi encontrado");
            throw;
        }
    }

    void FixedUpdate(){
        if (!player)
        {
            return;
        }
        Vector3 distance = player.position - transform.position;

        if (distance.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(distance.x), 1, 1);

        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
        
    }

    private void setPlayer(){
        player = Master.Instance.player.transform;
    }
    private void UnLinkPlayer(){
        player = null;
    }

    void IDamageable.TakeDamage(int damage){
        health -= damage;
        animator.SetTrigger("tgrHit");
        audioController.PlayTakeHit();
        if (health <= 0)
        {
            Death();
            Instantiate(coinPrefab, transform.position,Quaternion.identity);
        }
    }

    private void Death(){
        OnDeath?.Invoke();
        audioController.PlayDead();
        Master.Instance.gameManager.UpdateXp(xpAmount);

        rigid2D.velocity = Vector2.zero;
        GetComponent<CapsuleCollider2D>().enabled = false;
        player = null;
        animator.SetBool("isDead", true);
        GetComponentInChildren<SpriteRenderer>().sortingOrder--;
        
        float secondsToDestroyed = 5.0f;
        GameObject.Destroy(this.gameObject, secondsToDestroyed);
    }

    void OnCollisionEnter2D(Collision2D other){
        
        if (!other.collider.CompareTag("Player"))
            return;
        
        other.collider.GetComponent<IDamageable>().TakeDamage(damage);
        OnHitPlayer?.Invoke();
    }

    private void OnDisable()
    {
        PlayerScript.OnDead -=UnLinkPlayer;
    }
}
