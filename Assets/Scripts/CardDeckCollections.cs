using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardDeckCollections", order = 51)]
public class CardDeckCollections : ScriptableObject
{
    /// <summary>
    /// коллекция колод карт
    /// </summary>
    public Dictionary<string,int[]> _collections = new Dictionary<string,int[]>();

    /// <summary>
    /// создаём новую колоду
    /// </summary>
    /// <param name="nameDeck">название колоды</param>
    /// <param name="deckValue">список ID героя + ID карт</param>
    public void SetCardInDeck(string nameDeck, int[] deckValue)
    {
        _collections.Add(nameDeck, deckValue);
    }
    /// <summary>
    /// получаем нужную колоду по названию
    /// </summary>
    /// <param name="nameDeck">название колоды</param>
    /// <returns></returns>
    public int[] GetCardInDeck(string nameDeck)
    {
        int[] deckExample = _collections[nameDeck];
        return deckExample;
    }
}