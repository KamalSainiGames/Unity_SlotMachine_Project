using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    private int playerBalance = 50000;
    public Text playerbalancetext;
    public GameObject betpanel;

    public const string PlAYERBALANCE = "PLayerBalance";
    private void Start()
    {
        playerbalancetext.text = "$" + playerBalance.ToString();
    }
    public int PlayerBalance
    {
        get { return playerBalance; }
        set
        {
            playerBalance = value;
            playerbalancetext.text = "$" + value.ToString();

        }
    }

    int betplacingamt;
    public void BetPlacing(int amt)
    {
        if (PlayerBalance < amt)
        {
            Debug.Log("Not Enough Balance");
            return;
        }

        PlayerBalance -= amt;

        betplacingamt = amt;
    }

    int payoutamt;

    public void Payout(ItemType item)
    {
        payoutamt = 0;

        if (item == ItemType.Bar || item == ItemType.Seven)
        {
            payoutamt = betplacingamt * 5;
        }
        else if (item == ItemType.Cherry || item == ItemType.Bell)
        {
            payoutamt = betplacingamt * 2;
        }

        PlayerBalance += payoutamt;

        betpanel.SetActive(true);
    }
}
