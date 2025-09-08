using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private Player _player;
    
    
    public Player Player => _player;
    public Transform PlayerTrm => _player.transform;

    [SerializeField] private InputReader _input;

    public void InputOff() => _input.SetEnable(false);

    public void InputOn() => _input.SetEnable(true);
}
