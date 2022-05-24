using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    Vector2 viewInput;

    [SerializeField] private Transform leftRotate;
    [SerializeField] private Transform rightRotate;

    //Other components
    NetworkCharacterControllerCustom networkCharacterControllerPrototypeCustom;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerCustom>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void FixedUpdateNetwork()
    {
        //Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            //Rotate the view
            //TO DO: ROTATE WEAPON
            //networkCharacterControllerPrototypeCustom.Rotate(networkInputData.rotationInput);

            //Move
            //Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            Vector3 moveDirection = networkInputData.movementInput;
            moveDirection.Normalize();

            if (networkInputData.movementInput.x != 0)
            {
                Debug.Log(networkInputData.movementInput.x);
                if (networkInputData.movementInput.x < 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
                else if (networkInputData.movementInput.x > 0)
                {
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                }
            }



            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            //Jump
            if (networkInputData.isJumpPressed)
                networkCharacterControllerPrototypeCustom.Jump();
        }
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }
}
