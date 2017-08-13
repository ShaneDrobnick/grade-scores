using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;

namespace grade_scores
{
    public class Program
    {
        static void Main(string[] args)
        {

            string[] commandline = Environment.GetCommandLineArgs();
            try
            {

                if (File.Exists(commandline[1]))
                {
                    string fileDirectory = string.Empty;


                    List<string> fileLines = new List<string>();


                    var fileStream = new FileStream(commandline[1], FileMode.Open, FileAccess.Read);


                    using (var StreamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string singleLine;
                        while ((singleLine = StreamReader.ReadLine()) != null)
                        {
                            fileLines.Add(singleLine);
                        }
                    }

                    List<Name> names = MakeNameList(fileLines);
                    WriteNamesToFile(names, commandline[1]);

                }
                else
                {
                    throw new FileNotFoundException();

                }
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine("No file name given at commandline please give a file location - Exiting Program");
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("File not found - {0}  - Exiting Program", commandline[1]);
            }
            catch(FileFormatIncorrectException ex)
            {
                Console.WriteLine("Format of Input file is incorrect  - Exiting Program");
            }
            catch (ScoreValueIncorrectFormat ex)
            {
                Console.WriteLine("Score value input is not a number  - Exiting Program"); ;
            }

        }
        //Method Writes array to file
        private static void WriteNamesToFile(List<Name> names,string filename)
        {
            /*
             * assumption specifications does not give an outfile location, since there is none 
             * using the input location as the output
             */
            
            using (StreamWriter writer = new StreamWriter(filename+"-graded.txt"))
            {
                Console.WriteLine();
                foreach ( Name name in names)
                {
                    string temp = name.Last + "," + name.First + "," + name.Score.ToString();
                    writer.WriteLine(temp);
                    Console.WriteLine(temp);
                    Console.WriteLine();
 
                }

                Console.WriteLine("Finished: created {0}",filename+"-graded.txt");
            }

        }
        //Sorts list in order for highest score
        // Bubble sorting  yes not efficient but no specs given on efficiency 
        private static List<Name>Sort(List<Name> names)
        {
            Name temp;

            for ( int i = 0; i < names.Count; i++)
            {
                for ( int k = i+1; k < names.Count; k++ )
                {
                    if ( names[i].Score < names[k].Score)
                    {
                        temp = names[i];
                        names[i] = names[k];
                        names[k] = temp;
                    }
                }
            }

            return names;
        }
        //Sorts list by given name first or last if the scores are the same
        // true = first , false = surname
        private static List<Name>Sort(List<Name> names, bool which)
        {

            Name temp;

            for (int i = 0; i < names.Count; i++)
            {
                for (int k = i + 1; k < names.Count; k++)
                {
                    if ( which == false)
                    {
                        if(CheckNameOrder(names[i].Last,names[k].Last) == 1 && names[i].Score == names[k].Score)
                        {
                            temp = names[i];
                            names[i] = names[k];
                            names[k] = temp;
                        }
                    }
                    else
                    {
                        if (CheckNameOrder(names[i].First, names[k].First) == 1 && names[i].Score == names[k].Score)
                        {
                            if (CheckNameOrder(names[i].Last, names[k].Last) == 0)
                            {
                                temp = names[i];
                                names[i] = names[k];
                                names[k] = temp;
                            }
                        }
                    }

                }
            }

            return names;
        }
        //Takes list of unsplit string lines with ',' as divider and creates then returns list of Name Objects
        public static List<Name> MakeNameList(List<string> lines)
        {

            List<Name> names = new List<Name>();

            try
            {
                foreach (string item in lines)
                {
                    int tempInt = -1;
                    string[] temp = item.Split(',');
                    //add a check here incase it cant convert to an int!!
                    if (Int32.TryParse(temp[2], out tempInt) == true)
                    {
                        Int32.TryParse(temp[2], out tempInt);
                        Name tempName = new Name(temp[1], temp[0], tempInt);
                        names.Add(tempName);
                    }
                    else
                    {
                        throw new ScoreValueIncorrectFormat("Score value is not a number");
                    }
                }



                names = Sort(names); // sort by score
                names = Sort(names, false);//sort by surname
                names = Sort(names, true); // sort by first

                return names;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new FileFormatIncorrectException("Input File Format Incorrect");
            }

        }
        // checks string for alphabetical order
        //if a comes before b 
        // 0 same order , 1 b is second, -1 a is first
        public static int CheckNameOrder(string a,string b)
        {
            var A = a.ToUpper();
            var B = b.ToUpper();
            int result = string.Compare(A,B);

            return result;
        }
    }

  
}
