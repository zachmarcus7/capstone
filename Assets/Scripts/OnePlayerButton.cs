namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// This is a simple button class used on a button to switch
    /// scenes to the one-player scene.
    /// </summary>
    public class OnePlayerButton : MonoBehaviour
    {
        private Button _onePlayerButton;

        private void Start()
        {
            _onePlayerButton = GetComponent<Button>();
            _onePlayerButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene("Main");
        }
    }
}