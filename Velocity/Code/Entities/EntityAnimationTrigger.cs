using System;
using UnityEngine;

namespace Code.Entities
{
    public class EntityAnimationTrigger : MonoBehaviour, IEntityCompo
    {
        public event Action OnAnimationEndTrigger;
        public event Action OnAnimationStartTrigger;
        
        public void Initialize(Entity entity)
        {
        }
        
        private void AnimationEnd() => OnAnimationEndTrigger?.Invoke();
        private void AnimationStart() => OnAnimationStartTrigger?.Invoke();
    }
}