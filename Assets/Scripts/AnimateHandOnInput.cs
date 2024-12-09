using UnityEngine;
using UnityEngine.InputSystem;

// https://www.youtube.com/watch?v=8PCNNro7Rt0&t=701s 영상 참고
public class AnimateHandOnInput : MonoBehaviour
{
    [Header ("- Input Action Property")]
    public InputActionProperty triggerAnimationAction; // 트리거 버튼 애니메이션을 제어하는 InputActionProperty    
    public InputActionProperty gripAnimationAction; // 그립 버튼 애니메이션을 제어하는 InputActionProperty
                                                    
    [Header("- Animator")]
    public Animator handAnimator; // 손 애니메이션을 제어할 Animator 컴포넌트

    // Update는 매 프레임 호출되는 메서드로, 입력 값에 따라 애니메이션 값을 업데이트
    void Update()
    {
        // Trigger 값 읽기
        float triggerValue = triggerAnimationAction.action.ReadValue<float>();

        // Animator에 Trigger 값을 전달
        handAnimator.SetFloat("Trigger", triggerValue);

        // Grip 값 읽기
        float gripValue = gripAnimationAction.action.ReadValue<float>();

        // Animator에 Grip 값을 전달
        handAnimator.SetFloat("Grip", gripValue);
    }
}
