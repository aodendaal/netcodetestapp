using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class NetworkPlayerController : NetworkBehaviour
{
    [SerializeField] GameObject _playerCamera;
    private PlayerInput _playerInput;
    private CharacterController _characterController;

    private float _speed = 5.0f;
    private float _lookSpeed = 200.0f;

    private float _lookAngle = 0f;

    public override void OnNetworkSpawn()
    {
        var camera = GameObject.FindGameObjectWithTag("MainCamera");
        if (camera != null)
        {
            camera.SetActive(false);
        }

        if (!IsOwner)
        {
            GetComponent<PlayerInput>().enabled = false;
            _playerCamera.SetActive(false);
            enabled = false;
        }
    }

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();

        Rotation();

        Look();
    }

    private void Rotation()
    {
        var input = _playerInput.GetLookHorizontalInput();

        var v = new Vector3(0, input * _lookSpeed * 0.4f, 0);

        transform.Rotate(v, Space.Self);
    }

    private void Movement()
    {
        var move = _playerInput.GetMoveInput();

        _characterController.Move(move * _speed);
    }

    private void Look()
    {
        var input = _playerInput.GetLookVerticalInput();

        _lookAngle += input * _lookSpeed;
        _lookAngle = Mathf.Clamp(_lookAngle, -89f, 89f);

        _playerCamera.transform.localEulerAngles = new Vector3(_lookAngle * -1f, 0f, 0f);
    }
}
