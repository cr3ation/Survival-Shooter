using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public FantonHealth fantonHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public float timeDiff = 2f;
    public Transform[] spawnPoints;

    float timer = 0f;
    float nextSpawnTime = 0f;

    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
        nextSpawnTime = Random.Range(spawnTime, spawnTime + timeDiff);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < nextSpawnTime) { return; }

        timer = 0f;
        nextSpawnTime = Random.Range(spawnTime, spawnTime + timeDiff);
        Spawn();
    }


    void Spawn ()
    {
        if(fantonHealth.currentHealth <= 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
