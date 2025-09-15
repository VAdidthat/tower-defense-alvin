using System;
using Actors;
using UnityEngine;
using Lean.Touch;
using Stats;

namespace Alvin.TowerDefense.Combat
{
    public class Transformer : MonoBehaviour
    {
        [SerializeField] private GameObject popUpPrefab;
        private float transformPrice;
        private Inventory inventory;
        private ActorFactory actorFactory;
        private bool isPopup;
        private void Start()
        {
            actorFactory = GameObject.Find("ActorFactory").GetComponent<ActorFactory>();
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            popUpPrefab.SetActive(false);
        }

        void OnEnable()
        {
            LeanTouch.OnFingerTap += HandleFingerTap;
        }

        void OnDisable()
        {
            LeanTouch.OnFingerTap -= HandleFingerTap;
        }

        void HandleFingerTap(LeanFinger finger)
        {
            var ray = Camera.main.ScreenPointToRay(finger.ScreenPosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("GameController"))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        if (!isPopup)
                        {
                            popUpPrefab.SetActive(true);
                            isPopup = true;
                        }
                        else
                        {
                            popUpPrefab.SetActive(false);
                            isPopup = false;
                        }
                    }
                }
            }
            // if (Physics.Raycast(ray, out RaycastHit hit))
            // {
            //     if (hit.collider.gameObject == gameObject)
            //     {
            //         if (!isPopup)
            //         {
            //             popUpPrefab.SetActive(true);
            //             isPopup = true;
            //         }
            //         else
            //         {
            //             popUpPrefab.SetActive(false);
            //             isPopup = false;
            //         }
            //     }
            // }
        }        
        
        public void TransformTower(GameObject actor)
        {
            float price = actor.GetComponent<Actor>().StartingStatsCollection.Price;
            if (inventory.gold.Value >= price)
            {
                inventory.gold.Decrease(price);
                actorFactory.SpawnActor(actor, transform.position, transform.rotation);
                Destroy(transform.root.gameObject);
            }
            else
            {
                Debug.Log("Not enough gold");
            }
        }

        public void Sell(GameObject actor)
        {
            float price = transform.root.gameObject.GetComponent<Actor>().StartingStatsCollection.Price;
            inventory.gold.Increase(price);
            actorFactory.SpawnActor(actor, transform.position, transform.rotation);
            Destroy(transform.root.gameObject);
        }
    }
}