using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _lookAction;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Player/Move");
        _lookAction = InputSystem.actions.FindAction("Player/Look");

        _moveAction.Enable();
        _lookAction.Enable();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public Vector3 GetMoveInput()
    {
        var input = _moveAction.ReadValue<Vector2>();
        var move = new Vector3(input.x, 0, input.y);

        move = Vector3.ClampMagnitude(move, 1f);

        return move;
    }

    public float GetLookHorizontalInput()
    {
        var input = _lookAction.ReadValue<Vector2>();
        return input.x;
    }

    public float GetLookVerticalInput()
    {
        var input = _lookAction.ReadValue<Vector2>();
        return input.y;
    }
}
