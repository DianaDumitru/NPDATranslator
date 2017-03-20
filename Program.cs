using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCTema4
{
    class Program
    {
        static string path = @"Inputs.txt";
        static string line;
        static List<string> states;
        static string initial_state;
        static List<string> final_states;
        static List<string> input_alphabet;
        static List<string> output_alphabet;
        static List<string> stack_alphabet;
        static List<Transition> transitions;
        static Transition auxiliary;
        static string initial_stack_symbol;
        static string lambda = "L";
        static int contor = 0;
        static void Main(string[] args)
        {

            using (System.IO.StreamReader file =
                new System.IO.StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    switch (line)
                    {
                        case "States:":
                            line = file.ReadLine();
                            states = new List<string>(line.Split(' '));
                            break;
                        case "Initial state:":
                            initial_state = file.ReadLine();
                            break;
                        case "Final states:":
                            final_states = new List<string>(file.ReadLine().Split(' '));
                            break;
                        case "Input alphabet:":
                            input_alphabet = new List<string>(file.ReadLine().Split(' '));
                            break;
                        case "Output alphabet:":
                            output_alphabet = new List<string>(file.ReadLine().Split(' '));
                            break;
                        case "Stack alphabet:":
                            stack_alphabet = new List<string>(file.ReadLine().Split(' '));
                            break;
                        case "Initial stack symbol:":
                            initial_stack_symbol = file.ReadLine();
                            break;
                        case "Transitions:":
                            transitions = new List<Transition>();
                            while ((line = file.ReadLine()) != null)
                            {
                                List<string> pieces = new List<string>(line.Split(' '));
                                auxiliary = new Transition();
                                auxiliary.Left = new InstantaneousDescription(pieces.ElementAt(0), pieces.ElementAt(1), pieces.ElementAt(2));
                                auxiliary.Right = new List<NewDescription>();
                                //in case delta has more then one value, read all new descriptions
                                for (int i = 3; i < pieces.Count; i += 3)
                                {
                                    NewDescription nd = new NewDescription(pieces[i], pieces[i + 1], pieces[i + 2]);
                                    auxiliary.Right.Add(nd);
                                }
                                transitions.Add(auxiliary);
                            }
                            break;
                        default:
                            Console.WriteLine("Something went wrong while reading the inputs");
                            break;
                    }

                }
            }

            while (true)
            {
                Console.WriteLine("Please enter a string over this alphabet: {0}, or write STOP for exit:",String.Join(String.Empty,input_alphabet));
                string input = Console.ReadLine();
                if (input.ToUpper().Equals("STOP"))
                    break;
                if (CheckInput(input) == false)
                {
                    Console.WriteLine("The letters in the string you entered are not in:{0}\n",String.Join(" ", input_alphabet));
                }
                else
                {

                    contor = 0;
                    Queue<NPDAConfiguration> queue = new Queue<NPDAConfiguration>();
                    NPDAConfiguration e = new NPDAConfiguration(initial_state, input, initial_stack_symbol, String.Empty);
                    queue.Enqueue(e);
                    while (queue.Count != 0)
                    {
                        NPDAConfiguration current = queue.Dequeue();
                        if (current.input_string != String.Empty)
                        {
                            InstantaneousDescription delta_left = new InstantaneousDescription(current.state,
                                current.input_string[0].ToString(), current.stack_symbols[0].ToString());

                            //save in queue all new configurations where the NPDA can arrive starting from delta_left
                            foreach (Transition tr in transitions)
                            {
                                if (tr.Left.Equals(delta_left))
                                {
                                    foreach (NewDescription nd in tr.Right)
                                    {
                                        string new_top_of_stack;
                                        if (current.stack_symbols.Equals(initial_stack_symbol))
                                        {
                                            if (nd.New_top_of_stack.Equals(lambda))
                                                new_top_of_stack = initial_stack_symbol;
                                            else
                                                new_top_of_stack = nd.New_top_of_stack;
                                        }
                                        else
                                        {
                                            if (nd.New_top_of_stack.Equals(lambda))
                                                new_top_of_stack = current.stack_symbols.Substring(1);
                                            else
                                                new_top_of_stack = nd.New_top_of_stack + current.stack_symbols.Substring(1);
                                        }

                                        NPDAConfiguration new_configuration = new NPDAConfiguration(nd.New_state,
                                            current.input_string.Substring(1),
                                            new_top_of_stack, nd.Output + current.output_string);
                                        queue.Enqueue(new_configuration);
                                    }
                                }

                            }

                        }
                        //save in queue all new configurations where the NPDA can arrive using lambda transitions
                        InstantaneousDescription delta_left_lambda = new InstantaneousDescription(current.state,
                                lambda, current.stack_symbols[0].ToString());
                        
                        foreach (Transition tr in transitions)
                        {
                            if (tr.Left.Equals(delta_left_lambda))
                            {
                                foreach (NewDescription nd in tr.Right)
                                {
                                    string new_top_of_stack;
                                    if (current.stack_symbols.Equals(initial_stack_symbol))
                                    {
                                        if (nd.New_top_of_stack.Equals(lambda))
                                            new_top_of_stack = initial_stack_symbol;
                                        else
                                            new_top_of_stack = nd.New_top_of_stack;
                                    }
                                    else
                                    {
                                        if (nd.New_top_of_stack.Equals(lambda))
                                            new_top_of_stack = current.stack_symbols.Substring(1);
                                        else
                                            new_top_of_stack = nd.New_top_of_stack + current.stack_symbols.Substring(1);
                                    }
                                    NPDAConfiguration new_configuration = new NPDAConfiguration(nd.New_state,
                                        current.input_string,
                                        new_top_of_stack, nd.Output + current.output_string);
                                    queue.Enqueue(new_configuration);
                                }
                            }
                        }
                        if (final_states.Contains(current.state) && current.input_string.Length == 0)
                        {
                            contor++;
                            Console.WriteLine("{0} Output string: {1}", contor, new string(current.output_string.Replace('L', ' ').Trim().Reverse().ToArray()));

                        }

                    }


                }

            }
            Console.ReadKey();
        }

        private static bool CheckInput(string input)
        {
            foreach (char ch in input)
            {
                if (!input_alphabet.Any(x => x.Contains(ch)))
                {
                    return false;
                }
            }
            return true;
        }
    }


}
