using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextClickable : MonoBehaviour, IPointerClickHandler
{

    public GameObject Hand;
    public GameObject HandCPU1;
    public GameObject HandCPU2;
    public GameObject HandCPU3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TMP_Text textComponent;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Perform an action when the Text object is clicked
        Hand.SetActive(true);
        HandCPU1.SetActive(true);
        HandCPU2.SetActive(true);
        HandCPU3.SetActive(true);
        Destroy(textComponent);
    }
}
