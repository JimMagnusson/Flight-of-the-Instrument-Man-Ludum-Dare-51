using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType { idle, walking, running}

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float rotationSpeed = 1f;

    [SerializeField] private float walkingThreshold = 0.5f;
    
    [SerializeField] private float wallCheckDistance = 1f;

    //private Health health;
    private Animator animator;
    private MovementType movementType = MovementType.idle;

    private PlayerControls playerControls;
    
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
        //health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerControls.Standard.Move.ReadValue<Vector2>();
        
        // Rotate player
        if(move.magnitude > 0)
        {
            //Adding these vectors together will result in a position in the world, that is around your player.
            Vector3 goal = new Vector3(move.x, 0, move.y) + transform.position;

            //Now we create a target rotation, by creating a direction vector: (This would be just be inputVector in this case).
            Quaternion targetRotation = Quaternion.LookRotation(goal - transform.position);

            //Rotate smoothly to this target:
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
        
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, wallCheckDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            transform.position += new Vector3(move.x * moveSpeed * Time.deltaTime, 0, move.y * moveSpeed * Time.deltaTime);

        }
        
        
        //Debug.Log(move);
    }
}
