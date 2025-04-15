using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIListElement<T>
{
    private readonly UIListElementView _view;
    private T _data;

    public UIListElement(UIListElementView view, T data)
    {
        _view = view;
        _data = data;
    }
}