using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDictionaryTrainingSetProj2013
{
    class WordsIntuitiveCloud
    {
        
        int visual, emotional, rational;
        int rv, ev, er, all;

        public WordsIntuitiveCloud (string wrd)
        {
            Word = wrd;
        }

        public WordsIntuitiveCloud(int visual, int emotional, int rational)
        {
            Visual = visual;
            Emotional = emotional;
            Rational = rational;
        }

        public WordsIntuitiveCloud(int rv, int ev, int er, int all)
        {
            RV = rv;
            VE = ev;
            Er = er;
            All = all;
        }



        // the accessible propoerties for the 

        public string Word { get; set; }

        public int Visual
        {
            get
            {
                return visual;
            }
            set
            {
                visual = value;
            }
        }

        public int Emotional
        {
            get
            {
                return emotional;
            }
            set
            {
                emotional = value;
            }
        }

        public int Rational
        {
            get
            {
                return rational;
            }
            set
            {
                rational = value;
            }
        }

        public int RV
        {
            get
            {
                return rv;
            }
            set
            {
                rv = value;
                //rational = value;
            }
        }

        public int VE
        {
            get
            {
                return ev;
            }
            set
            {
                ev = value;
                //emotional = value;
            }
        }

        public int Er
        {
            get
            {
                return er;
            }
            set
            {
                er = value;
                //rational = value;
            }
        }

        public int All
        {
            get
            {
                return all;
            }
            set
            {
                all = value;
                //rational = value;
                //visual = value;
            }
        }
    }
}
