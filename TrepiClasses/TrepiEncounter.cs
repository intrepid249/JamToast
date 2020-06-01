using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JamToast.TrepiClasses
{
    class TrepiEncounter
    {
        public List<String> Options { get; private set; }
        public List<String> Results { get; private set; }

        public TrepiEncounter()
        {
            Options = new List<String>();
            Results = new List<String>();
        }

        public void SetOptions(params String[] options)
        {
            // Clear the Options list of any data that may previously exist
            Options.Clear();

            // Append the elements of the input parameter list to the Options field
            for (int i = 0; i < options.Length; i++)
            {
                Options.Add(options[i]);
            }
        }

        public void SetResults(params String[] results)
        {
            // Clear the Results list of any data that may previously exist
            Results.Clear();

            // Append the elements of the input parameter list to the Results field
            for (int i = 0; i < results.Length; i++)
            {
                Results.Add(results[i]);
            }
        }

        public int GetAndDisplayOptionsChoice()
        {
            // Make sure the choices are on a new line
            Console.WriteLine();

            // Display the choices to the console
            for (int i = 0; i < Options.Count; i++)
            {
                Console.WriteLine((i+1) + " " + Options[i]);
            }

            bool validResult = false;
            int choiceIndex = -1;

            // Hang the input in a while loop to check for valid input
            while (!validResult)
            {
                Console.WriteLine("Type the number corresponding to the choice:");

                // Check to make sure the input in a number, and the number corresponds to an option in the list
                if (int.TryParse(Console.ReadLine(), out choiceIndex) && choiceIndex > 0 && choiceIndex <= Options.Count)
                {
                    validResult = true;
                }
            }

            // Return the index of the option chosen
            return choiceIndex - 1;
        }

    }
}
