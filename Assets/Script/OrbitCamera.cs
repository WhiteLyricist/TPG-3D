using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target; //Сериализованная ссылка на объект, вокруг которого произодится облет.

    public float rotSpeed = 1.5f;
    private float _rotY;
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position; //Сохраняем наччальное смещение между камерой и целью.
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0)
        {
            _rotY += horInput * rotSpeed; //Медленный поворот камеры при помощи клавиш со стрелками.
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3; //Быстрый поворот при помощи мыгши.
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset); //Поддерживаем начальное смещение, сдвигаемое в соответсвии с поротом камеры.
        transform.LookAt(target); //Где бы ни находилась камеры, она всегда смотрит на цель.
    }
}
