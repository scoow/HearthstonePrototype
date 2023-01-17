using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DeckCards", menuName = "Card/CardsDeck", order = 51)]
public class CardPackSO : ScriptableObject
{
    public List<int> _cardDeckList;
    public string _cardDeckName;
    public int _heroIndex;
}