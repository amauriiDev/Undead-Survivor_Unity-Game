using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    private const string sceneGame = "Game";

    [Header("Atributos anexos Inspector")]
    [SerializeField]private Button btnPlay;
    [SerializeField]private Button btnExit;

    [Header("Audio")]
    [SerializeField]private AudioManager audioManager;

    void Awake()
    {
        btnPlay.onClick.AddListener(()=> OnButtonPlayClick());
        btnExit.onClick.AddListener(OnButtonExitClick);
    }
    private void Start()
    {
        audioManager = GetComponentInChildren<AudioManager>();
    }

    void PlayGame(){
        audioManager.PlaySelectSfx();
        SceneManager.LoadScene(sceneGame);
    }

    void ExitGame(){
        Application.Quit();
    }

    void OnDisable()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
    }

    void OnButtonPlayClick(){
        PlayGame();
    }
    void OnButtonExitClick(){
       ExitGame();
    }
}
