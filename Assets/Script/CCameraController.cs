using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraController : MonoBehaviour
{
    #region imspector
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _targetTr;
    [SerializeField] private float _smoothTime = 0.25f;
    #endregion

    #region private var
    private Vector3 _velocity;
    #endregion

    private void Awake()
    {
        if (_camera == null)
        {
            if(Camera.main != null)
            {
                _camera = Camera.main;
            }

            else
            {
                Debug.LogWarning("Missing Camra");
                enabled = false;
                return;
            }

        }
    }

    private void LateUpdate()
    {
        if(_targetTr == null)
        {
            return;
        }

        Vector3 desired = _targetTr.position;
        desired.z = _camera.transform.position.z;

        transform.position = Vector3.SmoothDamp
            (
                _camera.transform.position,                 
                desired,                            
                ref _velocity,                      
                Mathf.Max(0.0001f, _smoothTime)     
            );

        //_camera.transform.position = new Vector3(_targetTr.position.x, _targetTr.position.y, _camera.transform.position.z);
    }
}
