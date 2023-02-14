using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade: MonoBehaviour
{
    [Header("VARIAVEIS INDIVIDUAIS")]
    [SerializeField]private int id;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private string title;
    [SerializeField]private string description;
    [SerializeField]private Color colorText;

    //Variaveis da classe (incrementaveis)
    //[SerializeField]
    private int cost;
    //[SerializeField]
    private int level;


    //* GETTER'S E SETTER'S
    public int ID{ get => id;}
    public string Title { get => title;}
    public string Description { get => description;}
    public SpriteRenderer SpriteRenderer { get => spriteRenderer;}
    public Color ColorText { get => colorText;}

    
    public int Level { get => getLevel(); set => level = value; }
    public int Cost { get => getCost();}

    int getCost(){
        int[] costValues = {20, 50, 100, 120, 190, 220, 260, 290, 350, 500};
        return costValues[this.level-1];
    }
    int getLevel(){
       return (this.level > 9) ?10 : this.level;
    }
}
