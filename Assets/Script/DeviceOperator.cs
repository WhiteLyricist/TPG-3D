using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    
    public float radius = 1.5f; //Расстояние, на котором становится возможнйо активация устройства.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3")) //Реакция на кнопку ввода, заданную в настройках ввода Unity;
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); //Метод OverlapShere() возвращает список ближайших объектов.
            foreach (Collider hitCollider in hitColliders) 
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0.5f) //Сообщение отправляется только при корректной ориентации персонажа.
                {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); //Метод SendMassage() пытается вызывать именнованную функцию независимо от типа целоевого объекта.
                }
            }
        }    
    }
}
