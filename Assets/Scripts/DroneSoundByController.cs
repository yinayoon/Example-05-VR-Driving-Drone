using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class DroneSoundByController : MonoBehaviour
{
    // 드론 사운드
    public AudioClip droneSound;
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>(); // 동적으로 추가
        audioSource.loop = true;
        audioSource.clip = droneSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // 컨트롤러 입력 확인
        if (IsQuestControllerInputDetected())
        {
            // 입력이 감지시 소리 재생
            PlaySound();
        }
        else
        {
            // 입력이 없으면 소리를 멈춤
            StopSound();
        }
    }

    // 입력이 감지되었는지 확인
    private bool IsQuestControllerInputDetected()
    {
        // 왼쪽 컨트롤러
        if (IsButtonOrThumbstickActive(XRNode.LeftHand))
        {
            return true;
        }

        // 오른쪽 컨트롤러
        if (IsButtonOrThumbstickActive(XRNode.RightHand))
        {
            return true;
        }

        return false;
    }

    // 특정 컨트롤러에서 버튼 또는 Thumbstick의 입력을 감지
    private bool IsButtonOrThumbstickActive(XRNode node)
    {
        // 특정 컨트롤러(XRNode.LeftHand 또는 XRNode.RightHand)의 입력 장치를 가져옴
        List<InputDevice> devices = new List<InputDevice>();
        // GetDevicesAtXRNode : Unity의 XR(InputDevices) API를 사용하여 특정 XRNode(왼손 컨트롤러, 오른손 컨트롤러, HMD)와
        // 연관된 XR Input Device들을 가져오는 메서드
        InputDevices.GetDevicesAtXRNode(node, devices);

        // 입력 장치 리스트를 순회하며 입력 상태를 확인
        foreach (InputDevice device in devices)
        {
            // 트리거 버튼 입력 확인
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
                return true;

            // 그립 버튼 입력 확인
            if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool gripPressed) && gripPressed)
                return true;

            // 스틱의 움직임 확인 (X, Y 축 값이 0이 아닌 경우)
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValue) && thumbstickValue != Vector2.zero)
                return true;
        }

        return false;
    }

    // 오디오 재생
    private void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // 오디오 정지
    private void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
