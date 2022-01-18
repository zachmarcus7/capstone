namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used to launch the
    /// about pop up in the opening menu.
    /// </summary>
    public class TwoPlayerButton : MonoBehaviour
    {
        public Button GameButton;
        public TwoPlayerPopUp PopUp;

        private void Start()
        {
            GameButton = GetComponent<Button>();
            GameButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            PopUp.MakeActive();
        }
    }
}