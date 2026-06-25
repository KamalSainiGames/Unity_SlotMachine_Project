using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class ReelController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform itemsContainer;

    [Header("Settings")]
    [SerializeField] private float containerHeight = 1000f;
    [SerializeField] private float symbolHeight = 200f;

    [SerializeField] private float spinSpeed = 2600f;

    [SerializeField] private float spinDuration = 3f;

    private bool isSpinning;

    public List<ItemScript> itemList = new List<ItemScript>();

    // --------------------------------------------------

    public void Spin()
    {
        if (isSpinning)
            return;

        StartCoroutine(SpinRoutine());
    }

    // --------------------------------------------------
    private int randomindex;
    private IEnumerator SpinRoutine()
    {
        isSpinning = true;

        float timer = 0f;

        while (timer < spinDuration)
        {
            // MOVE REEL
            itemsContainer.anchoredPosition +=
                Vector2.up * spinSpeed * Time.deltaTime;

            // RECYCLE
            if (itemsContainer.anchoredPosition.y >= containerHeight)
            {
                itemsContainer.anchoredPosition -=
                    new Vector2(0, containerHeight);

                //MoveTopToBottom();
            }

            timer += Time.deltaTime;

            yield return null;
        }

        // ---------------- RANDOM STOP ----------------

        randomindex = Random.Range(1, itemList.Count-1);
        Debug.Log("random:" + randomindex);

        float targetY = (randomindex - 1)* symbolHeight;

        yield return itemsContainer
            .DOAnchorPosY(targetY, 0.25f)
            .SetEase(Ease.OutQuart)
            .WaitForCompletion();       

        isSpinning = false;
    }

    public ItemType GetCenterItem()
    {
        return itemList[randomindex].itemType;
    }
    // --------------------------------------------------

    private void MoveTopToBottom()
    {
        Transform first =
            itemsContainer.GetChild(0);

        first.SetAsLastSibling();

        // UPDATE LIST
        ItemScript temp = itemList[0];

        itemList.RemoveAt(0);

        itemList.Add(temp);
    }
}