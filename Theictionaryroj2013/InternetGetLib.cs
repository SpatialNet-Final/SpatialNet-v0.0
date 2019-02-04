using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syn.WordNet;
using System.Collections.Specialized;

namespace TheDictionaryTrainingSetProj2013
{
    public enum Owner : byte { Natural=0, Artificial=1, WordEmebeding=2, Books=4, Wiki=8, WordNet=16, }
   
    
    
    class InternetGetLibrary
    {
        

         public static List<string> ConceptNetGetAsync (string word, string relation="", int depth=1)
        {
            List<string> strng = new List<string>();
            var wrapper = new ConcepnetDotNet.ConceptNetWrapper("http://api.conceptnet.io", "en", true);
            
            var cncpt = wrapper.GetConceptNetQueryResults("node=/c/en/"+word);
            
            int i =0;
            //List<int> recording = new List<int>();
            List<string> strngTempAll1 = new List<string>();
            List<string> strngTempAll2 = new List<string>();
             while (i<depth)
             {
                // int j = 0;
            if(i==0)
            {
                List<string> strngTemp = new List<string>();
                foreach (var str in cncpt.edges)
                {
                    if (str.start.language == "en")
                    {
                        strngTemp.Add(str.end.label);
                    }
                }
                strngTemp = strngTemp.Select(x => x.ToString()).Distinct().ToList<string>();
                strngTempAll1.AddRange(strngTemp);
                strng.AddRange(strngTemp);
            }


                 if(i>0)
            {
                     foreach(string err in strngTempAll1)
                     {
                         cncpt = wrapper.GetConceptNetQueryResults("node=/c/en/" + err);
                         List<string> strngTemp = new List<string>();
                         foreach (var str in cncpt.edges)
                         {
                             if (str.start.language == "en")
                             {
                                 strngTemp.Add(str.end.label);
                             }
                         }
                         strngTemp = strngTemp.Select(x => x.ToString()).Distinct().ToList<string>();
                         strngTempAll2.AddRange(strngTemp);
                         strng.AddRange(strngTemp);
                     }
                     strngTempAll1 = strngTempAll2;
                     strngTempAll2= new List<string>();          
               
            }
            
            ++i;
             }

             return strng;
        }

       


       /* public static List<string> WikiSharpGetAsync (string word, int depth=1)
        {
            //w
        }*/
    }
}
