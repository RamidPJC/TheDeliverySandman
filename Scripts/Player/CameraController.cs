using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public enum CameraTargetType
{
    Player,
    Vehicle
}

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    private float xRot;
    private float yRot;

    [SerializeField] private Transform owner;

    [SerializeField] private Camera physicCamera;

    [SerializeField] private Transform tracker;

    private CinemachineCamera virtualCamera;
    private CinemachineInputAxisController inputController;

    private CameraTargetType currentTarget;
    private List<Transform> currentCameraSockets;
    private int currentCameraSocketId = 0;
    private Transform currentCameraSocketTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        virtualCamera = GetComponent<CinemachineCamera>();
        inputController = GetComponent<CinemachineInputAxisController>();

        SwitchTarget(owner, CameraTargetType.Player);
    }

    private void Update()
    {

        if (Input.GetButtonDown("CameraAngle"))
        {
            currentCameraSocketId++;
            ChangeCameraAngle();
        }

        xRot -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        yRot += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        switch (currentTarget)
        {
            case CameraTargetType.Player:
                xRot = Mathf.Clamp(xRot, -90, 80);
                owner.rotation = Quaternion.Euler(0, yRot, 0);
                currentCameraSocketTransform.rotation = Quaternion.Euler(xRot, currentCameraSocketTransform.eulerAngles.y, 0);
                tracker.rotation = Quaternion.Euler(xRot, currentCameraSocketTransform.eulerAngles.y, 0);
                break;
            case CameraTargetType.Vehicle:
                xRot = Mathf.Clamp(xRot, -30, 30);
                yRot = Mathf.Clamp(yRot, -90, 90);
                currentCameraSocketTransform.localRotation = Quaternion.Euler(xRot, yRot, 0);
                break;
        }
    }

    private void ChangeCameraAngle()
    {
        if (currentCameraSocketId > currentCameraSockets.Count-1)
        {
            currentCameraSocketId = 0;
        }

        SetNewCameraSettings();
    }

    public void SwitchTarget(Transform target, CameraTargetType targetType)
    {
        currentTarget = targetType;

        currentCameraSockets = target.GetComponentInParent<CameraSockets>().GetCameraSockets();
        currentCameraSocketId = 0;

        SetNewCameraSettings();
    }

    private void SetNewCameraSettings()
    {
        currentCameraSocketTransform = currentCameraSockets[currentCameraSocketId];
        inputController.enabled = false;

        xRot = 0;
        yRot = 0;

        foreach (var comp in virtualCamera.GetComponents<CinemachineComponentBase>())
        {
            DestroyImmediate(comp);
        }

        virtualCamera.Follow = currentCameraSocketTransform;
        virtualCamera.LookAt = currentCameraSocketTransform;

        CameraSocketSettings settings = currentCameraSocketTransform.GetComponent<CameraSocketSettings>();
        switch (settings.socketPositionControlMode)
        {
            case PositionControlMode.Follow:
                CinemachineFollow follow = virtualCamera.AddComponent<CinemachineFollow>();
                follow.FollowOffset = settings.positionFollowOffset;
                follow.TrackerSettings.PositionDamping = settings.damping;
                break;
            case PositionControlMode.OrbitalFollow:
                CinemachineOrbitalFollow orbFollow = virtualCamera.AddComponent<CinemachineOrbitalFollow>();
                orbFollow.TrackerSettings.PositionDamping = settings.damping;
                inputController.enabled = true;
                break;
        }

        switch (settings.socketRotationControlMode)
        {
            case RotationControlMode.RotateWithFollowTarget:
                CinemachineRotateWithFollowTarget rotFollow = virtualCamera.AddComponent<CinemachineRotateWithFollowTarget>();
                break;
            case RotationControlMode.RotationComposer:
                CinemachineRotationComposer rotComposer = virtualCamera.AddComponent<CinemachineRotationComposer>();
                break;
        }
    }
}
