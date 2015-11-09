using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordAnalysis
{
    class MainProgram
    {
        static void Main()
        {
            bool runprogram = true;
            // 1- bool that allows the program to loop so mutiple analysis can be completed in one program session.
            while (runprogram == true)
            {
                string userchoice, userinput, userspaces = "", longtext;
                // 1- string that stores the users choice.
                // 2- string that holds the users string input.
                // 3- string that removes the spaces from the input allowing charaters to be counted.
                // 4- a string that contains the long words within the inputted string.

                int charcount = 0, sentencecount = 0, wordcount = 0, wordvowels = 0, wordconsonant = 0;
                // 1- a counter used to calculate the charaters within the inputted string.
                // 2- a counter used to calculate the sentences within the inputted string
                // 3- a counter used to calculate the words within the inputted string.
                // 4- a counter to calculate the amount of vowels within the inputted string.
                // 5- a counter to calculate the amount of consonants within the inputted string.

                double wordaverage = 0, averagelength = 0, letteraverage = 0;
                // 1- double that stores the average word count.
                // 2- double that stores the average word length.
                // 3- double that stores the average letter count.

                string loadedtext = @"E:\My Documents\Github\TextAnalysis\TextAnalysis\loadedtext.txt";
                string longtextfile = @"E:\My Documents\Github\TextAnalysis\TextAnalysis\longtext.txt";
                // 1- file used so the user can input long strings of text.
                // 1- file that contains words over 7 letters long.

                Console.WriteLine("Please input an option.");// the console asks the user to input their choice of option.
                Console.WriteLine("Press 1 to input your own string.");// explains choice 1.
                Console.WriteLine("Press 2 to import a text file.");// explains choice 2.
                Console.WriteLine("Press q to terminate the program.");// explains choice q.
                Console.WriteLine("Please ensure that you put a full stop at the end of every sentence.");// reminds them to use grammatical correctness to aid the programs working.

                userchoice = Convert.ToString(Console.ReadLine());// converts the numercal choice of the user into a string value.

                try// a try statment to stop the program from crashing when an invalid input is made.
                {
                    switch (userchoice)// a switch statment based on the users choices. 1, 2 or an invalid character.
                    {
                        case "1":// code for user choice 1.

                            Console.WriteLine("Please input your text\n");// the console asks the user to type in their string data.
                            userinput = (Console.ReadLine());//assignes the string variable "userinput" to the inputted string.

                            split(userinput, ref wordcount, ref averagelength);// calls the method "split" to split the string and find its length and average word length.

                            analysis(userinput, ref userspaces, ref charcount, ref sentencecount, ref wordvowels, ref wordconsonant);// calls the method "analysis" to analyse the users string data.
                            average(charcount, sentencecount, wordcount, ref wordaverage, ref letteraverage);// calls the method "average" to determine the average letters and words witihin the string.

                            display(charcount, sentencecount, wordcount, wordvowels, wordconsonant, wordaverage, averagelength, letteraverage);// calls the method that displays the analysis of the text.

                            break;//passes statment on to the next part of the code.

                        case "2":// code for user choice 2.

                            userinput = File.ReadAllText(loadedtext);// sets the string value "user input" to the text file "loaded text".
                            Console.WriteLine("Loaded text is: \n\n{0}\n", userinput);// displys to the user witihin the program what the loaded text says.

                            split(userinput, ref wordcount, ref averagelength);// calls the method "split" to split the string and find its length and average word length.

                            string[] length = userinput.Split(' ');// creates an array called "length" and assigns the values of the split "user input" to it.

                            foreach (string x in length)// a foreach loop that runs for each string assigned to the array "length"
                                if (x.Length >= 7)// if the length of string "x" is greater then or equal to 7 execute the statment within.
                                {
                                    longtext = x;// assigns the string value "longtext" to the word in string "x" that is greater or equal to 7.
                                    File.AppendAllText(longtextfile, longtext);// writes each of the words equal or greater than 7 to the the file associated with the string "longtextfile".                                
                                }

                            analysis(userinput, ref userspaces, ref charcount, ref sentencecount, ref wordvowels, ref wordconsonant);// calls the method analysis to analyse the users string data.
                            average(charcount, sentencecount, wordcount, ref wordaverage, ref letteraverage);// calls the method "average" to determine the average letters and words witihin the string.

                            display(charcount, sentencecount, wordcount, wordvowels, wordconsonant, wordaverage, averagelength, letteraverage);// calls the method that displays the analysis of the text.

                            break;//passes statment on to the next part of the code.

                        case "q"://code choice for user choice q

                            runprogram = false;//sets run program to false, terminating the program.
                            Environment.Exit(0);// exits the program fully.
                            break;//passes statment on to the next part of the code.

                        default:// the default case for when either numer 1 or 2 has not been entered.

                            Console.WriteLine("That is not a valid input");//makes the console display "That is not a valid input".
                            break;

                    }
                }
                catch// when an error is found the catch statment runs instead of the program crashing.
                {
                    Console.WriteLine("Exception Handled - Crash");// the console displays "Exception Handled - Crash" when the catch statment prevents a crash.
                }
            }

            Console.ReadLine();

        }
        private static void display(int charcount, int sentencecount, int wordcount, int wordvowels, int wordconsonant, double wordaverage, double averagelength, double letteraverage)// a method that collates all the numerical text analysis.
        {
            Console.WriteLine("\nThere are {0} characters within your inputted string.\n", charcount);// tells the user how many characters there are within the inputted string.
            Console.WriteLine("There are {0} sentences within your inputted string.\n", sentencecount);// tells the user how many sentences there are within the inputted string.
            Console.WriteLine("There are {0} words within your inputted string.\n", wordcount);// tells the user how many words there are within the inputted string.
            Console.WriteLine("The average words per sentence is {0}.\n", wordaverage);// tells the user the average words per sentence that are within the inputted string.
            Console.WriteLine("The average length of word is {0}.\n", averagelength);// tells the user the average length of word that is within the inputted string.
            Console.WriteLine("The average letters per sentence is {0}.\n", letteraverage);// tells the average letters per sentence that is within the inputted string.
            Console.WriteLine("There are {0} vowels within the inputted string.\n", wordvowels);// tells the user how many vowels there are within the inputted string.
            Console.WriteLine("There are {0} consonants within the inputted string.\n", wordconsonant);// tells the user how many consonants there are within the inputted string. 
        }

        private static void split(string userinput, ref int wordcount, ref double averagelength)// a method that splits the inputted por loaded string then finds its length and average length of word.
        {
            wordcount = userinput.Split(' ').Length;// sets the integer value "wordcount" to the numerical count of each space within the string.
            averagelength = Math.Round(userinput.Split(' ').Average(s => s.Length), 2);// sets in interger value "averagelength" to the average character length in between spaces then rounds it to 2dp.
        }

        private static void average(int charcount, int sentencecount, int wordcount, ref double wordaverage, ref double letteraverage)// a method that works out the average number of words and letters in the string.
        {
            wordaverage = wordcount / sentencecount;// divides the amount of words by the amount of sentences to calculate the average.                     
            letteraverage = charcount / sentencecount;// divides the amount of letters by the amount of sentences calculate the average. 

        }

        private static void analysis(string userinput, ref string userspaces, ref int charcount, ref int sentencecount, ref int wordvowels, ref int wordconsonant)// a method that analyses the users inputted or loaded string.
        {
            foreach (char s in userinput)// a foreach loop that removes spaces from the the string and counts the characters in the string.
            {
                if (s == ' ')// if a character within the string is a space, it passes control of the statment onto the next section of code.
                    continue;// this passes control of the statment onto the next section of code when the if statment avove is true.
                {
                    userspaces += s;// adds the value of spaces to the values of spaces and characters.
                    if (s == '.')// if a character within the string is a full stop then execute the statment within.
                    {
                        charcount--;// subtracts 1 from the counter when a full stop is used.                                    
                        sentencecount++;// adds one to the sentence counter.

                    }
                    charcount++;// adds 1 to the character counter.
                }

                if (s == ',')// if a character witihin the string is a comma, then execute the statment within.
                {
                    charcount--;// substracts one from the character counter.
                }

                switch (s)// a switch statment that counts the amount of vowels and consonants within the inputted string.
                {
                    case 'A':// if the character within the string is 'A'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'a':// if the character within the string is 'a'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'E':// if the character within the string is 'E'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'e':// if the character within the string is 'e'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'I':// if the character within the string is 'I'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'i':// if the character within the string is 'i'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'O':// if the character within the string is 'O'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'o':// if the character within the string is 'o'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'U':// if the character within the string is 'U'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    case 'u':// if the character within the string is 'u'
                        wordvowels++;// adds one to the vowel count.
                        break;//passes statment on to the next part of the switch.
                    default:// if the characters inputted are not vowels then they must be consonants, so the default adds one to the consonant counter.
                        wordconsonant++;
                        if (s == ' ')// if a character witihin the string is a space, then execute the statment within.
                        {
                            wordconsonant--;// subtracts one from the consonant count.
                        }
                        if (s == '.')// if a character witihin the string is a full stop, then execute the statment within.
                        {
                            wordconsonant--;// subtracts one from the consonant count.
                        }
                        break;//passes statment on to the next part of the switch.
                }
            }
        }
    }
}
