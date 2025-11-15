using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;

    public float speedUpFactor = 0.7f;
    public float minDelayLimit = 0.2f;

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

        // 의자 y축 조정
        if (objInstance.name.ToLower().Contains("chair"))
        {
            Vector3 pos = objInstance.transform.position;
            pos.y += 0.4f;
            objInstance.transform.position = pos;
        }

        // 진행 바 기반 생성 속도 증가
        float progressFactor = 1f;
        if (distanceBar != null)
            progressFactor = 1f - distanceBar.GetProgress();

        float nextMinDelay = Mathf.Max(minSpawnDelay * Mathf.Pow(speedUpFactor, 1f - progressFactor), minDelayLimit);
        float nextMaxDelay = Mathf.Max(maxSpawnDelay * Mathf.Pow(speedUpFactor, 1f - progressFactor), minDelayLimit);

        Invoke("Spawn", Random.Range(nextMinDelay, nextMaxDelay));
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("Spawn");
    }
}
