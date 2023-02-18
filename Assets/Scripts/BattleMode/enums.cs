namespace Hearthstone
{
    public enum Players
    {
        First,
        Second
    }
    public enum Classes
    {
        Druid,
        Hunter,
        Mage,
        Paladin,
        Priest,
        Rogue,
        Shaman,
        Warlock,
        Warrior
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
        HaveCharge
    }
    /// <summary>
    /// ��� ����� ��� ������� �����
    /// </summary>
    public enum BattleCryTargets : byte
    {
        None,
        Self,
        Single,
        SingleFriend,
        AllEnemies,
        AllFriends,
        All,
        MinionType
    }
    public enum MinionType
    {
        None,
        Murloc,
        Beast
    }
}