namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used to launch the
    /// about pop up in the opening menu.
    /// </summary>
    public class AboutButton : MonoBehaviour
    {
        public Button GameButton;
        public AboutPopUp PopUp;

        private void Start()
        {
            GameButton = GetComponent<Button>();
            GameButton.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            PopUp.MakeActive();
        }
    }
}