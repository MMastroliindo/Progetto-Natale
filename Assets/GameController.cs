using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.SearchService;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

public class GameController : MonoBehaviour
{
    public GameObject Cards;
    public Material InitialMaterial;
    public PlayingCards Playingcards;
    string path;
    Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        Cards.SetActive(false);
        materials = Resources.LoadAll<Material>("BackColor_Red");
        Playingcards.Eventhandler += OnCardsEnabled;
    }

    private void OnCardsEnabled(object sender, EventArgs e)
    {
        System.Random rnd = new(Guid.NewGuid().GetHashCode());
        for (int i = 0; i < Cards.transform.childCount; i++)
        {
            Material mat = materials[rnd.Next(0, materials.Length)];
            Cards.transform.GetChild(i).GetComponent<MeshRenderer>().material = mat;
            Debug.Log(mat);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}