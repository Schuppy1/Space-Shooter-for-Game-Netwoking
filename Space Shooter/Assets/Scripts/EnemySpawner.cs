using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    Camera cam;
    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }

    void SpawnEnemy()
    {
        Vector3 left = cam.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 right = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float randomX = Random.Range(left.x, right.x);

        Vector3 spawnPos = new Vector3(randomX, left.y + 1f, 0);

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // RANDOM MOVEMENT
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.movementType = (MovementType)Random.Range(0, 3);

        // RANDOM SHOOTING
        EnemyShooter shooter = enemy.GetComponent<EnemyShooter>();
        shooter.shootType = (ShootType)Random.Range(0, 4);
    }
}
