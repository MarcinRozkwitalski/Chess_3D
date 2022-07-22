using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundObject : MonoBehaviour
{
    [SerializeField]
    private float _mouseSensitivity = 3.0f;

    private float _rotationX = 20f;
    private float _rotationY;
    
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _distanceFromTarget = 8.0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        if(Input.GetMouseButton(1))
        {
            _rotationX -= mouseY;
            _rotationY += mouseX;

            _rotationX = Mathf.Clamp(_rotationX, 5, 89);

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            _distanceFromTarget--;
            if(_distanceFromTarget < 4.0f) _distanceFromTarget = 4.0f;
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            _distanceFromTarget++;
            if(_distanceFromTarget > 12.0f) _distanceFromTarget = 12.0f;
        }

        transform.position = _target.position - transform.forward * _distanceFromTarget;
    }
}