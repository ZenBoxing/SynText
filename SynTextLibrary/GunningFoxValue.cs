using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynTextLibrary
{
    public class GunningFoxValue
    {
        public int FoxIndex { get; set; }
        public string ReadingLevelByGrade { get; set; }

        public GunningFoxValue(int foxIndex, string readingLevelByGrade)
        {
            FoxIndex = foxIndex;
            ReadingLevelByGrade = readingLevelByGrade;
        }
    }
}
