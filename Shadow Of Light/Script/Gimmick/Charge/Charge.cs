using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Charge : MonoBehaviour
{
    public List<GameObject> cable = new List<GameObject>();

    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private float interactionRadius;
    [SerializeField] private int maxEnerge;
    private int currentEnerge;
    private bool isCanInteraction = false;
    private bool isInInterRadius = false;

    private Charge charger;

    private Energe player;

    private SpriteRenderer _e;

    private float playerAndMeDistance;

    private void Awake()
    {
        Initalize();
    }

    private void Initalize()
    {
        player = InputManager.Instance.player.GetComponent<Energe>();
        currentEnerge = maxEnerge;
        charger = GetComponent<Charge>();
        _e = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _e.DOFade(0, 0);
    }

    private void Update()
    {
        Interaction();
    }

    private void Interaction()
    {
        isInInterRadius = Physics2D.OverlapCircle(transform.position, interactionRadius, playerLayer);

        if (isInInterRadius)
        {
            if (!isCanInteraction)
            {
                InputManager.Instance.inputR.onInteraction += GiveEnerge;
                isCanInteraction = true;
                _e.DOFade(1, 0.5f);
            }
        }

        if (!isInInterRadius)
        {
            if (isCanInteraction)
            {
                InputManager.Instance.inputR.onInteraction -= GiveEnerge;
                isCanInteraction = false;
                _e.DOFade(0, 0.5f);
            }

        }

    }

    public void GiveEnerge()
    {

        if (currentEnerge - 1 < 0)
        {
            FailGetEnerge();
            return;
        }
        currentEnerge--;
        player.GetEnerge(charger);
    }

    public void GetEnerge()
    {
        currentEnerge++;
    }

    private void FailGetEnerge()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
        Gizmos.color = Color.white;
    }
}
