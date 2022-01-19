namespace MLBreakout
{
    using UnityEngine;

    /// <summary>
    /// Pop up that appears in opening menu.
    /// It explains what the game is about.
    /// </summary>
    public class AboutPopUp : MonoBehaviour
    {
        private static bool _active;
        public AboutPopUp Instance;
        public GameObject PopUp;

        private void Start()
        {
            _active = false;
        }

        private void Update()
        {
            if (_active)
            {
                PopUp.SetActive(true);
            }
            else
            {
                PopUp.SetActive(false);
            }
        }

        public void MakeActive()
        {
            _active = true;
        }

        public void MakeInactive()
        {
            _active = false;
        }
    }
}