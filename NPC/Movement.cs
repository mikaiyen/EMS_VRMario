using System;
using UnityEngine;
using Oculus.Interaction;

public class Movement : MonoBehaviour
{
    public bool EnableLinearMovement = true;
    public bool EnableRotation = true;
    public bool HMDRotatesPlayer = true;
    public float RotationAngle = 45.0f;
    public float Speed = 0.0f;
    public float JumpForce = 5.0f;             // 跳躍的力度
    public OVRCameraRig CameraRig;

    private bool ReadyToSnapTurn;
    public bool isGrounded = true;             // 檢查是否在地面上
    private Rigidbody _rigidbody;
    public CharacterController PlayerCH;

    public event Action CameraUpdated;
    public event Action PreCharacterMove;

    public static float gravity = -15f;
    public static Vector3 movementValue = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        CameraRig ??= GetComponentInChildren<OVRCameraRig>();
    }

    private void Update()
    {
        if(PlayerCH)
        {
            movementValue.y += gravity* Time.deltaTime;
            PlayerCH.Move(movementValue* Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        CameraUpdated?.Invoke();
        PreCharacterMove?.Invoke();

        if (HMDRotatesPlayer) RotatePlayerToHMD();
        if (EnableLinearMovement) JoystickMovement();
        if (OVRInput.GetDown(OVRInput.Button.One) && isGrounded) Jump(); // 檢測 A 按鈕並執行跳躍
    }

    void RotatePlayerToHMD()
    {
        Transform root = CameraRig.trackingSpace;
        Transform centerEye = CameraRig.centerEyeAnchor;

        Vector3 prevPos = root.position;
        Quaternion prevRot = root.rotation;

        transform.rotation = Quaternion.Euler(0.0f, centerEye.rotation.eulerAngles.y, 0.0f);

        root.position = prevPos;
        root.rotation = prevRot;
    }

    void JoystickMovement()
    {
        Transform cameraTransform = CameraRig.centerEyeAnchor; 
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = right.y = 0;  // 忽略垂直方向
        Vector3 moveDir = (forward.normalized * primaryAxis.y + right.normalized * primaryAxis.x) * Speed * Time.fixedDeltaTime;
        
        //_rigidbody.MovePosition(_rigidbody.position + moveDir);
        PlayerCH.Move(moveDir);
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {    
        // 確保玩家只有在接觸地面時才可以跳躍
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Back to ground");
            isGrounded = true;
        }
    }
}
