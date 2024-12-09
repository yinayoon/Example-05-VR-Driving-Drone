using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneReloader : MonoBehaviour
{
    // Input Action을 저장할 변수
    public InputAction rightHandBButton; // 오른손 B 버튼
    public InputAction leftHandYButton;  // 왼손 Y 버튼

    void Start()
    {
        InitializeInputActions();
    }

    void Update()
    {
        // 버튼 입력 확인
        if (rightHandBButton.triggered || leftHandYButton.triggered)
        {
            RestartScene();
        }
    }

    private void InitializeInputActions()
    {
        // 오른손 B 버튼 초기화
        rightHandBButton = new InputAction("RightHandBButton", InputActionType.Button);
        rightHandBButton.AddBinding("<XRController>{RightHand}/secondaryButton"); // B 버튼 경로
        rightHandBButton.Enable(); // 활성화

        // 왼손 Y 버튼 초기화
        leftHandYButton = new InputAction("LeftHandYButton", InputActionType.Button);
        leftHandYButton.AddBinding("<XRController>{LeftHand}/secondaryButton"); // Y 버튼 경로
        leftHandYButton.Enable(); // 활성화
    }

    // 현재 씬을 다시 로드
    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
