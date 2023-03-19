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
        /// ���������� �������� �����
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
                    card_ModelArrey[i].SpriteCard = cardSO[i + stepUpdatePage].SpriteCard;
                    card_ModelArrey[i]._cardClassInDeck = cardSO[i + stepUpdatePage]._cardClass;
                    card_ModelArrey[i].gameObject.GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>().color = Color.white;
                    if (_pageBookModel._createDeckState == CreateDeckState.CreateDeck)// ���� ������ ��������� � ��������� ��������
                    {
                        if(cardSO[i + stepUpdatePage]._cardClass != _contentDeck_Model.ClassHeroInDeck)
                        {
                            card_ModelArrey[i].gameObject.GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>().color = Color.red;                            
                        }           

                        if (cardSO[i + stepUpdatePage]._cardClass == CardClasses.Universal)                        
                            card_ModelArrey[i].gameObject.GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>().color = Color.white;                        
                    }
                    
                    card_ModelArrey[i].IdCard = cardSO[i + stepUpdatePage].IdCard;
                    card_ModelArrey[i].ManaCostCard = cardSO[i + stepUpdatePage].ManaCostCard;
                    card_ModelArrey[i].AtackDamageCard = cardSO[i + stepUpdatePage].AtackDamageCard;
                    card_ModelArrey[i].HealthCard = cardSO[i + stepUpdatePage].HealthCard;
                    card_ModelArrey[i].NameCard = cardSO[i + stepUpdatePage].NameCard;
                    card_ModelArrey[i].DescriptionCard = cardSO[i + stepUpdatePage].DescriptionCard;
                    OnUpdatePageBook?.Invoke();
                }
            }            
        }
    }
}