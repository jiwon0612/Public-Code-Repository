using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHPUI : UIToolkitParents
{
    private Health _playerHealth;

    private float _hpValue;
    private float _prevHp;

    private float HpValue
    {
        get { return _hpValue; }
        set
        {
            if (!Mathf.Approximately(value, _prevHp))
            {
                OnHpUIChange?.Invoke(value / _playerHealth.MaxHealth * 100);
                _prevHp = _hpValue;
                _hpValue = value;
                _hpProgressBar.value = _hpValue / _playerHealth.MaxHealth * 100;
            }
        }
    }

    private ProgressBar _hpProgressBar;

    public Action<float> OnHpUIChange;

    protected override void OnEnable()
    {
        base.OnEnable();
        _playerHealth = GameObject.Find("Player").GetComponent<Health>();
        _hpProgressBar = Root.Q<ProgressBar>("HPProgressBar");
    }

    private void Update()
    {
        HpValue = _playerHealth.GetCurrentHealth();
    }

    // [SerializeField] private UITester _tester;
    //
    // private float _hpValue;
    //
    // private float HpValue
    // {
    //     get { return _hpValue; }
    //     set
    //     {
    //         OnHpUIChange?.Invoke(value / _testMaxHealth * 100);
    //         _hpValue = value;
    //     }
    // }
    //
    // private float _testMaxHealth = 100;
    //
    // private ProgressBar _hpProgressBar;
    //
    // public Action<float> OnHpUIChange;
    //
    // protected override void OnEnable()
    // {
    //     base.OnEnable();
    //
    //     _hpProgressBar = Root.Q<ProgressBar>("HPProgressBar");
    //     _tester.OnTestHealthChange += UIHealthChange;
    //
    // }
    //
    // private void OnDisable()
    // {
    //     _tester.OnTestHealthChange -= UIHealthChange;
    // }
    //
    // private void UIHealthChange(float health)
    // {
    //     HpValue = 100 * (health / _testMaxHealth);
    //     _hpProgressBar.value = HpValue;
    // }
}