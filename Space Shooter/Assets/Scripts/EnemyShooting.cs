using UnityEngine;

public enum ShootType
{
    None,
    Straight,
    Spread,
    Circle
}

public class EnemyShooter : MonoBehaviour
{
    public ShootType shootType;
    public float fireRate = 1f;
    public int bulletCount = 8;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            timer = 0;

            switch (shootType)
            {
                case ShootType.Straight:
                    ShootStraight();
                    break;

                case ShootType.Spread:
                    ShootSpread();
                    break;

                case ShootType.Circle:
                    ShootCircle();
                    break;
            }
        }
    }

    void ShootStraight()
    {
        Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Enemy, transform.position);

        if (bullet != null)
            bullet.SetDirection(Vector2.down);
    }

    void ShootSpread()
    {
        for (int i = -2; i <= 2; i++)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Enemy, transform.position);

            if (bullet != null)
            {
                Vector2 dir = Quaternion.Euler(0, 0, i * 10) * Vector2.down;
                bullet.SetDirection(dir);
            }
        }
    }

    void ShootCircle()
    {
        float angleStep = 360f / bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Enemy, transform.position);

            if (bullet != null)
            {
                Vector2 dir = Quaternion.Euler(0, 0, i * angleStep) * Vector2.up;
                bullet.SetDirection(dir);
            }
        }
    }
}
