using UnityEngine;


public class Bullet : MonoBehaviour
{

    public float speed = 8f;


    private Transform target;

    private int damage;




    public void SetTarget(
        Transform enemy,
        int dmg)
    {

        target = enemy;

        damage = dmg;

    }





    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }




        transform.position =
            Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );




        float distance =
            Vector2.Distance(
                transform.position,
                target.position
            );



        if (distance < 0.1f)
        {

            EnemyHealth hp =
                target.GetComponent<EnemyHealth>();


            if (hp != null)
            {
                hp.TakeDamage(damage);
            }


            Destroy(gameObject);

        }

    }

}