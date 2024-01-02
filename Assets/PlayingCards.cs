using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayingCards : MonoBehaviour
{
    public event EventHandler Eventhandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        OnActivated(new EventArgs());
    }

    public void OnActivated(EventArgs args)
    {
        EventHandler handler = Eventhandler;
        handler?.Invoke(transform, args);
    }
}
