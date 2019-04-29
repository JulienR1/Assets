 using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    public Camera myCamera;
    public float mouseSensitivity;

    public LayerMask lookMask;

    [Header("Animations")]
    public float normalFOV = 60;
    public float dashFOV = 70;
    public float accelerationPercent = 0.05f;
    private float startFOVTime, startMaxFOVTime, endMaxFOVTime, endFOVTime;

    private Vector3 moveInput;
    private Vector2 mouseInput;
    private Vector2 mouseVelocity;

    private float xAxisClamp;

    private ShopManager shop;
    private int currency;

    private Enemy currentEnemy;

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

    protected override void LookInFront()
    {
        RaycastHit hit;
        Ray ray = new Ray(myCamera.transform.position, myCamera.transform.forward);
        if(Physics.Raycast(ray,out hit))
        {
            switch (hit.transform.gameObject.tag)
            {
                case "Shop":
                    shop = hit.transform.GetComponent<ShopManager>();
                    shop.inContact(transform.position);
                    break;
                case "Enemy":
                    currentEnemy = hit.transform.GetComponent<Enemy>();
                    break;
            }
        }
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

        myCamera.transform.Rotate(Vector3.left * mouseVelocity.y);
        transform.Rotate(Vector3.up * mouseVelocity.x);
    }

    private void ClampXAxisRotation(float value)
    {
        Vector3 eulerRotation = myCamera.transform.eulerAngles;
        eulerRotation.x = value;
        myCamera.transform.eulerAngles = eulerRotation;
    }

    protected override void Animate()
    {
        base.Animate();
        if (controller.GetIsDashing())
        {
            if (endFOVTime != controller.GetDashEndTime())
            {
                endFOVTime = controller.GetDashEndTime();
                startFOVTime = endFOVTime - stats.dashTime;
                startMaxFOVTime = endFOVTime - (1 - accelerationPercent) * stats.dashTime;
                endMaxFOVTime = endFOVTime - accelerationPercent * stats.dashTime;
            }
            if (Time.time < startMaxFOVTime)
                myCamera.fieldOfView = Mathf.Lerp(normalFOV, dashFOV, (Time.time - startFOVTime) / (startMaxFOVTime - startFOVTime));
            else if (Time.time > endMaxFOVTime)            
                myCamera.fieldOfView = Mathf.Lerp(dashFOV, normalFOV, (Time.time - endMaxFOVTime) / (endFOVTime - endMaxFOVTime));
            else
                myCamera.fieldOfView = dashFOV;
        }
        else
            myCamera.fieldOfView = normalFOV;
    }

    public int GetCurrency()
    {
        return currency;
    }

    public bool AddCurrency(int amount)
    {
        if (amount < 0)
            return false;
        currency += amount;
        return true;
    }

    public bool RemoveCurrency(int amount)
    {
        if (amount < 0)
            return false;
        if (amount > currency)
            return false;
        currency -= amount;
        return true;
    }

    public bool verifDoublons(Weapon item) {

        for(int i = 0; i < this.weapons.Count; i++) {
            if(item == this.weapons[i]) {
                return true;
            }
        }
        return false;
    }

    public void AddWeapon(Weapon item) {

        this.weapons.Add(item);

    }

    public override void Die()
    {
        FamePoints.MortFame();
        SceneManager.LoadScene("MenuManager", LoadSceneMode.Additive);
        base.Die();
       
    }

}
