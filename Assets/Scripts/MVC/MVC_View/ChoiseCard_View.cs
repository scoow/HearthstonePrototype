using Hearthstone;
using UnityEngine;

/// <summary>
/// класс содержит метод реализующий замену текущих настроек карты на новые 
/// </summary>
public class ChoiseCard_View : MonoBehaviour , IChange
{
    public IReadable _readable;

    private void Awake()
    {
        _readable = FindObjectOfType<PageBook_Controller>();
    }


    /// <summary>
    /// смена настроек увеличенной карты
    /// </summary>
    /// <param name="settingsZoomingCard">настройки увеличенной карты</param>
    /// <param name="settingsChioseCard">настройки выбранной карты</param>
    public void ChangeViewCard(CardSettings_Model settingsZoomingCard , int choiseCardId)
    {
        CardSO_Model chioseCardSettings = _readable.GetCard(choiseCardId);
        settingsZoomingCard.ManaCost.text = chioseCardSettings._manaCostCard.ToString();
        settingsZoomingCard.Name.text = chioseCardSettings._nameCard;
        settingsZoomingCard.AtackDamage.text = chioseCardSettings._atackDamageCard.ToString();
        settingsZoomingCard.Healt.text = chioseCardSettings._healtCard.ToString();
        settingsZoomingCard.Description.text = chioseCardSettings._descriptionCard;
        settingsZoomingCard.SpriteCard.sprite = chioseCardSettings._spriteCard;                
    }   
}