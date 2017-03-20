using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCTema4
{
    class NewDescription
    {
        public NewDescription()
        {

        }
        public NewDescription(string nstate,string ntop, string output)
        {
            this.new_state = nstate;
            this.new_top_of_stack = ntop;
            this.output = output;
        }
        private string new_state;
        private string new_top_of_stack;
        private string output;
        /// <summary>
        /// the state in whish the NPDA goes
        /// </summary>
        public string New_state
        {
            get
            {
                return new_state;
            }

            set
            {
                new_state = value;
            }
        }

        /// <summary>
        /// what the top of the stack will contain after the current transaction
        /// </summary>
        public string New_top_of_stack
        {
            get
            {
                return new_top_of_stack;
            }

            set
            {
                new_top_of_stack = value;
            }
        }

        /// <summary>
        /// the output string
        /// </summary>
        public string Output
        {
            get
            {
                return output;
            }

            set
            {
                output = value;
            }
        }
    }
}
