using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //*variavel atribuida no inspector
    [SerializeField]private GameObject[] enemiesPrefabs;

    //constantes
    private const float initalSpawnTime = 3.0f;
    private const float spawnRange = 7.0f;


    //* atributos
    Transform player;
    //[SerializeField]
    private float spawnTime;
    //[SerializeField]
    private int enemiesQuantToSpawn;
    //[SerializeField]
    private int enemiestypeToSpawn;
    

    //variaveis de deltaTime
    //[SerializeField]
    private float spawnOvertime;

    void Start()
    {
        try{
            player = Master.Instance.player.transform;
        }
        catch (System.Exception){
            Debug.Log("Jogador nao foi encontrado");
            throw;
        }
        spawnTime = initalSpawnTime;
        spawnOvertime = spawnTime;
        enemiesQuantToSpawn = 1;
        enemiestypeToSpawn = 1;
    }

    void FixedUpdate()
    {
        if(!player)
            return;

        spawnOvertime-= Time.fixedDeltaTime;
        if (spawnOvertime <= 0){
            spawnOvertime = spawnTime;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 1; i <= enemiesQuantToSpawn; i++)
        {
            Instantiate(
                RandomEnemy(), 
                RandomPosition(), 
                Quaternion.identity);
        }
    }

    private Vector3 RandomPosition(){
        Vector3 center = player.position;
        float radius = spawnRange;
        float angle = Random.Range(0.0f, 360.0f);

        float x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        return new Vector3(x, y, 0);
    }
    private GameObject RandomEnemy(){
        int rand = Random.Range(0,enemiestypeToSpawn);
        return enemiesPrefabs[rand];
    }
    private int RandomDirection(){
        return (Random.Range(0, 2) == 0)?1 :-1;
    }

    public void SetDifficulty(Wave wave){

        this.enemiesQuantToSpawn = wave.EnemiesQuant;
        this.enemiestypeToSpawn = wave.EnemiesType;
        this.spawnTime = wave.SpawnRate;
    }
}
