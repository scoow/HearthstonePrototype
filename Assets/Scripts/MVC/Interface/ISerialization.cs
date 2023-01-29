using System.Collections.Generic;

public interface ISerialization
{
    public void SaveDeck(List<int> contentDeck);
    public List<int> LoadDeck();
}