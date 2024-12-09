using UnityEngine;
using UnityEngine.InputSystem;

// https://www.youtube.com/watch?v=8PCNNro7Rt0&t=701s ���� ����
public class AnimateHandOnInput : MonoBehaviour
{
    [Header ("- Input Action Property")]
    public InputActionProperty triggerAnimationAction; // Ʈ���� ��ư �ִϸ��̼��� �����ϴ� InputActionProperty    
    public InputActionProperty gripAnimationAction; // �׸� ��ư �ִϸ��̼��� �����ϴ� InputActionProperty
                                                    
    [Header("- Animator")]
    public Animator handAnimator; // �� �ִϸ��̼��� ������ Animator ������Ʈ

    // Update�� �� ������ ȣ��Ǵ� �޼����, �Է� ���� ���� �ִϸ��̼� ���� ������Ʈ
    void Update()
    {
        // Trigger �� �б�
        float triggerValue = triggerAnimationAction.action.ReadValue<float>();

        // Animator�� Trigger ���� ����
        handAnimator.SetFloat("Trigger", triggerValue);

        // Grip �� �б�
        float gripValue = gripAnimationAction.action.ReadValue<float>();

        // Animator�� Grip ���� ����
        handAnimator.SetFloat("Grip", gripValue);
    }
}
