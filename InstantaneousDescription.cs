using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCTema4
{
    class InstantaneousDescription
    {
        public InstantaneousDescription(string st,string read_symbol,string top_of_stack)
        {
            this.State = st;
            this.Input_ch = read_symbol;
            this.Stack_ch = top_of_stack;
        }
        public InstantaneousDescription()
        {

        }
        private string state;
        private string input_ch;
        private string stack_ch;

        /// <summary>
        /// delta(q,symbol,stacksymbol) state is q
        /// </summary>
        public string State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        /// <summary>
        /// delta(q,symbol,stacksymbol) input_ch ce simbol se citeste de pe banda de intrare
        /// input_ch este symbol
        /// </summary>
        public string Input_ch
        {
            get
            {
                return input_ch;
            }

            set
            {
                input_ch = value;
            }
        }

        /// <summary>
        /// delta(q,symbol,stacksymbol) stack_ch este ce se gaseste in varful stivei
        /// </summary>
        public string Stack_ch
        {
            get
            {
                return stack_ch;
            }

            set
            {
                stack_ch = value;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            InstantaneousDescription id = (InstantaneousDescription)obj;
            return (state.Equals(id.state)) && (input_ch.Equals(id.input_ch))
                && (stack_ch.Equals(id.stack_ch));
        }
    }
}
