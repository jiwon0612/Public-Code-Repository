using System;
using Code.Entities;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class SpeedUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private EntityMover mover;
        
        private void Update()
        {
            text.text = $"{mover.Velocity} m/s";
        }
    }
}