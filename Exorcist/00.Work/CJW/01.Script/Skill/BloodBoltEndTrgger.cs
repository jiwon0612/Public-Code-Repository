using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBoltEndTrgger : MonoBehaviour
{
    private BloodBolt _bolt;

    private void Awake()
    {
        _bolt = transform.parent.GetComponent<BloodBolt>();
    }

    public void AttackTrgger()
    {
        _bolt.Attack();
    }

    public void EndTrgger()
    {
        _bolt.EndAniamation();
    }
    
    public void PlaySound() => _bolt.PlaySound();
}
