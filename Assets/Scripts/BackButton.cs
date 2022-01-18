namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Simple button class. Used to close
    /// the about pop up in the opening menu.
    /// </summary>
    public class BackButton : MonoBehaviour
    {
        public Button GameButton;
        public AboutPopUp PopUp;

        private void Start()
        {
            GameButton = GetComponent<Button>();
            GameButton.onClick.AddListener(TaskOnClick);
        }

        public void TaskOnClick()
        {
            PopUp.MakeInactive();
        }
    }
}
