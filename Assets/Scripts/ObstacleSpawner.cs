
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private List<Transform> obscacles;
    [SerializeField] private List<Transform> props;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRange = 3;
    [SerializeField] private float spawnFrequency = 1;
    [SerializeField] private float propsSpawnFrequency = 0.1f;
    [SerializeField] private Vector3 defaultSpawnPoint;


    private const float StartSpawnPoint = 10;

    void Start()
    {
        StartCoroutine(SlowUpdate());
        StartCoroutine(SpawnProps());

        for (int i = 3; i < defaultSpawnPoint.z/StartSpawnPoint; i++)
        {
            for (int j = 6; j < 10; j++)
            {
                var obst = props[Random.Range(0, props.Count)];

                var spawnPoint = new Vector3(defaultSpawnPoint.x, defaultSpawnPoint.y, i * StartSpawnPoint + player.position.z);

                Instantiate(obst, spawnPoint + Vector3.right * Random.Range(-spawnRange, spawnRange), obst.rotation);

                obst = obscacles[Random.Range(0, obscacles.Count)];
                spawnPoint = new Vector3(defaultSpawnPoint.x, defaultSpawnPoint.y, i * StartSpawnPoint + player.position.z);

                Instantiate(obst, spawnPoint + Vector3.right * Random.Range(-spawnRange, spawnRange), obst.rotation);
            }
        }
    }

    private IEnumerator SpawnProps()
    {
        while (true)
        {
            yield return new WaitForSeconds(propsSpawnFrequency);

            var obst = props[Random.Range(0, props.Count)];
            var spawnPoint = new Vector3(defaultSpawnPoint.x, defaultSpawnPoint.y, defaultSpawnPoint.z + player.position.z);

            Instantiate(obst, spawnPoint + Vector3.right * Random.Range(-spawnRange, spawnRange), obst.rotation);
        }
    }

    // Update is called once per frame
    IEnumerator SlowUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnFrequency);

            var obst = obscacles[Random.Range(0, obscacles.Count)];
            var spawnPoint = new Vector3(defaultSpawnPoint.x, defaultSpawnPoint.y, defaultSpawnPoint.z + player.position.z);

            Instantiate(obst, spawnPoint + Vector3.right * Random.Range(-spawnRange, spawnRange), obst.rotation);
        }
    }
}
