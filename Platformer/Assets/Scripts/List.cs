using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable] public class List
{
    public DoubleNode head;
    public int counter = 0; // счетчик узлов 


    public DoubleNode Head
    {
        get { return head; }
        set { value = head; }
    }

    public List()
    {
        head = new DoubleNode();
        head.Next = head;
        head.Prev = head;
    }

    /// <summary>
    /// Добавляем узел по возрастанию
    /// </summary>
    /// <param name="data"></param>
    public void Add(double data)
    {
        DoubleNode temp = new DoubleNode(data); // new node 
        DoubleNode p = head.Next;

        while (p != head && p.Data < data)
            p = p.Next;


        // если новое время меньше, чем какое-то другое
        if (counter < 3)
        {
            temp.Next = p;
            temp.Prev = p.Prev;
            p.Prev.Next = temp;
            p.Prev = temp;

            counter++;
        }
        else if (counter >= 2 && p != head)
        {
            temp.Next = p;
            temp.Prev = p.Prev;
            p.Prev.Next = temp;
            p.Prev = temp;

            counter++;
        }

    }

    /// <summary>
    /// Удаляем лишний узел
    /// </summary>
    public void DeleteLast()
    {
        if (counter > 3)
        {
            head.Prev.Prev.Next = head;
            head.Prev = head.Prev.Prev;
            counter--;
        }
    }

    /// <summary>
    /// Выводим строку
    /// </summary>
    /// <returns></returns>
    public string Display()
    {
        string ans = "";
        int i = 0;
        DoubleNode temp = head.Next;
        while (temp != head)
        {
            i++;
            ans += (i + ") " + Convert.ToString(temp.Data) + "\n");
            temp = temp.Next;
        }

        return ans;
    }

    

}
   
    // задачи на 17 августа:
    // 1) сортировать время;
    // 2) удалять 3+ записи 
    // 3) чтобы постоянно висело, а не появялось только в конце;
    // 4) написать функция округления числа до до;
    // доп. 5) придумать и оформить красиво в виде таблички

