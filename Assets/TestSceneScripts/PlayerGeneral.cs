using Fusion;
using UnityEngine;

public class PlayerGeneral : NetworkBehaviour
{
    [SerializeField] protected Ball _prefabBall;

    [Networked] protected TickTimer delay { get; set; }

    protected NetworkCharacterController _cc;
    protected Vector3 _forward;

    [SerializeField] protected float velocity = 5f;

    protected void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();
        _forward = transform.forward;
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NewNetworkInputData data))
        {
            data.direction.Normalize();
            _cc.Move(velocity * data.direction * Runner.DeltaTime);

            if (data.direction.sqrMagnitude > 0)
                _forward = data.direction;

            if (delay.ExpiredOrNotRunning(Runner))
            {
                if ((data.buttons & NewNetworkInputData.MOUSEBUTTON1) != 0)
                {
                    delay = TickTimer.CreateFromSeconds(Runner, 0.5f);
                    Runner.Spawn(_prefabBall,
                    transform.position + _forward, Quaternion.LookRotation(_forward),
                    Object.InputAuthority, (runner, o) =>
                    {
                        o.GetComponent<Ball>().Init();
                    });
                }
            }

            if((data.keys & NewNetworkInputData.JUMPBUTTON) != 0)
            {
                Debug.Log("SALTANDO ");
            }
        }
    }
}
