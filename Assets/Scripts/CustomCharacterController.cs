using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour{ 	
	public Animator anim;
    public Rigidbody rig;
    public Transform mainCamera;
    public float jumpForce = 3.5f; 
    public float walkingSpeed = 2f;
    public float runningSpeed = 6f;
    public float currentSpeed;
    private float animationInterpolation = 1f;
    public FixedJoystick fixedJoystick;
    public float horizontal;
    public float vertical;
    public float lerpMultiplayer=7f;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    void Run()
    {
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1.5f, Time.deltaTime * 3);

#if PLATFORM_ANDROID
       anim.SetFloat("x", horizontal * animationInterpolation);
       anim.SetFloat("y", vertical * animationInterpolation);
#else
        anim.SetFloat("x", Input.GetAxis("Horizontal") * 1.5f);
        anim.SetFloat("y", Input.GetAxis("Vertical") * 1.5f);
#endif
        currentSpeed = Mathf.Lerp(currentSpeed, runningSpeed, Time.deltaTime * 3);
    }
    void Walk()
    {
        animationInterpolation = Mathf.Lerp(animationInterpolation, 1f, Time.deltaTime * 3);
#if PLATFORM_ANDROID
        anim.SetFloat("x", horizontal * animationInterpolation);
        anim.SetFloat("y", vertical * animationInterpolation);
#else
        anim.SetFloat("x", Input.GetAxis("Horizontal") * animationInterpolation);
        anim.SetFloat("y", Input.GetAxis("Vertical") * animationInterpolation);
#endif

        currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
    }
    private void Update()
    {
        horizontal = Mathf.Lerp(horizontal,fixedJoystick.Horizontal,Time.deltaTime*lerpMultiplayer);
        vertical = Mathf.Lerp(vertical, fixedJoystick.Vertical, Time.deltaTime * lerpMultiplayer);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        
        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                Walk();
            }
            else
            {
                Run();
            }
        }
        else
        {
            Walk();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(Enums.Result.Jump.ToString());
        }
    }
    void FixedUpdate()
    {
        Vector3 movingVector;
        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        camF.y = 0;
        camR.y = 0;
#if PLATFORM_ANDROID
        movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horizontal * currentSpeed,currentSpeed);
#else
        movingVector = Vector3.ClampMagnitude(camF.normalized * Input.GetAxis("Vertical") * currentSpeed + camR.normalized * Input.GetAxis("Horizontal") * currentSpeed, currentSpeed);
#endif
        anim.SetFloat("magnitude", movingVector.magnitude/currentSpeed);
        rig.velocity = new Vector3(movingVector.x, rig.velocity.y,movingVector.z);
        rig.angularVelocity = Vector3.zero;
    }
    public void Jump()
    {
        rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}