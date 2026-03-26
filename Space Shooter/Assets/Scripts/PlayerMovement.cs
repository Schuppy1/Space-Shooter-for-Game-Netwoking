using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;
    public int maxHealth = 3;
    private int currentHealth;

    private bool isInvincible = false; // prevents multiple hits
    public float invincibilityTime = 0.5f; // half a second cooldown

    Camera cam;
    float camWidth;
    float camHeight;

    void Start()
    {
        currentHealth = maxHealth;
        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(x, y);
        transform.Translate(move * speed * Time.deltaTime);

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        Vector3 camPos = cam.transform.position;

        float halfPlayerWidth = 0.5f;
        float halfPlayerHeight = 0.5f;

        pos.x = Mathf.Clamp(pos.x, camPos.x - camWidth / 2 + halfPlayerWidth, camPos.x + camWidth / 2 - halfPlayerWidth);
        pos.y = Mathf.Clamp(pos.y, camPos.y - camHeight / 2 + halfPlayerHeight, camPos.y + camHeight / 2 - halfPlayerHeight);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy Bullet") && !isInvincible)
        {
            other.gameObject.SetActive(false); // return bullet to pool
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        // Optional: make player flash here
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Player died! Trigger respawn or Game Over here.");
    }
}