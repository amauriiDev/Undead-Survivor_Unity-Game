using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI: MonoBehaviour
{
    [Header("Atributos anexos Inspector")]
    [SerializeField]private Image imageUI;
    [SerializeField]private Text titleUI;
    [SerializeField]private Text descriptionUI;
    [SerializeField]private Text levelUI;
    [SerializeField]private Text costUI;


    public void SetUpgradeUI(Upgrade upgrade){
        imageUI.sprite = upgrade.SpriteRenderer.sprite;
        titleUI.text = upgrade.Title;
        titleUI.color = upgrade.ColorText;
        descriptionUI.text = upgrade.Description;
        levelUI.text = SetLevelString(upgrade.Level);
        if(costUI) costUI.text = upgrade.Cost.ToString();
    }
    public void SetSprite(Sprite sprite){
        imageUI.sprite = sprite;
    }
    public void SetTitle(string title, Color color = default){
        titleUI.text = title;
        if (color != default)
            titleUI.color = color;
    }
    public void SetDescription(string description){
        descriptionUI.text = description;
    }
    public void SetLevel(int level){

        levelUI.text = (level > 9)
            ?$"Lv. MAX"
            :$"Lv. {level}";
    }
    private string SetLevelString(int level){
        return (level > 9)
            ?$"Lv. MAX"
            :$"Lv. {level}";
    }
    public void SetCost(int value){
        costUI.text = value.ToString();
    }
}
