using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Script
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_ActionCameraGameObject;

        private void Start()
        {
            BaseAction.OnActionStarted += OnActionStarted;
            BaseAction.OnActionCompleted += OnActionCompleted;

            HideActionCamera();
        }

        private void ShowActionCamera()
        {
            m_ActionCameraGameObject.SetActive(true);
        }

        private void HideActionCamera()
        {
            m_ActionCameraGameObject.SetActive(false);
        }

        private void OnActionStarted(object sender, EventArgs e)
        {
            switch(sender)
            {
                case ShootAction shootAction:

                    Unit shooterUnit = shootAction.GetUnit();
                    Unit targetUnit = shootAction.GetTargetunit();

                    Vector3 cameraCharacterHeight = Vector3.up * 3f;
                    Vector3 shootDir = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;
                    Vector3 shoulderOffset = Quaternion.Euler(0, 90, 0) * shootDir * 1f; // x offset
                    Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharacterHeight + shoulderOffset + (shootDir * -1);


                    m_ActionCameraGameObject.transform.position = actionCameraPosition;
                    m_ActionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
                    ShowActionCamera();
                    break;
            }
        }

        private void OnActionCompleted(object sender, EventArgs e)
        {
            switch (sender)
            {
                case ShootAction:
                    HideActionCamera();
                    break;
            }
        }

    }
}

