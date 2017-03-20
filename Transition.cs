using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCTema4
{
    class Transition
    {
        public Transition()
        {

        }
        private InstantaneousDescription left;
        
        private List<NewDescription> right;

        public InstantaneousDescription Left
        {
            get
            {
                return left;
            }

            set
            {
                left = value;
            }
        }

        /// <summary>
        /// gets or sets the list with the possible values that delta function takes, given the instantaneous description left.
        /// </summary>
        public List<NewDescription> Right
        {
            get
            {
                return right;
            }

            set
            {
                right = value;
            }
        }
    }
}
