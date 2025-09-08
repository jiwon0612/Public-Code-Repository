using UnityEngine;

namespace Code.Entities
{
    public class EntityAnimator : MonoBehaviour, IEntityCompo
    {
        private Animator _animator;
        public void Initialize(Entity entity)
        {
            _animator = GetComponent<Animator>();
        }
        
        public void SetParam(int hash, float value) => _animator.SetFloat(hash, value);
        public void SetParam(int hash, int value) => _animator.SetInteger(hash, value);
        public void SetParam(int hash, bool value) => _animator.SetBool(hash, value);
        public void SetParam(int hash) => _animator.SetTrigger(hash);
    }
}