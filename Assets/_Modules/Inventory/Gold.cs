
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gold
{
    private float goldValue;
    private TextMeshProUGUI goldText;
    [ShowInInspector] public float Value
    {
        private set
        {
            goldValue = value;
        }
        get
        {
            return goldValue;
        }
    }


    public Gold(float _goldValue, TextMeshProUGUI _goldText)
    {
        Value = _goldValue;
        goldText = _goldText;
        ShowValue();
    }
    public void Increase(float amount)
    {
        goldValue += amount;
        ShowValue();
    }
    public void Decrease(float amount)
    {
        goldValue -= amount;
        ShowValue();
    }

    public void ShowValue()
    {
        goldText.text = "Gold: " + Value;
    }
}