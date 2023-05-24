using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static CameraShake _instance;
    public static CameraShake Instance => _instance;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeIntensity;
    [SerializeField] public AnimationCurve shakeCurve;
    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;

    private float _shakeTimerTotal;
    private float _shakeDuration;
    private bool _isCameraShaking;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        _virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera()
    {
        _shakeTimerTotal = shakeDuration;
        _shakeDuration = shakeDuration;
        if (!_isCameraShaking)
        {
            StartCoroutine(ScreenShakeCoroutine());
        }
    }

    private IEnumerator ScreenShakeCoroutine()
    {
        _isCameraShaking = true;
        float elapsedTime = 0f;
        while (elapsedTime < _shakeDuration)
        {
            elapsedTime += Time.deltaTime;

            _virtualCameraNoise.m_AmplitudeGain = shakeIntensity * shakeCurve.Evaluate(elapsedTime / _shakeDuration);
            yield return null;
        }
        _isCameraShaking = false;
    }
}