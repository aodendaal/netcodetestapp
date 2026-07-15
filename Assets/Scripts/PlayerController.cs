using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _speed = 5f;

    private InputSystem_Actions _actions;

    private NetworkTransform _transform;

    void Awake()
    {
        _actions = new InputSystem_Actions();
        _transform = GetComponent<NetworkTransform>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var movement = _actions.Player.Move.ReadValue<Vector2>();

        _transform.transform.position += new Vector3(movement.x, 0, movement.y) * _speed * Time.deltaTime;
    }

    void OnEnable()
    {
        _actions.Player.Enable();
    }

    void OnDisable()
    {
        _actions.Player.Disable();
    }
}
