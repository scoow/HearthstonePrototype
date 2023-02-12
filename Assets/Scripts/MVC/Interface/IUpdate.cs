using System.Collections.Generic;
namespace Hearthstone
{
    public interface IUpdate
    {
        void UpdatePageBook(List<CardSO_Model> listSO, Card_Model[] arreyCardSettings, int id);
    }
}