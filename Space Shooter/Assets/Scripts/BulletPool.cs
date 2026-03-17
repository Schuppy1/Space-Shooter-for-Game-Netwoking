using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    Player,
    Enemy
}

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;

    public int poolSize = 200;

    List<GameObject> playerPool = new List<GameObject>();
    List<GameObject> enemyPool = new List<GameObject>();

    void Awake()
    {
        Instance = this;

        // Player bullets
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab);
            bullet.SetActive(false);
            playerPool.Add(bullet);
        }

        // Enemy bullets
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab);
            bullet.SetActive(false);
            enemyPool.Add(bullet);
        }
    }

    public Bullet GetBullet(BulletType type, Vector3 position)
    {
        List<GameObject> pool = (type == BulletType.Player) ? playerPool : enemyPool;

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = position;
                pool[i].SetActive(true);
                return pool[i].GetComponent<Bullet>();
            }
        }

        return null;
    }
}