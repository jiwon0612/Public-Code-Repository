using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatSO",menuName ="SO/Stat")]
public class Stat : ScriptableObject
{
    public float moveSpeed;
    public float JumpForce;
    public float clouchSpeed;
    public int energy;
    public bool isUseTool;
    
}
