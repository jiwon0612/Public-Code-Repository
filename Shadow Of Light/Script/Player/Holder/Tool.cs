using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Tool : MonoBehaviour
{
    public bool isUse = false;

    public UnityEvent OnFireEvent;
    public UnityEvent OnFalseFireEvent;

    protected Vector3 dir;

    protected Energe playerEnerge;

    protected virtual void Awake()
    {
        playerEnerge = transform.parent.transform.parent.GetComponent<Energe>();
    }

    public virtual void EnterTool()
    {
        InputManager.Instance.inputR.onUseTool += UseTool;
        OnFireEvent.AddListener(Fire);

    }

    public void UseTool()
    {
        if (playerEnerge.currentEnergy <= 0)
        {
            OnFalseFireEvent?.Invoke();
        }
        if (playerEnerge.currentEnergy > 0)
        {
            Vector3 dir3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dir3.z = 0;
            dir = dir3.normalized;
            OnFireEvent?.Invoke();
        }
    }

    public virtual void Fire()
    {
        playerEnerge.UseEnerge();
    }

    public virtual void ExitTool()
    {
        InputManager.Instance.inputR.onUseTool -= UseTool;
        OnFireEvent.RemoveListener(Fire);
    }
}
