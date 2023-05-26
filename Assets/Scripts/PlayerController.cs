using System.Collections;
using System.Collections.Generic;
using Interactables;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField]  float rotationSpeed;
    [SerializeField]  float angleStep = 22.5f; 
    [SerializeField] GameObject holdPosition;

    // Property for the CanMove variable
    public static bool CanMove { get; set; }
    
    
    private Vector2 _movementInput;
    private Rigidbody _rb;
    private Input _input;
    
    // public getter and setter for the held object
    public Interactable HeldObject { get; set; }

    public Transform HoldPosition => holdPosition.transform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = new Input();
        CanMove = true;
    }
    
    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Interact.performed -= OnInteract;
    }

    private void Update()
    {

        _movementInput = _input.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_movementInput.magnitude > 0.1f)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        if (!CanMove) return;
        float targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        Vector3 moveDirection = targetRotation * new Vector3(0, 0, 1);
        moveDirection.Normalize();

        Vector3 movementVelocity = moveDirection * (movementSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(_rb.position + movementVelocity);
    }

    private void Rotate()
    {
        if (_movementInput.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.y) * Mathf.Rad2Deg;
            
            // Round target angle to the nearest angle step
            targetAngle = Mathf.Round(targetAngle / angleStep) * angleStep;

            Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetAngle, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
    
    private void OnInteract(InputAction.CallbackContext obj)
    {
        if (!obj.performed) return;
        float interactDistance = 2f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactDistance);

        Interactable closestInteractable = null;
        float closestDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Interactable interactable))
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        if (closestInteractable != null)
        {
            closestInteractable.OnInteract(this);
        }
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
