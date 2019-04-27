using UnityEngine;

public class Player : Entity
{
    public Camera myCamera;
    public float mouseSensitivity;

    private Vector3 moveInput;
    private Vector2 mouseInput;
    private Vector2 mouseVelocity;

    private float xAxisClamp;

    private void Awake()
    {
        SetCusorLock(CursorLockMode.Locked);
        xAxisClamp = 0;
    }

    protected override void Update()
    {
        GetUserInput();
        LookAtCursor();
        base.Update();
    }

    private void GetUserInput()
    {
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = moveInput.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Dash();
    }

    private void SetCusorLock(CursorLockMode lockMode)
    {
        Cursor.lockState = lockMode;
    }

    private void LookAtCursor()
    {
        mouseVelocity = mouseInput * mouseSensitivity * Time.deltaTime;
        xAxisClamp += mouseVelocity.y;

        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            mouseVelocity.y = 0;
            ClampXAxisRotation(270);
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            mouseVelocity.y = 0;
            ClampXAxisRotation(90);
        }

     /*   myCamera.transform.Rotate(Vector3.left * mouseVelocity.y);
        transform.Rotate(Vector3.up * mouseVelocity.x);*/
    }

    private void ClampXAxisRotation(float value)
    {
        Vector3 eulerRotation = myCamera.transform.eulerAngles;
        eulerRotation.x = value;
        myCamera.transform.eulerAngles = eulerRotation;
    }

}
