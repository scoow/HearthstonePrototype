using Hearthstone;
using System.Collections.Generic;

public interface IUpdate
{
    void UpdatePageBook(List<CardSO_Model> listSO, Card_Model[] arreyCardSettings, int id);
}