using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float maximumSpeed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePreiod;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    public bool isJumping;
    public bool isMoving;
    public bool slow = false;

    public GameObject warningIcon;
    public GameObject warningRunText;
    public GameObject warningPulseText;

    private string[] allowedScenes = { "Zone1_set1", "Zone1_set2", "Zone1_set3" };

    void Start()
    {
        warningIcon.SetActive(false);
        warningRunText.SetActive(false);
        warningPulseText.SetActive(false);
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;

        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived += OnDataReceived;
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            warningIcon.SetActive(true);
            warningPulseText.SetActive(true);
            inputMagnitude /= 2;
            slow = true;
        }
        else
        {
            warningIcon.SetActive(false);
            warningPulseText.SetActive(false);
            slow = false;
        }

        animator.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);

        float speed = inputMagnitude * maximumSpeed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
            ySpeed = -0.5f; // Reset ySpeed when grounded
        }

        if (Input.GetButtonDown("Jump") && IsAllowedScene())
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePreiod)
        {
            characterController.stepOffset = originalStepOffset;
            animator.SetBool("IsGrounded", true);
            animator.SetBool("IsJumping", false);
            isJumping = false;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePreiod)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            warningRunText.SetActive(false);
            isMoving = true;
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            warningPulseText.SetActive(false);
            warningIcon.SetActive(true);
            warningRunText.SetActive(true);
            isMoving = false;
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnDestroy()
    {
        if (SerialManager.instance != null)
        {
            SerialManager.instance.OnDataReceived -= OnDataReceived;
        }
    }

    private void OnDataReceived(string data)
    {
        if (data == "Button 2 pressed" && IsAllowedScene())
        {
            jumpButtonPressedTime = Time.time;
        }
    }

    private bool IsAllowedScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        foreach (string scene in allowedScenes)
        {
            if (currentSceneName == scene)
            {
                return true;
            }
        }
        return false;
    }
}