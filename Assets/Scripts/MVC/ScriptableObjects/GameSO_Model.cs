using UnityEngine;

public class GameSO_Model : ScriptableObject
{
    [SerializeField] private Sprite _spriteCard;
    [SerializeField] private int _idCard;

    public Sprite SpriteCard { get => _spriteCard; }
    public int IdCard { get => _idCard; }
}