using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Slider slider;
    
    [SerializeField] private int coins;
    void Start()
    {
        coinsText.text = "Coins " + coins + "₵";
    }

    public void ChangeHpSliderValue(float hp, float maxHp)
    {
        slider.maxValue = maxHp;
        slider.value = hp;
    }

    public void ChangeCoinsValue(int value)
    {
        coins += value;
        coinsText.text = "Coins " + coins + "₵";
    }
    
    public int GetCoinsValue()
    {
        return coins;
    }
}
