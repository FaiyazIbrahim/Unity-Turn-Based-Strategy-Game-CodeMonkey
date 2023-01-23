using UnityEngine;

namespace Script
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField] private GameObject m_BulletProjectile;
        [SerializeField] private Transform m_ShootPoint;
        

        private MoveAction _moveAction;
        private ShootAction _shootAction;


        private void Awake()
        {
            if(TryGetComponent<MoveAction>(out MoveAction moveAction))
            {
                _moveAction = moveAction;
            }

            if(TryGetComponent<ShootAction>(out ShootAction shootAction))
            {
                _shootAction = shootAction;
            }
        }

        private void Start()
        {
            _moveAction.OnStatMoving += OnStartMoving;
            _moveAction.OnStopMoving += OnStopMoving;

            _shootAction.OnShoot += OnShoot;
        }


        private void OnStartMoving()
        {
            // start walking animation 
        }

        private void OnStopMoving()
        {
            //walking animation stops
        }

        private void OnShoot(Unit targetUnit)
        {
            // play shoot animation

            GameObject bulletProjectile = Instantiate(m_BulletProjectile, m_ShootPoint.position, Quaternion.identity);
            BulletProjectile bullet = bulletProjectile.GetComponent<BulletProjectile>();

            Vector3 targetUnitShootposition = targetUnit.transform.position;
            targetUnitShootposition.y = m_ShootPoint.position.y;
            bullet.SetUp(targetUnitShootposition);
        }
    }
}

