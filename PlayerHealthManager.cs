using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    private bool canBeHit = true;
    public AudioSource HitSound;
    // Called when the script instance is being loaded
    void Awake()
    {
        currentHealth = maxHealth;
        HitSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // You can add any additional logic related to player health here
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);
    }

    private void Die()
    {
        // You can customize the behavior when the player dies, such as showing a game over screen or restarting the level.
        Debug.Log("Player has died!");

        // For demonstration purposes, reset the player's position and health
        transform.position = Vector3.zero;
        currentHealth = maxHealth;
    }

private void OnCollisionEnter(Collision collision)
{

    if (collision.gameObject.CompareTag("enemy") && canBeHit)
    {
        TakeDamage(5);
        StartCoroutine(Cooldown());
        HitSound.Play();
    }
}

    private IEnumerator Cooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(1.0f);  // Adjust the cooldown duration as needed
        canBeHit = true;
    }
}