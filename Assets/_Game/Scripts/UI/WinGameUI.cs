using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGameUI : MonoBehaviour
{
    [Header("Atributos anexos Inspector")]
    [SerializeField]Button btnBackMenu;
    
    private void Awake()
    {
        btnBackMenu.onClick.AddListener(OnButtonBackMenuClick);
    }

    private void OnButtonBackMenuClick(){
        Master.Instance.gameManager.BackToMenu();
    } 
}
