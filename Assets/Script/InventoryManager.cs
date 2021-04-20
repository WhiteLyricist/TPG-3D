using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{

    private Dictionary<string,int> _item;
    public ManagerStatus status { get; private set; } //Свойство читается от куда угодно, но задается в этом сценарии.

    public string equippedItem { get; private set; }

    public void Startup() 
    {
        Debug.Log("Inventory manager starting..."); //Сюда иду все задачи запуска с долгим временем выполнения.

        _item = new Dictionary<string, int>(); //При объявлении словаря указываются два типа: тип ключа и тип значения.

        status = ManagerStatus.Started; //Для задач с долгим временем выполнения используем состояния 'Initializing'
    }

    private void DisplayItem() //Выводим на консоль сообщение о текущем инвентаре.
    {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string,int> item in _item) 
        {
            itemDisplay += item.Key + "(" + item.Value + ")";
        }
        Debug.Log(itemDisplay);
    }

    public void AddItem(string name) //Другие сценарии не могут напрямую управлять списком элементов, но могут вызывать этот метод.
    {
        if (_item.ContainsKey(name)) //Перед вводом новых данных проверяем, не существует ли уже такой записи.
        {
            _item[name] += 1;
        }
        else 
        {
            _item[name] = 1;
        }

        DisplayItem();
    }

    public List<string> GetItemList() //Возвращаем список всех ключей словаря.
    {
        List<string> list = new List<string>(_item.Keys);
        return list;
    }

    public int GetItemCount(string name) //Возвращаем кол-во указанных элементов в инвенторе.
    {
        if (_item.ContainsKey(name)) 
        {
            return _item[name];
        }
        return 0;
    }

    public bool ConsumeItem(string name) 
    {
        if (_item.ContainsKey(name)) //Проверяем, есть ли в инвентаре нужный элемент.
        {
            _item[name]--;
            if (_item[name] == 0) //Удаляем запись, если кол-во становится равным 0.
            {
                _item.Remove(name);
            }
        }
        else //Отвечаем, что в инвентаре нет нужэного эдемента.
        {
            Debug.Log("cannot consume " + name);
            return false;
        }
        DisplayItem();
        return true;
    }

    public bool EquipItem(string name) 
    {
        if (_item.ContainsKey(name) && equippedItem != name) //Проверяем, что в инвентаре есть указанный элемент, но он ещё не подготовлен.
        {
            equippedItem = name;
            Debug.Log("Equipped " + name);
            return true;
        }
        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

}
