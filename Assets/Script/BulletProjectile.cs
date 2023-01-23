using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField] private ParticleSystem m_HitParticle;
    private TrailRenderer _trailRenderer;
    private Vector3 _targetPosition;


    private void Awake()
    {
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }


    public void SetUp(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }


    private void Update()
    {
        Vector3 moveDir = (_targetPosition - transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, _targetPosition);

        transform.position += moveDir * 100f * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, _targetPosition);

        if (distanceBeforeMoving < distanceAfterMoving)
        {
            transform.position = _targetPosition;
            _trailRenderer.transform.parent = null;

            Instantiate(m_HitParticle, _targetPosition, Quaternion.identity);

            Destroy(gameObject);
            
        }
    }

}
