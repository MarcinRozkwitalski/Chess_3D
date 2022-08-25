using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraAroundObject : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 3.0f;

    public Vector3 targetObjectNextPosition;
    float _time;
    private float _rotationX = 20f;
    private float _rotationY;

    RaycastHit hit;
    
    [SerializeField] private Transform _target;

    [SerializeField] private float _distanceFromTarget = 8.0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        if(Input.GetMouseButtonDown(2))
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100f));
            Vector3 direction = worldMousePosition - Camera.main.transform.position;

            if(Physics.Raycast (Camera.main.transform.position, direction, out hit, 100f))
            {
                targetObjectNextPosition = new Vector3(hit.point.x, 0.11f, hit.point.z);
            }
        }

        _target.transform.position = Vector3.MoveTowards(_target.transform.position, targetObjectNextPosition, 10f * Time.deltaTime);

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