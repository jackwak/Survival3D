using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;

    private float xRot;
    private float yRot;

    private void Awake()
    {
        _playerTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (Camera.main != null)
            _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        yRot += mouseX;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        _playerTransform.localRotation = Quaternion.Euler(0, yRot, 0);
        _cameraTransform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
}
