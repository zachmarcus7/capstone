namespace MLBreakout
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>
    /// This is a class for changing the sprite
    /// of a button when a user hovers over it.
    /// </summary>
    public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _mouseOver;
        public Sprite ButtonOne;
        public Sprite ButtonTwo;
        public Image SelectedImage;

        private void Start()
        {
            _mouseOver = false;
            SelectedImage = GetComponent<Image>();
            SelectedImage.sprite = ButtonTwo;
        }

        private void Update()
        {
            if (_mouseOver)
            {
                SelectedImage.sprite = ButtonTwo;
            }
            else
            {
                SelectedImage.sprite = ButtonOne;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseOver = false;
        }
    }
}