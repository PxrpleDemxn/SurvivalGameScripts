using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyAttributes _enemyAttributes;
    void Start()
    {
        _enemyAttributes = new()
        {
            health = 100,
            maxHealth = 100
        };
    }

    // Update is called once per frame
    void Update()
    {
        float sinus = Mathf.Sin(Time.time * 5) * 0.1f;
        transform.Translate(0 + sinus,0,0);
    }

    public void TakeDamage(float damage)
    {
        _enemyAttributes.health -= damage;
        Debug.Log(transform.name + " takes " + damage);
        if (_enemyAttributes.health <= 0)
        {
            Destroy(gameObject);            
        }
    }
}
