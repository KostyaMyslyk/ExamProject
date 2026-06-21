using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;

    public float speed = 2f;

    private int currentIndex = 0;

    public Health baseHealth;

    public void Init(Transform[] points, Health baseHp)
    {
        waypoints = points;
        baseHealth = baseHp;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentIndex];

        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(
            transform.position,
            target.position) < 0.1f)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Length)
            {
                ReachBase();
            }
        }
    }

    void ReachBase()
    {
        int damage = 1;

        EnemyHealth enemyHealth =
            GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            damage = enemyHealth.GetDamage();
        }

        if (baseHealth != null)
        {
            baseHealth.TakeDamage(damage);
        }

        if (enemyHealth != null)
        {
            enemyHealth.ReachBase();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}