using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(string message) : base(message)
    {

    }
}

public class GameController : MonoBehaviour
{
    #region variables
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
    public GameObject OFold;
    public GameObject ErrorMessage;
    GameObject[] hands;
    Material[] deck;
    List<Material> deckMaterials;
    public GameObject CPU1Chips;
    public GameObject CPU2Chips;
    public GameObject CPU3Chips;
    public GameObject Player5s;
    public GameObject Player10s;
    public GameObject Player20s;
    public GameObject Player50s;
    public GameObject Chip5;
    public GameObject Chip10;
    public GameObject Chip20;
    public GameObject Chip50;
    bool playerPlaying = true;
    bool CPU1Playing = true;
    bool CPU2Playing = true;
    bool CPU3Playing = true;
    int minBet = 25;
    int pot = 0;
    int playerBet;
    int CPU1Bet;
    int CPU2Bet;
    int CPU3Bet;
    int turn;
    int[] playerMoney;
    int[] CPU1Money;
    int[] CPU2Money;
    int[] CPU3Money;
    #endregion
    // Start is called before the first frame update
    void Start()
    {

        playerMoney = new int[] { 5, 4, 3, 2 };
        CPU1Money = new int[] { 5, 4, 3, 2 };
        CPU2Money = new int[] { 5, 4, 3, 2 };
        CPU3Money = new int[] { 5, 4, 3, 2 };


        //Creation of chips
        Chips();



        Lowest.GetComponent<TextMeshProUGUI>().text = "Bet " + minBet;

        Lowest.GetComponent<BetLowest>().Handler += BetLowest;
        Half.GetComponent<BetHalf>().Handler += BetHalf;
        Pot.GetComponent<BetPot>().Handler += BetPot;
        AllIn.GetComponent<BetAllIn>().Handler += BetAllIn;
        OFold.GetComponent<Fold>().Handler += Fold;


        Lowest.SetActive(false);
        Half.SetActive(false);
        Pot.SetActive(false);
        AllIn.SetActive(false);
        OFold.SetActive(false);


        System.Random rnd = new(Guid.NewGuid().GetHashCode());
        deck = new Material[40];


        Hand.SetActive(false);
        HandCPU1.SetActive(false);
        HandCPU2.SetActive(false);
        HandCPU3.SetActive(false);


        hands = new GameObject[] { Hand, HandCPU1, HandCPU2, HandCPU3 };
        Playingcards.Eventhandler += OnHandEnabled;


        List<Material> list;
        list = new();
        list.AddRange(Resources.LoadAll<Material>("BackColor_Red"));


        int index;
        for (int j = 0; j < deck.Length; j++)
        {
            index = rnd.Next(0, 40 - j);
            deck[j] = list[index];
            list.RemoveAt(index);
        }
        deckMaterials = deck.ToList();
    }

