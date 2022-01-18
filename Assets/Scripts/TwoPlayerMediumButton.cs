namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used on a button to switch
    /// scenes to the two-player medium scene.
    /// </summary>
    public class TwoPlayerMediumButton : MonoBehaviour
    {
        public Button MediumButton;

        private void Start()
        {
            MediumButton = GetComponent<Button>();
            MediumButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene("TwoPlayerMedium");
        }
    }
}