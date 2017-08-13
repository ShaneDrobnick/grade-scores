using Microsoft.VisualStudio.TestTools.UnitTesting;
using grade_scores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using grade_scores;

namespace grade_scores.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void WriteNamesToFileTest()
        {
            // Not Implemented
            Assert.Fail();
        }

        [TestMethod()]
        public void SortTest()
        {
            // Not Implemented
            Assert.Fail();
        }

        [TestMethod()]
        public void SortTest1()
        {
            // Not Implemented
            Assert.Fail();
        }

        [TestMethod()]
        public void MakeNameListTest()
        {
            // Input file layout checks
            List<string> wrongSplitter = new List<string>();
            wrongSplitter.Add("BARRY.DARRY.79");

            try
            {
                Program.MakeNameList(wrongSplitter);
                Assert.Fail();
            }
            catch (FileFormatIncorrectException ex)
            {
                Console.WriteLine("Input File Format Incorrect");
            }

            wrongSplitter.Clear();
            wrongSplitter.Add("Smith,John,81");
            List<Name> names = new List<Name>();
            bool test = true;
            names = Program.MakeNameList(wrongSplitter);
            if ( names[0].First.ToUpper() != "JOHN")
            {
                test = false;
            }
            if (names[0].Last.ToUpper() != "SMITH")
            {
                test = false;
            }
            if (names[0].Score != 81)
            {
                test = false;
            }

            if ( test == false)
            {
                Assert.Fail("Incorrect format returned from Names");
            }
            // Check if Score is a number not a string
            wrongSplitter.Clear();
            wrongSplitter.Add("Smith,John,Peter");
            try
            {
                Program.MakeNameList(wrongSplitter);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Score not a number: "+ex);
            }
            
            
        
        }

        [TestMethod()]
        public void CheckNameOrderTest()
        {
            string testcase1 = "JoHn";
            string testcase2 = "jOhN";
            string testOrder1 = "Alpha";
            string testOrder2 = "Blpha";

            int expected = 0;

            //Check if case impacts result
            Assert.AreEqual(expected, Program.CheckNameOrder(testcase1, testcase2));

            //Checks if result value matches expected outcome
            expected = -1;
            Assert.AreEqual(expected, Program.CheckNameOrder(testOrder1, testOrder2));

            expected = 1;
            Assert.AreEqual(expected, Program.CheckNameOrder(testOrder2, testOrder1));

        }
    }
}