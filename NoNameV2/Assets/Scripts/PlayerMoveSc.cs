using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMoveSc : MonoBehaviour
{
    [Header("Hareket Deðerleri")]
    [SerializeField] private float normalSpeed = 7f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpHeight = 2f;
    private float gravity = -9.81f * 2;

    [Header("Ground")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float maxGroundAngle = 45f; // Max eðim derecesi

    private CharacterController controller;

    Vector3 velocity;
    public bool isGrounded;
    public Vector3 lastPosition;

    public bool isMoving;



    private void Start()
    {
        controller = GetComponent<CharacterController>();


    }

    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        CheckGround();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * normalSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * runSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        lastPosition = transform.position;
    }

    private void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance + 0.1f, groundLayerMask))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            if (angle <= maxGroundAngle)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = controller.isGrounded; // Use CharacterController's built-in isGrounded check
        }
    }

    public void MoveBackwardOnFire()
    {
        StartCoroutine(PerformRecoil());
    }

    private IEnumerator PerformRecoil()
    {
        float recoilDistance = 5f;
        float recoilDuration = 0.5f;
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;
        Vector3 backwardDirection = -transform.forward;

        while (elapsedTime < recoilDuration)
        {
            float step = (recoilDistance / recoilDuration) * Time.deltaTime;
            controller.Move(backwardDirection * step);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}
    
