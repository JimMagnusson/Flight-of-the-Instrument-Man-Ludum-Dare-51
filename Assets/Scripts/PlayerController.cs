using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MovementType { idle, walking, running}

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float rotationSpeed = 1f;

    [SerializeField] private float walkingThreshold = 0.5f;

    [SerializeField] private Camera cam;
    
    private Health health;
    private Animator animator;
    
    private MovementType movementType = MovementType.idle;

    private PlayerControls playerControls;
    
    private CharacterController controller;

    private Quaternion targetRotation;

    private WheelRotator _wheelRotator;
    
    private UIManager uiManager;

    private GameController _gameController;
    
    private float startYVal;

    private void Awake() {
        playerControls = new PlayerControls();
        uiManager = FindObjectOfType<UIManager>();
        _gameController = FindObjectOfType<GameController>();
    }
    
    private void OnEnable() {
        playerControls.Enable();
    }
    
    private void OnDisable() {
        playerControls.Disable();
    }
    
    void Start()
    {
        startYVal = transform.position.y;
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        
        controller = GetComponent<CharacterController>();

        _wheelRotator = GetComponent<WheelRotator>();
        health.OnDeathEvent += HealthOnOnDeathEvent;
    }

    private void HealthOnOnDeathEvent(Health obj)
    {
        // Death SFX and VFX
        //gameObject.SetActive(false);
        
        // TODO: Show retry screen.
        uiManager.ShowRetryImage();
        _gameController.GameState = GameState.retry;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Standard.Exit.triggered)
        {
            Application.Quit();
        }
        
        if (_gameController.GameState == GameState.retry && playerControls.Standard.Retry.triggered)
        {
            FindObjectOfType<LevelLoader>().ReloadScene();
        }

        if (_gameController.GameState == GameState.retry)
        {
            return;
        }
        
        Vector2 input = playerControls.Standard.Move.ReadValue<Vector2>();
        Vector3 aimInput = Vector3.zero;
        

        Vector2 aimControllerInput = playerControls.Standard.AimController.ReadValue<Vector2>();
        Vector2 aimMouseInput = playerControls.Standard.Aim.ReadValue<Vector2>();
        Vector2 aimMouseInputDelta = playerControls.Standard.MouseDelta.ReadValue<Vector2>();
        
        if (aimMouseInputDelta.magnitude > 0)
        {
            Vector3 mouseInput3 = new Vector3(aimMouseInput.x, aimMouseInput.y, 0);
            Ray ray = cam.ScreenPointToRay(mouseInput3);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Vector3 hitPointNew = new Vector3(hit.point.x, 0, hit.point.z);
                Vector3 towardMouse = hitPointNew - transform.position;
                towardMouse.Normalize();
                aimInput = towardMouse;
            }
        }
        
        if (aimControllerInput.magnitude > 0)
        {
            aimInput = new Vector3(aimControllerInput.x, 0, aimControllerInput.y);
        }
        
        if (aimInput != Vector3.zero)
        {
            //Rotate smoothly to this target:
            targetRotation = Quaternion.LookRotation(aimInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
        // Movement part

        Vector3 move = new Vector3(input.x, 0, input.y);
        
        if(input.magnitude > 0)
        {
            controller.Move(move * Time.deltaTime * moveSpeed);
            transform.position = new Vector3(transform.position.x, startYVal, transform.position.z);
            _wheelRotator.RotateWheels(true, input.magnitude * moveSpeed);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health.RecieveDamage(1);
        }
    }
}
