using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    
    [Header("Cash price")]
    [SerializeField] private int cashPrice;
    
    [SerializeField] private TextMeshProUGUI textButtonInfo;
    [SerializeField] private TextMeshProUGUI textCostUpgrade;
    
    private GameManager gameManager;
    private Player player;
    

    private void Start()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        textCostUpgrade.text = cashPrice + "₵";
      
    }

   
    public void AttackUpgrade()
    {
        if (gameManager.GetCoinsValue()>= cashPrice)
        {
            gameManager.ChangeCoinsValue(-cashPrice);
            
            player.damage += 3;
            cashPrice += 5;
            textButtonInfo.text = "Damage: " + player.damage;
            textCostUpgrade.text = cashPrice + "₵";
        }
    }
    
    public void SpeedUpgrade()
    {
        if (gameManager.GetCoinsValue() >= cashPrice)
        {
            gameManager.ChangeCoinsValue(-cashPrice);

            player.ChangeAttackSpeed(0.1f);
            cashPrice += 5;
            textButtonInfo.text = "Attack Speed: " + player.attackSpeed;
            textCostUpgrade.text = cashPrice + "₵";
        }
    }
    
    public void HpUpgrade()
    {
        if (gameManager.GetCoinsValue()>= cashPrice)
        {
            gameManager.ChangeCoinsValue(-cashPrice);

            player.hp += 20;
            player.maxHp += 20;
            cashPrice += 5;
            textButtonInfo.text = "HP: " + player.maxHp;
            textCostUpgrade.text = cashPrice + "₵";
        }
    }

}
