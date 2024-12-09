using UnityEngine;
using TMPro;

public class RingCollision : MonoBehaviour
{
    [Header ("- GUI")]
    public TextMeshProUGUI scoreText;// ���� ���� GUI

    [Header("- AudioClip")]
    public AudioClip ringSound; // �浹 ȿ����

    private int score = 0;
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource �ʱ�ȭ
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // ���� UI�� �ʱ�ȭ
        UpdateScoreUI();
    }

    // Ʈ���� �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring1"))
        {
            PlayCollisionSound(ringSound); // �浹 ȿ���� ���
            ScoreAndDestroy(1, other.gameObject); // ���� �߰� �� ������Ʈ ����
        }
        else if (other.CompareTag("Ring3"))
        {
            PlayCollisionSound(ringSound); // �浹 ȿ���� ���
            ScoreAndDestroy(3, other.gameObject); // ���� �߰� �� ������Ʈ ����
        }
        else if (other.CompareTag("Ring5"))
        {
            PlayCollisionSound(ringSound); // �浹 ȿ���� ���
            ScoreAndDestroy(5, other.gameObject); // ���� �߰� �� ������Ʈ ����
        }
    }

    // ���� UI�� ������Ʈ
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // ���� ������ ���ڿ��� ��ȯ�Ͽ� ǥ��
        }
    }

    // ������ �߰� �� �浹 ������Ʈ ����
    private void ScoreAndDestroy(int _score, GameObject obj)
    {
        score += _score;

        // ���� UI ������Ʈ
        UpdateScoreUI();

        Destroy(obj);
    }

    // �浹 �� ȿ���� ���
    private void PlayCollisionSound(AudioClip sound)
    {
        if (sound != null && audioSource != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
