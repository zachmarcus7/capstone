namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used to close the pop
    /// up for choosing a two-player level in the opening menu. 
    /// </summary>
    public class TwoPlayerBackButton : MonoBehaviour
    {
        public Button BackButton;
        public TwoPlayerPopUp PopUp;

        private void Start()
        {
            BackButton = GetComponent<Button>();
            BackButton.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            PopUp.MakeInactive();
        }
    }
}
