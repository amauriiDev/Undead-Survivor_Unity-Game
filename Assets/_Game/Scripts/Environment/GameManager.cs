using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Wave {
    [SerializeField]private int id;             // 1 a 10, número da onda
    [SerializeField]private float timer;        // tempo de conclusão da onda
    [SerializeField]private int enemiesType;    // 0 a 5[exclusivo], quais inimigos serão invocados
    [SerializeField]private int enemiesQuant;   // quantidade de inimigos que serão invocados de uma vez
    [SerializeField]private float spawnRate;    // intervalo de 'spawn' dos inimigos

    public int Id { get => id;}
    public float Timer { get => timer;}
    public int EnemiesType { get => enemiesType;}
    public int EnemiesQuant { get => enemiesQuant;}
    public float SpawnRate { get => spawnRate;}
}
public class GameManager : MonoBehaviour
{
    [Header("CONSTANTES")]
    private const string sceneMenu = "MainMenu";

    [Header("INSTÂNCIAS DE OUTRAS CLASSES")]
    UserData userData;
    PlayerScript playerScript;
    AudioManager audioManager;

    //* objetos anexados no inspector
    [SerializeField]Spawner spawner;
    [SerializeField]GameUI gameUI;
    [SerializeField]LevelUpUI levelUpUI;
    [SerializeField]GameOverUI gameOverUI;
    [SerializeField]WinGameUI winGameUI;
    [SerializeField]ShopUI shopUI;

    [Header("ATRIBUTOS")]
    //[SerializeField]
    private int currentWave;

    [Header("Classe Editavel")]
    [SerializeField]private Wave[] waves;

    //[SerializeField]
    private float currentTime;


    private void Awake()
    {
        shopUI.ResetAllUpgrades();
    }
    private void OnEnable()
    {
        EnemyBehaviour.OnHitPlayer+= UpdateHealth;
        EnemyBehaviour.OnDeath+= UpdateEnemiesScore;
        PlayerScript.OnDead+= GameOver;
    }

    private void Start()
    {
        userData = new UserData();
        playerScript = Master.Instance.player.GetComponent<PlayerScript>();
        audioManager = Master.Instance.audioManager.GetComponent<AudioManager>();
        currentTime = waves[0].Timer;
        currentWave = 0;
        Time.timeScale = 1;
        InitiGameUI();
    }

    private void FixedUpdate()
    {
        currentTime -= Time.fixedDeltaTime;
        int minutes = (int)currentTime / 60;
        float seconds = currentTime % 60;
        if (currentTime <= 0.0f)
        {
            WonTheWave();
            gameUI.SetTxtTimer(0);
        }
        gameUI.SetTxtTimer(minutes, seconds);

    }

    private void WonTheWave()
    {
        PauseGame();
        spawner.enabled= false;
        audioManager.PlayWinSfx();
        StartCoroutine(CleanArea());

        currentWave+=1;
        if (currentWave  > 9)
        {
            winGameUI.gameObject.SetActive(true);
            return;
        }
        DisableAll();
        EnableShopUI();
    }

    private IEnumerator CleanArea(){
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
            yield return new WaitForSecondsRealtime(0.05f);
        }

        Coin[] coins = FindObjectsOfType<Coin>();
        foreach (var coin in coins)
        {
            Destroy(coin.gameObject);
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
    public void NextWave(){
        Wave wave = waves[currentWave];
        this.currentTime = wave.Timer;
        gameUI.SetTxtWave(wave.Id);
        spawner.SetDifficulty(wave);

        ResumeGame();
        spawner.enabled = true;
        gameUI.SetCoins(userData.Coin);
        shopUI.gameObject.SetActive(false);
        
    }
    private void EnableShopUI(){
        DisableAll();
        shopUI.UpdateAllUpgradesUI();
        shopUI.gameObject.SetActive(true);
        shopUI.setCoins(userData.Coin);
    }

    private void InitiGameUI(){
        gameUI.SetMaxValueXP(userData.XpToNextLevel());
        gameUI.SetValueXP(userData.Xp);
        gameUI.SetMaxValueHP(playerScript.Health);
        gameUI.SetValueHP(playerScript.Health);
        gameUI.SetLevel(userData.Level);
        gameUI.SetEnemiesKilled(userData.EnemiesKilled);
        gameUI.SetCoins(userData.Coin);
        gameUI.SetTxtTimer(waves[0].Timer);
        gameUI.SetTxtWave(waves[0].Id);

        spawner.SetDifficulty(waves[0]);
    }

    public void UpdateEnemiesScore(){
        userData.EnemiesKilled+=1;
        gameUI.SetEnemiesKilled(userData.EnemiesKilled);
    }
    public void UpdateXp(int value){
        userData.Xp += value;
        gameUI.SetValueXP(userData.Xp);

        if (userData.Xp >= userData.XptoLevelUp)
        {
            userData.LevelUp();
            gameUI.levelUp(userData.Level, userData.Xp, userData.XptoLevelUp);
            EnableLevelUpUI();
        }
    }
    public void UpdateCoin(int value){
        userData.Coin+= value;
        gameUI.SetCoins(userData.Coin);
    }
    private void UpdateHealth(){
        gameUI.SetValueHP(playerScript.Health);
    }

    private void EnableLevelUpUI()
    {
        PauseGame();
        DisableAll();
        audioManager.PlayLevelUpSfx();
        this.levelUpUI.gameObject.SetActive(true);
    }

    private void DisableAll(){
        this.gameOverUI.gameObject.SetActive(false);
        this.levelUpUI.gameObject.SetActive(false);
        this.winGameUI.gameObject.SetActive(false);
        this.shopUI.gameObject.SetActive(false);
    }
    public void PauseGame(){
        Time.timeScale = 0;
    }
    public void ResumeGame(){
        Time.timeScale = 1;
    }

    private void GameOver(){
        DisableAll();
        PauseGame();
        spawner.enabled = false;
        gameOverUI.gameObject.SetActive(true);
        audioManager.PlayLoseSfx();

    }
    public void LevelUp(int index){
        audioManager.PlaySelectSfx();
        switch (index)
        {
            case 0:     // Attack Speed
                playerScript.IncreaseAttackSpeed(userData.Level);
                break;
            case 1:     // damage
                playerScript.IncreaseDamage();
                break;
            case 2:     // health
                playerScript.IncreaseHealth();
                UpdateHealth();
                break;
            case 3:     // speed
                playerScript.IncreaseSpeed();
                break;
            default:
                break;
        }
        ResumeGame();
        this.levelUpUI.gameObject.SetActive(false);
    }
    public void BuyUpgrade(Upgrade upgrade){

        int index = upgrade.ID;
        if (userData.Coin < upgrade.Cost || upgrade.Level > 9)
            return;
        
        userData.Coin -= upgrade.Cost;
        shopUI.setCoins(userData.Coin);
        shopUI.BuyItem(index);
        switch (index)
        {
            case 0:     // Attack Speed
                playerScript.IncreaseAttackSpeed(userData.Level);
                break;
            case 1:     // damage
                playerScript.IncreaseDamage();
                break;
            case 2:     // health
                playerScript.IncreaseHealth();
                UpdateHealth();
                break;
            case 3:     // speed
                playerScript.IncreaseSpeed();
                break;
            default:
                break;
        }
    }

    public void BackToMenu(){
        audioManager.PlaySelectSfx();
        SceneManager.LoadScene(sceneMenu);
    }

    private void OnDisable()
    {
        EnemyBehaviour.OnHitPlayer-= UpdateHealth;
        EnemyBehaviour.OnDeath-= UpdateEnemiesScore;
    }
}
