using Syn.WordNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace TheDictionaryTrainingSetProj2013
{
    class LocalGetLibrary
    {
        public static Word2vec.Tools.Vocabulary wiki_6b_vocab = null;
        public static Word2vec.Tools.Vocabulary twitter_vocab = null;
        private static WordNetEngine wordNet=null;


        public static  List<string> WordnetGetAsync(string word, int depth = 1)
        {
            InitWordNet();

            //..........lemmatize the word parameter.......(revise)

            List<string> strng = new List<string>();
            try
            {
                var synSetList = wordNet.GetSynSets(word);


                foreach (var synSet in synSetList)
                {
                    strng.AddRange(synSet.Words);
                }

                strng = strng.Select(x => x.ToString()).Distinct().ToList<string>();

                if (depth > 1)
                {
                    var strngTemp1 = new List<string>(strng);
                    List<string> strngTemp2 = new List<string>();
                    int i = 1;

                    while (i < depth)
                    {
                        foreach (var wrd in strngTemp1)
                        {
                            synSetList = wordNet.GetSynSets(wrd);

                            foreach (var synSet2 in synSetList)
                            {
                                strng.AddRange(synSet2.Words);
                                strngTemp2.AddRange(synSet2.Words);
                            }
                        }
                        strngTemp2 = strngTemp2.Select(x => x.ToString()).Distinct().ToList<string>();
                        strngTemp1 = strngTemp2;
                        strngTemp2 = new List<string>();
                        ++i;
                    }
                }
            }
            catch (Exception e) { }

            strng = strng.Select(x => x.ToString()).Distinct().ToList<string>();
            return strng;



            //var lema = LemmaSharp.LemmatizerPrebuilt.GetLexicon(LemmaSharp.LanguagePrebuilt.English);
            //var lm = LemmaSharp.LemmatizerSettings.()
            // LzmaAlone.Properties.Settings()
            // RestSharp.
            //Sparc.TagCloud.

            //Abodit.ExpressionParser.Parser.

            //IEnumerable<AboditNLP.Lexeme> er = AboditNLP.FluentBuilder.CreateVerb(word);
            //foreach (AboditNLP.Lexeme lxm in er)
            //{
            //    strng.Add(lxm.Text);
            //    //lxm.Synset.Lexemes
            //}


        }

        public static void InitWordNet()
        {
            if (wordNet == null)
            {
                // the syn.wordnet library load (not the Abodit library, which is strangely more popular)
                var directory = Directory.GetCurrentDirectory();
                wordNet = new WordNetEngine();
                directory += @"\wordnet\";

                wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
                wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
                wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
                wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);

                wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
                wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
                wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
                wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);

                wordNet.Load();
            }
        }

        public static  List<string> Glove_Wiki_6b_GetAsync(string word, int distance = 10, int depth = 1)
        {
            Init_wiki_6b_vocab();

            List<string> strng = new List<string>();

            if (wiki_6b_vocab.ContainsWord(word))
            {
                var wordsDistanceto = wiki_6b_vocab.Distance(word, distance);
                foreach (var wrdDist in wordsDistanceto)
                {
                    strng.Add(wrdDist.Representation.WordOrNull);
                }


                //the cosine distance between the words
                //var sortedLst = new SortedList<double, string>();
                //var sortedLst2 = new SortedList<double, string>();
                /*var wordRep = wiki_6b_vocab.GetRepresentationFor(word);

                var wordVec = wordRep.NumericVector;
                var wordVecD = Array.ConvertAll<float, double>(wordVec, new Converter<float, double>(FDConverter));
                int i = 0; 
                foreach (var vecRep in vocabFin.Words)
                {
                    var vecRepD = Array.ConvertAll<float, double>(vecRep.NumericVector, new Converter<float, double>(FDConverter));
                    Accord.Math.Distance.Cosine(wordVecD, vecRepD);
                    sortedLst.Add(Accord.Math.Distance.Cosine(wordVecD, vecRepD), vecRep.WordOrNull);

                    i++;
                    
                    if (101- i == 1)
                    {
                        for (int j=0; j<80; j++)
                        {
                            if (sortedLst.Count>20)
                            { 
                        sortedLst.RemoveAt(20);
                            }
                            
                        }
                        i = 20;
                    }
                }

                strng.AddRange(sortedLst.Values);*/
            }
            return strng;
        }

        public static void Init_wiki_6b_vocab()
        {
            if (wiki_6b_vocab == null)
            {
                var directory = Directory.GetCurrentDirectory();
                var wordNet = new WordNetEngine();
                directory += @"\ConceptNet\";

                var streamReader = new StreamReader(Path.Combine(directory, "glove.6B.50d.txt"));

                var vocabReader = new Word2vec.Tools.Word2VecTextReader();
                wiki_6b_vocab = vocabReader.Read(streamReader);
            }
        }



        public static List<string> Glove_CommonCrawl_42b_GetAsync(string word, int distance = 10, int depth = 1)
        {
            var directory = Directory.GetCurrentDirectory();
            var wordNet = new WordNetEngine();
            directory += @"\ConceptNet\";

            var streamReader = new StreamReader(Path.Combine(directory, "glove.42B.300d.txt"));
            //streamReader.BaseStream.

            var vocabReader = new Word2vec.Tools.Word2VecTextReader();
            var vocabFin = vocabReader.Read(streamReader);

            List<string> strng = new List<string>();

            if (vocabFin.ContainsWord(word))
            {
                var wordsDistanceto = vocabFin.Distance(word, distance);
                foreach (var wrdDist in wordsDistanceto)
                {
                    strng.Add(wrdDist.Representation.WordOrNull);
                }
            }
            return strng;
        }

        public static List<string> Glove_CommonCrawl_42b_GetMlNet(string word, int distance = 10, int depth = 1)
        {
            var directory = Directory.GetCurrentDirectory();
            var wordNet = new WordNetEngine();
            directory += @"\ConceptNet\";

            var path = Path.Combine(directory, "glove.42B.300d.txt");
            //streamReader.BaseStream.

            //var vocabReader = new Word2vec.Tools.Word2VecTextReader();
            //var vocabFin = vocabReader.Read(streamReader);
            return new List<string>();
        }


            public static List<string> Glove_Twitter_GetAsync(string word, int distance = 10, int depth = 1)
        {
            Init_Glove_Twitter();

            List<string> strng = new List<string>();

            if (twitter_vocab.ContainsWord(word))
            {
                var wordsDistanceto = twitter_vocab.Distance(word, distance);
                foreach (var wrdDist in wordsDistanceto)
                {
                    strng.Add(wrdDist.Representation.WordOrNull);
                }
            }
            return strng;
        }

        public static void Init_Glove_Twitter()
        {
            if (twitter_vocab == null)
            {
                var directory = Directory.GetCurrentDirectory();
                var wordNet = new WordNetEngine();
                directory += @"\ConceptNet\";

                var streamReader = new StreamReader(Path.Combine(directory, "glove.twitter.27B.50d.txt"));

                var vocabReader = new Word2vec.Tools.Word2VecTextReader();
                twitter_vocab = vocabReader.Read(streamReader);
            }
        }

        public static List<string> ContextAlgoritmFin (string wrd, int wordNnetDepth=2, SynSetRelation[] synsRelation= null, int conceptNnetDepth=2, int nearstNeighborsGlove=100, bool wordnetonly=false, bool conceptnetonly=false, bool gloveonly=false)
        {
            var wrdList = new List<string>();

            

            // use wordnet first then conceptnet
            InitWordNet();
            if (synsRelation == null)
            {
                synsRelation = new SynSetRelation[] { SynSetRelation.InstanceHypernym, SynSetRelation.SimilarTo };
            }

            try
            {
                var synSetList = wordNet.GetSynSets(wrd);

                WrdNetDepthNavigate(synsRelation, wrdList, synSetList, wordNnetDepth);

                wrdList = wrdList.Select(x => x.ToString()).Distinct().ToList<string>();

               }

            catch (Exception er){ }
            if(wordnetonly)
            { return wrdList; }


            // use conceptnet first then conceptnet
            List<string> tempWrdListConcept = new List<string>();
            tempWrdListConcept = InternetGetLibrary.ConceptNetGetAsync(wrd, depth: 1);
            if (conceptnetonly)
            { return tempWrdListConcept; }

            wrdList.AddRange(tempWrdListConcept);
            // select similarity
            List<string> tempWrdList= new List<string>();
            Init_Glove_Twitter();
            Init_wiki_6b_vocab();
            foreach(string wrD in wrdList)
            {
                tempWrdList.AddRange( Glove_Twitter_GetAsync(wrD));
                tempWrdList.AddRange(Glove_Wiki_6b_GetAsync(wrD));
            }

            if (gloveonly)
            { return tempWrdList.Select(x => x.ToString()).Distinct().ToList<string>(); ; }
            wrdList.AddRange(tempWrdList);
            wrdList = wrdList.Select(x => x.ToString()).Distinct().ToList<string>();

            return wrdList;
        }


        //static int ind = 0;
        private static void WrdNetDepthNavigate(SynSetRelation[] synsRelation, List<string> wrdList, List<SynSet> synSetList, int depth=2)
        {
            //List<string> strng = new List<string>();

            //ind = 0;
            //foreach (var synSet in synSetList)
            //{
            //    wrdList.AddRange(synSet.Words);

            //var snsets = synSet.GetRelatedSynSets(synsRelation, false);
            //    //if(depth==ind) { ind--; return; }
            //    //ind++;
            //    //WrdNetDepthNavigate(synsRelation, wrdList, snsets, depth);

            //}
            //if (depth>1)
            //{
            //    var snsets = synSet.GetRelatedSynSets(synsRelation, false);

            //}


            int i = 0;
            //List<int> recording = new List<int>();
            List<string> strngTempAll1 = new List<string>();
            List<string> strngTempAll2 = new List<string>();
            while (i < depth)
            {
                // int j = 0;
                if (i == 0)
                {
                    List<string> strngTemp = new List<string>();
                    foreach (var synSet in synSetList)
                    {
                        
                            strngTemp.AddRange(synSet.Words);
                        
                    }
                    strngTemp = strngTemp.Select(x => x.ToString()).Distinct().ToList<string>();
                    strngTempAll1.AddRange(strngTemp);
                    wrdList.AddRange(strngTemp);
                }


                if (i > 0)
                {
                    var synSetTemp = new List<SynSet>();
                    foreach (var synSet in synSetList)
                    {
                        var senseserr = synSet.GetRelatedSynSets(synsRelation, false);
                        synSetTemp.AddRange(senseserr);
                        //var senses = wordNet.GetSynSets(err);
                        //var synStLst = senses.GetRelatedSynSets(synsRelation, false)(err);
                        List<string> strngTemp = new List<string>();
                        foreach (var sens in senseserr)
                        {
                           
                            
                                strngTemp.AddRange(sens.Words);
                            
                            
                        }
                        strngTemp = strngTemp.Select(x => x.ToString()).Distinct().ToList<string>();
                        strngTempAll2.AddRange(strngTemp);
                        wrdList.AddRange(strngTemp);
                    }

                    synSetList = synSetTemp;
                    strngTempAll1 = strngTempAll2;
                    strngTempAll2 = new List<string>();

                }
                 
                ++i;
            }

            return ;
        }


        /* private static double FDConverter(float input)
         {
             return (double)input;
         }*/
    }
}
