namespace Hearthstone
{
    public enum Players
    {
        First,
        Second
    }
    public enum CardClasses
    {
        Druid,
        Hunter,
        Mage,
        Paladin,
        Priest,
        Rogue,
        Shaman,
        Warlock,
        Warrior,
        Universal,
        Assist
    }
    public enum CardType
    {
        Minion,
        Spell,
        Weapon
    }
    public enum SceneType
    {
        Battle,
        ChooseHero,
        DeckBuilder,
        MainMenu
    }
    public enum MulliganCardPositionEnum
    {
        First,
        Second,
        Third,
        Fourth
    }
    public enum BattleCryType : byte
    {
        No�ry,
        DealDamage,
        RaiseParametrs,
        Heal,
        SummonAssistant,
        GetCardInDeck,
        PermanentEffect,
        ActivateAbility,
        EventEffect,
        SingleEffect
    }
    /// <summary>
    /// ��� ����� ��� ������� �����
    /// </summary>
    public enum Target : byte
    {
        None,
        Self,
        Single,
        SingleFriend,
        AllEnemies,
        AllFriends,
        All,
        MinionType,
        Hero,
        SingleFriendByCondition
    }
    public enum MinionType
    {
        None,
        Murloc,
        Beast
    }

    public enum AbilityCurrentCard
    {
        None,
        PermanentEffect,
        DivineShield,
        Provocation,
        Berserk,
        Charge,
        GetCard,
        ChangeHealt,
        ChangeAtack        
    }

    /// <summary>
    /// ������ �������� �������� ����/�������/��������� ���������
    /// </summary>
    public enum ChangeHealthType
    {
        Healing,
        DealDamage,
        RaiseParametrs
    }
    /// <summary>
    /// ���� �� ��� ��������� ����� - � ������, � ���� ��� �� �����
    /// </summary>
    public enum CardState
    {
        Deck,
        Mulligan,
        Hand,
        Board
    }

    /// <summary>
    /// ����� �������� ������
    /// </summary>
    public enum CreateDeckState
    {
        ChoiseHero,
        ChoiseDeck,
        CreateDeck,
    }
}