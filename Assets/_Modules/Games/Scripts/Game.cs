using Actors;
using Alvin.TowerDefense.Combat;
using Stats;
using UnityEngine;

namespace Alvin.TowerDefense.Games

{
    [DefaultExecutionOrder(-9999)]
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject buildPointPrefab;
        [SerializeField] private GameObject castlePrefab;
        [SerializeField] private GameObject gameOverUi;
        public bool gameOver;
        private Vector3 buildPointPosition = new Vector3(-15, 0, 5);
        private Vector3 buildPointPosition2 = new Vector3(15, 0, -5);
        private Vector3 buildPointPosition3 = new Vector3(-5, 0, -15);
        private Vector3 buildPointPosition4 = new Vector3(-35, 0, 5);
        private Vector3 castlePosition = new Vector3(15, 0, -15);

        private IService[] services;
        // Awake phai la noi chay dau tien cua game
        private void Awake()
        {
            // Khoi tao cac thanh phan theo thu tu xac dinh
            // Init database
            // Init save game
            // Init audio
            // Init enemy
            // Init level

            this.services = GetComponents<IService>(); 
            foreach (IService service in this.services)
            {
                service.Initialize();
            }
        }

        private void InitDatabase()
        {
            
        }

        private void InitSaveGame()
        {
            
        }

        private void InitAudio()
        {
            
        }
        
        
        private void Start()
        {
            Debug.Log("Game Started");
            // khoi tao level, map scene, castleFactory, inventoryFactory, towerFactory, waypointFactory, enemyFactory, ...
            GameObject stateMachineGo = new GameObject();
            StateMachine stateMachine = stateMachineGo.AddComponent<StateMachine>();
            stateMachine.name = "StateMachine";
            
            // khoi tao ActorFactory
            GameObject actorFactoryGo = new GameObject();
            ActorFactory actorFactory = actorFactoryGo.AddComponent<ActorFactory>();
            actorFactory.name = "ActorFactory";

            //khoi tao tower
            Actor buildPoint = actorFactory.SpawnActor(buildPointPrefab, buildPointPosition, Quaternion.identity);
            buildPoint.name = "BuildPoint";            
            Actor buildPoint2 = actorFactory.SpawnActor(buildPointPrefab, buildPointPosition2, Quaternion.identity);
            buildPoint2.name = "BuildPoint2";
            Actor buildPoint3 = actorFactory.SpawnActor(buildPointPrefab, buildPointPosition3, Quaternion.identity);
            buildPoint3.name = "BuildPoint3";
            Actor buildPoint4 = actorFactory.SpawnActor(buildPointPrefab, buildPointPosition4, Quaternion.identity);
            buildPoint4.name = "BuildPoint4";
            
            Actor castle = actorFactory.SpawnActor(castlePrefab, castlePosition, Quaternion.identity);
            castle.name = "Castle";
        }

        public void GameOver()
        {
            gameOver = true;
            Time.timeScale = 0f;
            gameOverUi.SetActive(true);
        }
    }
}