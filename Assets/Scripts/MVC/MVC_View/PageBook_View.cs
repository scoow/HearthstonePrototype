using Hearthstone;
using System;
using System.Collections.Generic;
using UnityEngine;


public class PageBook_View : MonoBehaviour , IUpdate
{
    public Action OnUpdatePageBook;


    /// <summary>
    /// обновление страницы книги
    /// </summary>
    /// <param name="cardSO"></param>
    /// <param name="card_ModelArrey"></param>
    /// <param name="pageCounter"></param>
    public void UpdatePageBook(List<CardSO_Model> cardSO, Card_Model[] card_ModelArrey, int pageCounter )
    {
        int stepUpdatePage = pageCounter * card_ModelArrey.Length;
        for (int i = 0; i <= card_ModelArrey.Length - 1; i++)
        {
            card_ModelArrey[i]._spriteCard = cardSO[i + stepUpdatePage]._spriteCard;
            card_ModelArrey[i]._idCard = cardSO[i + stepUpdatePage]._idCard;
            card_ModelArrey[i]._manaCostCard = cardSO[i + stepUpdatePage]._manaCostCard;
            card_ModelArrey[i]._atackDamageCard = cardSO[i + stepUpdatePage]._atackDamageCard;
            card_ModelArrey[i]._healthCard = cardSO[i + stepUpdatePage]._healthCard;
            card_ModelArrey[i]._nameCard = cardSO[i + stepUpdatePage]._nameCard;
            card_ModelArrey[i]._descriptionCard = cardSO[i + stepUpdatePage]._descriptionCard;
        }
        OnUpdatePageBook?.Invoke();
    }
}