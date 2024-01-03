using Assets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //5, 10, 20, 50
    public GameObject Hand;
    public GameObject HandCPU1;
    public GameObject HandCPU2;
    public GameObject HandCPU3;
    public Material InitialMaterial;
    public PlayingCards Playingcards;
    public GameObject Deck;
    public Material DeckMaterial;
    public GameObject TableCards;
    public GameObject Lowest;
    public GameObject Half;
    public GameObject Pot;
    public GameObject AllIn;
    GameObject[] hands;
    Material[] deck;
    List<Material> deckMaterials;
    int minBet = 25;
    int pot;
    int playerBet;
    int CPU1Bet;
    int CPU2Bet;
    int CPU3Bet;
    int[] playerMoney;
    int[] CPU1Money;
    int[] CPU2Money;
    int[] CPU3Money;
    // Start is called before the first frame update
    void Start()
    {
        playerMoney = new int[] {5, 4, 3, 2 };
        CPU1Money = new int[] { 5, 4, 3, 2 };
        CPU2Money = new int[] { 5, 4, 3, 2 };
        CPU3Money = new int[] { 5, 4, 3, 2 };
        Lowest.GetComponent<TextMeshPro>().text = "Bet " + minBet;
        Lowest.GetComponent<BetLowest>().Handler += BetLowest;
        Half.GetComponent<BetHalf>().Handler += BetHalf;
        Pot.GetComponent<BetPot>().Handler += BetPot;
        AllIn.GetComponent<BetAllIn>().Handler += BetAllIn;
        Lowest.SetActive(false);
        Half.SetActive(false);
        Pot.SetActive(false);
        AllIn.SetActive(false);
        System.Random rnd = new(Guid.NewGuid().GetHashCode());
        deck = new Material[40];
        Hand.SetActive(false);
        HandCPU1.SetActive(false);
        HandCPU2.SetActive(false);
        HandCPU3.SetActive(false);
        hands = new GameObject[]{Hand, HandCPU1, HandCPU2, HandCPU3 };
        Playingcards.Eventhandler += OnHandEnabled;
        List<Material> list;
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

    private void BetAllIn(object sender, IDEventArgs e)
    {
        int bet = 0;
        switch (e.ID)
        {
            case 0:
                playerBet = bet;
                Pay(bet, playerMoney);
                break;
            case 1:
                CPU1Bet = bet;
                Pay(minBet, CPU1Money);
                break;
            case 2:
                CPU2Bet = bet;
                Pay(minBet, CPU2Money);
                break;
            case 3:
                CPU3Bet = bet;
                Pay(minBet, CPU3Money);
                break;
        }
    }

    private void BetPot(object sender, IDEventArgs e)
    {
        int bet = 0;
        while (bet <= pot)
        {
            bet += 5;
        }
        bet -= 5;

        switch (e.ID)
        {
            case 0:
                playerBet = bet;
                Pay(bet, playerMoney);
                break;
            case 1:
                CPU1Bet = bet;
                Pay(minBet, CPU1Money);
                break;
            case 2:
                CPU2Bet = bet;
                Pay(minBet, CPU2Money);
                break;
            case 3:
                CPU3Bet = bet;
                Pay(minBet, CPU3Money);
                break;
        }
    }

    private void BetHalf(object sender, IDEventArgs e)
    {
        int bet = 0;
        while(bet <= pot / 2)
        {
            bet += 5;
        }
        bet -= 5;

        switch (e.ID)
        {
            case 0:
                playerBet = bet;
                Pay(bet, playerMoney);
                break;
            case 1:
                CPU1Bet = bet;
                Pay(minBet, CPU1Money);
                break;
            case 2:
                CPU2Bet = bet;
                Pay(minBet, CPU2Money);
                break;
            case 3:
                CPU3Bet = bet;
                Pay(minBet, CPU3Money);
                break;
        }
    }

    private void BetLowest(object sender, IDEventArgs e)
    {
        switch (e.ID)
        {
            case 0:
                playerBet = minBet;
                Pay(minBet, playerMoney);
                break;
            case 1:
                CPU1Bet = minBet;
                Pay(minBet, CPU1Money);
                break;
            case 2:
                CPU2Bet = minBet;
                Pay(minBet, CPU2Money);
                break;
            case 3:
                CPU3Bet = minBet;
                Pay(minBet, CPU3Money);
                break;
        }
    }

    private void OnHandEnabled(object sender, EventArgs e)
    {
        Lowest.SetActive(true);
        Half.SetActive(true);
        Pot.SetActive(true);
        AllIn.SetActive(true);
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
        for (int i = 0; i < TableCards.transform.childCount; i++)
        {
            TableCards.transform.GetChild(i).GetComponent<MeshRenderer>().material = deckMaterials[0];
            deckMaterials.RemoveAt(0);
        }
        RevealCard(0);
    }


    void RevealCard(int index)
    {
        Transform card = TableCards.transform.GetChild(index);
        card.Rotate(0, 0, -180);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pay(int amount, int[] total)
    {
            while (total[3] < amount)
            {
                amount -= 50;
                total[3]--;
            }
            while (total[2]  < amount)
            {
                amount -= 20;
                total[2]--;
            }
            while (total[1] < amount)
            {
                amount -= 10;
                total[1]--;
            }
            while (total[0] < amount)
            {
                amount -= 5;
                total[0]--;
            }
    }

}