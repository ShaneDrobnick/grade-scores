using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;

using System.IO;

namespace grade_scores
{
    public class Program
    {
        static void Main(string[] args)
        {


            string[] commandline = Environment.GetCommandLineArgs();
            try
            {
                //Console.WriteLine(commandline[1]);
                if (File.Exists(commandline[1]))
                {
                    string fileDirectory = string.Empty;
                    //string namesList = string.Empty;

                    //string[] filenametempsplit = Path.GetFileName(commandline[1]).Split('.');
                    //string filenametemp = filenametempsplit[0];

                    List<string> fileLines = new List<string>();

                    //need to add a check file exists etc and is complant etc here
                    var fileStream = new FileStream(commandline[1], FileMode.Open, FileAccess.Read);


                    using (var StreamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string singleLine;
                        while ((singleLine = StreamReader.ReadLine()) != null)
                        {
                            fileLines.Add(singleLine);
                        }
                    }

                    //namesList = fileLines.ToArray();
                    List<Name> names = MakeNameList(fileLines);
                    WriteNamesToFile(names, commandline[1]);

                    /*
                    foreach (string item in fileLines)
                    {
                        Console.WriteLine(item);
                        makeNameArray();
                    }
                    */

                    //writeNamesToFile();
                    //UnitTests.nameObjectTest();
                }
                else
                {
                    throw new FileNotFoundException();

                }
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine("No file name given at commandline please give a file location");
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("File not found - {0}", commandline[1]);
            }







        }
        //Method Writes array to file
        public static void WriteNamesToFile(List<Name> names,string filename)
        {
            /*
             * assumption specifications does not give an outfile location, since there is none 
             * I could just dump it in default directory but for simplicity since the question asks for a file in from
             * C: I will output back to C: but this is an ambiguity issue, I could have it take the drive letter 
             * from the input arg and reuse that but this is going even more off the specs I have been given
             */
            
            //change the e back after test
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
           //Console.WriteLine("called");
        }
        //Sorts list in order for highest score
        // Bubble sorting  yes not efficient but no specs given on efficiency 
        public static List<Name>Sort(List<Name> names)
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
        public static List<Name>Sort(List<Name> names, bool which)
        {

            Name temp;

            for (int i = 0; i < names.Count; i++)
            {
                for (int k = i + 1; k < names.Count; k++)
                {
                    if ( which == false)
                    {
                        //if (names[i].Score < names[k].Score)
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

            foreach (string item in lines)
            {
                int tempInt = 0;
                string[] temp = item.Split(',');
                //add a check here incase it cant convert to an int!!
                Int32.TryParse(temp[2], out tempInt);
                Name tempName = new Name(temp[1],temp[0],tempInt);
                names.Add(tempName);
            }
            names = Sort(names);
            names = Sort(names, false);
            names = Sort(names, true);

            return names;
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
