namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used on a button to switch
    /// scenes to the one-player ml agent scene.
    /// </summary>
    public class MLPlayerButton : MonoBehaviour
    {
        private Button _mlPlayerButton;

        private void Start()
        {
            _mlPlayerButton = GetComponent<Button>();
            _mlPlayerButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene("MLAgentScreen");
        }
    }
}