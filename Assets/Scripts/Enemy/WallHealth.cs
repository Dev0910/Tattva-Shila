using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 60)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red * 10;
        }

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
