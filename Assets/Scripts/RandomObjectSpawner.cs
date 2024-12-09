using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [Header ("- Prefab")]
    public GameObject[] prefabs; // 생성할 프리팹 배열

    [Header ("- Prefab Count")]
    public int[] prefabCounts; // 각 프리팹의 생성 개수를 설정하는 배열 (prefabs 배열의 각 인덱스와 동일해야 함)
        
    private BoxCollider spawnArea; // 프리팹이 생성될 범위를 정의하는 BoxCollider

    void Start()
    {
        // BoxCollider 가져오기
        spawnArea = GetComponent<BoxCollider>();

        // 프리팹 생성
        SpawnPrefabs();
    }

    // 프리팹 랜덤 생성함수
    void SpawnPrefabs()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            // 현재 프리팹의 생성 개수만큼 반복
            for (int j = 0; j < prefabCounts[i]; j++)
            {
                // 랜덤 위치 계산
                Vector3 randomPosition = GetRandomPositionWithinBounds();

                // 프리팹 생성 및 부모 오브젝트 설정
                Instantiate(prefabs[i], randomPosition, Quaternion.identity).gameObject.transform.SetParent(transform);
            }
        }
    }

    // BoxCollider 범위 내에서 랜덤한 위치를 반환
    Vector3 GetRandomPositionWithinBounds()
    {
        // BoxCollider의 중심 위치와 크기를 기준으로 랜덤한 위치 계산
        Vector3 center = spawnArea.center + transform.position; // BoxCollider 중심
        Vector3 size = spawnArea.size;

        // X, Y, Z 축에서 랜덤 값을 생성
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        // 랜덤 위치 반환
        return new Vector3(randomX, randomY, randomZ);
    }
}
