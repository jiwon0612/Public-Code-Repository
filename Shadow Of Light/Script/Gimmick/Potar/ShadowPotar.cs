using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPotar : MonoBehaviour
{
    private Transform player;

    private Portal potar1;
    private Portal potar2;

    private void Awake()
    {
        player = InputManager.Instance.player.transform;

        potar1 = transform.GetChild(0).GetComponent<Portal>();
        potar2 = transform.GetChild(1).GetComponent<Portal>();
        
    }

    private void OnEnable()
    {
        InputManager.Instance.inputR.onInteraction += Teleport;
    }

    private void OnDisable()
    {
    }

    public void Teleport()
    {
        if (potar1.IsOnPlayer && !PlayerManager.Instance.isLight)
        {
            player.position = new Vector2(potar2.transform.position.x, potar2.transform.position.y + 0.5f);
        }
        if (potar2.IsOnPlayer && !PlayerManager.Instance.isLight)
        {
            player.position = new Vector2(potar1.transform.position.x, potar1.transform.position.y + 0.5f);
        }
    }
}
