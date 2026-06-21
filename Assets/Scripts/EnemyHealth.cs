using UnityEngine;


public class EnemyHealth : MonoBehaviour
{

    public enum EnemyType
    {
        Normal,
        Fast,
        Tank
    }



    public EnemyType type;



    private int currentHp;



    public int hp;

    public int reward;

    public int baseDamage;



    public ResourceManager resourceManager;

    public WaveSpawner spawner;





    void Start()
    {
        currentHp = hp;
    }





    public void SetupType(EnemyType newType)
    {
        type = newType;



        EnemyMovement movement =
            GetComponent<EnemyMovement>();


        SpriteRenderer sprite =
            GetComponent<SpriteRenderer>();




        switch (type)
        {


            case EnemyType.Normal:


                hp = 6;

                reward = 20;

                baseDamage = 2;


                if (movement != null)
                    movement.speed = 2f;


                if (sprite != null)
                    sprite.color = Color.green;


                break;






            case EnemyType.Fast:


                hp = 14;

                reward = 40;

                baseDamage = 4;


                if (movement != null)
                    movement.speed = 3f;


                if (sprite != null)
                    sprite.color = Color.blue;


                break;






            case EnemyType.Tank:


                hp = 30;

                reward = 60;

                baseDamage = 8;


                if (movement != null)
                    movement.speed = 1f;


                if (sprite != null)
                    sprite.color = Color.red;


                break;

        }



        ApplyFactionBonus(movement);



        currentHp = hp;
    }






    void ApplyFactionBonus(EnemyMovement movement)
    {

        if (movement == null)
            return;

        if (FactionManager.Instance != null &&
        FactionManager.Instance.selectedFaction ==
        FactionManager.Faction.Ice)
        {

            movement.speed *= 0.8f;

        }

    }






    public void TakeDamage(int damage)
    {

        currentHp -= damage;



        if (currentHp <= 0)
        {
            Die();
        }

    }







    void Die()
    {

        if (resourceManager != null)
        {
            resourceManager.AddMoney(reward);
        }




        if (spawner != null)
        {
            spawner.EnemyDied();
        }




        Destroy(gameObject);

    }








    public int GetDamage()
    {
        return baseDamage;
    }







    public void ReachBase()
    {

        if (spawner != null)
        {
            spawner.EnemyDied();
        }


        Destroy(gameObject);

    }

}