using UnityEngine;

public class RandomPrefabSpawner : MonoBehaviour
{
    [Header ("- Prefab")]
    public GameObject[] prefabs; // ������ ������ �迭

    [Header ("- Prefab Count")]
    public int[] prefabCounts; // �� �������� ���� ������ �����ϴ� �迭 (prefabs �迭�� �� �ε����� �����ؾ� ��)
        
    private BoxCollider spawnArea; // �������� ������ ������ �����ϴ� BoxCollider

    void Start()
    {
        // BoxCollider ��������
        spawnArea = GetComponent<BoxCollider>();

        // ������ ����
        SpawnPrefabs();
    }

    // ������ ���� �����Լ�
    void SpawnPrefabs()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            // ���� �������� ���� ������ŭ �ݺ�
            for (int j = 0; j < prefabCounts[i]; j++)
            {
                // ���� ��ġ ���
                Vector3 randomPosition = GetRandomPositionWithinBounds();

                // ������ ���� �� �θ� ������Ʈ ����
                Instantiate(prefabs[i], randomPosition, Quaternion.identity).gameObject.transform.SetParent(transform);
            }
        }
    }

    // BoxCollider ���� ������ ������ ��ġ�� ��ȯ
    Vector3 GetRandomPositionWithinBounds()
    {
        // BoxCollider�� �߽� ��ġ�� ũ�⸦ �������� ������ ��ġ ���
        Vector3 center = spawnArea.center + transform.position; // BoxCollider �߽�
        Vector3 size = spawnArea.size;

        // X, Y, Z �࿡�� ���� ���� ����
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        // ���� ��ġ ��ȯ
        return new Vector3(randomX, randomY, randomZ);
    }
}
