using UnityEngine;
using TMPro;


public class Health : MonoBehaviour
{
    public int maxHp = 10;

    private int currentHp;


    public TextMeshProUGUI hpText;

    public GameManager gameManager;



    void Start()
    {
        currentHp = maxHp;

        UpdateUI();
    }



    public void TakeDamage(int damage)
    {
        if (gameManager != null && gameManager.gameEnded)
            return;


        currentHp -= damage;


        if (currentHp < 0)
            currentHp = 0;


        UpdateUI();


        if (currentHp <= 0)
        {
            Die();
        }
    }



    void UpdateUI()
    {
        if (hpText != null)
        {
            hpText.text =
                currentHp.ToString();
        }
    }



    void Die()
    {
        if (gameManager != null)
        {
            gameManager.LoseGame();
        }
    }



    public bool IsAlive()
    {
        return currentHp > 0;
    }
}