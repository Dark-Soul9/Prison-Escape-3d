using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log($"{gameObject.name} took {damageAmount} damage. Remaining HP: {currentHealth}");

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        // Replace this with proper death logic
        Destroy(gameObject);
    }
}
