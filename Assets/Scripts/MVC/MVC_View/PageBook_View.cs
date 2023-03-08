using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{

    public class PageBook_View : MonoBehaviour, IUpdate
    {
        public Action OnUpdatePageBook;
        private ContentDeck_Model _contentDeck_Model;
        private PageBook_Model _pageBookModel;

        private void Awake()
        {
            _contentDeck_Model = FindObjectOfType<ContentDeck_Model>();
            _pageBookModel = FindObjectOfType<PageBook_Model>();
        }


        /// <summary>
        /// обновление страницы книги
        /// </summary>
        /// <param name="cardSO"></param>
        /// <param name="card_ModelArrey"></param>
        /// <param name="pageCounter"></param>
        public void UpdatePageBook(List<CardSO_Model> cardSO, Card_Model[] card_ModelArrey, int pageCounter)
        {
            for(int i = 0; i <= card_ModelArrey.Length-1; i++)
            {
                card_ModelArrey[i].gameObject.SetActive(false);
            }

            int stepUpdatePage = pageCounter * card_ModelArrey.Length;
            for (int i = 0; i <= card_ModelArrey.Length-1; i++)
            {

                if (i + stepUpdatePage >= cardSO.Count) return;
                {
                    CardSO_Model tempCard_SO = cardSO[i + stepUpdatePage];
                    card_ModelArrey[i].gameObject.SetActive(true);                    
                    card_ModelArrey[i]._spriteCard = cardSO[i + stepUpdatePage]._spriteCard;
                    card_ModelArrey[i]._cardClassInDeck = cardSO[i + stepUpdatePage]._cardClass;
                    card_ModelArrey[i].gameObject.GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>().color = Color.white;
                    if (_pageBookModel._createDeckState == CreateDeckState.CreateDeck)// если колода находится в состоянии создания
                    {
                        if(cardSO[i + stepUpdatePage]._cardClass != _contentDeck_Model._classHeroInDeck)
                        {
                            card_ModelArrey[i].gameObject.GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>().color = Color.red;
                            //card_ModelArrey[i].gameObject.GetComponentInChildren<CardFrontMarker>().GetComponent<SpriteRenderer>().color = Color.grey;
                        }              

                        if (cardSO[i + stepUpdatePage]._cardClass == Classes.Universal)                        
                            card_ModelArrey[i].gameObject.GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>().color = Color.white;                        
                    }
                    
                    card_ModelArrey[i]._idCard = cardSO[i + stepUpdatePage]._idCard;
                    card_ModelArrey[i]._manaCostCard = cardSO[i + stepUpdatePage]._manaCostCard;
                    card_ModelArrey[i]._atackDamageCard = cardSO[i + stepUpdatePage]._atackDamageCard;
                    card_ModelArrey[i]._healthCard = cardSO[i + stepUpdatePage]._healthCard;
                    card_ModelArrey[i]._nameCard = cardSO[i + stepUpdatePage]._nameCard;
                    card_ModelArrey[i]._descriptionCard = cardSO[i + stepUpdatePage]._descriptionCard;
                    OnUpdatePageBook?.Invoke();
                }           

            }
            //OnUpdatePageBook?.Invoke();
        }
    }
}