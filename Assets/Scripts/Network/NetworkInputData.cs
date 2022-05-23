using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON1 = 0x01;

    public byte buttons;
    public Vector2 movementInput;
    public NetworkBool isJumpPressed;
}
