using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class DoubleNode
{
    private double data; // it's variable keeps up info about round's time
    private DoubleNode next;
    private DoubleNode prev;

    public double Data
    {
        get { return data; }
        set { data = value; }
    }

    public DoubleNode Next
    {
        get { return next; }
        set { next = value; }
    }

    public DoubleNode Prev
    {
        get { return prev; }
        set { prev = value; }
    }

    public DoubleNode(double _data)
    {
        data = _data;
    }

    public DoubleNode()
    {

    }



}
