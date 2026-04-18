using UnityEngine;

public class BossAI : MonoBehaviour
{
    [Header("Stats")]
    public int maxHP = 50;
    private int currentHP;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float sideRange = 6f;
    public float frequency = 1.5f;

    [Header("Attack")]
    public float attackRate = 2f;

    private float timer;
    private float startY;

    private Transform player;

    void Start()
    {
        currentHP = maxHP;
        startY = transform.position.y;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        MovePattern();

        timer += Time.deltaTime;

        if (timer >= attackRate)
        {
            timer = 0f;
            ChooseAttack();
        }
    }

    void MovePattern()
    {
        // Boss moves side to side near top of screen
        float x = Mathf.Sin(Time.time * frequency) * sideRange;
        transform.position = new Vector2(x, startY);
    }

    void ChooseAttack()
    {
        int random = Random.Range(0, 4);

        switch (random)
        {
            case 0:
                ShootStraightBurst();
                break;

            case 1:
                ShootSpread();
                break;

            case 2:
                ShootCircle();
                break;

            case 3:
                AimAtPlayer();
                break;
        }
    }

    void ShootStraightBurst()
    {
        for (int i = -2; i <= 2; i++)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Enemy, transform.position);

            if (bullet != null)
            {
                Vector2 dir = Quaternion.Euler(0, 0, i * 8) * Vector2.down;
                bullet.SetDirection(dir);
            }
        }
    }

    void ShootSpread()
    {
        for (int i = -3; i <= 3; i++)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Enemy, transform.position);

            if (bullet != null)
            {
                Vector2 dir = Quaternion.Euler(0, 0, i * 15) * Vector2.down;
                bullet.SetDirection(dir);
            }
        }
    }

    void ShootCircle()
    {
        int bullets = 16;
        float step = 360f / bullets;

        for (int i = 0; i < bullets; i++)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Enemy, transform.position);

            if (bullet != null)
            {
                Vector2 dir = Quaternion.Euler(0, 0, i * step) * Vector2.up;
                bullet.SetDirection(dir);
            }
        }
    }

    void AimAtPlayer()
    {
        if (player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;

        for (int i = -1; i <= 1; i++)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Enemy, transform.position);

            if (bullet != null)
            {
                Vector2 spread = Quaternion.Euler(0, 0, i * 10) * dir;
                bullet.SetDirection(spread);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            other.gameObject.SetActive(false);
            TakeDamage(1);
        }
    }
}
