using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    // constantes
    const float initalSpeed = 5.0f;
    

    // Instancias de outras classes
    PlayerInput playerInput;
    PlayerScript playerScript;
    AnimationController animationController;


    //* atributos da classe
    //[SerializeField]
    private Vector2 movement;
    //[SerializeField]
    private float speed;

    //* Getter's e Setter's
    public float Speed { get => speed; set => speed = value; }

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
        playerScript = GetComponent<PlayerScript>();
        animationController = GetComponentInChildren<AnimationController>();

        speed = initalSpeed;
    }
   
    void FixedUpdate()
    {
        Vector3 vec3Movement = movement * speed * Time.fixedDeltaTime;
        transform.position+=vec3Movement;
    }

    public void Movement(InputAction.CallbackContext content){
        movement = content.ReadValue<Vector2>();

        if (!playerScript.IsTargeting)
            setLocalRotation(movement.x);

        if (content.started){
            playerScript.IsAttacking = false; 
            playerScript.IsMoving = true; 
            animationController.setIsWalking(true);
        }
        if (content.canceled){
            playerScript.IsAttacking = true; 
            playerScript.IsMoving = false;  
            animationController.setIsWalking(false);
        }
    }

    // direcionando o sprite do jogador para andar pro lado correto ou mirando o inimigo;
    public void setLocalRotation(float direction){
        if (direction == 0)
            return;

        transform.localRotation = (direction > 0)
        ?   Quaternion.Euler(0, 0, 0)
        :   transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
