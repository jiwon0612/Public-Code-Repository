using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AucioBar : MonoBehaviour
{
    [SerializeField] private AudioDataSO data;
    private Scrollbar bar;

    private void Awake()
    {
        bar = GetComponent<Scrollbar>();
    }

    private void Start()
    {
        bar.value = data.Value;

    }
    public void OnValueCha()
    {
        data.Value = bar.value;
    }
}
