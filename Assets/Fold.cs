using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fold : MonoBehaviour, IPointerClickHandler
{

    public event EventHandler<IDEventArgs> Handler;
    public void OnPointerClick(PointerEventData eventData)
    {
        EventHandler<IDEventArgs> handler = Handler;
        handler?.Invoke(this, new IDEventArgs(0));
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
