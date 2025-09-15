using Alvin.TowerDefense.Games;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveClassTotal
{
    public List<MiniWavesClass> miniWave;
}

[System.Serializable]
public class MiniWavesClass
{
    public int totalEnemy;
    public GameObject enemyPrefab;
}


public class Waypoints : MonoBehaviour
{
    [SerializeField] private GameObject[] wayPoints;
    public List<WaveClassTotal> Waves;
    private float timeBetweenWave = 2;
    private int currentBigWave;
    private int currentMiniWave;
    private float lastTimeSpawn;
    private float timeBetweenSpawn = 1;
    private int enemySpawn;
    private Inventory inventory;
    private Game game;
    private ActorFactory actorFactory;

    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        game = GameObject.Find("Game").GetComponent<Game>();
        actorFactory = GameObject.Find("ActorFactory").GetComponent<ActorFactory>();
        lastTimeSpawn = Time.time;
    }

    public GameObject[] GetWayPoints()
    {
        return wayPoints;
    }

    void Update()
    {
        float wavesSpawnDelay = Time.time - lastTimeSpawn;
        if (!game.gameOver)
        {
            if (currentBigWave < Waves.Count)
            {
                if (currentMiniWave < Waves[currentBigWave].miniWave.Count)
                {
                    if ((enemySpawn == 0 && wavesSpawnDelay > timeBetweenWave) || (0 < enemySpawn && enemySpawn < Waves[currentBigWave].miniWave[currentMiniWave].totalEnemy && wavesSpawnDelay > timeBetweenSpawn))
                    {
                        actorFactory.SpawnActor(Waves[currentBigWave].miniWave[currentMiniWave].enemyPrefab, gameObject.transform.position, gameObject.transform.rotation);
                        enemySpawn++;
                        lastTimeSpawn = Time.time;
                    }

                    if (enemySpawn == Waves[currentBigWave].miniWave[currentMiniWave].totalEnemy &&
                        GameObject.FindWithTag("Enemy") == null)
                    {
                        enemySpawn = 0;
                        lastTimeSpawn = Time.time;
                        currentMiniWave++;
                    }
                }
                else
                {
                    currentMiniWave = 0;
                    currentBigWave++;
                    inventory.gold.Increase(100);
                }
            }
            else
            {
                game.gameOver = true;
            }
        }
   
    }
}
