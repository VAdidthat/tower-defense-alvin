using Alvin.TowerDefense.Games;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [ShowInInspector] public Gold gold{private set; get;}
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private float goldStart;
    

    private void Start()
    {
        gold = new Gold(goldStart, goldText);
        
    }
}
