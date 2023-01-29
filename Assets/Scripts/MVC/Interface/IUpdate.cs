using Hearthstone;
using System.Collections.Generic;

public interface IUpdate
{
    void UpdatePageBook(List<CardSO_Model> listSO, CardSettings_Model[] arreyCardSettings, int id);
}