using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(x, y);
        transform.Translate(move * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy Bullet"))
        {
            Destroy(gameObject); // destroy player
            other.gameObject.SetActive(false); // return bullet to pool
        }
    }
}