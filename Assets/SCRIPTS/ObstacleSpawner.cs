using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Lista de Obstáculos")]
    public GameObject[] obstaclePrefabs; // Arraste os seus prefabs para cá

    [Header("Tempo de Spawn (Segundos)")]
    public float minTime = 1.5f; // Tempo mínimo entre um obstáculo e outro
    public float maxTime = 3f;   // Tempo máximo

    private float nextSpawnTime;

    void Start()
    {
        CalcularProximoSpawn();
    }

    void Update()
    {
        // Se o tempo do jogo passou do tempo planejado, spawna!
        if (Time.time >= nextSpawnTime)
        {
            Spawn();
            CalcularProximoSpawn();
        }
    }

    void Spawn()
    {
        if (obstaclePrefabs.Length == 0) return;

        // Escolhe um obstáculo aleatório da lista (Lixeira ou Livro)
        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject escolhido = obstaclePrefabs[randomIndex];

        // Cria o obstáculo exatamente na posição do Spawner
        Instantiate(escolhido, transform.position, Quaternion.identity);
    }

    void CalcularProximoSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minTime, maxTime);
    }
}