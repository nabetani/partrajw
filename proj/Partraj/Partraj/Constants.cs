using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partraj
{
    static class Constants
    {
        internal const int randomSeed = 0;
        internal const bool highQuality = true;
        internal const int divisionInterval = 80;
        internal const int maxTime = (int)(divisionInterval * 8);
        internal const float accelaration = 1-2e-3f;
        internal const float divisionV0 = 1.05e-2f;
        internal const float imageRange = 2;
        internal const double forceBase = 3e-5;
        internal const double rotForce = 0;// Math.PI / 32;
        internal const double distBase = 0.75;
        internal const double penWidth = 2;
        internal const double colorPower = 50;
    }
}
