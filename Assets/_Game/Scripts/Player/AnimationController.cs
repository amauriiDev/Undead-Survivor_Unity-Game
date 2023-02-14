using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    // Constantes para nome das variáveis no Animator
    private const string isDead = "isDead";
    private const string isWalking = "isWalking";

    // Variáveis de controle
    private bool _isDead;
    private bool _isWalking;

    private void Awake() {
        animator = GetComponent<Animator>();
        _isDead= false;
        _isWalking = false;
    }

    public void setIsDead(bool value){
        _isDead = value;
        animator.SetBool(isDead,value);
    }
    public void setIsWalking(bool value){
        _isWalking = value;
        animator.SetBool(isWalking, value);
    }

    public bool getIsDead(){
        return _isDead;
    }
    public bool getIsWalking(){
        return _isWalking;
    }

}
