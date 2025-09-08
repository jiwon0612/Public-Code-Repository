using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public bool IsDead { get; private set; }
        public bool IsCanHit { get; set; }
        
        protected Dictionary<Type, IEntityCompo> _compos;

        protected virtual void Awake()
        {
            _compos = new Dictionary<Type, IEntityCompo>();
            
            AddComponent();
            InitCompo();
            AfterInitCompo();
            
            IsDead = false;
        }

        protected virtual void AddComponent()
        {
            GetComponentsInChildren<IEntityCompo>().ToList().ForEach(compo => 
                _compos.Add(compo.GetType(), compo));
        }

        protected virtual void InitCompo()
        {
            _compos.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        protected virtual void AfterInitCompo()
        {
            _compos.Values.OfType<IAfterInit>().ToList().ForEach(compo =>
                compo.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : IEntityCompo
        {
            if (_compos.TryGetValue(typeof(T), out var compo))
                return (T)compo;
            
            if (!isDerived) return default(T);

            Type findType = _compos.Keys.FirstOrDefault(type =>
                type.IsSubclassOf(typeof(T)));
            
            if (findType != null)
                return (T)_compos[findType];
            
            return default;
        }

        public virtual void Dead()
        {
            IsDead = true;
        }
    }
}