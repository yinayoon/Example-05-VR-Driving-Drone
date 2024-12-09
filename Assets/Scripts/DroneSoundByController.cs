using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class DroneSoundByController : MonoBehaviour
{
    // ��� ����
    public AudioClip droneSound;
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource �ʱ�ȭ
        audioSource = gameObject.AddComponent<AudioSource>(); // �������� �߰�
        audioSource.loop = true;
        audioSource.clip = droneSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // ��Ʈ�ѷ� �Է� Ȯ��
        if (IsQuestControllerInputDetected())
        {
            // �Է��� ������ �Ҹ� ���
            PlaySound();
        }
        else
        {
            // �Է��� ������ �Ҹ��� ����
            StopSound();
        }
    }

    // �Է��� �����Ǿ����� Ȯ��
    private bool IsQuestControllerInputDetected()
    {
        // ���� ��Ʈ�ѷ�
        if (IsButtonOrThumbstickActive(XRNode.LeftHand))
        {
            return true;
        }

        // ������ ��Ʈ�ѷ�
        if (IsButtonOrThumbstickActive(XRNode.RightHand))
        {
            return true;
        }

        return false;
    }

    // Ư�� ��Ʈ�ѷ����� ��ư �Ǵ� Thumbstick�� �Է��� ����
    private bool IsButtonOrThumbstickActive(XRNode node)
    {
        // Ư�� ��Ʈ�ѷ�(XRNode.LeftHand �Ǵ� XRNode.RightHand)�� �Է� ��ġ�� ������
        List<InputDevice> devices = new List<InputDevice>();
        // GetDevicesAtXRNode : Unity�� XR(InputDevices) API�� ����Ͽ� Ư�� XRNode(�޼� ��Ʈ�ѷ�, ������ ��Ʈ�ѷ�, HMD)��
        // ������ XR Input Device���� �������� �޼���
        InputDevices.GetDevicesAtXRNode(node, devices);

        // �Է� ��ġ ����Ʈ�� ��ȸ�ϸ� �Է� ���¸� Ȯ��
        foreach (InputDevice device in devices)
        {
            // Ʈ���� ��ư �Է� Ȯ��
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
                return true;

            // �׸� ��ư �Է� Ȯ��
            if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed) && gripPressed)
                return true;

            // ��ƽ�� ������ Ȯ�� (X, Y �� ���� 0�� �ƴ� ���)
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValue) && thumbstickValue != Vector2.zero)
                return true;
        }

        return false;
    }

    // ����� ���
    private void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // ����� ����
    private void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
