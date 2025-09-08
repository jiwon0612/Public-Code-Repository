using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClearedUI : UIToolkitParents
{
    [SerializeField] Health bossHealth;
    private Label _clearedLabel;
    protected override void OnEnable()
    {
        base.OnEnable();
        _clearedLabel = Root.Q<Label>("ClearedLabel");

        if(bossHealth != null)
        {
            bossHealth.OnDead += (a) => ShowClearedUI("레드 데빌");
        }
    }
    public void ShowClearedUI(string bossName)
    {
        _clearedLabel.text = $"{bossName} 격파";
        _clearedLabel.AddToClassList("show");
        
        StartCoroutine(WaitAndHide());
    }
    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(5f);
        _clearedLabel.RemoveFromClassList("show");
    }
}
