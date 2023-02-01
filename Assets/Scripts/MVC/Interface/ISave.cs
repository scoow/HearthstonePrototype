using System.Collections.Generic;

namespace Hearthstone
{
    public interface ISave
    {
        void SaveDeck(List<int> contentDeck);
    }
}