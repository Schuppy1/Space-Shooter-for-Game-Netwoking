using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;

    float timer;
    Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void OnEnable()
    {
        timer = lifeTime;
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
