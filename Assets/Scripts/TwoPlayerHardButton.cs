namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used on a button to switch
    /// scenes to the two-player hard scene.
    /// </summary>
    public class TwoPlayerHardButton : MonoBehaviour
    {
        public Button HardButton;

        private void Start()
        {
            HardButton = GetComponent<Button>();
            HardButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene("TwoPlayerHard");
        }
    }
}