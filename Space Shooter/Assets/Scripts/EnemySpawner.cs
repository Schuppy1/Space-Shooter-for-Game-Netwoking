using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Normal Enemy")]
    public GameObject enemyPrefab;

    [Header("Boss Enemy")]
    public GameObject bossPrefab;

    [Header("Spawn Settings")]
    public float spawnRate = 2f;

    private Camera cam;

    public int enemiesDefeated = 0;
    private bool bossAlive = false;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        // If boss exists, stop normal enemy spawning
        if (bossAlive)
            return;

        // Spawn boss every 50 kills
        if (enemiesDefeated > 0 && enemiesDefeated % 50 == 0)
        {
            SpawnBoss();
            bossAlive = true;
            return;
        }

        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float randomX = Random.Range(left.x, right.x);
        Vector3 spawnPos = new Vector3(randomX, left.y + 1f, 0);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        EnemyMovement move = enemy.GetComponent<EnemyMovement>();
        if (move != null)
            move.movementType = (MovementType)Random.Range(0, 4);

        EnemyShooter shoot = enemy.GetComponent<EnemyShooter>();
        if (shoot != null)
            shoot.shootType = (ShootType)Random.Range(0, 4);
    }

    void SpawnBoss()
    {
        Vector3 top = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));

        // Slightly below top of screen
        Vector3 spawnPos = new Vector3(top.x, top.y - 1.5f, 0);

        Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        Debug.Log("Boss Spawned");
    }

    // Call when a normal enemy dies
    public void EnemyDefeated()
    {
        enemiesDefeated++;
    }

    // Call when boss dies
    public void BossDefeated()
    {
        bossAlive = false;

        // Prevent repeat spawn at same count
        enemiesDefeated++;

        Debug.Log("Boss Defeated - Normal enemies resumed");
    }
}