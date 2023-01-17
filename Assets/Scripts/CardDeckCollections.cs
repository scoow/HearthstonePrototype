using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CardDeckCollections", order = 51)]
public class CardDeckCollections : ScriptableObject
{
    /// <summary>
    /// ��������� ����� ����
    /// </summary>
    public Dictionary<string,int[]> _collections = new Dictionary<string,int[]>();

    /// <summary>
    /// ������ ����� ������
    /// </summary>
    /// <param name="nameDeck">�������� ������</param>
    /// <param name="deckValue">������ ID ����� + ID ����</param>
    public void SetCardInDeck(string nameDeck, int[] deckValue)
    {
        _collections.Add(nameDeck, deckValue);
    }
    /// <summary>
    /// �������� ������ ������ �� ��������
    /// </summary>
    /// <param name="nameDeck">�������� ������</param>
    /// <returns></returns>
    public int[] GetCardInDeck(string nameDeck)
    {
        int[] deckExample = _collections[nameDeck];
        return deckExample;
    }
}