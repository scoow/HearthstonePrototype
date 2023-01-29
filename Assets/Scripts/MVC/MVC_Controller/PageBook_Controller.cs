using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone
{
    [RequireComponent(typeof(PageBook_Model))]
    public class PageBook_Controller : MonoBehaviour, IReadable
    {         
        private PageBook_Model _pageBook_Model;
        private PageBook_View _pageBook_View;
        //private IUpdate _pageBook_View;
        private int _maxPageNumber;
        private int _minPageNumber = 0;
        private int _pageCounter = 0;
        

        private void Start()
        {
            _pageBook_Model = GetComponent<PageBook_Model>();            
            _pageBook_View = GetComponent<PageBook_View>();
            _pageBook_View.UpdatePageBook(_pageBook_Model._resultCollection , _pageBook_Model._cardTemplatePrefabs, _pageCounter);
        }       

        /// <summary>
        /// ��������� ��������
        /// </summary>
        public void NextPage()
        {
            if ((_pageBook_Model._resultCollection.Count % _pageBook_Model._cardTemplatePrefabs.Length) == 0)
                _maxPageNumber = _pageBook_Model._resultCollection.Count / _pageBook_Model._cardTemplatePrefabs.Length;
            else
                _maxPageNumber = _pageBook_Model._resultCollection.Count / _pageBook_Model._cardTemplatePrefabs.Length + 1;

            _pageCounter++;
            if (_pageCounter > _maxPageNumber)
                _pageCounter = _maxPageNumber;
            _pageBook_View.UpdatePageBook(_pageBook_Model._resultCollection, _pageBook_Model._cardTemplatePrefabs, _pageCounter);
        }

        /// <summary>
        /// ���������� ��������
        /// </summary>
        public void PreviousPage()
        {
            _pageCounter--;
            if (_pageCounter < _minPageNumber)
                _pageCounter = _minPageNumber;
            _pageBook_View.UpdatePageBook(_pageBook_Model._resultCollection, _pageBook_Model._cardTemplatePrefabs, _pageCounter);            
        }

        /// <summary>
        /// ������� ������ ����� �� ID
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public CardSO_Model GetCard(int cardId)
        {
            CardSO_Model cardSO_example = _pageBook_Model._cardsDictionary[cardId];
            return cardSO_example;
        }
    }   

}