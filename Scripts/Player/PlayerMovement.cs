using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float gravity = -50f;
    [SerializeField] float turnSmooth = .1f;
    [SerializeField] float groundRadius = .3f;

    [SerializeField] Transform groundCheck = default;
    [SerializeField] LayerMask whatIsGround = default;

    [Header("Roll")]
    [SerializeField] float rollSpeed;
    [SerializeField] float dodgeCoolDown;
    [SerializeField] float rollTime;


    CharacterController controller;
    Animator anim;
    Rigidbody rigid;
    Vector3 velocity;

    float inputH;
    float inputV;
    float turnSmoothTimer;
    float rollCoolDown;
    float lastRoll = -100f;
    float rollTimeLeft;

    bool isGrounded;
    bool canMove = true;
    bool isRunning;
    bool isRolling;

    Vector3 direction;

    private void Awake()
    {
        instance = this;

        controller = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }
   
    void Update()
    {
        if (PlayerHealth.instance.GetDead())
        {
            rigid.isKinematic = true;
            this.gameObject.isStatic = true;
            return;
        }
        else
        {
            rigid.isKinematic = false;
            this.gameObject.isStatic = false;
        }

        CheckSurroundings();
        CheckMovement();
        UpdateAnimation();
        Dodge();

    }

    void CheckSurroundings()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void CheckMovement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        direction = new Vector3(inputH, 0f, inputV).normalized;

        if (direction.magnitude > 0.01f && canMove)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothTimer, turnSmooth);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * movementSpeed * Time.deltaTime);
            isRunning = true;
        }
        else if (!canMove)
        {
            isRunning = false;
        }
        else
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= (lastRoll + rollCoolDown))
                TryDodge();
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    private void TryDodge()
    {
        isRolling = true;
        rollTimeLeft = rollTime;
        lastRoll = Time.time;
    }

    void Dodge()
    {
        if (isRolling)
        {
            if(rollTimeLeft > 0)
            {
                anim.SetTrigger("roll");
                velocity = direction * rollSpeed;
                rollTimeLeft -= Time.deltaTime;
            }

            if(rollTimeLeft <= 0)
            {
                velocity = 0;
                isRolling = false;
            }
        }
    }



    void UpdateAnimation()
    {

        isRunning = direction.magnitude > 0.01 ? true : false;
        anim.SetBool("isRunning", isRunning);
        //anim.SetFloat("movementVelocityY", velocity.y);
    }

    public void GetMovement(bool set)
    {
        canMove = set;
    }






}
