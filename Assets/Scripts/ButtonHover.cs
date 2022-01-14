using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite button_one;
    public Sprite button_two;
    public Image image;
    private bool mouse_over = false;


    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = button_two;
    }

    void Update()
    {
        if (mouse_over)
            image.sprite = button_two;
        else
            image.sprite = button_one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
}