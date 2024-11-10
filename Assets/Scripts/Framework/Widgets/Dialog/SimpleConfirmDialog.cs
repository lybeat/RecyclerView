using System;
using NaiQiu.Framework.View;
using TMPro;
using UnityEngine.UI;

namespace NaiQiu.Framework.View
{
    public class SimpleConfirmDialog : Dialog
    {
        private TMP_Text contentText;
        private Button confirmBtn;
        private Button cancelBtn;

        private Action OnConfirm;

        public override void OnCreate(UIManager viewManager)
        {
            base.OnCreate(viewManager);

            contentText = transform.Find("ContentText").GetComponent<TMP_Text>();
            confirmBtn = transform.Find("ButtonContainer/ConfirmBtn").GetComponent<Button>();
            cancelBtn = transform.Find("ButtonContainer/CancelBtn").GetComponent<Button>();

            confirmBtn.onClick.AddListener(OnConfirmClick);
            cancelBtn.onClick.AddListener(OnCancelClick);
        }

        private void OnConfirmClick()
        {
            Close();
            OnConfirm?.Invoke();
        }

        private void OnCancelClick()
        {
            Close();
        }

        public void SetContent(string content)
        {
            contentText.text = content;
        }

        public void SetConfirmCallback(Action action)
        {
            OnConfirm = action;
        }
    }
}