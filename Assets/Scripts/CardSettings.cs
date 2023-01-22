using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hearthstone
{
    public class CardSettings : MonoBehaviour
    {
        private Text _name;
        private Text _description;
        public int _id;
        private SpriteRenderer _spriteCard;
        private Text _manaCost;
        private Text _atackDamage;
        private Text _healt;        

        public Text Name { get => _name;}
        public Text Description { get => _description;}
        public int Id { get => _id; set => _id = value; }
        public SpriteRenderer SpriteCard { get => _spriteCard; }
        public Text ManaCost { get => _manaCost;}
        public Text AtackDamage { get => _atackDamage;}
        public Text Healt { get => _healt;}

        private void Awake()
        {
            if (gameObject.tag == "CardTemplate")
            {
                _description = GetComponentInChildren<TextDiscriptionMarker>().GetComponent<Text>();
                _atackDamage = GetComponentInChildren<TextAtackMarker>().GetComponent<Text>();
                _healt = GetComponentInChildren<TextHealthMarker>().GetComponent<Text>();
                _spriteCard = GetComponentInChildren<SpriteRendererMarker>().GetComponent<SpriteRenderer>();
            }
            _name = GetComponentInChildren<TextCardNameMarker>().GetComponent<Text>();
            _manaCost = GetComponentInChildren<TextManaCostMarker>().GetComponent<Text>();

        }

    }
}