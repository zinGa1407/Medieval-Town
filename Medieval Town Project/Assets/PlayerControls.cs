using UnityEngine;
using System.Collections;


public class PlayerControls : MonoBehaviour {

    private Camera _mainCamera;
    private Rigidbody playerBody;

    //Player Rotation
    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -60F;
    private float maximumY = 60F;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 playerMovement;

    public float mouseSensitivity;
    private float rotationX = 0F;
    private float rotationY = 0F;
    private Quaternion originalRotation;

   
    void Start () {
        _mainCamera = gameObject.transform.GetChild(0).GetComponent<Camera>();
        originalRotation = transform.localRotation;

        playerBody = gameObject.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update () {
        //Camera + rotation
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY += Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);

        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        transform.localRotation = originalRotation * xQuaternion;
        _mainCamera.gameObject.transform.localRotation = originalRotation * yQuaternion;

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(Vector3.ClampMagnitude(moveDirection, 1));
        moveDirection = moveDirection * 5f;
        moveDirection = new Vector3(moveDirection.x, playerBody.velocity.y, moveDirection.z);
        playerBody.velocity = moveDirection;


        //Duck (improve smooth)
        //if (Input.GetKeyDown(KeyCode.LeftControl)) _mainCamera.transform.localPosition = new Vector3(0f, -0.2f, 0f);
        //if (Input.GetKeyUp(KeyCode.LeftControl)) _mainCamera.transform.localPosition = new Vector3(0f, 0f, 0f);

        // QUIT / EXIT
        //if (Input.GetKeyDown(KeyCode.Escape)) UnityEditor.EditorApplication.isPlaying = false;
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
         angle += 360f;
        if (angle > 360f)
         angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
