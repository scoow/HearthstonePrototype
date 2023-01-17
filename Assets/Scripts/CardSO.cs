using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card/CardExample", order =51)]
public class CardSO : ScriptableObject
{
    public string _name;
    public string _description;
    public GameObject _cardPrefab;
    public Sprite _spriteCard;

    public int _manaCost;
    public int _atackDamage;
    public int _healt;    
}