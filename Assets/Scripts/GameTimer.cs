using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Header("- Time")]    
    public float gameDuration = 60f; // ���� ���� �ð� ���� (�� ����)

    [Header("- GUI")]    
    public TextMeshProUGUI gameOverText; // ���� ���� �޽��� GUI Text    
    public TextMeshProUGUI timerText; // ���� �ð� ǥ�� GUI Text

    [Header("- Component for Player Move Control")]
    public MonoBehaviour[] controllersToDisable; // �÷��̾��� �������̳� �Է��� �ߴ��ϱ� ���� ���

    private float timer;
    private bool isGameEnded = false;

    void Start()
    {
        // ������ ���۵Ǹ� Ÿ�̸Ӹ� ������ ���� �ð����� �ʱ�ȭ
        timer = gameDuration;

        // UI �ʱ�ȭ
        if (gameOverText != null)
        {
            // ���� ���� �޽��� ����
            gameOverText.gameObject.SetActive(false); 
        }

        if (timerText != null)
        {
            // �ð� �ʱ�ȭ
            timerText.text = FormatTime(timer); 
        }
    }

    void Update()
    {
        // ������ ����� ���¶�� Ÿ�̸Ӹ� ������Ʈ���� ����
        if (isGameEnded) return;

        // Time.deltaTime�� ����Ͽ� Ÿ�̸� ���� ����
        timer -= Time.deltaTime;

        // ���� �ð��� UI�� ������Ʈ
        if (timerText != null)
        {
            timerText.text = FormatTime(timer); // ���� �ð��� "��:��" �������� ǥ��
        }

        // �ð��� 0 ���ϰ� �Ǹ� ���� ���� ó��
        if (timer <= 0)
        {
            EndGame();
        }
    }

    // ���� ���� ó��
    void EndGame()
    {
        // ���� ���� ���·� ��ȯ
        isGameEnded = true;
                
        // controllersToDisable �迭�� ���Ե� ��� ��ũ��Ʈ�� ��Ȱ��ȭ
        foreach (var controller in controllersToDisable)
        {
            if (controller != null)
            {
                controller.enabled = false; // ��Ʈ�ѷ� ��ũ��Ʈ ��Ȱ��ȭ
            }
        }

        // ���� ���� �޽��� Ȱ��ȭ
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true); // ���� ���� �޽��� UI�� Ȱ��ȭ
            gameOverText.text = "Game Over!"; // �޽��� �ؽ�Ʈ ����
        }
    }

    string FormatTime(float time)
    {        
        int minutes = Mathf.FloorToInt(time / 60); // ���� �ð��� �� ������ ���
        int seconds = Mathf.FloorToInt(time % 60); // ���� �ð��� �� ������ ���
        return $"{minutes:00}:{seconds:00}";
    }
}
