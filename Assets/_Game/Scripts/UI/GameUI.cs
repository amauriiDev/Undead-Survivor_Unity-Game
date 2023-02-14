using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    [Header("Atributos anexos Inspector")]
    [SerializeField]private Slider sliderXpBar;
    [SerializeField]private Slider sliderHpBar;
    [SerializeField]private Text txtLevel;
    [SerializeField]private Text txtEnemiesKilled;
    [SerializeField]private Text txtCoinsAmount;
    [SerializeField]private Text txtTimer;
    [SerializeField]private Text txtWave;


    [Header("CANVA DE JOGO PAUSADO")]
    [SerializeField]private GameObject cnvPauseGame;
    [SerializeField]private Button btnResumeGame;
    [SerializeField]private Button btnBackMenu;
    [SerializeField]private Button btnQuitGame;

    private void OnEnable()
    {
        btnResumeGame.onClick.AddListener(OnButtonResumeGameClick);
        btnBackMenu.onClick.AddListener(OnButtonBackMenuClick);
        btnQuitGame.onClick.AddListener(OnButtonQuiteGameClick);
    }

    public void SetMaxValueXP(int value){
        sliderXpBar.maxValue = value;
    }
    public void SetValueXP(int value){
        sliderXpBar.value = value;
    }
    public void SetMaxValueHP(int value){
        sliderHpBar.maxValue = value;
    }
    public void SetValueHP(int value){
        sliderHpBar.value = value;
    }
    public void SetLevel(int value){
        txtLevel.text = value.ToString();
    }
    public void SetEnemiesKilled(int value){
        txtEnemiesKilled.text = value.ToString();
    }
    public void SetCoins(int value){
        txtCoinsAmount.text = value.ToString();
    }
    public void SetTxtTimer(int minutes, float seconds){

        if (minutes <= 0  && seconds <= 0){
            txtTimer.text = "00:00.00";
            return;
        }
        // formatando a string com minutos e segundos
        txtTimer.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
    public void SetTxtTimer(float time){        // sobrecarga do método
        int minutes = (int)time / 60;
        float seconds = time % 60;

        if (minutes <= 0  && seconds <= 0){
            txtTimer.text = "00:00.00";
            return;
        }
        // formatando a string com minutos e segundos
        txtTimer.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }
    public void SetTxtWave(int value){
        txtWave.text = $"Wave {value}:";
    }

    public void levelUp(int levelValue,int newXp, int maxXpValue){
        txtLevel.text = levelValue.ToString();
        sliderXpBar.value = newXp;
        sliderXpBar.maxValue = maxXpValue;
    }

    public void OnKeyPauseGamePressed(InputAction.CallbackContext context){
        if (context.performed)
        {
            if (cnvPauseGame.activeSelf)
                Resume();
            else
                Pause();
        }
    }

    private void Resume(){
        Master.Instance.gameManager.ResumeGame();
        cnvPauseGame.SetActive(false);
    }
    private void Pause(){
        Master.Instance.gameManager.PauseGame();
        cnvPauseGame.SetActive(true);
    }

    //* Events Button's
    private void OnButtonBackMenuClick(){
        Master.Instance.gameManager.BackToMenu();
    }
    private void OnButtonResumeGameClick(){
        Master.Instance.audioManager.PlaySelectSfx();
        Resume();
    }
    private void OnButtonQuiteGameClick(){
        Master.Instance.audioManager.PlaySelectSfx();
        Application.Quit();
    }

    private void OnDisable()
    {
        btnResumeGame.onClick.RemoveListener(OnButtonResumeGameClick);
        btnBackMenu.onClick.RemoveListener(OnButtonBackMenuClick);
        btnQuitGame.onClick.RemoveListener(OnButtonQuiteGameClick);
    }
}
