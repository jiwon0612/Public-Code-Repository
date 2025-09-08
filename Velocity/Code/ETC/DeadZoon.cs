using System;
using UnityEngine;

namespace Code.ETC
{
    public class DeadZoon : MonoBehaviour
    {
        [SerializeField] private Player.Player player;

        private void Update()
        {
            if (player.transform.position.y <= 1)
            {
                player.Dead();
            }
        }
    }
}