using UnityEngine;


public class Tower : MonoBehaviour
{
    public float range = 3f;

    public float attackCooldown = 0.75f;

    public int damage = 1;


    public int level = 1;

    public int upgradeCost = 20;


    public ResourceManager resourceManager;


    public GameObject bulletPrefab;


    private float timer;



    void Update()
    {
        timer += Time.deltaTime;


        if (timer >= attackCooldown)
        {
            Shoot();

            timer = 0;
        }
    }





    void Shoot()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");



        foreach (GameObject enemy in enemies)
        {

            float distance =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );


            if (distance <= range)
            {

                EnemyHealth hp =
                    enemy.GetComponent<EnemyHealth>();


                if (hp != null)
                {

                    int finalDamage = damage;



                    if (FactionManager.Instance != null &&
                    FactionManager.Instance.selectedFaction ==
                    FactionManager.Faction.Fire)
                    {
                        finalDamage++;
                    }




                    GameObject bullet =
                        Instantiate(
                            bulletPrefab,
                            transform.position,
                            Quaternion.identity
                        );



                    Bullet bulletScript =
                        bullet.GetComponent<Bullet>();



                    bulletScript.SetTarget(
                        enemy.transform,
                        finalDamage
                    );


                    return;
                }
            }
        }
    }







    public void Upgrade()
    {

        if (resourceManager != null)
        {

            if (!resourceManager.SpendMoney(upgradeCost))
            {
                return;
            }

        }


        level++;

        damage++;

        upgradeCost *= 2;

    }






    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            range
        );
    }
}