    void Chips()
    {
        #region PlayerChips
        for (int i = 0; i < Player5s.transform.childCount; i++)
        {
            Destroy(Player5s.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Player10s.transform.childCount; i++)
        {
            Destroy(Player10s.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Player20s.transform.childCount; i++)
        {
            Destroy(Player20s.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < Player50s.transform.childCount; i++)
        {
            Destroy(Player50s.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < playerMoney[0]; i++)
        {
            GameObject instance = Instantiate(Chip5, new Vector3(Player5s.transform.position.x, Player5s.transform.position.y + 0.5f * (i + 1), Player5s.transform.position.z), Quaternion.identity, Player5s.transform);
            instance.transform.localScale = new Vector3(4f, 4f, 4f);
        }
        for (int i = 0; i < playerMoney[1]; i++)
        {
            GameObject instance = Instantiate(Chip10, new Vector3(Player10s.transform.position.x, Player10s.transform.position.y + 0.5f * (i + 1), Player10s.transform.position.z), Quaternion.identity, Player10s.transform);
            instance.transform.localScale = new Vector3(4f, 4f, 4f);
        }
        for (int i = 0; i < playerMoney[2]; i++)
        {
            GameObject instance = Instantiate(Chip20, new Vector3(Player20s.transform.position.x, Player20s.transform.position.y + 0.5f * (i + 1), Player20s.transform.position.z), Quaternion.identity, Player20s.transform);
            instance.transform.localScale = new Vector3(4f, 4f, 4f);
        }
        for (int i = 0; i < playerMoney[3]; i++)
        {
            GameObject instance = Instantiate(Chip50, new Vector3(Player50s.transform.position.x, Player50s.transform.position.y + 0.5f * (i + 1), Player50s.transform.position.z), Quaternion.identity, Player50s.transform);
            instance.transform.localScale = new Vector3(4f, 4f, 4f);
        }
        #endregion
        CPUChips(CPU1Chips, CPU1Money);
        CPUChips(CPU2Chips, CPU2Money);
        CPUChips(CPU3Chips, CPU3Money);
    }
    void CPUChips(GameObject cpuchips, int[] cpumoney)
    {
        Transform chips;
        GameObject[] OChips = new GameObject[4] {Chip5, Chip10, Chip20, Chip50 };
        for (int i = 0; i < cpuchips.transform.childCount; i++)
        {
            chips = cpuchips.transform.GetChild(i);
            for(int j = 0; j < chips.childCount; j++)
            {
                Destroy(chips.GetChild(j).gameObject);
            }
        }
        for(int i = 0; i < cpuchips.transform.childCount; i++)
        {
            chips = cpuchips.transform.GetChild(i);
            for (int j = 0; j < cpumoney[i]; j++)
            {
                GameObject instance = Instantiate(OChips[i], new Vector3(chips.position.x, chips.position.y + 0.5f * (i + 1), chips.position.z), Quaternion.identity, chips);
                instance.transform.localScale = new Vector3(4f, 4f, 4f);
            }
        }
    }

    public int CPUBet(int[] CPUMoney, int potSize) //0 = fold, 1 = minbet, 2 = half, 3 = pot, 4 = allin
    {
        System.Random rnd = new();

        int totalMoney = CPUMoney[0] * 5 + CPUMoney[1] * 10 + CPUMoney[2] * 20 + CPUMoney[3] * 50;

        if (totalMoney < 25)
        {
            return 0;
        }

        if (totalMoney == minBet)
        {
            return 1;
        }

        int odds;
        odds = rnd.Next(5);
        if (odds < 2)
        {
            return odds;
        }
        if (odds == 2)
        {
            if (potSize > totalMoney)
                return odds;
            else
                return 1;
        }
        if (odds == 3)
        {
            if (potSize > totalMoney)
                return odds;
            else if (totalMoney / 2 < potSize)
                return 2;
            else
                return 1;
        }
        if (odds == 4)
        {
            if (potSize > totalMoney)
                return odds;
            else if (totalMoney / 2 < potSize)
                return 3;
            else
                return 2;
        }
        return 1;
    }

    IEnumerator ShowMessage()
    {
        ErrorMessage.SetActive(true);
        yield return new WaitForSeconds(5);
        ErrorMessage.SetActive(false);
    }

    void RoundBegin(int bet)
    {
        OFold.SetActive(false);
        Lowest.SetActive(false);
        AllIn.SetActive(false);
        Half.SetActive(false);
        Pot.SetActive(false);

        SubmitBet(bet);
        pot += bet;
        try
        {
            switch (CPUBet(CPU1Money, pot))
            {
                case 0:
                    Fold(HandCPU1, new IDEventArgs(1));
                    break;
                case 1:
                    BetLowest(HandCPU1, new IDEventArgs(1));
                    break;
                case 2:
                    BetHalf(HandCPU1, new IDEventArgs(1));
                    break;
                case 3:
                    BetPot(HandCPU1, new IDEventArgs(1));
                    break;
                case 4:
                    BetAllIn(HandCPU1, new IDEventArgs(1));
                    break;
            }
            if (CPU1Bet > Money(CPU1Money))
                throw new NotEnoughMoneyException("Not Enough Money");
        }
        catch (NotEnoughMoneyException)
        {
            if (Money(CPU1Money) < minBet)
            {
                Fold(HandCPU1, new IDEventArgs(1));
            }
            else
                BetLowest(HandCPU1, new IDEventArgs(1));
        }
        SubmitBet(CPU1Bet);
        pot += CPU1Bet;
        try
        {
            switch (CPUBet(CPU2Money, pot))
            {
                case 0:
                    Fold(HandCPU2, new IDEventArgs(2));
                    break;
                case 1:
                    BetLowest(HandCPU2, new IDEventArgs(2));
                    break;
                case 2:
                    BetHalf(HandCPU2, new IDEventArgs(2));
                    break;
                case 3:
                    BetPot(HandCPU2, new IDEventArgs(2));
                    break;
                case 4:
                    BetAllIn(HandCPU2, new IDEventArgs(2));
                    break;
            }
            if (CPU1Bet > Money(CPU1Money))
                throw new NotEnoughMoneyException("Not Enough Money");
        }
        catch (NotEnoughMoneyException)
        {
            if (Money(CPU2Money) < 25)
            {
                Fold(HandCPU2, new IDEventArgs(2));
            }
            else
                BetLowest(HandCPU2, new IDEventArgs(2));
        }
        SubmitBet(CPU2Bet);
        pot += CPU2Bet;
        try
        {
            switch (CPUBet(CPU3Money, pot))
            {
                case 0:
                    Fold(HandCPU3, new IDEventArgs(3));
                    break;
                case 1:
                    BetLowest(HandCPU3, new IDEventArgs(3));
                    break;
                case 2:
                    BetHalf(HandCPU3, new IDEventArgs(3));
                    break;
                case 3:
                    BetPot(HandCPU3, new IDEventArgs(3));
                    break;
                case 4:
                    BetAllIn(HandCPU3, new IDEventArgs(3));
                    break;
            }
            if (CPU1Bet > Money(CPU1Money))
                throw new NotEnoughMoneyException("Not Enough Money");
        }
        catch (NotEnoughMoneyException)
        {
            if (Money(CPU3Money) < 25)
            {
                Fold(HandCPU3, new IDEventArgs(3));
            }
            else
                BetLowest(HandCPU3, new IDEventArgs(3));
        }
        SubmitBet(CPU3Bet);
        pot += CPU3Bet;

        if(!CPU1Playing) HandCPU1.SetActive(false);
        if(!CPU2Playing) HandCPU2.SetActive(false);
        if(!CPU3Playing) HandCPU3.SetActive(false);

        turn++;
        StartRound();
    }

    int Money(int[] money)
    {
        return money[0] * 5 + money[1] * 10 + money[2] * 20 + money[3] * 50;
    }

    void StartRound()
    {
        if (!(playerPlaying || CPU1Playing || CPU2Playing || CPU3Playing))
            ResetTurn();
        
        RevealCard(turn);

        OFold.SetActive(true);
        Lowest.SetActive(true);
        AllIn.SetActive(true);
        Half.SetActive(true);
        Pot.SetActive(true);


    }

    void ResetTurn()
    {
        
    }

    void SubmitBet(int bet)
    {
        Debug.Log(bet);
        while (bet - 50 >= 0)
        {
            bet -= 50;
            Instantiate(Chip50, new Vector3(-0.5f, 0, -1.92f), Quaternion.identity).transform.localScale = new Vector3(4, 4, 4);
        }
        while (bet - 20 >= 0)
        {
            bet -= 20;
            Instantiate(Chip20, new Vector3(0.5f, 0, -1.92f), Quaternion.identity).transform.localScale = new Vector3(4, 4, 4);
        }
        while (bet - 10 >= 0)
        {
            bet -= 10;
            Instantiate(Chip10, new Vector3(1.5f, 0, -1.92f), Quaternion.identity).transform.localScale = new Vector3(4, 4, 4);
        }
        while (bet - 5 >= 0)
        {
            bet -= 5;
            Instantiate(Chip5, new Vector3(2.5f, 0, -1.92f), Quaternion.identity).transform.localScale = new Vector3(4, 4, 4);
        }
    }

    #region bet/fold

    private void Fold(object sender, IDEventArgs e)
    {
        switch (e.ID)
        {
            case 0:
                playerBet = 0;
                playerPlaying = false;
                Hand.SetActive(false);
                break;
            case 1:
                CPU1Bet = 0;
                CPU1Playing = false;
                HandCPU1.SetActive(false);
                break;
            case 2:
                CPU2Bet = 0;
                CPU2Playing = false;
                HandCPU2.SetActive(false);
                break;
            case 3:
                CPU3Bet = 0;
                CPU3Playing = false;
                HandCPU3.SetActive(false);
                break;
        }
        if (e.ID == 0)
            RoundBegin(0);
    }

    private void BetAllIn(object sender, IDEventArgs e)
    {
        int bet;
        try
        {
            switch (e.ID)
            {
                case 0:
                    bet = 5 * playerMoney[0] + 10 * playerMoney[1] + 20 * playerMoney[2] + 50 * playerMoney[3];
                    playerBet = bet;
                    Pay(bet, playerMoney);
                    break;
                case 1:
                    bet = 5 * CPU1Money[0] + 10 * CPU1Money[1] + 20 * CPU1Money[2] + 50 * CPU1Money[3];
                    CPU1Bet = bet;
                    Pay(bet, CPU1Money);
                    break;
                case 2:
                    bet = 5 * CPU2Money[0] + 10 * CPU2Money[1] + 20 * CPU2Money[2] + 50 * CPU2Money[3];
                    CPU2Bet = bet;
                    Pay(bet, CPU2Money);
                    break;
                case 3:
                    bet = 5 * CPU3Money[0] + 10 * CPU3Money[1] + 20 * CPU3Money[2] + 50 * CPU3Money[3];
                    CPU3Bet = bet;
                    Pay(bet, CPU3Money);
                    break;
                default:
                    bet = 0;
                    break;
            }
        }
        catch (NotEnoughMoneyException)
        {
            if (e.ID == 0)
                StartCoroutine(ShowMessage());
            return;
        }
        if (e.ID == 0)
            RoundBegin(bet);
        Chips();
    }

    private void BetPot(object sender, IDEventArgs e)
    {
        int bet = 0;
        while (bet <= pot)
        {
            bet += 5;
        }
        bet -= 5;
        if (pot < minBet)
            bet = minBet;
        try
        {
            switch (e.ID)
            {
                case 0:
                    playerBet = bet;
                    Pay(bet, playerMoney);
                    break;
                case 1:
                    CPU1Bet = bet;
                    Pay(bet, CPU1Money);
                    break;
                case 2:
                    CPU2Bet = bet;
                    Pay(bet, CPU2Money);
                    break;
                case 3:
                    CPU3Bet = bet;
                    Pay(bet, CPU3Money);
                    break;
            }
        }
        catch (NotEnoughMoneyException)
        {
            if (e.ID == 0)
                StartCoroutine(ShowMessage());
            return;
        }
        if (e.ID == 0)
            RoundBegin(bet);
        Chips();
    }

    private void BetHalf(object sender, IDEventArgs e)
    {
        int bet = 0;
        while (bet <= pot / 2)
        {
            bet += 5;
        }
        bet -= 5;
        if (pot / 2 < minBet)
            bet = minBet;
        try
        {
            switch (e.ID)
            {
                case 0:
                    playerBet = bet;
                    Pay(bet, playerMoney);
                    break;
                case 1:
                    CPU1Bet = bet;
                    Pay(bet, CPU1Money);
                    break;
                case 2:
                    CPU2Bet = bet;
                    Pay(bet, CPU2Money);
                    break;
                case 3:
                    CPU3Bet = bet;
                    Pay(bet, CPU3Money);
                    break;
            }
        }
        catch (NotEnoughMoneyException)
        {
            if (e.ID == 0)
                StartCoroutine(ShowMessage());
            return;
        }
        if (e.ID == 0)
            RoundBegin(bet);
        Chips();
    }

    private void BetLowest(object sender, IDEventArgs e)
    {
        try
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
        catch (NotEnoughMoneyException)
        {
            if (e.ID == 0)
                StartCoroutine(ShowMessage());
            return;
        }
        if (e.ID == 0)
            RoundBegin(minBet);
        Chips();
    }

    #endregion

    void RevealCard(int index)
    {
        Transform card = TableCards.transform.GetChild(index);
        card.Rotate(0, 0, -180);
    }

    private void OnHandEnabled(object sender, EventArgs e)
    {
        Lowest.SetActive(true);
        AllIn.SetActive(true);
        OFold.SetActive(true);
        if (pot != 0)
        {
            Half.SetActive(true);
            Pot.SetActive(true);
        }
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
        turn = 0;
        RevealCard(turn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pay(int amount, int[] total)
    {
        if (amount > Money(total))
        {
            throw new NotEnoughMoneyException("Not enough money");
        }
        while (amount >= 50 && total[3] > 0)
        {
            amount -= 50;
            total[3]--;
        }
        while (amount >= 20 && total[2] > 0)
        {
            amount -= 20;
            total[2]--;
        }
        while (amount >= 10 && total[1] > 0)
        {
            amount -= 10;
            total[1]--;
        }
        while (amount >= 5 && total[0] > 0)
        {
            amount -= 5;
            total[0]--;
        }
    }

}