using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnPoint 
    {
       public Transform point1;
       public Transform point2;
    }

    public static Action OnPressF = delegate { };

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private List<SpawnPoint> points = new List<SpawnPoint>();

    private GameObject _player1;
    private GameObject _player2;

    private int _spawnPoint;

    private void Awake()
    {
        if (_player1 == null || _player2 == null)
        {
            _player1 = Instantiate(player1) as GameObject;
            _player2 = Instantiate(player2) as GameObject;

            SwitchPlayers.players.Add(_player1);
            SwitchPlayers.players.Add(_player2);
        }

        if (points == null || points.Count < 1) return;

        int i = UnityEngine.Random.Range(0, points.Count);

        player1.transform.position = points[i].point1.position;
        player2.transform.position = points[i].point2.position;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F)) 
        {
            Debug.Log("Press F");
            OnPressF();
        }
    }

}
