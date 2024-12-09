using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("- Time")]    
    public float gameDuration = 60f; // 게임 제한 시간 설정 (초 단위)

    [Header("- GUI")]    
    public TextMeshProUGUI gameOverText; // 게임 종료 메시지 GUI Text    
    public TextMeshProUGUI timerText; // 남은 시간 표시 GUI Text

    [Header("- Component for Player Move Control")]
    public MonoBehaviour[] controllersToDisable; // 플레이어의 움직임이나 입력을 중단하기 위해 사용

    private float timer;
    private bool isGameEnded = false;

    void Start()
    {
        // 게임이 시작되면 타이머를 설정된 제한 시간으로 초기화
        timer = gameDuration;

        // UI 초기화
        if (gameOverText != null)
        {
            // 게임 종료 메시지 숨김
            gameOverText.gameObject.SetActive(false); 
        }

        if (timerText != null)
        {
            // 시간 초기화
            timerText.text = FormatTime(timer); 
        }
    }

    void Update()
    {
        // 게임이 종료된 상태라면 타이머를 업데이트하지 않음
        if (isGameEnded) return;

        // Time.deltaTime을 사용하여 타이머 값을 줄임
        timer -= Time.deltaTime;

        // 남은 시간을 UI에 업데이트
        if (timerText != null)
        {
            timerText.text = FormatTime(timer); // 남은 시간을 "분:초" 형식으로 표시
        }

        // 시간이 0 이하가 되면 게임 종료 처리
        if (timer <= 0)
        {
            EndGame();
        }
    }

    // 게임 종료 처리
    void EndGame()
    {
        // 게임 종료 상태로 전환
        isGameEnded = true;
                
        // controllersToDisable 배열에 포함된 모든 스크립트를 비활성화
        foreach (var controller in controllersToDisable)
        {
            if (controller != null)
            {
                controller.enabled = false; // 컨트롤러 스크립트 비활성화
            }
        }

        // 게임 종료 메시지 활성화
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true); // 게임 종료 메시지 UI를 활성화
            gameOverText.text = "Game Over!"; // 메시지 텍스트 설정
        }
    }

    string FormatTime(float time)
    {        
        int minutes = Mathf.FloorToInt(time / 60); // 남은 시간을 분 단위로 계산
        int seconds = Mathf.FloorToInt(time % 60); // 남은 시간을 초 단위로 계산
        return $"{minutes:00}:{seconds:00}";
    }
}
