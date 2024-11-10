using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NaiQiu.Framework.View
{
    public class ItemViewHolderNormal : ViewHolder, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject frame;
        private Image bgImage;
        private Image iconImage;
        private TMP_Text nameText;
        private TMP_Text countText;

        public override void FindUI()
        {
            frame = transform.Find("Frame").gameObject;
            bgImage = transform.Find("BgImage").GetComponent<Image>();
            iconImage = transform.Find("IconImage").GetComponent<Image>();
            nameText = transform.Find("NameText").GetComponent<TMP_Text>();
            countText = transform.Find("CountText").GetComponent<TMP_Text>();
        }

        public override void BindViewData<T>(T data)
        {
            ItemData itemData = data as ItemData;

            bgImage.sprite = GameManager.SpriteManager.GetSprite($"Background_Item_{itemData.rarity}_Star");
            iconImage.sprite = GameManager.SpriteManager.GetSprite(itemData.icon);
            nameText.text = itemData.name;
            countText.text = Random.Range(1, 10).ToString();
        }

        public override void BindChoiceState(bool state)
        {
            frame.SetActive(state);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            RectTransform.DOScale(Vector3.one * 1.05f, 0.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RectTransform.DOScale(Vector3.one, 0.2f);
        }
    }
}
