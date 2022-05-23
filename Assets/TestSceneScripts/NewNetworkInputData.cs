using Fusion;
using UnityEngine;

public struct NewNetworkInputData : INetworkInput
{
    public const byte MOUSEBUTTON1 = 0x01;
    public const byte MOUSEBUTTON2 = 0x02;
    public const byte JUMPBUTTON = 0x03;

    public byte buttons;
    public byte keys;
    public Vector3 direction;
}