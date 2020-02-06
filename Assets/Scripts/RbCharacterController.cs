using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class RbCharacterController : MonoBehaviour
{
    public float speed = 5f;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
    private Rigidbody rb;

    private AudioSource footstep_Sound;
    [SerializeField] private AudioClip[] footstep_Clip;
    public float volume_Min, volume_Max;
    private float accumulated_Distance;
    [HideInInspector] public float step_Distance;

    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;

    private float walkVolumeMin = 0.1f, walkVolumeMax = 0.5f;
    private float sprintVolume = 0.8f;

    private float sprint_Value = 100f;
    public float sprint_Treshold = 10f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;

        footstep_Sound = GetComponent<AudioSource>();
        volume_Min = walkVolumeMin;
        volume_Max = walkVolumeMax;
        step_Distance = walkStepDistance;
    }

    void Update()
    {
        CheckToPlayFootstepSound();
        Sprint();
    }

    void FixedUpdate()
    {
        if (grounded)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            targetVelocity *= speed;

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (canJump && Input.GetButton("Jump"))
            {
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
            }
        }

        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));

        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }


    private void Sprint()
    {
        if (sprint_Value > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = sprintSpeed;

                step_Distance = sprintStepDistance;
                volume_Min = sprintVolume;
                volume_Max = sprintVolume;
            }


            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = walkSpeed;

                step_Distance = walkStepDistance;
                volume_Min = walkVolumeMin;
                volume_Max = walkVolumeMax;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint_Value -= sprint_Treshold * Time.deltaTime;

            if (sprint_Value <= 0f)
            {
                sprint_Value = 0f;

                // reset the speed and sound
                speed = walkSpeed;
                step_Distance = walkStepDistance;
                volume_Min = walkVolumeMin;
                volume_Max = walkVolumeMax;
            }
        }
        else
        {
            if (sprint_Value != 100f)
            {
                sprint_Value += (sprint_Treshold / 2f) * Time.deltaTime;

                if (sprint_Value > 100f)
                {
                    sprint_Value = 100f;
                }
            }
        }
    }

    void CheckToPlayFootstepSound()
    {
        //Debug.Log(rb.velocity);

        // if we are NOT on the ground
        if (!grounded)
            return;

        if (rb.velocity.sqrMagnitude >= 3)
        {
            // accumulated distance is the value how far can we go 
            // e.g. make a step or sprint, or move while crouching
            // until we play the footstep sound
            //Debug.Log("ac start"+accumulated_Distance);
            accumulated_Distance += Time.deltaTime;
           // Debug.Log("ac end"+accumulated_Distance);
            //Debug.Log("sd:"+ step_Distance);
            //Debug.Log("rb:"+rb.velocity.sqrMagnitude);


            if (accumulated_Distance > step_Distance)
            {
                //Debug.Log("ac:"+ accumulated_Distance);
                //Debug.Log("sd:"+ step_Distance);

                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0f;
            }
        }
        else
        {
            accumulated_Distance = 0f;
        }

    }



}
