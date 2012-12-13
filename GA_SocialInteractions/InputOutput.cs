using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;

namespace GA_SocialInteractions
{
    static class InputOutput
    {
        /* 
        Format of the input file:
        <m := #knapsacks> <n := #objects>
        <n values of objects>
        <m knapsack constraints>
        <A := nxm matrix of weights>
 
        <known optimum> 
        */

        public static int objectsNumber { get; set; }

        public static KnapsackList ReadInput(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(Path.GetFullPath(path));
                throw new FileNotFoundException();
            }

            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                string input = "";

                while ((s = sr.ReadLine()) != null)
                {
                    input += s;
                }

                string[] inputSplited = input.Split(new[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                int[] inputInt = new int[inputSplited.Length];

                for (int i = 0; i < inputSplited.Length; i++)
                {
                    inputInt[i] = Convert.ToInt32(inputSplited[i]);
                }

                int numberOfKnapsacks = inputInt[0];
                int numberOfObjects = inputInt[1];
                objectsNumber = numberOfObjects;

                int offset = 2;
                int[] values = new int[numberOfObjects];

                for (int i = 0; i < numberOfObjects; i++)
                {
                    values[i] = inputInt[offset + i];
                }

                offset += numberOfObjects;
                int[] constraints = new int[numberOfKnapsacks];

                for (int i = 0; i < numberOfKnapsacks; i++)
                {
                    constraints[i] = inputInt[offset + i];
                }

                offset += numberOfKnapsacks;
                int[][] weights = new int[numberOfKnapsacks][];

                for (int i = 0; i < numberOfKnapsacks; i++)
                {
                    weights[i] = new int[numberOfObjects];

                    for (int j = 0; j < numberOfObjects; j++)
                    {
                        weights[i][j] = inputInt[offset + i * numberOfObjects + j];
                    }
                }

                return makeKnapsackList(weights, values, constraints);
            }
        }

        private static KnapsackList makeKnapsackList(int[][] weights, int[] values, int[] constraints) {
            List<Knapsack> list = new List<Knapsack>();

            if (weights.Count() != constraints.Count()) {
                    throw new ArgumentException("Error in making KnapsackList - maybe wrong input");
            }
            for (int i = 0; i < weights.Count(); i++)
            {
                if (weights[i].Count() != values.Count())
                {
                    throw new ArgumentException("Error in making KnapsackList - maybe wrong input");
                }
            }
            for (int i = 0; i < weights.Count() ; i++) {
                int[] weightsOneKnapsack = new int[weights[i].Count()];
                for (int j = 0; j < weights[i].Count(); j++) {
                    weightsOneKnapsack[j] = weights[i][j];
                }
                list.Add(new Knapsack(weightsOneKnapsack, values, constraints[i]));
            }
            return new KnapsackList(list);
        }

        public static void WriteOutput(string path, Individual ind)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);  //dots instead of commas as decimal marks

            //StreamWriter(path) to overwrite the file, StreamWriter(path, true) to append
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.Write(ind.Value + " " + ind.fitness + " " + ind.strategy + " ");

                for (int i = 0; i < ind.chromosome.Count; i++)
                {
                    if (ind[i])
                        sw.Write("1 ");
                    else
                        sw.Write("0 ");
                }

                sw.WriteLine();
            }
        }

    }

}
