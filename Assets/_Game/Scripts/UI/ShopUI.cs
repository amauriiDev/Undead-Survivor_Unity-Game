using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Atributos anexos Inspector")]
    [SerializeField]private UpgradeUI[] upgradesUI;
    [SerializeField]private Upgrade[] upgrades;

    [SerializeField]private Text txtCoin;
    [SerializeField]private Button btnBackToMenu;
    [SerializeField]private Button btnNextWave;
    

    //variavel de controle
    WaitForSecondsRealtime timeToEnable;
    private void OnEnable()
    {
        EnableNextWaveButton();

        btnBackToMenu.onClick.AddListener(OnButtonBackToMenuClick);
        btnNextWave.onClick.AddListener(OnButtonNextWaveClick);
    }
    private void Start()
    {
        timeToEnable = new WaitForSecondsRealtime(5.0f);
        for (int i = 0; i < upgradesUI.Length; i++)
        {
            upgradesUI[i].SetUpgradeUI(upgrades[i]);
            int id = upgrades[i].ID;
            upgradesUI[i].GetComponent<Button>().onClick.AddListener(()=> OnButtonBuyClick(id));       
        }
       
    }

    private void EnableNextWaveButton(){
        StartCoroutine(IEnableButton());
    }
    private IEnumerator IEnableButton()
    {
        yield return timeToEnable;
        btnNextWave.interactable = true;
    }

    public void BuyItem(int index){
        UpgradeUI localUI = upgradesUI[index];
        Upgrade localUP = upgrades[index];
        
        localUP.Level+=1;
        localUI.SetLevel(localUP.Level);
        localUI.SetCost(localUP.Cost);

        EnableNextWaveButton();
    }
    public void setCoins(int value){
        txtCoin.text = value.ToString();
    }

    public void UpdateAllUpgradesUI(){
        for (int i = 0; i < upgradesUI.Length; i++)
        {
            upgradesUI[i].SetCost(upgrades[i].Cost);
            upgradesUI[i].SetLevel(upgrades[i].Level);
        }
    }
    public void ResetAllUpgrades(){

        foreach (var item in upgrades){
            item.Level = 1; 
        }

    }

    //* Eventos de Botoes
    private void OnButtonBuyClick(int index){
        Master.Instance.audioManager.PlaySelectSfx();
        Master.Instance.gameManager.BuyUpgrade(upgrades[index]);
    }
    private void OnButtonBackToMenuClick(){
        Master.Instance.audioManager.PlaySelectSfx();
        Master.Instance.gameManager.BackToMenu();
    }
    private void OnButtonNextWaveClick(){
        Master.Instance.audioManager.PlaySelectSfx();
        Master.Instance.gameManager.NextWave();
    }

    private void OnDisable()
    {
        btnNextWave.interactable = false;
        btnBackToMenu.onClick.RemoveAllListeners();
        btnNextWave.onClick.RemoveAllListeners();
    }
}
