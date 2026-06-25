using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    public static SlotMachine instance;

    [SerializeField] private ReelController[] reels;
    [SerializeField] private Button HandleBtn;
   
    private void Awake()
    {
        instance = this;
    }

    public void Spin()
    {
        StartCoroutine(SpinRoutine());
    }


    private IEnumerator SpinRoutine()
    {
        reels[0].Spin();

        yield return new WaitForSeconds(.5f);
        reels[1].Spin();

        yield return new WaitForSeconds(.5f);
        reels[2].Spin();

        yield return new WaitForSeconds(2.5f);

        CheckWin();

        HandleBtn.interactable = true;
    }

    public void CheckWin()
    {
        ItemType first =
            reels[0].GetCenterItem();

        bool allSame = true;

        for (int i = 1; i < reels.Length; i++)
        {
            if (reels[i].GetCenterItem() != first)
            {
                allSame = false;
                break;
            }
        }

        if (allSame)
        {
            Debug.Log("WIN : " + first);
            StartCoroutine(ShowWinning());      
        }
        else
        {
            Debug.Log("LOSE");
            GameManager.instance.betpanel.SetActive(true);
        }
    }

    public GameObject wintext;

    IEnumerator ShowWinning()
    {
        wintext.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        wintext.SetActive(false);
        GameManager.instance.Payout(reels[0].GetCenterItem());
    }
}

public enum ItemType
{
    Cherry,
    Bar,
    Seven,
    Bell
}
