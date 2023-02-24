using UnityEngine;
using Zenject;

namespace Hearthstone
{
    /// <summary>
    /// ������ �� ����� ������� ������� ���� ��� ��������� ������
    /// </summary>
    public class HandManager : MonoBehaviour
    {
        [Inject]
        private readonly LoadDeck_Controller _loadDeckController;        
        private Transform _playerDeck;
        private Transform _enemyDeck;
        [SerializeField]
        private GameObject _cardPrefab;
        [Inject]
        private readonly PageBook_Model _pageBook_Model;
        [Inject]
        private readonly MulliganManager _mulliganManager;
        private void Start()
        {
            _playerDeck = GameObject.Find("PlayerDeck").transform;
            _enemyDeck = GameObject.Find("EnemyDeck").transform;
            int layerStep = 0;
           // _mulliganManager = FindObjectOfType<MulliganManager>();zenject
            foreach (int i in _loadDeckController._activeDeck)
            {
                var newCard = Instantiate(_cardPrefab, _playerDeck.position, _playerDeck.rotation);
                newCard.transform.parent = _mulliganManager.transform;
                var battlemodeCardView = newCard.GetComponent<BattleModeCard_View>();
                var battleModeCard = newCard.GetComponent<BattleModeCard>();

                var newCardModel = newCard.GetComponent<Card_Model>();
                var newlayerRenderUp = newCard.GetComponent<LayerRenderUp>();
                newlayerRenderUp.LayerUp(layerStep);
                layerStep--;
                var newCardView = newCard.GetComponent<Card_View>();
                newCardModel.SetCardSettings(i);//������� � ��� ����� ��� �������� ��������
                battlemodeCardView.SetSettingsCardInBattle();
                newCardView.ChangeViewCard();
                battleModeCard.SetSide(Players.First);
            }

            foreach (int i in _loadDeckController._activeDeck)
            {
                var newCard = Instantiate(_cardPrefab, _enemyDeck.position, _enemyDeck.rotation);
                newCard.transform.parent = _mulliganManager.transform;
                var battlemodeCardView = newCard.GetComponent<BattleModeCard_View>();
                var battleModeCard = newCard.GetComponent<BattleModeCard>();

                var newCardModel = newCard.GetComponent<Card_Model>();
                var newlayerRenderUp = newCard.GetComponent<LayerRenderUp>();
                newlayerRenderUp.LayerUp(layerStep);
                layerStep--;
                var newCardView = newCard.GetComponent<Card_View>();
                newCardModel.SetCardSettings(i);//������� � ��� ����� ��� �������� ��������
                battlemodeCardView.SetSettingsCardInBattle();
                newCardView.ChangeViewCard();
                battleModeCard.SetSide(Players.Second);
            }
        }
    }
}