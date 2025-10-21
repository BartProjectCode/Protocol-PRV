using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Food : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 3f;
    public Vector3 zoneCenter;
    public Vector3 zoneSize = new Vector3(10f, 0f, 10f);

    public KeyCode spawnKey = KeyCode.Mouse0;

    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        if (prefab == null) return;

        Vector3 randomPos = new Vector3(
            Random.Range(zoneCenter.x - zoneSize.x / 2, zoneCenter.x + zoneSize.x / 2),
            zoneCenter.y,
            Random.Range(zoneCenter.z - zoneSize.z / 2, zoneCenter.z + zoneSize.z / 2)
        );

        Instantiate(prefab, randomPos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(zoneCenter, zoneSize);
    }
}

