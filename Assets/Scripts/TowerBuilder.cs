using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class TowerBuilder : MonoBehaviour
{
    public GameObject towerPrefab;

    public Transform[] towerSpots;

    public int[] towerCosts;

    public ResourceManager resourceManager;


    public TextMeshProUGUI infoText;


    private GameObject[] builtTowers;


    private int selectedTower = -1;



    void Start()
    {
        builtTowers = new GameObject[towerSpots.Length];

        UpdateUI();
    }





    void Update()
    {
        CheckSelect();


        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            BuildSelectedTower();
        }


        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            UpgradeSelectedTower();
        }
    }





    void CheckSelect()
    {
        CheckKey(0, Key.Digit1);
        CheckKey(1, Key.Digit2);
        CheckKey(2, Key.Digit3);
        CheckKey(3, Key.Digit4);
        CheckKey(4, Key.Digit5);
        CheckKey(5, Key.Digit6);
    }






    void CheckKey(int index, Key key)
    {
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            selectedTower = index;

            UpdateUI();
        }
    }






    void BuildSelectedTower()
    {
        if (selectedTower < 0)
            return;


        if (builtTowers[selectedTower] != null)
            return;



        int cost = towerCosts[selectedTower];



        if (resourceManager.SpendMoney(cost))
        {
            builtTowers[selectedTower] =
                Instantiate(
                    towerPrefab,
                    towerSpots[selectedTower].position,
                    Quaternion.identity
                );


            Tower tower =
                builtTowers[selectedTower]
                .GetComponent<Tower>();


            if (tower != null)
            {
                tower.resourceManager =
                    resourceManager;
            }
        }


        UpdateUI();
    }







    void UpgradeSelectedTower()
    {
        if (selectedTower < 0)
            return;


        if (builtTowers[selectedTower] == null)
            return;



        Tower tower =
            builtTowers[selectedTower]
            .GetComponent<Tower>();


        if (tower != null)
        {
            tower.Upgrade();
        }


        UpdateUI();
    }







    void UpdateUI()
    {
        if (infoText == null)
            return;



        if (selectedTower < 0)
        {
            infoText.text =
                "Select tower slot (1-6)";

            return;
        }



        string text =
            "Slot: " +
            (selectedTower + 1);



        if (builtTowers[selectedTower] == null)
        {
            text +=
                "\nEmpty" +
                "\nBuild cost: " +
                towerCosts[selectedTower] +
                "\n\nB - Build";
        }
        else
        {
            Tower tower =
                builtTowers[selectedTower]
                .GetComponent<Tower>();


            text +=
                "\nLevel: " + tower.level +
                "\nDamage: " + tower.damage +
                "\nUpgrade cost: " + tower.upgradeCost +
                "\n\nU - Upgrade";
        }



        infoText.text = text;
    }
}
