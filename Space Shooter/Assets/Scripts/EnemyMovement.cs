using UnityEngine;
using static UnityEngine.UI.ScrollRect;
public enum MovementType
{
    Straight,
    SideToSide,
    ZigZag,
    StopAndShoot
}
public class EnemyMovement : MonoBehaviour
{
    public MovementType movementType;
    public float speed = 3f;
    public float amplitude = 2f;
    public float frequency = 2f;

    float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        switch (movementType)
        {
            case MovementType.Straight:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;

            case MovementType.SideToSide:
                float x = startX + Mathf.Sin(Time.time * frequency) * amplitude;
                transform.position = new Vector2(x, transform.position.y - speed * Time.deltaTime);
                break;

            case MovementType.ZigZag:
                transform.Translate(new Vector2(Mathf.Sin(Time.time * frequency), -1) * speed * Time.deltaTime);
                break;

            case MovementType.StopAndShoot:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            Debug.Log("Enemy Hit");

            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
