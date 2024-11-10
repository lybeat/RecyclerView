using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Image fillImage;

    [SerializeField]
    private Dircetion dircetion;

    private float progress;
    public float Progress => progress;

    private void Awake()
    {
        if (fillImage == null)
        {
            fillImage = transform.Find("Fill").GetComponent<Image>();
        }
        fillImage.type = Image.Type.Filled;
        fillImage.fillAmount = 0.2f;
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        switch (dircetion)
        {
            case Dircetion.LeftToRight:
                fillImage.fillMethod = Image.FillMethod.Horizontal;
                fillImage.fillOrigin = (int)Image.OriginHorizontal.Left;
                break;
            case Dircetion.RightToLeft:
                fillImage.fillMethod = Image.FillMethod.Horizontal;
                fillImage.fillOrigin = (int)Image.OriginHorizontal.Right;
                break;
            case Dircetion.BottomToTop:
                fillImage.fillMethod = Image.FillMethod.Vertical;
                fillImage.fillOrigin = (int)Image.OriginVertical.Bottom;
                break;
            case Dircetion.TopToBottom:
                fillImage.fillMethod = Image.FillMethod.Horizontal;
                fillImage.fillOrigin = (int)Image.OriginVertical.Top;
                break;
        }
    }

    public void SetDirection(Dircetion dircetion)
    {
        this.dircetion = dircetion;
        UpdateDirection();
    }

    public void SetProgress(float progress)
    {
        this.progress = progress;
        fillImage.fillAmount = progress;
    }

    public enum Dircetion
    {
        LeftToRight,
        RightToLeft,
        BottomToTop,
        TopToBottom
    }
}
