using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardSystem
{
    public interface IPiece<TPosition>
    {
        void OnMoved(TPosition position);

        void OnTaken();
    }
}
