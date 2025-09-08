using System;
using Code.Core.EventSystem;
using Code.Entities;
using Code.FSM;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Player
{
    public enum GroundVerdict
    {
        Bad,
        Ok,
        Good,
        Perfect
    }
    
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        [field: SerializeField] public GameEventChannelSO PlayerChannel { get; private set; }
        [field: SerializeField] public GameEventChannelSO SceneChannel { get; private set; }

        [field: SerializeField] public float Perfect { get; private set; }
        [field: SerializeField] public float Good { get; private set; }
        [field: SerializeField] public float Ok { get; private set; }
        //[field: SerializeField] public float Bad { get; private set; }
        
        public bool IsGroundRolling { get; set; }
        public bool IsAirRolling { get; set; }

        [SerializeField] private StateSO[] states;

        private EntityMover _mover;
        private PlayerHealth _health;

        private EntityStateMachine _stateMachine;
        
        [field: SerializeField] public AudioClip groundRandingSound { get; private set; }

        protected override void InitCompo()
        {
            base.InitCompo();
            _mover = GetCompo<EntityMover>();
            _health = GetCompo<PlayerHealth>();
            _stateMachine = new EntityStateMachine(this, states);
            
            _health.OnHitEvent.AddListener(HandleHitEvent);
            PlayerChannel.AddListener<BoostEvent>(HandleBoostEvent);
        }

        private void OnDestroy()
        {
            PlayerChannel.RemoveListener<BoostEvent>(HandleBoostEvent);
        }

        private void HandleBoostEvent(BoostEvent obj)
        {
            _mover.AddAcceleration(obj.value);
        }

        private void HandleHitEvent(float beforeHealth, float current)
        {
            if (beforeHealth > current)
            {
                _mover.SubtractionAcceleration(25f);
            }
        }

        private void Start()
        {
            _stateMachine.ChangeState("DASHCHARGE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string stateName)
        {
            _stateMachine.ChangeState(stateName);
        }

        public GroundVerdict CalculateGroundVerdict(float angle)
        {
            if (angle <= Perfect)
                return GroundVerdict.Perfect;
            else if (angle <= Good)
                return GroundVerdict.Good;
            else if (angle <= Ok)
                return GroundVerdict.Ok;
            else 
                return GroundVerdict.Bad;
        }

        public override void Dead()
        {
            base.Dead();
            SceneChannel.RaiseEvent(SceneEvents.SceneChange.Init(SceneManager.GetActiveScene().buildIndex));
        }
    }
}