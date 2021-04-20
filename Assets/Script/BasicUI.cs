using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{

    private void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 30;
        int buffer = 10;

        List<string> itemList = Managers.Inventory.GetItemList();
        if (itemList.Count == 0)  //Отображаем сообщение информирующее об отсуствии инвенторя.
        {
            GUI.Box(new Rect(posX, posY, width, height), "No, Items");
        }
        foreach (string item in itemList) 
        {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item); //Метод, загружающий ресурсы из папки Resources.
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));
            posX += width + buffer; //При каждом прохождении цикла сдвигаемся в сторону.
        }

        string equipped = Managers.Inventory.equippedItem;
        if (equipped != null) //Отображаем подготовленный элемент.
        {
            posX = Screen.width - (width - buffer);
            Texture2D image = Resources.Load("Icons/" + equipped) as Texture2D;
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
        }

        posX = 10;
        posY += height + buffer;

        foreach (string item in itemList) //Просматриваем все элементы в цикле для создания кнопок.
        {
            if (item != "health")
            {
                if (GUI.Button(new Rect(posX, posY, width, height), "Equip " + item))   //Запускаем вложенный код при щелчке на кнопке.
                {
                    Managers.Inventory.EquipItem(item);
                }
            }
            if (item == "health") 
            {
                if (GUI.Button(new Rect(posX, posY, width, height), "Use Health")) //Запускаем вложенный код при щелчке на кнопке.
                {
                    Managers.Inventory.ConsumeItem("health");
                    Managers.Player.ChangeHealth(25);
                }
            }
            posX += width + buffer;
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
