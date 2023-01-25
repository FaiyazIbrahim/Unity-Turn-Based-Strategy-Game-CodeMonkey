using UnityEngine;
using System;


namespace Script
{
    public class HealthSystem : MonoBehaviour
    {
        public event Action OnDeath;
        public event Action<float> OnDamage;

        [SerializeField] private float m_Health;
        private float m_HealthMax = 100f;

        private void Awake()
        {
            m_Health = m_HealthMax;
            
        }


        private void Start()
        {
            OnDamage?.Invoke(GetHealthNormalized());
        }


        public void Damage(float damageAmount)
        {
            m_Health -= damageAmount;

            OnDamage?.Invoke(GetHealthNormalized());

            if (m_Health <= 0)
            {
                m_Health = 0;
                Die();
            }
        }


        private void Die()
        {
            OnDeath?.Invoke();
        }

        public float GetHealthNormalized()
        {
            return m_Health / m_HealthMax;
        }
    }
}
