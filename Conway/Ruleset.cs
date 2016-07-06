using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conway
{
    public class Ruleset
    {
        private List<int> birthRules;
        private List<int> surviveRules;

        public List<int> BirthRules { get { return birthRules; } }
        public List<int> SurviveRules { get { return surviveRules; } }

        public Ruleset(IEnumerable<int> birth, IEnumerable<int> survive)
        {
            birthRules = birth.ToList();
            surviveRules = survive.ToList();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("B");
            foreach (int i in birthRules)
                builder.Append(i);
            builder.Append("/S");
            foreach (int i in surviveRules)
                builder.Append(i);

            return builder.ToString();
        }
    }
}
