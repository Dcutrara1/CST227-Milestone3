using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestone_1___Grid_Based_Game
{
    interface IPlayable
    {
        void GetCount();
        void BuildBoard(int Rows, int Cols);
        void ActivateBombs(int Rows, int Cols);
        void LocateNeighbors(int Rows, int Cols);
    }
}
