using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelUpUI : MonoBehaviour
{
    [Header("Atributos anexos Inspector")]

    [SerializeField]private Upgrade[] upgrades;
    [SerializeField]private UpgradeUI[] upgradesUI;

    private void Awake()
    {
        foreach (var upgrade in upgrades){
            upgrade.Level = 1;            
        }
        for (int j = 0; j < upgradesUI.Length; j++){
            int index = upgrades[j].ID;
            upgradesUI[j].GetComponent<Button>().onClick.AddListener(() => OnButtonUpgradeClick(index));
        }
    }

    private void OnEnable()
    {
        SetPowerUps();
    }

    private void SetPowerUps()
    {
        for (int i = 0; i < upgradesUI.Length; i++){
            upgradesUI[i].SetUpgradeUI(upgrades[i]);         
        }
    }

    void OnButtonUpgradeClick(int index){
        Master.Instance.gameManager.LevelUp(index);
        upgrades[index].Level+=1;
        upgradesUI[index].SetLevel(upgrades[index].Level);
    }
}
