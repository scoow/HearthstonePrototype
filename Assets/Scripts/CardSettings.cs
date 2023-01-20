using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class CardSettings : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;
        //[SerializeField] private int _id;
        [SerializeField] private SpriteRenderer _spriteCard;
        [SerializeField] private Text _manaCost;
        [SerializeField] private Text _atackDamage;
        [SerializeField] private Text _healt;

        public Text Name { get => _name;}
        public Text Description { get => _description;}
        //public int Id { get => _id;}
        public SpriteRenderer SpriteCard { get => _spriteCard; }
        public Text ManaCost { get => _manaCost;}
        public Text AtackDamage { get => _atackDamage;}
        public Text Healt { get => _healt;}
    }
}