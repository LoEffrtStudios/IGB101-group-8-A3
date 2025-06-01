using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;   // Prefab reference
    public int numberOfPickups = 5;   // Total pickups to spawn
    public Vector3 spawnArea = new Vector3(10f, 1f, 10f); // Range for spawning

    void Start()
    {
        SpawnPickups();
    }

    void SpawnPickups()
    {
        Vector3[] spawnPositions = new Vector3[]
        {
        new Vector3(60f, 1.5f, 5f),
        new Vector3(65f, 1.5f, -5f),
        new Vector3(57, 1.5f, -4f),
        new Vector3(65f, 1.5f, -2f),
        new Vector3(60f, 1.5f, 3f)
        };

        for (int i = 0; i < numberOfPickups; i++)
        {
            Instantiate(pickupPrefab, spawnPositions[i], Quaternion.identity);
        }
    }
}
