namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used on a button to switch
    /// scenes to the two-player easy scene (outer space).
    /// </summary>
    public class TwoPlayerEasyButton : MonoBehaviour
    {
        public Button EasyButton;

        private void Start()
        {
            EasyButton = GetComponent<Button>();
            EasyButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene("TwoPlayerEasy");
        }
    }
}