using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour
{
    #region Singleton Without DontDestroy
    private static Master instance;
    public static Master Instance { get { return instance; } }


    
    public GameManager gameManager {get; private set;}
    public GameObject player {get; private set;}
    public AudioManager audioManager {get; private set;}
    
    private void Awake() {
        
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }else{
            instance = this;
        }

        gameManager = GetComponentInChildren<GameManager>();
        player = GetComponentInChildren<PlayerController>().gameObject;
        audioManager = GetComponentInChildren<AudioManager>();
        
    }
    #endregion
}
