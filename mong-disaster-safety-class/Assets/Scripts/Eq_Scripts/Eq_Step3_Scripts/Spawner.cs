using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minSpawnDelay;
    public float maxSpawnDelay;
    public GameObject[] gameObjects;

    private bool isSpawning = true;

    void Start()
    {
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    // 오브젝트 랜덤 생성
    void Spawn()
    {
        // 게임 종료 시 생성 중단
        if (!isSpawning) return;

        GameObject randomObject = gameObjects[Random.Range(0, gameObjects.Length)];
        Instantiate(randomObject, transform.position, Quaternion.identity);
        Invoke("Spawn", Random.Range(minSpawnDelay, maxSpawnDelay));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("Spawn");
    }
}
