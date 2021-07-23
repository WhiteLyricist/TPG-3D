using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayers : MonoBehaviour
{
    public static List<Camera> camers = new List<Camera>();
    public static List<GameObject> players = new List<GameObject>();

    private RelativeMovement rmp1;
    private RelativeMovement rmp2;

    private bool _active;

    // Start is called before the first frame update
    void Start()
    {
        SceneController.OnPressF += PressF;

        rmp1 = players[0].GetComponent<RelativeMovement>();
        rmp1.enabled = true;
        rmp2 = players[1].GetComponent<RelativeMovement>();
        rmp2.enabled = false;

        if (camers[0].name == "Camera 1")
        {
            _active = true;
            camers[0] = Camera.main;
            camers[1].enabled = false;
        }
        else 
        {
            _active = false;
            camers[1] = Camera.main;
            camers[0].enabled = false;
        }
    }

    private void PressF()
    {
        if (_active == true)
        {
            Debug.Log("Camera 1");

            _active = false;

            camers[1].enabled = true;
            camers[0].enabled = false;
            camers[1] = Camera.main;

            rmp1.enabled = false;
            rmp2.enabled = true;
        }
        else 
        {
            Debug.Log("Camera 2");

            _active = true;

            camers[0].enabled = true;
            camers[1].enabled = false;
            camers[0] = Camera.main;

            rmp2.enabled = false;
            rmp1.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        SceneController.OnPressF -= PressF;
    }

}
