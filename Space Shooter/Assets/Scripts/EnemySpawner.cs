using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Wave Settings")]
    public int baseEnemiesPerWave = 5;
    public float baseSpawnDelay = 0.7f;
    public float timeBetweenWaves = 3f;

    [Header("Scaling")]
    public float enemyIncreaseRate = 1.5f;
    public float spawnSpeedIncrease = 0.03f;
    public float enemySpeedIncrease = 0.2f;

    private int currentWave = 1;
    private int enemiesAlive = 0;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            int enemiesThisWave = Mathf.RoundToInt(baseEnemiesPerWave + currentWave * enemyIncreaseRate);
            float spawnDelay = Mathf.Max(0.2f, baseSpawnDelay - currentWave * spawnSpeedIncrease);

            Debug.Log($"Wave {currentWave} | Enemies: {enemiesThisWave}");

            for (int i = 0; i < enemiesThisWave; i++)
            {
                SpawnEnemy(currentWave);
                yield return new WaitForSeconds(spawnDelay);
            }

            // Wait until all enemies are gone
            yield return new WaitUntil(() => enemiesAlive <= 0);

            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
        }
    }

    void SpawnEnemy(int wave)
    {
        Vector3 spawnPos = GetTopSpawnPosition();

        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        enemiesAlive++; // track enemies on screen

        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.movementType = (MovementType)Random.Range(0, 4);
        movement.spawner = this;
        movement.speed += wave * enemySpeedIncrease;

        // Optional shooting
        EnemyShooter shooter = enemy.GetComponent<EnemyShooter>();
        if (shooter != null)
        {
            shooter.shootType = (ShootType)Random.Range(0, 4);
        }
    }

    Vector3 GetTopSpawnPosition()
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        Vector3 camPos = cam.transform.position;

        float x = Random.Range(-camWidth / 2, camWidth / 2);
        float y = camHeight / 2; // top edge

        return new Vector3(x, y, 0) + camPos;
    }

    public void EnemyDied()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        // Debug.Log("Enemies alive: " + enemiesAlive);
    }
}