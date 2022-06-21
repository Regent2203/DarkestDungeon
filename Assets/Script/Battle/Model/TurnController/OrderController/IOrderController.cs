using System.Collections.Generic;

namespace DarkestDungeon.Battle
{
    public interface IOrderController
    {
        void CreateOrder();
        bool GetNextIndex(out int next);
    }
}