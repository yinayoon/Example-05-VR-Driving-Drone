using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneReloader : MonoBehaviour
{
    // Input Action�� ������ ����
    public InputAction rightHandBButton; // ������ B ��ư
    public InputAction leftHandYButton;  // �޼� Y ��ư

    void Start()
    {
        InitializeInputActions();
    }

    void Update()
    {
        // ��ư �Է� Ȯ��
        if (rightHandBButton.triggered || leftHandYButton.triggered)
        {
            RestartScene();
        }
    }

    private void InitializeInputActions()
    {
        // ������ B ��ư �ʱ�ȭ
        rightHandBButton = new InputAction("RightHandBButton", InputActionType.Button);
        rightHandBButton.AddBinding("<XRController>{RightHand}/secondaryButton"); // B ��ư ���
        rightHandBButton.Enable(); // Ȱ��ȭ

        // �޼� Y ��ư �ʱ�ȭ
        leftHandYButton = new InputAction("LeftHandYButton", InputActionType.Button);
        leftHandYButton.AddBinding("<XRController>{LeftHand}/secondaryButton"); // Y ��ư ���
        leftHandYButton.Enable(); // Ȱ��ȭ
    }

    // ���� ���� �ٽ� �ε�
    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
