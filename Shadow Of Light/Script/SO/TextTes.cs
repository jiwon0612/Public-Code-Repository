using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextTes : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Stat _stat;
    [SerializeField] private PlayerMove _move;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        _text.SetText($"{inputReader.Console.ToString()} : {_stat.moveSpeed} : {_move.isCanMove}");

    }
}
