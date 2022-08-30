using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraAroundObject : MonoBehaviour
{
    GameHandler gameHandler;

    [SerializeField] private float _mouseSensitivity = 3.0f;

    public Vector3 targetObjectNextPosition;
    float _time;
    public bool _moveTarget = false;
    public float _rotationX = 20f;
    public float _rotationY;
    public float mouseX;
    public float mouseY;

    RaycastHit hit;
    
    [SerializeField] public Transform _target;

    [SerializeField] private float _distanceFromTarget = 8.0f;

    void Start() 
    {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        if(gameHandler._gameHasBeenStarted)
        {
            if(Input.GetMouseButtonDown(2) && gameHandler._gameHasBeenStarted)
            {
                Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100f));
                Vector3 direction = worldMousePosition - Camera.main.transform.position;

                if(Physics.Raycast (Camera.main.transform.position, direction, out hit, 100f))
                {
                    targetObjectNextPosition = new Vector3(hit.point.x, 1f, hit.point.z);
                    _moveTarget = true;
                }
            }

            if(_moveTarget && gameHandler._gameHasBeenStarted)
            {
                _target.transform.position = Vector3.MoveTowards(_target.transform.position, targetObjectNextPosition, 10f * Time.deltaTime);
                if(_target.transform.position == targetObjectNextPosition)
                {
                    _moveTarget = false;
                }
            }

            if(Input.GetMouseButton(1) && gameHandler._gameHasBeenStarted)
            {
                _rotationX -= mouseY;
                _rotationY += mouseX;

                _rotationX = Mathf.Clamp(_rotationX, 5, 89);

                transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
            }

            if(Input.GetAxisRaw("Mouse ScrollWheel") > 0 && gameHandler._gameHasBeenStarted)
            {
                _distanceFromTarget--;
                if(_distanceFromTarget < 4.0f) _distanceFromTarget = 4.0f;
            }

            if(Input.GetAxisRaw("Mouse ScrollWheel") < 0 && gameHandler._gameHasBeenStarted)
            {
                _distanceFromTarget++;
                if(_distanceFromTarget > 12.0f) _distanceFromTarget = 12.0f;
            }

            transform.position = _target.position - transform.forward * _distanceFromTarget;
        }
        else if(!gameHandler._gameHasBeenStarted)
        {
            float targetValue = 18f;
            float targetValue2 = 40f;

            if(_moveTarget)
            {
                _target.transform.position = Vector3.MoveTowards(_target.transform.position, targetObjectNextPosition, 2f * Time.deltaTime);
                transform.LookAt(_target);
                transform.Translate(Vector3.right * 2 * Time.deltaTime);
                transform.position = _target.position - transform.forward * _distanceFromTarget;
                if(_target.transform.position == targetObjectNextPosition)
                {
                    _moveTarget = false;
                }
            }
            else
            {
                if(_distanceFromTarget <= targetValue)
                {
                    _distanceFromTarget += 3 * Time.deltaTime;
                    transform.position = _target.position - transform.forward * _distanceFromTarget;
                }

                if(_rotationX <= targetValue2 - 0.5f)
                {
                    _rotationX += 30 * Time.deltaTime;
                    _rotationX = Mathf.Clamp(_rotationX, 5, 89);
                    transform.localEulerAngles = new Vector3(_rotationX, transform.rotation.y, 0);
                }
                else if(_rotationX >= targetValue2 + 0.5f)
                {
                    _rotationX -= 30 * Time.deltaTime;
                    _rotationX = Mathf.Clamp(_rotationX, 5, 89);
                    transform.localEulerAngles = new Vector3(_rotationX, transform.rotation.y, 0);
                }
                else
                {
                    transform.LookAt(_target);
                    transform.Translate(Vector3.right * 2f * Time.deltaTime);
                }
            }
        }
    }
}