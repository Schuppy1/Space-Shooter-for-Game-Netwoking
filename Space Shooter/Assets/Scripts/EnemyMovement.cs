using UnityEngine;

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

    public EnemySpawner spawner;

    float startX;

    Camera cam;
    float camWidth;
    float camHeight;

    void Start()
    {
        startX = transform.position.x;

        cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
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

        ClampHorizontal();
        CheckIfOffScreen();
    }

    void ClampHorizontal()
    {
        Vector3 camPos = cam.transform.position;

        float minX = camPos.x - camWidth / 2;
        float maxX = camPos.x + camWidth / 2;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }

    void CheckIfOffScreen()
    {
        float bottomY = cam.transform.position.y - camHeight / 2;

        if (transform.position.y < bottomY - 1f)
        {
            DestroyEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            other.gameObject.SetActive(false);
            DestroyEnemy();
        }
    }

    void DestroyEnemy()
    {
        if (spawner != null)
            spawner.EnemyDied();

        Destroy(gameObject);
    }

    // Extra safety: ensure EnemyDied always called
    void OnDestroy()
    {
        if (spawner != null)
            spawner.EnemyDied();
    }
}