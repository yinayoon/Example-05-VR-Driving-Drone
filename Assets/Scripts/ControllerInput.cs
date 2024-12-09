using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    // 움직일 대상이 되는 카메라 오브젝트
    [Header("- Camera Object")]
    public Transform movableCamera;

    // 비행기 모델
    [Header("- Dron Model")]
    public Transform dronModel;

    // 이동, 회전, 기울기 속도 설정
    [Header("- Float for Movement")]
    public float horizontalSpeed = 5.0f; // 앞뒤 이동 속도
    public float verticalSpeed = 1.5f; // 위/아래 이동 속도
    public float rotationSpeed = 100.0f; // 좌우 회전 속도
    public float rollSpeed = 50.0f; // 기울기 속도
    public float maxRollAngle = 5.0f; // 최대 기울기 각도

    // Input Actions
    private InputAction leftTriggerAction;
    private InputAction leftGripAction;
    private InputAction leftJoystickAction;

    private InputAction rightTriggerAction;
    private InputAction rightGripAction;
    private InputAction rightJoystickAction;

    // 현재 Roll 상태를 저장 (부드러운 기울기를 위한 값임)
    private float currentRollAngle = 0.0f;

    void Start()
    {
        // Input Actions 초기화
        InitializeInputActions();
    }

    void Update()
    {
        // 왼손 입력 처리
        HandleLeftHandInputs();

        // 오른손 입력 처리
        HandleRightHandInputs();
    }

    
    // InputAction 초기화 및 컨트롤러 입력 경로 설정
    private void InitializeInputActions()
    {
        // 왼손 Trigger Action
        leftTriggerAction = new InputAction("LeftTrigger", InputActionType.Value);
        leftTriggerAction.AddBinding("<XRController>{LeftHand}/trigger"); // 왼손 트리거 버튼 경로
        leftTriggerAction.Enable();

        // 왼손 Grip Action
        leftGripAction = new InputAction("LeftGrip", InputActionType.Value);
        leftGripAction.AddBinding("<XRController>{LeftHand}/grip"); // 왼손 그립 버튼 경로
        leftGripAction.Enable();

        // 왼손 Thumbstick
        leftJoystickAction = new InputAction("LeftJoystick", InputActionType.Value);
        leftJoystickAction.AddBinding("<XRController>{LeftHand}/thumbstick"); // 왼손 Thumbstick 경로
        leftJoystickAction.Enable();

        // 오른손 Trigger Action
        rightTriggerAction = new InputAction("RightTrigger", InputActionType.Value);
        rightTriggerAction.AddBinding("<XRController>{RightHand}/trigger"); // 오른손 트리거 버튼 경로
        rightTriggerAction.Enable();

        // 오른손 Grip Action
        rightGripAction = new InputAction("RightGrip", InputActionType.Value);
        rightGripAction.AddBinding("<XRController>{RightHand}/grip"); // 오른손 그립 버튼 경로
        rightGripAction.Enable();

        // 오른손 Thumbstick
        rightJoystickAction = new InputAction("RightJoystick", InputActionType.Value);
        rightJoystickAction.AddBinding("<XRController>{RightHand}/thumbstick"); // 오른손 Thumbstick 경로
        rightJoystickAction.Enable();
    }

    
    // 왼손 컨트롤러의 입력 값을 처리
    private void HandleLeftHandInputs()
    {
        // 스틱 입력 값 가져오기 (이동 및 회전)
        Vector2 leftJoystickValue = leftJoystickAction.ReadValue<Vector2>();
        if (leftJoystickValue != Vector2.zero)
        {
            HandleHorizontalMovement(leftJoystickValue.y); // Z 축 방향 이동
            RotateObject(leftJoystickValue.x); // X 축 방향 회전
        }

        // 트리거 및 그립 입력 값을 사용한 위/아래 이동 처리
        HandleVerticalMovement(leftTriggerAction.ReadValue<float>(), leftGripAction.ReadValue<float>());
    }

    // 오른손 컨트롤러의 입력 값을 처리
    private void HandleRightHandInputs()
    {
        // 스틱 입력 값 가져오기 (이동 및 회전)
        Vector2 rightJoystickValue = rightJoystickAction.ReadValue<Vector2>();
        if (rightJoystickValue != Vector2.zero)
        {
            HandleHorizontalMovement(rightJoystickValue.y); // Z 축 방향 이동
            RotateObject(rightJoystickValue.x); // X 축 방향 회전
        }

        // 트리거 및 그립 입력 값을 사용한 위/아래 이동 처리
        HandleVerticalMovement(rightTriggerAction.ReadValue<float>(), rightGripAction.ReadValue<float>());
    }

    // Z축 기준으로 카메라를 앞뒤로 이동
    private void HandleHorizontalMovement(float inputY)
    {
        Vector3 movement = new Vector3(0, 0, inputY) * horizontalSpeed * Time.deltaTime;

        if (movableCamera != null)
        {
            movableCamera.Translate(movement, Space.Self); // 로컬 공간 기준으로 이동
        }
    }

    // 스틱 입력을 바탕으로 드론의 회전과 기울기를 처리
    private void RotateObject(float inputX)
    {
        if (dronModel == null) return;

        // Y 축 기준 회전
        float yawRotation = inputX * rotationSpeed * Time.deltaTime;
        if (movableCamera != null)
        {
            movableCamera.Rotate(0, yawRotation, 0, Space.Self);
        }

        // Z 축 기준 기울기
        float targetRollAngle = -inputX * maxRollAngle; // 스틱 입력에 따라 목표 기울기 각도 설정
        currentRollAngle = Mathf.Lerp(currentRollAngle, targetRollAngle, rollSpeed * Time.deltaTime); // 부드럽게 기울이기

        Vector3 localRotation = dronModel.localEulerAngles;
        localRotation.z = currentRollAngle;
        dronModel.localEulerAngles = localRotation;
    }

    // 트리거 및 그립 입력 값을 사용한 위/아래 이동 처리
    private void HandleVerticalMovement(float triggerValue, float gripValue)
    {
        // 위로 이동 (트리거)
        if (triggerValue > 0.1f)
        {
            Vector3 upwardMovement = Vector3.up * triggerValue * verticalSpeed * Time.deltaTime;
            if (movableCamera != null)
            {               
                movableCamera.Translate(upwardMovement);
            }
        }

        // 아래로 이동 (그립)
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
