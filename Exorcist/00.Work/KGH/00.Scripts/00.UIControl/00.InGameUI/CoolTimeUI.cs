using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoolTimeUI : UIToolkitParents
{
    [SerializeField] private List<PlayerSkill> playerSkills;
    
    private List<RadialFillElement> _skillCoolTimeUIs;
    
    protected override void OnEnable()
    {
        base.OnEnable();
    
        _skillCoolTimeUIs = Root.Query<RadialFillElement>("radial-fill-element").ToList();
        
    }
    
    private void Update()
    {
        for(int i = 0 ; i < playerSkills.Count; i++)
        {
            if (playerSkills[i].IsCooldown)
            {
                _skillCoolTimeUIs[i].value = playerSkills[i]._cooldownTimer / playerSkills[i]._cooldown;
            }
            else
            {
                _skillCoolTimeUIs[i].value = 0;
            }
        }
    }
}
