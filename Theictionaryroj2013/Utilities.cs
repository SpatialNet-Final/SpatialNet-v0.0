using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML;
using System.IO;
using System.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Accord;
using Accord.Statistics;
using Accord.Math;
using Accord.Math.Decompositions;
using Accord.Statistics.Analysis;
//using TheDictionaryTrainingSetProj2013.ArrayExtension;
//using Microsoft.ML.Models;


namespace TheDictionaryTrainingSetProj2013
{

    public static class ArrayExtension
    {

        public static double[][] ToDouble(this float[][] arr)
        {
            if (arr == null)
                return null;

            double[][] output = new double[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                output[i] = Array.ConvertAll(arr[i], x => (double)x);

                //for (int j = 0; j < arr[i].Length; j++)
                //{
                //    output[i][j]= (double) arr[i][j];
                //}

            }

            return output;

        }

        public static double[] ToDouble(this float[] arr) =>
                                         Array.ConvertAll(arr, x => (double)x);

    }


    static class  Utilities
    {
        public static double[][] ToDouble(float[][] arr)
        {
            if (arr == null)
                return null;

            double[][] output = new double[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                //output[i] = Array.ConvertAll(arr[i], x => (double)x);

                //for (int j = 0; j < arr[i].Length; j++)
                //{
                    output[i] = ToDouble1(arr[i]);
                //}

            }

            return output;

        }

        public static double[] ToDouble1( float[] arr)
        {
           return Array.ConvertAll<float, double>(arr, x => (double)x);
        }
                                         
        public static string Lemmatizations(string word)
        {
            //string result;
            //var er = new Sparc.TagCloud.TagCloudSetting();
            //var lmtz = er.Lemmatizer;
            //result = lmtz.Lemmatize(word);

            var gdstmr = new Iveonik.Stemmers.EnglishStemmer();
            return gdstmr.Stem(word);

            LemmaSharp.Lemmatizer lm = new LemmaSharp.Lemmatizer();
            //lm.
            var lx = LemmaSharp.LemmatizerPrebuiltCompact.GetLexicon(LemmaSharp.LanguagePrebuilt.English);

            //lx.

        }

        public static double[][] PCAReducedMatrix(float[][] input, int numOfDimensions = 2)
        {
            //float[,] errf = new float[input.Length,50];
            //Matrix.CopyTo<float>(input, errf);
            //errf.Convert<float, double>();
            //Matrix.Convert<float, double>(errf);
            //Matrix.tod

            //var data = Matrix.ToDouble(input);
            //Matrix.cop

            
            var pca = new PrincipalComponentAnalysis(ToDouble(input), AnalysisMethod.Standardize);
            
            pca.Compute();
            var finalData = pca.Transform(ToDouble(input), numOfDimensions);
            return finalData;
        }

         static double  errConvert(float err)
        {
            return (double)err;
        }


        private static IDataView glove_wiki_6b_MLNET_DataView = null;

        public static Dictionary<uint, string> Glove_Wiki_6b_MLNET(string word, int numClusters = 10, int depth = 1)
        {
            MLContext mlContext = new MLContext(seed: 0);

            if (glove_wiki_6b_MLNET_DataView == null)
            {
                var directory = Directory.GetCurrentDirectory();
                directory += @"\ConceptNet\";

                //Create ML Context with seed for repeteable/deterministic results

                TextLoader textLoader = mlContext.Data.CreateTextReader(new[]
                                                       {
                                                           new TextLoader.Column("Word", DataKind.Text, 0),
                                                           new TextLoader.Column("Vector", DataKind.Num, 1,50)

                                                       },
                                                           hasHeader: false,
                                                           separatorChar: ' '
                                                       );
                // STEP 1: Common data loading configuration
                //glove_wiki_6b_MLNET_DataView = mlContext.Data.ReadFromTextFile<Wiki_6b_vocabulary>(path: Path.Combine(directory, "glove.6B.50d.txt"), separatorChar: ' ', allowQuotedStrings: false, supportSparse: false);

                glove_wiki_6b_MLNET_DataView = textLoader.Read(Path.Combine(directory, "glove.6B.50d.txt"));


            }



            // Use KMeans clustering
            var trainer = mlContext.Clustering.Trainers.KMeans(features: "Vector", clustersCount: numClusters);
            var trainedModel = trainer.Fit(glove_wiki_6b_MLNET_DataView);

            var prediction = trainedModel.CreatePredictionEngine<Wiki_6b_vocabulary, ClusterPrediction>(mlContext);
            //glove_wiki_6b_MLNET_DataView.AsEnumerable<wiki_6b_vocabulary>().Where<wiki_6b_vocabulary>(x=>x.)

            bool err(int x) => true;
            var iterator = glove_wiki_6b_MLNET_DataView.GetRowCursor(err);

            Dictionary<uint, string> clusteredDectionary = new Dictionary<uint, string>();

            while (iterator.MoveNext())
            {
                string wrd = "";
                float[] vctr = new float[50];
                var itr0 = iterator.GetGetter<string>(0);
                var itr1 = iterator.GetGetter<float[]>(1);
                itr0.Invoke(ref wrd); itr1.Invoke(ref vctr);
                var vocb = new Wiki_6b_vocabulary() { Word = wrd, Vector = vctr };
                var prdct = new ClusterPrediction();

                prediction.Predict(vocb, ref prdct);
                clusteredDectionary.Add(prdct.PredictedLabel, wrd);
            }




            return clusteredDectionary;
        }

        public static double[][] PlotListString_PCA(List<string> wrdList, bool normalize=true)
        {
            //word2vec proxwm test
            

            LocalGetLibrary.Init_wiki_6b_vocab();
            float[][] vecList = new float[wrdList.Count*2][];
            
            int i = 0;
            //float[][] vecList vectemp = new float[2];
            List<bool> ert = new List<bool>();
            foreach (string wrd in wrdList)
            {
                i++;
                try
                {
                    
                    //ert.Add(LocalGetLibrary.wiki_6b_vocab.ContainsWord(wrd));
                    var wordsDistanceto = LocalGetLibrary.wiki_6b_vocab.Distance(wrd, 2);
                    foreach (var wrrd in wordsDistanceto)
                    {
                        vecList[i] = wrrd.Representation.NumericVector;
                    //vecList[i] = //LocalGetLibrary.wiki_6b_vocab.GetRepresentationFor().NumericVector;
                    }
                } catch(Exception e) { }
                

            }
           

            //if normalize
            if(normalize)
            {
               var norm = new Accord.Statistics.Filters.Normalization();
                //norm.Detect(Matrix.ToDouble(vecList));
                //var err = Matrix.ToDouble(vecList);
                return norm.Apply(Utilities.PCAReducedMatrix(vecList));

                //vecList.

                //Matrix.Normalize()
            }


            return Utilities.PCAReducedMatrix(vecList);
        }

        public class Wiki_6b_vocabulary
        {
            [Column(ordinal: "0", name: "Word")]
            public string Word { get; set; }

            [ColumnName("Vector")]
            [LoadColumn(1, 50)]//[VectorType(50)]
            public float[] Vector { get; set; }
        }


        public class ClusterPrediction
        {
            public uint PredictedLabel;
            public float[] Score;
        }


    }
}
