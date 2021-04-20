using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))] //Убеждаемся что различные диспетчиры существуют.
[RequireComponent(typeof(InventoryManager))]

public class Managers : MonoBehaviour
{

    public static PlayerManager Player { get; private set; } //Статические свойства, котороми остльной код пользуется для доуступа к диспетчерам.
    public static InventoryManager Inventory { get; private set; }

    private List<IGameManager> _startSequence; //Список диспетчеров, который просматтривается в цикле во время стартовой последовательности.

    private void Awake()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Player);
        _startSequence.Add(Inventory);

        StartCoroutine(StartupManagers()); //Асинхронно загружаем стартовую последовательность.
    }

    private IEnumerator StartupManagers() 
    {
        foreach (IGameManager manager in _startSequence) {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModules) //Продолжаем цикл, пока не начнут работать все диспетчеры.
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequence) 
            {
                if (manager.status == ManagerStatus.Started) 
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
                Debug.Log("Progress: " + numReady + "/" + numModules);
            yield return null; //Остановка на один кадр перед следующей проверкой.
        }

        Debug.Log("All manager started up");
    }
}
