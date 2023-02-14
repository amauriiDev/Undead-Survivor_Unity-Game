using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData 
{
    //* atributos da classe
    int coin;
    int level;
    int xp;
    int xptoLevelUp;
    int enemiesKilled;

    public UserData()
    {
        this.xp = 0;
        this.enemiesKilled = 0;
        this.coin = 0;
        this.level = 1;
        this.xptoLevelUp = 10;
    }

    //* Getter's e Setter's
    public int Level { get => level; private set => level = value; }
    public int XptoLevelUp { get => xptoLevelUp; private set => xptoLevelUp = value; }
    public int Xp { get => xp; set => xp = value; }
    public int EnemiesKilled { get => enemiesKilled; set => enemiesKilled = value; }
    public int Coin { get => coin; set => coin = value; }


    public int XpToNextLevel(){
        XptoLevelUp = this.level * 20;
        return XptoLevelUp;
    }
    public void LevelUp(){
        level++;
        xp-= xptoLevelUp;
        xptoLevelUp = XpToNextLevel();
    }
}
