using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Hand;
    public GameObject HandCPU1;
    public GameObject HandCPU2;
    public GameObject HandCPU3;
    public Material InitialMaterial;
    public PlayingCards Playingcards;
    public GameObject Deck;
    public Material DeckMaterial;
    Material[] materials;
    GameObject[] hands;
    Material[] deck;
    List<Material> deckMaterials;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new(Guid.NewGuid().GetHashCode());
        deck = new Material[40];
        Hand.SetActive(false);
        HandCPU1.SetActive(false);
        HandCPU2.SetActive(false);
        HandCPU3.SetActive(false);
        materials = Resources.LoadAll<Material>("BackColor_Red");
        hands = new GameObject[]{Hand, HandCPU1, HandCPU2, HandCPU3 };
        Playingcards.Eventhandler += OnHandEnabled;
        System.Collections.Generic.List<Material> list;
        list = new();
        list.AddRange(Resources.LoadAll<Material>("BackColor_Red"));
        int index;
        for(int j = 0; j < deck.Length; j++)
        {
            index = rnd.Next(0, 40 - j);
            deck[j] = list[index];
            list.RemoveAt(index);
        }
        deckMaterials = deck.ToList();
    }

    private void OnHandEnabled(object sender, EventArgs e)
    {
        foreach (GameObject hand in hands)
        {
            System.Random rnd = new(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < hand.transform.childCount; i++)
            {
                hand.transform.GetChild(i).GetComponent<MeshRenderer>().material = deckMaterials[0];
                deckMaterials.RemoveAt(0);
            }
            Deck.GetComponent<MeshRenderer>().material = DeckMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}