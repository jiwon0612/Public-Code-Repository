using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Energe : MonoBehaviour
{
    [SerializeField] private Stat data;
    private int MaxEnergy;

    public Action<int> OnGetEnergeEvent;
    public Action<int> OnUseEnergeEvent;

    [SerializeField] private EnergeBar uIBar;

    private Charge charger;

    public int currentEnergy = 0;

    private void Awake()
    {
        MaxEnergy = data.energy;
        ResetEnerge();
    }

    private void ResetEnerge()
    {
        currentEnergy = 0;
        OnGetEnergeEvent += uIBar.AddIcon;
        OnUseEnergeEvent += uIBar.UseIcon;
    }

    private void OnDestroy()
    {
        OnGetEnergeEvent -= uIBar.AddIcon;
        OnUseEnergeEvent -= uIBar.UseIcon;
    }

    public void GetEnerge(Charge value)
    {
        if(currentEnergy + 1 > MaxEnergy)
        {
            return;
        }
        charger = value;
        currentEnergy++;
        OnGetEnergeEvent?.Invoke(currentEnergy);
    }

    public void UseEnerge()
    {
        if(currentEnergy <= 0) return;
        currentEnergy--;
        charger.GetEnerge();
        OnUseEnergeEvent?.Invoke(currentEnergy);
    }

    
}
