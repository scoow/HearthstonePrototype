using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiseCardSettings : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Text _name;    
    [SerializeField] private Text _manaCost;     
    private int _id;

    public Text Name { get => _name; }    
    public Text ManaCost { get => _manaCost; }    
    public int Id { get => _id; }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        /*_spriteEmission.gameObject.SetActive(true);
        _settingsZoomingCard.SpriteCard.sprite = _settingsChioseCard.SpriteCard.sprite;
        _settingsZoomingCard.Name.text = _settingsChioseCard.Name.text;
        _settingsZoomingCard.ManaCost.text = _settingsChioseCard.ManaCost.text;
        _settingsZoomingCard.AtackDamage.text = _settingsChioseCard.AtackDamage.text;
        _settingsZoomingCard.Healt.text = _settingsChioseCard.Healt.text;
        _settingsZoomingCard.Description.text = _settingsChioseCard.Description.text;
        _zoomingCard.gameObject.SetActive(true);*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}