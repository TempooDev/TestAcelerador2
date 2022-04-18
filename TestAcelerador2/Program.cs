using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace TestAcelerador2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
       
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "inputs.txt")))
            {
                for (int i = 0; i < 1000; i++)
                {
                    for (int j = 0; j < 1024; j++)
                    {
                        outputFile.Write((r.NextDouble()*2 -1 ) + " ");
                    }
                    outputFile.WriteLine();
                }
            }

            List<Tuple<int, double>> resultados = new List<Tuple<int, double>>();
            
            foreach(string line in System.IO.File.ReadLines(Path.Combine(docPath, "inputs.txt")))
            {
                string[] nums = line.Split(' ');
                double[] S =new double[1024];
                double[] T = new double[16];
                int i = 0;
                foreach (var num  in nums)
                {
                    if(num != "") { 
                        S[i] = Convert.ToDouble(num, CultureInfo.InvariantCulture);
                    }
                    i++;
                }
                resultados.Add(fir(S, T));
            }

            Tuple<int,double>[] res = resultados.ToArray();
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "outputs.txt")))
            {
                for (int i = 0; i < 1000; i++)
                { 
                   outputFile.WriteLine(res[i].Item1 +" "+res[i].Item2);
                }
            }
        }

        private static Tuple<int, double> fir(double[] s, double[] t)
        {
            int i=0;
            double err=double.MaxValue;
            double min = 0.0;
            for (int j = 0; j < 1024-16; j++)
            {
                for (int r = 0; r < 16 - 1; r++)
                {
                    min += (s[j + r] - t[r]) *(s[j + r] - t[r]);
                }
                if (min < err)
                {
                    err = min;
                    i = j;
                }
                min = 0.0;
            }
            return new Tuple<int,double>(i, err);
        }
    }
}