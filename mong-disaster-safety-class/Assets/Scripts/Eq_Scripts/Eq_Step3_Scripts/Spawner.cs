using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minSpawnDelay;
    public float maxSpawnDelay;

    public float speedUpFactor = 0.8f; // 생성 간격
    public float minDelayLimit = 0.2f;  // 최소 간격 제한

    public GameObject[] gameObjects;
    private bool isSpawning = true;

    [SerializeField] private DistanceBar distanceBar;

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
        GameObject objInstance = Instantiate(randomObject, transform.position, Quaternion.identity);

        // 의자 위치 조정
        if (objInstance.name.Contains("chair"))
        {
            Vector3 pos = objInstance.transform.position;
            pos.y += 0.4f;
            objInstance.transform.position = pos;
        }

        // DistanceBar 진행률 기반으로 스폰 간격 조정
        float progress = 0f;
        if (distanceBar != null)
            progress = distanceBar.GetProgress();

        float nextMinDelay = Mathf.Max(minSpawnDelay * (1f - 0.7f * progress) * speedUpFactor, minDelayLimit);
        float nextMaxDelay = Mathf.Max(maxSpawnDelay * (1f - 0.7f * progress) * speedUpFactor, minDelayLimit);

        Invoke("Spawn", Random.Range(nextMinDelay, nextMaxDelay));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("Spawn");
    }
}
