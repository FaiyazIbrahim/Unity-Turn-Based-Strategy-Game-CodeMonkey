using System;
using UnityEngine;

namespace Script
{
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask m_GroundLayer;
        public static MouseWorld Instance;
        
        private Camera _camera;

        private void Awake()
        {
            Instance = this;
            _camera = Camera.main;
        }
        


        public static Vector3 GetHitPoint()
        {
            Ray ray = Instance._camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, Instance.m_GroundLayer);
            return hit.point;
        }
    }
}