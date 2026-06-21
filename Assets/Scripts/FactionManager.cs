using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class FactionManager : MonoBehaviour
{
    public static FactionManager Instance;


    public TextMeshProUGUI factionText;

    public ResourceManager resourceManager;


    public bool factionChosen = false;



    public enum Faction
    {
        None,
        Fire,
        Ice,
        Nature
    }



    public Faction selectedFaction =
        Faction.None;



    void Awake()
    {
        Instance = this;
    }



    void Start()
    {
        if (factionText != null)
        {
            factionText.gameObject.SetActive(true);
        }
    }



    void Update()
    {
        if (factionChosen)
            return;



        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SelectFaction(Faction.Fire);
        }



        if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SelectFaction(Faction.Ice);
        }



        if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            SelectFaction(Faction.Nature);
        }
    }






    void SelectFaction(Faction faction)
    {

        selectedFaction = faction;


        factionChosen = true;



        if (faction == Faction.Nature)
        {
            if (resourceManager != null)
            {
                resourceManager.AddMoney(200);
            }
        }





        if (factionText != null)
        {

            switch (faction)
            {

                case Faction.Fire:

                    factionText.text =
                        "Faction: Fire Tribe\n" +
                        "Bonus: +1 Tower Damage";

                    break;




                case Faction.Ice:

                    factionText.text =
                        "Faction: Ice Tribe\n" +
                        "Bonus: -20% Enemy Speed";

                    break;




                case Faction.Nature:

                    factionText.text =
                        "Faction: Merchants\n" +
                        "Bonus: +200 Starting Money";

                    break;

            }


            factionText.gameObject.SetActive(true);
        }

    }

}
