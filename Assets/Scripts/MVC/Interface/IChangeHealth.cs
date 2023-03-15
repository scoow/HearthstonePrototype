namespace Hearthstone
{
    public interface IChangeHealth
    {
        public void ChangeHealthValue(int incomingValue, ChangeHealthType changeHealthType);

    }
}