using UnityEngine;
using UnityEngine.Events;
using System;

public class InputManager : MonoSingleton<InputManager>
{
    public InputReader inputR;
    [SerializeField] private PlayerManager playerM;
    [field : SerializeField] public GameObject player { get; private set; }
    public GameObject mainCam;
    public GameObject cam1;
    [SerializeField] private ShadowPotar potar;
    private PlayerMove movePlayer;
    private PlayerRenderer rndererPlayer;


    public ITool useTool;

    public Action onInput;

    private void Awake()
    {
        movePlayer = player.transform.GetComponent<PlayerMove>();
        rndererPlayer = player.transform.GetChild(0).GetComponent<PlayerRenderer>();

        inputR.onInputJump += movePlayer.PlayerJump;
        inputR.onTransMap += playerM.StartChangeMap;
        onInput += InputM;

    }

    
    private void Update()
    {
        onInput?.Invoke();
    }

    private void OnDisable()
    {
        inputR.onInputJump -= movePlayer.PlayerJump;
        inputR.onTransMap -= playerM.StartChangeMap;
        onInput = null;
        inputR.onChangeTool = null;
        inputR.onInteraction = null;
        inputR.onUseTool = null;
    }

    public void InputM()
    {
        movePlayer.PlayerMovemante(inputR.inputX);
        rndererPlayer.PlayerFlip(inputR.inputX);
        movePlayer.PlayerUpDown(inputR.inputY);
        movePlayer.WallSliding();

    }
}
