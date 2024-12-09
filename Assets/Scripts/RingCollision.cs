using UnityEngine;
using TMPro;

public class RingCollision : MonoBehaviour
{
    [Header ("- GUI")]
    public TextMeshProUGUI scoreText;// 현재 점수 GUI

    [Header("- AudioClip")]
    public AudioClip ringSound; // 충돌 효과음

    private int score = 0;
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // 점수 UI를 초기화
        UpdateScoreUI();
    }

    // 트리거 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring1"))
        {
            PlayCollisionSound(ringSound); // 충돌 효과음 재생
            ScoreAndDestroy(1, other.gameObject); // 점수 추가 및 오브젝트 제거
        }
        else if (other.CompareTag("Ring3"))
        {
            PlayCollisionSound(ringSound); // 충돌 효과음 재생
            ScoreAndDestroy(3, other.gameObject); // 점수 추가 및 오브젝트 제거
        }
        else if (other.CompareTag("Ring5"))
        {
            PlayCollisionSound(ringSound); // 충돌 효과음 재생
            ScoreAndDestroy(5, other.gameObject); // 점수 추가 및 오브젝트 제거
        }
    }

    // 점수 UI를 업데이트
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // 현재 점수를 문자열로 변환하여 표시
        }
    }

    // 점수를 추가 후 충돌 오브젝트 제거
    private void ScoreAndDestroy(int _score, GameObject obj)
    {
        score += _score;

        // 점수 UI 업데이트
        UpdateScoreUI();

        Destroy(obj);
    }

    // 충돌 시 효과음 재생
    private void PlayCollisionSound(AudioClip sound)
    {
        if (sound != null && audioSource != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}
