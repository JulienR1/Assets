using UnityEngine;

public class Player : Entity
{
    private Vector3 moveInput;

    protected override void Update()
    {
        GetUserInput();
        base.Update();
    }

    private void GetUserInput()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection = moveInput.normalized;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            Dash();
    }

}
