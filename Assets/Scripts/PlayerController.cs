using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType { idle, walking, running}

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float rotationSpeed = 1f;

    [SerializeField] private float walkingThreshold = 0.5f;
    
    private Health health;
    private Animator animator;
    
    private MovementType movementType = MovementType.idle;

    private PlayerControls playerControls;
    
    private CharacterController controller;

    private Quaternion targetRotation;

    private WheelRotator _wheelRotator;
    
    private void Awake() {
        playerControls = new PlayerControls();
    }
    
    private void OnEnable() {
        playerControls.Enable();
    }
    
    private void OnDisable() {
        playerControls.Disable();
    }
    
    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        
        controller = GetComponent<CharacterController>();

        _wheelRotator = GetComponent<WheelRotator>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 input = playerControls.Standard.Move.ReadValue<Vector2>();

        Vector3 move = new Vector3(input.x, 0, input.y);
        
        // Rotate player
        if(input.magnitude > 0)
        {
            controller.Move(move * Time.deltaTime * moveSpeed);
            _wheelRotator.RotateWheels(true, input.magnitude * moveSpeed);
            
            //Adding these vectors together will result in a position in the world, that is around your player.
            Vector3 goal = move + transform.position;

            //Now we create a target rotation, by creating a direction vector: (This would be just be inputVector in this case).
            targetRotation = Quaternion.LookRotation(goal - transform.position);
            
            //Rotate smoothly to this target:
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

    }
}
