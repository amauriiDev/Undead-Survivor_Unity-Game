using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //* Atributos do GameObject
    Animator animator;
    Rigidbody2D rigid2D;


    //constante(atributos) da classe
    //[SerializeField]
    private const float speed = 5.0f;
    //[SerializeField]
    private const float timeHitAnimation = 0.125f;
    //[SerializeField]
    private const int normalAngle = -90;    //rotate sprite to right


    [SerializeField]private int damage;

    private void Awake() {
        animator = GetComponent<Animator>();
        rigid2D = GetComponent<Rigidbody2D>();

    }

    private void Start() {

        //? Caso nao acerte ninguem em 5 sec, sera destruido
        Destroy(this.gameObject, 5);
    }

    public void Initi(int damage, Transform targetPosition, float playerDirection){
        this.damage = damage;
        
        Vector3 dir = targetPosition.position - transform.position;
        Vector3 rotation = Quaternion.LookRotation(dir).eulerAngles;
        rigid2D.velocity = dir * speed;
        transform.eulerAngles = new Vector3(0, playerDirection, normalAngle-rotation.x);
    }

    private void HitTarget(){
        rigid2D.velocity = Vector2.zero;
        animator.SetTrigger("tgrHit");
        Destroy(this.gameObject, timeHitAnimation);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy"))
        {
            HitTarget();
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
