using UnityEngine;
using TMPro;


public class ResourceManager : MonoBehaviour
{
    public int money = 50;


    public TextMeshProUGUI moneyText;



    void Start()
    {
        UpdateUI();
    }




    public void AddMoney(int amount)
    {
        money += amount;

        UpdateUI();
    }





    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;

            UpdateUI();

            return true;
        }


        return false;
    }





    void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text =
                "Coins: " + money;
        }
    }
}
