using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonInfo : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField]
    private GameObject infoText;

    private void Start()
    {
        infoText = gameObject.transform.GetChild(1).gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoText.SetActive(false);

    }
}
