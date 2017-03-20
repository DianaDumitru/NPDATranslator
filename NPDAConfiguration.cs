using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCTema4
{
    class NPDAConfiguration
    {
        public NPDAConfiguration()
        {

        }
        public NPDAConfiguration(string state, string input_string, string stack_symbols, string output_string)
        {
            this.state = state;
            this.input_string = input_string;
            this.stack_symbols = stack_symbols;
            this.output_string = output_string;
        }
        public string state;
        public string input_string;
        public string stack_symbols;
        public string output_string;

    }
}
