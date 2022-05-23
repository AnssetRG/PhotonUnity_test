using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero;
    Vector3 mousePosition = Vector2.zero;
    bool isJumpButtonPressed = false;
    private Vector3 lookDir;

    [SerializeField] private Transform leftRotate;
    [SerializeField] private Transform rightRotate;

    private Camera cam;

    //Other components
    CharacterMovementHandler characterMovementHandler;
    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //View input
        //mousePosition.x = Input.GetAxis("Mouse X");
        //mousePosition.y = Input.GetAxis("Mouse Y") * -1; //Invert the mouse look

        AimRotation();

        //characterMovementHandler.SetViewInputVector(viewInputVector);

        //Move input
        moveInputVector.x = Input.GetAxis("Horizontal");
        //moveInputVector.y = Input.GetAxis("Vertical");

        if (moveInputVector.x != 0)
        {
            Debug.Log(moveInputVector.x);
            if (moveInputVector.x < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (moveInputVector.x > 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumpButtonPressed = true;
        }

        
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //View data
        //networkInputData.rotationInput = viewInputVector.x;

        //Move data
        networkInputData.movementInput = moveInputVector;

        //Jump data
        networkInputData.isJumpPressed = isJumpButtonPressed;

        isJumpButtonPressed = false;

        return networkInputData;
    }

    private void AimRotation()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        lookDir = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //float angle = Vector3.Angle(mousePosition, Vector3.right);
        Debug.Log(angle);
        Vector2 v2TP = transform.position;
        Vector2 v2MP = mousePosition;
        Debug.DrawRay(v2TP, v2MP * 50f, Color.blue);
        //transform.eulerAngles = new Vector3(0, 0, angle);

        /*if (Mathf.Abs(angle) < 85)
        {
            onRightTurn.Invoke();
        }
        else if (Mathf.Abs(angle) > 95)
        {
            onLeftTurn.Invoke();
        }*/
    }

}
