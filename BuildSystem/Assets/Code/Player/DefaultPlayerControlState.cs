using UnityEngine;

public class DefaultPlayerControlState : IPlayerControlState
{
    private const float _GRAVITY_VELOCITY = 0f;

    private readonly Player _player;

    private float _movementSpeed = 5f;
    private float _rotationSpeed = 60f;

    private IInteractionHandler _activeContinuousInteractionHandler;


    public DefaultPlayerControlState(Player player)
    {
        _player = player;
    }


    public void Enable()
    {

    }

    public void Disable()
    {

    }

    public void Update()
    {
        UpdateCursor();
        UpdateMovement();
        UpdateRotation();
        UpdateInteraction();
    }


    private void UpdateCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateMovement()
    {
        var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        moveInput *= _movementSpeed;
        moveInput = Vector2.ClampMagnitude(moveInput, _movementSpeed);

        var motion = new Vector3(moveInput.x, _GRAVITY_VELOCITY, moveInput.y);
        motion = _player.view.Root.TransformDirection(motion);

        motion *= Time.deltaTime;

        _player.view.CharController.Move(motion);
    }

    private void UpdateRotation()
    {
        var rotationInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        rotationInput *= _rotationSpeed * Time.deltaTime;

        _player.view.Camera.transform.Rotate(Vector3.right, -rotationInput.y);
        _player.view.Root.Rotate(Vector3.up, rotationInput.x);
    }

    private void UpdateInteraction()
    {
        if (_activeContinuousInteractionHandler != null)
        {
            if (!_activeContinuousInteractionHandler.UpdateContinuousInteraction())
            {
                _activeContinuousInteractionHandler = null;
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = _player.view.Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (!hit.collider.TryGetComponent<IInteractableObject>(out var interactable))
                {
                    return;
                }

                var handler = _player.GetInteractionHandler(interactable);
                handler.Interact(interactable, out var isContinuousInteraction);

                if (isContinuousInteraction)
                {
                    _activeContinuousInteractionHandler = handler;
                }
            }
        }
    }
}