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
        Warrior,
        Universal
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
        NoСry,
        DealDamage,
        RaiseParametrs,
        Heal,
        SummonAssistant,
        GetCardInDeck,
        PermanentEffect,
        ActivateAbility,
        EventEffect
    }
    /// <summary>
    /// Тип целей для боевого клича
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
        Hero
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
}