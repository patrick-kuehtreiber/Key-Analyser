using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Key_Analyser
{
    class Ngraph
    {
        public string Graph { get; set; } //The actual ngraph of this object
        public int Duration { get; set; }//The duration of the ngraph (averaged)
        public int Amount { get; set; }//how often the n-graph was entered - used to calculate averages
        public Ngraph(string graph, int duration, int amount)
        {
            Graph = graph;
            Duration = duration;
            Amount = amount;
        }

        /// <summary>
        /// Calculates the distance R between this and Ngraph n
        /// </summary>
        /// <param name="n">New ngraph to compare with</param>
        /// <returns>The distance R</returns>
        public double calcDistanceR(Ngraph n)
        {
            return 0d;
        }


        /// <summary>
        /// Calculates the distance A between this and Ngraph n
        /// </summary>
        /// <param name="n">New ngraph to compare with</param>
        /// <returns>The distance A</returns>
        public double calcDistanceA(Ngraph n)
        {
            return 0d;
        }

    }
}
