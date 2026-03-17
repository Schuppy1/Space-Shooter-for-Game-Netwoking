using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float fireRate = 0.25f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && timer >= fireRate)
        {
            timer = 0;

            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Player, transform.position);

            if (bullet != null)
                bullet.SetDirection(Vector2.up);
        }
    }
}
