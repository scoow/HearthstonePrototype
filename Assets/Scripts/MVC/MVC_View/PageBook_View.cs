using Hearthstone;
using System.Collections.Generic;
using UnityEngine;


public class PageBook_View : MonoBehaviour , IUpdate
{ 
    /// <summary>
    /// обновление страницы книги
    /// </summary>
    /// <param name="cardSO"></param>
    /// <param name="cardSettings"></param>
    /// <param name="pageCounter"></param>
    public void UpdatePageBook(List<CardSO_Model> cardSO, CardSettings_Model[] cardSettings, int pageCounter )
    {
        int stepUpdatePage = pageCounter * cardSettings.Length;
        for (int i = 0; i <= cardSettings.Length - 1; i++)
        {
            cardSettings[i].SpriteCard.sprite = cardSO[i + stepUpdatePage]._spriteCard;
            cardSettings[i].Id = cardSO[i + stepUpdatePage]._idCard;
            cardSettings[i].ManaCost.text = cardSO[i + stepUpdatePage]._manaCostCard.ToString();
            cardSettings[i].AtackDamage.text = cardSO[i + stepUpdatePage]._atackDamageCard.ToString();
            cardSettings[i].Healt.text = cardSO[i + stepUpdatePage]._healtCard.ToString();
            cardSettings[i].Name.text = cardSO[i + stepUpdatePage]._nameCard.ToString();
            cardSettings[i].Description.text = cardSO[i + stepUpdatePage]._descriptionCard.ToString();
        }
    }
}