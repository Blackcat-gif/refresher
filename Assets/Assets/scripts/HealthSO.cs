using UnityEngine;

[CreateAssetMenu(fileName = "HealthSO", menuName = "Scriptable Objects/HealthSO")]
public class HealthSO : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }
}
