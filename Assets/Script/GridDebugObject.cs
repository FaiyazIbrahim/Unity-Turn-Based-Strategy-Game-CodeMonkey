using System;
using UnityEngine;
using TMPro;

namespace Script
{
    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] private TextMeshPro m_Text;
        private GridObject _gridObject;
        
        public void SetDebugObject(GridObject gridObject)
        {
            _gridObject = gridObject;
        }

        private void Update()
        {
            m_Text.text = _gridObject.ToString();
        }
    }
}