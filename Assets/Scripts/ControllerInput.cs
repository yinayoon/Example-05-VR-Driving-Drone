using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    // ������ ����� �Ǵ� ī�޶� ������Ʈ
    [Header("- Camera Object")]
    public Transform movableCamera;

    // ����� ��
    [Header("- Dron Model")]
    public Transform dronModel;

    // �̵�, ȸ��, ���� �ӵ� ����
    [Header("- Float for Movement")]
    public float horizontalSpeed = 5.0f; // �յ� �̵� �ӵ�
    public float verticalSpeed = 1.5f; // ��/�Ʒ� �̵� �ӵ�
    public float rotationSpeed = 100.0f; // �¿� ȸ�� �ӵ�
    public float rollSpeed = 50.0f; // ���� �ӵ�
    public float maxRollAngle = 5.0f; // �ִ� ���� ����

    // Input Actions
    private InputAction leftTriggerAction;
    private InputAction leftGripAction;
    private InputAction leftJoystickAction;

    private InputAction rightTriggerAction;
    private InputAction rightGripAction;
    private InputAction rightJoystickAction;

    // ���� Roll ���¸� ���� (�ε巯�� ���⸦ ���� ����)
    private float currentRollAngle = 0.0f;

    void Start()
    {
        // Input Actions �ʱ�ȭ
        InitializeInputActions();
    }

    void Update()
    {
        // �޼� �Է� ó��
        HandleLeftHandInputs();

        // ������ �Է� ó��
        HandleRightHandInputs();
    }

    
    // InputAction �ʱ�ȭ �� ��Ʈ�ѷ� �Է� ��� ����
    private void InitializeInputActions()
    {
        // �޼� Trigger Action
        leftTriggerAction = new InputAction("LeftTrigger", InputActionType.Value);
        leftTriggerAction.AddBinding("<XRController>{LeftHand}/trigger"); // �޼� Ʈ���� ��ư ���
        leftTriggerAction.Enable();

        // �޼� Grip Action
        leftGripAction = new InputAction("LeftGrip", InputActionType.Value);
        leftGripAction.AddBinding("<XRController>{LeftHand}/grip"); // �޼� �׸� ��ư ���
        leftGripAction.Enable();

        // �޼� Thumbstick
        leftJoystickAction = new InputAction("LeftJoystick", InputActionType.Value);
        leftJoystickAction.AddBinding("<XRController>{LeftHand}/thumbstick"); // �޼� Thumbstick ���
        leftJoystickAction.Enable();

        // ������ Trigger Action
        rightTriggerAction = new InputAction("RightTrigger", InputActionType.Value);
        rightTriggerAction.AddBinding("<XRController>{RightHand}/trigger"); // ������ Ʈ���� ��ư ���
        rightTriggerAction.Enable();

        // ������ Grip Action
        rightGripAction = new InputAction("RightGrip", InputActionType.Value);
        rightGripAction.AddBinding("<XRController>{RightHand}/grip"); // ������ �׸� ��ư ���
        rightGripAction.Enable();

        // ������ Thumbstick
        rightJoystickAction = new InputAction("RightJoystick", InputActionType.Value);
        rightJoystickAction.AddBinding("<XRController>{RightHand}/thumbstick"); // ������ Thumbstick ���
        rightJoystickAction.Enable();
    }

    
    // �޼� ��Ʈ�ѷ��� �Է� ���� ó��
    private void HandleLeftHandInputs()
    {
        // ��ƽ �Է� �� �������� (�̵� �� ȸ��)
        Vector2 leftJoystickValue = leftJoystickAction.ReadValue<Vector2>();
        if (leftJoystickValue != Vector2.zero)
        {
            HandleHorizontalMovement(leftJoystickValue.y); // Z �� ���� �̵�
            RotateObject(leftJoystickValue.x); // X �� ���� ȸ��
        }

        // Ʈ���� �� �׸� �Է� ���� ����� ��/�Ʒ� �̵� ó��
        HandleVerticalMovement(leftTriggerAction.ReadValue<float>(), leftGripAction.ReadValue<float>());
    }

    // ������ ��Ʈ�ѷ��� �Է� ���� ó��
    private void HandleRightHandInputs()
    {
        // ��ƽ �Է� �� �������� (�̵� �� ȸ��)
        Vector2 rightJoystickValue = rightJoystickAction.ReadValue<Vector2>();
        if (rightJoystickValue != Vector2.zero)
        {
            HandleHorizontalMovement(rightJoystickValue.y); // Z �� ���� �̵�
            RotateObject(rightJoystickValue.x); // X �� ���� ȸ��
        }

        // Ʈ���� �� �׸� �Է� ���� ����� ��/�Ʒ� �̵� ó��
        HandleVerticalMovement(rightTriggerAction.ReadValue<float>(), rightGripAction.ReadValue<float>());
    }

    // Z�� �������� ī�޶� �յڷ� �̵�
    private void HandleHorizontalMovement(float inputY)
    {
        Vector3 movement = new Vector3(0, 0, inputY) * horizontalSpeed * Time.deltaTime;

        if (movableCamera != null)
        {
            movableCamera.Translate(movement, Space.Self); // ���� ���� �������� �̵�
        }
    }

    // ��ƽ �Է��� �������� ����� ȸ���� ���⸦ ó��
    private void RotateObject(float inputX)
    {
        if (dronModel == null) return;

        // Y �� ���� ȸ��
        float yawRotation = inputX * rotationSpeed * Time.deltaTime;
        if (movableCamera != null)
        {
            movableCamera.Rotate(0, yawRotation, 0, Space.Self);
        }

        // Z �� ���� ����
        float targetRollAngle = -inputX * maxRollAngle; // ��ƽ �Է¿� ���� ��ǥ ���� ���� ����
        currentRollAngle = Mathf.Lerp(currentRollAngle, targetRollAngle, rollSpeed * Time.deltaTime); // �ε巴�� ����̱�

        Vector3 localRotation = dronModel.localEulerAngles;
        localRotation.z = currentRollAngle;
        dronModel.localEulerAngles = localRotation;
    }

    // Ʈ���� �� �׸� �Է� ���� ����� ��/�Ʒ� �̵� ó��
    private void HandleVerticalMovement(float triggerValue, float gripValue)
    {
        // ���� �̵� (Ʈ����)
        if (triggerValue > 0.1f)
        {
            Vector3 upwardMovement = Vector3.up * triggerValue * verticalSpeed * Time.deltaTime;
            if (movableCamera != null)
            {               
                movableCamera.Translate(upwardMovement);
            }
        }

        // �Ʒ��� �̵� (�׸�)
        if (gripValue > 0.1f)
        {
            Vector3 downwardMovement = Vector3.down * gripValue * verticalSpeed * Time.deltaTime;
            if (movableCamera != null)
            {
                movableCamera.Translate(downwardMovement);
            }
        }
    }
}
