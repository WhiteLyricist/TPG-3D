using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{

    [SerializeField] private Vector3 dPos; //Смещение открытой двери, относительно закрытой.

    private bool _open = false; //Переменная для слежения состояния двери.
    private bool _openTr = false; //Переменная для слежения состояния двери.

    public void Operate() 
    {
        if (_open) //Открываем или закрываем дверь в зависимости от её состояния.
        {
            Vector3 pos = transform.position - dPos;
            iTween.MoveTo(gameObject, iTween.Hash("y",pos.y,"time", 2.0f, "easetype",iTween.EaseType.easeInExpo));
        }
        else
        {
            Vector3 pos = transform.position + dPos;
            iTween.MoveTo(gameObject, iTween.Hash("y", pos.y, "time", 2.0f, "easetype", iTween.EaseType.easeInExpo));
        }
        _open = !_open;
    }

    public void Activate() //Открываем дверь если закрыта.
    {
        if (!_openTr) 
        {
            Vector3 pos = transform.position + dPos;
            iTween.MoveTo(gameObject, iTween.Hash("y", pos.y, "time", 2.0f, "easetype", iTween.EaseType.easeInExpo));
            _openTr = true;
        }
    }

    public void Deactivate() //Закрываем дверь если открыта.
    {
        if (_openTr)
        {
            Vector3 pos = transform.position - dPos;
            iTween.MoveTo(gameObject, iTween.Hash("y", pos.y, "time", 2.0f, "easetype", iTween.EaseType.easeInExpo));
            _openTr = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
