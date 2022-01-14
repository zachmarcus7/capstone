using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite buttonOne;
    public Sprite buttonTwo;
    public Image image;
    private bool mouseOver;


    void Start()
    {
        mouseOver = false;
        image = GetComponent<Image>();
        image.sprite = buttonTwo;
    }

    void Update()
    {
        if (mouseOver)
            image.sprite = buttonTwo;
        else
            image.sprite = buttonOne;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}