using UnityEngine;
using System;


namespace Script
{
    public class HealthSystem : MonoBehaviour
    {
        public event Action OnDeath;

        [SerializeField] private float m_Health = 100f;

        public void Damage(float damageAmount)
        {
            m_Health -= damageAmount;

            if(m_Health <= 0)
            {
                m_Health = 0;
                Die();
            }
        }


        private void Die()
        {
            OnDeath?.Invoke();
        }
    }
}
