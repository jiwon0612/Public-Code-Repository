using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossHPUI : UIToolkitParents
{
    [SerializeField] bool isRedDevil;

    private Enemy _boss;
    private Health _bossHealth;

    private float _bossHpValue;

    private float BossHpValue
    {
        get { return _bossHpValue; }
        set
        {
            OnBossHpUIChange?.Invoke(value);
            _bossHpValue = value;
        }
    }

    private ProgressBar _bossProgressBar;
    private VisualElement _bossHealthContainer;

    public Action<float> OnBossHpUIChange;
    private Label _bossTitle;

    protected override void OnEnable()
    {
        base.OnEnable();
        _bossProgressBar = Root.Q<ProgressBar>("BossHealthbar");
        _boss = FindObjectOfType<Enemy>();
        if (!_boss)
        {
            _bossHealthContainer = Root.Q<VisualElement>("BossHealthContainer");
            _bossHealthContainer.AddToClassList("disable");
            return;
        }

        _bossHealth = _boss.GetComponent<Health>();
        _bossTitle = Root.Q<Label>("BossTitle");
        _bossTitle.text = _boss.name;

    }

    private void Update()
    {
        if (!_boss) return;
        UIBossHealthChange();
    }

    private void UIBossHealthChange()
    {
        BossHpValue = 100 * ((float)_bossHealth.GetCurrentHealth() / _bossHealth.MaxHealth);
        _bossProgressBar.value = BossHpValue;
    }

    public void SetBoss(Enemy boss)
    {
        _boss = boss;
        _bossHealth = _boss.GetComponent<Health>();
        _bossTitle = Root.Q<Label>("BossTitle");
        _bossTitle.text = _boss.name;
        Debug.Log(_bossHealthContainer);
        _bossHealthContainer.RemoveFromClassList("disable");

        if (isRedDevil)
        {
            Debug.Log("asdasdasdasd");
            _bossHealth.OnDead += (a) => HideBossHealth();
        }
    }

    public void HideBossHealth() => _bossHealthContainer.AddToClassList("disable");
}