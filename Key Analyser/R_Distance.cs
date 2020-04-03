using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Key_Analyser
{
    class R_Distance
    {
        public List<Ngraph> V1 { get; set; }
        public List<Ngraph> V2 { get; set; }
        public R_Distance(List<Ngraph> v1, List<Ngraph> v2)
        {
            v1.Sort((x, y) => x.Duration.CompareTo(y.Duration));
            v2.Sort((x, y) => x.Duration.CompareTo(y.Duration));
            V1 = v1;
            V2 = v2;
        }
        /// <summary>
        /// Calculates the relative distance between two Lists<Ngraph>
        /// </summary>
        /// <returns>Relative Distance R</returns>
        public double calc()
        {
            //dis_max
            double dis_max = V1.Count * V1.Count / 2;
            //Debug.WriteLine("R: vi.count " + V1.Count);
            //Debug.WriteLine("R: dismax " + dis_max);
            dis_max = Math.Floor(dis_max);
            if (dis_max == 0) return 0d; //if V1.Count==1 then dis_max==0

            //disorder of V1 resp V2
            double dis = 0d;
            int cntv1 = 0;
            int cntv2 = 0;
            foreach (Ngraph n1 in V1)
            {
                foreach (Ngraph n2 in V2)
                {
                    if (n1.Graph.Equals(n2.Graph))
                    {
                        //Debug.WriteLine("R func graph: " + n2.Graph);
                        //Debug.WriteLine("R func cntv2: " + cntv2);
                        //Debug.WriteLine("R func cntv1: " + cntv1);
                        dis += Math.Abs(cntv2 - cntv1);
                        break;
                    }
                    cntv2++;
                }
                cntv1++;
                cntv2 = 0;
            }


            double R = dis / dis_max;
            //Debug.WriteLine("R in func: " + R);
            return R;
        }
    }

}
