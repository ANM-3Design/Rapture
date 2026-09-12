using UnityEngine;
using Unity.Cinemachine; // CM2: use "Cinemachine" instead
using System.Collections;

public class CameraJuice : MonoBehaviour
{
    public static CameraJuice Instance;

    CinemachineCamera vcam;
    CinemachineImpulseSource impulseSource;

    public float defaultFov = 40f;
    public float zoomOutFov = 55f;
    public float punchInFov = 30f;
    public float punchDuration = 0.1f;
    public float zoomOutSpeed = 3f;
    public float zoomBackSpeed = 5f;
    public float shakeForce = 0.5f;

    float targetFov;

    void Awake()
    {
        Instance = this;
        vcam = GetComponent<CinemachineCamera>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Start()
    {
        targetFov = defaultFov;
        var lens = vcam.Lens;
        lens.FieldOfView = defaultFov;
        vcam.Lens = lens;
    }

    void Update()
    {
        float speed = (targetFov == defaultFov) ? zoomBackSpeed : zoomOutSpeed;
        var lens = vcam.Lens;
        lens.FieldOfView = Mathf.Lerp(lens.FieldOfView, targetFov, Time.deltaTime * speed);
        vcam.Lens = lens;
    }

    public void OnFire()
    {
        StopAllCoroutines();
        StartCoroutine(FireRoutine());
        impulseSource.GenerateImpulse(shakeForce);
    }

    public void OnDock() => targetFov = defaultFov;

    IEnumerator FireRoutine()
    {
        targetFov = punchInFov;
        var lens = vcam.Lens;
        lens.FieldOfView = punchInFov; // instant snap-in, the "punch"
        vcam.Lens = lens;
        yield return new WaitForSeconds(punchDuration);
        targetFov = zoomOutFov; // eases wider for the rest of the flight
    }
}