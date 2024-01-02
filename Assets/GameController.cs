using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Hand;
    public GameObject HandCPU1;
    public GameObject HandCPU2;
    public GameObject HandCPU3;
    GameObject[] hands;
    public Material InitialMaterial;
    public PlayingCards Playingcards;
    Material[] materials;
    Material[,] decks;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new(Guid.NewGuid().GetHashCode());
        decks = new Material[40, 4];
        Hand.SetActive(false);
        HandCPU1.SetActive(false);
        HandCPU2.SetActive(false);
        HandCPU3.SetActive(false);
        materials = Resources.LoadAll<Material>("BackColor_Red");
        hands = new GameObject[]{Hand, HandCPU1, HandCPU2, HandCPU3 };
        Playingcards.Eventhandler += OnHandEnabled;
        System.Collections.Generic.List<Material> list;
        for (int i = 0; i < decks.GetLength(1); i++)
        {
            list = new();
            list.AddRange(Resources.LoadAll<Material>("BackColor_Red"));
            for(int j = 0; j < decks.GetLength(0); j++)
                decks[j, i] = list[rnd.Next(0, 40-j)];
        }
    }

    private void OnHandEnabled(object sender, EventArgs e)
    {
        foreach (GameObject hand in hands)
        {
            System.Random rnd = new(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < hand.transform.childCount; i++)
            {
                Material mat = materials[rnd.Next(0, materials.Length)];
                hand.transform.GetChild(i).GetComponent<MeshRenderer>().material = mat;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}