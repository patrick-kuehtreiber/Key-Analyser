using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Key_Analyser
{
    class A_Distance
    {
        public List<Ngraph> V1 { get; set; }
        public List<Ngraph> V2 { get; set; }
        public double T { get; set; }
        public A_Distance(List<Ngraph> v1, List<Ngraph> v2, double t)
        {
            V1 = v1;
            V2 = v2;
            T = t;
        }
        /// <summary>
        /// Calculates the absolute distance between two Lists<Ngraph>
        /// </summary>
        /// <returns>Absolute Distance A</returns>
        public double calc()
        {
            if (V1.Count == 0) return 0d;
            double t = T;

            double sim_cnt = 0d;
            double sim_val = 0d;
            foreach (Ngraph n1 in V1)
            {
                //use field amount for similarity:
                //1 if similar, 0 if not
                n1.Amount = 0;
                foreach (Ngraph n2 in V2)
                {
                    if (n2.Graph.Equals(n1.Graph))
                    {
                        sim_val = (double)Math.Max(n1.Duration, n2.Duration) / (double)Math.Min(n1.Duration, n2.Duration);
                        //Debug.WriteLine("simval: " + sim_val);
                        if (sim_val <= t)
                        {
                            n1.Amount = 1;
                            sim_cnt++;
                        }
                        break;
                    }
                }
            }

            //            Debug.WriteLine("sim: " + sim_cnt);
            //          Debug.WriteLine("cnt: " + V1.Count);
            double A = 1 - sim_cnt / V1.Count;
            return A;
        }
    }
}
