using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private List<GimmickETC> gimmicks = new List<GimmickETC>();
    private SpriteRenderer spriteR;
    private void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        spriteR.color = Color.black;
    }

    public void StartGimmick()
    {
        foreach (GimmickETC item in gimmicks)
        {
            item.OperationGimmick();
        }
        spriteR.color = new Color32(255,255,255,255);
    }
}
