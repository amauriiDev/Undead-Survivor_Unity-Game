using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    [Header("ATRIBUTO INDIVIDUAL")]
    [SerializeField]private int amount = 1;

    //* variaveis anexadas no inspector
    [SerializeField]AudioSource audioSourceSfx;
    [SerializeField]AudioClip coinSfx;


   private void PickUp(){
        Master.Instance.gameManager.UpdateCoin(amount);
        audioSourceSfx.PlayOneShot(coinSfx);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this.gameObject,0.3f);
   }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PickUp();
    }
}
