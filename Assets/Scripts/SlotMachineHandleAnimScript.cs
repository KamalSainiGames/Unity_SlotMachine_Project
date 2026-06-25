using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlotMachineHandleAnimScript : MonoBehaviour
{
    public GameObject UpHandle;
    public GameObject DownHandle;

    [SerializeField] private float pullAngle = -90f;

    [SerializeField] private Button handleButton;

    private bool isPulling;

    public void Pull()
    {
        if (isPulling)
            return;

        isPulling = true;

        handleButton.interactable = false;

        Sequence seq = DOTween.Sequence();

        seq.Append(
            UpHandle.transform.DOLocalRotate(
                new Vector3(pullAngle, 0, 0),
                0.3f
            ).SetEase(Ease.OutQuad)
        );

        seq.AppendCallback(() =>
        {
            UpHandle.SetActive(false);
            DownHandle.SetActive(true);

            // START SLOT HERE
            SlotMachine.instance.Spin();
        });

        seq.AppendInterval(0.2f);

        seq.AppendCallback(() =>
        {
            DownHandle.SetActive(false);
            UpHandle.SetActive(true);
        });

        seq.Append(
            UpHandle.transform.DOLocalRotate(
                Vector3.zero,
                0.3f
            ).SetEase(Ease.OutElastic)
        );

        seq.OnComplete(() =>
        {
            isPulling = false;
        });
    }
}