using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MajorAssesmentCab201
{
    class Program
    {
        public static void Main(string[] args)
        {
            int lineNumber, seqNumber;
            string seqID, searchFileName, projectFolder, dnaSearch, inputFileName, outputFileName, indexFileName, searchSeq, searchWord;
            FileInfo[] indexFile = null, inputFile = null, searchFile = null;
            FileStream outputFileStream, fileStream;


            // validating the number of statements in the argument is not 0
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter the level number and respective parameters you want to search for.");
            }
            else if (args.Length > 1)
            {
                //input file is the 16S.fastas file
                inputFileName = args[1];

                if (inputFileName.Contains(".fasta"))
                {
                    //retriving file from directory
                    DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory());
                    projectFolder = directoryInfo.Parent.FullName;
                    directoryInfo = new DirectoryInfo(projectFolder);

                    //if the input file is not null retrive the input file and set it to "file"
                    if (inputFileName != null)
                    {
                        inputFile = directoryInfo.GetFiles(inputFileName);
                    }
                    else
                    {
                        Console.WriteLine("The input file name is not found. Please enter a correct input file");
                        Environment.Exit(0);
                    }

                    //if the file searched for in location 1 of the argument exists then go into the switch statment
                    if (inputFile[0] != null)
                    {
                        try
                        {
                            switch (args[0])
                            {

                                //variables called within the switch statements because some elements hold
                                //the same position in the argument depending on level  
                                case "-level1":
                                    try
                                    {
                                        if (args.Length > 2 && args.Length == 4)
                                        {
                                            //in -level1 args[2] is held by lineNumber
                                            // converting args[] to int because args[] is a string
                                            lineNumber = Convert.ToInt32(args[2]);
                                            seqNumber = Convert.ToInt32(args[3]);

                                            //fasta file in filestream to be read
                                            fileStream = new FileStream(inputFile[0].FullName, FileMode.Open, FileAccess.Read);
                                            //calling level 1 class and employing the method
                                            Level1.SequentialAccess(lineNumber, seqNumber, fileStream);
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter the correct parameters for a -level1 search, two integer numbers.");
                                    }
                                    break;

                                case "-level2":
                                    try
                                    {
                                        if (args.Length == 3 && args.Length > 2)
                                        {
                                            //in -level2 args[2] is held by seqID
                                            seqID = args[2];

                                            //fasta file in filestream to be read
                                            fileStream = new FileStream(inputFile[0].FullName, FileMode.Open, FileAccess.Read);

                                            //calling level 2 class and employing the method
                                            Level2.SequenceID(seqID, fileStream);

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter the correct parameters for a -level2 search, sequence id.");
                                    }
                                    break;

                                case "-level3":
                                    try
                                    {
                                        if (args.Length > 2 && args.Length == 4)
                                        {
                                            //use FullName to get the full file path then pass it to the function
                                            //this avoids the "file not found" and location error
                                            //in SearchSequenceId method use File.ReadAllLines function which requires the full path as the parameter
                                            searchFileName = args[2];

                                            searchFile = directoryInfo.GetFiles(searchFileName);

                                            outputFileName = args[3];
                                            outputFileStream = File.Create(Path.Combine(projectFolder + "\\" + outputFileName));

                                            //calling level 3 class and employing the method
                                            Level3.SearchSequenceID(inputFile[0].FullName, searchFileName, outputFileStream);
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter the correct number of parameters for a -level3 search, query file and results file.");
                                    }
                                    break;
                                case "-level4":
                                    try
                                    {
                                        if (args.Length > 2 && args.Length == 5)
                                        {
                                            //accessing the index file created in a different solution from memory
                                            //both solutions are contained within the main folder
                                            //the index file is in the cab201 folder which is in a second cab 201 folder which is accessed in main folder
                                            indexFileName = args[2];
                                            indexFile = directoryInfo.GetFiles(indexFileName);

                                            searchFileName = args[3];
                                            searchFile = directoryInfo.GetFiles(searchFileName);
                                            outputFileName = args[4];

                                            outputFileStream = File.Create(Path.Combine(projectFolder + "\\" + outputFileName));

                                            //calling level 4 class and employing the method
                                            Level4.IndexFileAccess(inputFile[0].FullName, indexFile[0].FullName, searchFile[0].FullName, outputFileStream);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Please enter the correct parameters for a -level4 search, index file, query file, and result file.");
                                    }
                                    break;

                                case "-level5":
                                    try
                                    {
                                        if (args.Length == 3 && args.Length > 2)
                                        { 
                                            dnaSearch = args[2];
                                            Level5.MatchDNAString(inputFile[0].FullName, dnaSearch);
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter the correct parameter for a -level4 search, a DNA line.");
                                    }
                                    break;
                                case "-level6":
                                    try
                                    {
                                        if (args.Length == 3 && args.Length > 2)
                                        {
                                            searchWord = args[2];
                                            Level6.MatchMetaDataWord(inputFile[0].FullName, searchWord);
                                        }

                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter the correct parameter for a -level6 search, meta-data word.");
                                    }
                                    break;
                                case "-level7":
                                    try
                                    {
                                        if (args.Length == 3 && args.Length > 2)
                                        {
                                            searchSeq = args[2];
                                            Level7.MatchSeqWildCard(inputFile[0].FullName, searchSeq);
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter the correct parameter for a -level7 search, sequence containing wild cards.");
                                    }
                                    break;
                            }
                        }
                        catch
                        {
                            Console.WriteLine(String.Format("Unknown level number: {0}. Please enter a correct level. ", args[0]));
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect filename. Please check the filename and rerun the program.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number of parameters.");
            }

        }
    }
    class Level1
    {
        // (-level1) Sequential access using a starting position in the file
        public static void SequentialAccess(int lineNumber, int seqNumber, FileStream fileName)
        {
            int lineCounter = 1;
            bool readDnaLine = false, linePrinted = false;
            string line;
            StreamReader stream = new StreamReader(fileName);
            long totalLength = fileName.Length;
            //if lineNumber divided by 2 has a remainder of zero output a statment for the user
            // then exit the eniorment using exit code 0 which means success
            if (lineNumber % 2 == 0)
            {
                Console.WriteLine("Please enter an odd integer.");
                Environment.Exit(0);
            }
            else
            {
                //including both dna lines and sequence lines
                int totalPrintLineNumber = lineNumber + (seqNumber * 2);

                //while it is not the end of the file
                while (!stream.EndOfStream)
                {
                    //reading each line of the file 
                    line = stream.ReadLine();
                    if (lineCounter >= lineNumber)
                    {
                        if (lineCounter < totalPrintLineNumber)
                        {
                            if (line.ToString().StartsWith(">"))
                            {
                                //sequence id line
                                Console.WriteLine(line);
                                readDnaLine = false;
                            }
                            else if (!readDnaLine)
                            {
                                //printing dna line
                                Console.WriteLine(line);
                                readDnaLine = true;
                            }

                            linePrinted = true;
                        }
                    }
                    lineCounter++;
                }
                //will print all possible outputs until the file has reached its end
                if (lineCounter < totalPrintLineNumber)
                {
                    Console.WriteLine("No more sequences present as per the input requested.");
                }

                if (!linePrinted)
                {
                    Console.WriteLine("The line number or sequence id entered is not valid. Please re-enter search items and re-run the program.");
                }
            }
        }
    }
    class Level2
    {
        // (-level2) Sequential access to a specific sequence by sequence-id.
        public static void SequenceID(string seqID, FileStream fileInputStream)
        {
            StreamReader stream = new StreamReader(fileInputStream);
            //setting bool flag to false
            bool searchSuccess = false, readDnaLine = true;
            string line; int lineCounter = 1;
            Console.WriteLine("The sequence you searched for: ");

            while (!stream.EndOfStream)
            {
                line = stream.ReadLine();

                if (line.Contains(seqID))
                {
                    //sequence id line
                    Console.WriteLine(line);
                    readDnaLine = false;
                    searchSuccess = true;
                }
                else if (!readDnaLine)
                {
                    //dna line
                    Console.WriteLine(line);
                    readDnaLine = true;
                }
                lineCounter++;
            }

            // if it is not searchSuccess
            if (!searchSuccess)
            {
                Console.WriteLine("The sequence id you searched for was not found in the file.");
            }

        }
    }
    class Level3
    {
        // (-level3) Sequential access to find a set of sequence-ids given in a query file, and writing the output to a specified result file
        public static void SearchSequenceID(string inputFileName, string searchFileName, FileStream outputFileStream)
        {

            //put the stream from steamWriter in outputFile
            StreamWriter streamWriter = new StreamWriter(outputFileStream);

            //search validation flag
            bool searchSuceesful = false;

            //employing the File.ReadAllLines method to open the file read the lines and place in a string array
            string[] inputFileString = File.ReadAllLines(inputFileName);
            string[] searchFileString = File.ReadAllLines(searchFileName);

            for (int i = 0; i < searchFileString.Length; i++)
            {
                //making sure items searched for are in the original file
                for (int j = 0; j < inputFileString.Length; j++)
                {
                    if (inputFileString[j].Contains(searchFileString[i]))
                    {
                        if (Regex.IsMatch(searchFileString[i], @"\bNR_.{8}"))
                        {
                            //if the items in input file contain the items searched for in the search file the streamwriter will write the line in the new file
                            streamWriter.WriteLine(inputFileString[j]);
                            searchSuceesful = true;
                            break;
                        }
                    }
                    else
                    {
                        searchSuceesful = false;
                    }
                }
                if (!searchSuceesful)
                {
                    Console.WriteLine("Error, sequence {0} was not found in the search.", searchFileString[i]);
                }
            }
            streamWriter.Close();
            outputFileStream.Close();
        }
    }
    class Level4
    {
        // (-level4) Index file access searching index file and comparing to query file then writing output in specified result file
        public static void IndexFileAccess(string inputFileName, string indexFile, string searchFileName, FileStream outputFileStream)
        {

            //put the work from steamWriter in outputFile
            StreamWriter streamWriter = new StreamWriter(outputFileStream);

            //search validation flag
            bool searchSuccesful = false;
            string[] searchFileString = File.ReadAllLines(searchFileName);
            byte[] newLine;
            int counter = 0, beginOffset;

            string[] indexFileString = File.ReadAllLines(indexFile);

            FileStream inputFileStream = new FileStream(inputFileName, FileMode.Open, FileAccess.Read);
            StreamReader inputStreamReader = new StreamReader(inputFileStream);

            for (int i = 0; i < searchFileString.Length; i++)
            {
                //making sure items searched for are in the original file
                for (int j = 0; j < indexFileString.Length; j++)
                {
                    if (indexFileString[j].Contains(searchFileString[i]))
                    {
                        //split string at space between metadata line and offset
                        string[] currentMetadata = indexFileString[j].Split(' ');

                        beginOffset = Convert.ToInt16(currentMetadata[1]);

                        inputStreamReader.BaseStream.Seek(beginOffset, SeekOrigin.Begin);

                        do
                        {
                            newLine = Encoding.ASCII.GetBytes(Environment.NewLine);
                            streamWriter.WriteLine(inputStreamReader.ReadLine());
                            counter = counter + 1;
                            searchSuccesful = true;
                        } while (counter <= 2);

                        break;
                    }
                    else
                    {
                        searchSuccesful = false;
                    }
                }
                if (!searchSuccesful)
                {
                    Console.WriteLine("Error, sequence {0} was not found in the search.", searchFileString[i]);
                }
            }
            streamWriter.Close();
            outputFileStream.Close();
        }
    }
    class Level5
    {
        public static void MatchDNAString(string inputFileName, string dnaSearch)
        {
            //setting bool flag to false
            bool matchSuccess = false;

            //Read only /2 lines
            string[] fileLines = File.ReadAllLines(inputFileName);

            //need to check if item searched is actually a dna line
            for (int i = 0; i < fileLines.Length; i++)
            {
                Regex regex = new Regex(dnaSearch);
                matchSuccess = regex.IsMatch(fileLines[i]);

                //if user input is searchable
                if (matchSuccess)
                {
                    Console.WriteLine(fileLines[i - 1]);
                    MatchCollection mc = Regex.Matches(fileLines[i - 1], @"\bNR_.{8}");
                }
            }

            // if it is not searchSuccess
            if (!matchSuccess)
            {
                Console.WriteLine("The DNA line you searched for was not found in the file.");
            }
        }
    }
    class Level6
    {
        public static void MatchMetaDataWord(string inputFileName, string searchWord)
        {
            //setting bool flag to false
            bool searchSuccess = false;
            string[] fileLines = File.ReadAllLines(inputFileName);
            for (int i = 0; i < fileLines.Length; i++)
            {
                if (fileLines[i].Contains(searchWord))
                {
                    if (Regex.IsMatch(fileLines[i], @"\bNR_.{8}"))
                    {
                        //if user input is searchable
                        string pattern = @"\bNR_.{8}";
                        MatchCollection matchCol = Regex.Matches(fileLines[i], pattern);
                        //printing more than one match
                        for (int j = 0; j < matchCol.Count; j++)
                        {
                            Console.WriteLine(matchCol[j].Value);
                        }

                        searchSuccess = true;
                    }
                    //read only the dna lines
                    i = i + 2;
                }
                else
                {
                    searchSuccess = false;
                }
            }
            // if it is not searchSuccess
            if (!searchSuccess)
            {
                Console.WriteLine("The word you searched for was not found in the file.");
            }
        }
    }
    class Level7
    {
        public static void MatchSeqWildCard(string inputFileName, string searchSeq)
        {
            //setting bool flag to false
            bool matchSuccess = false;
            string[] fileLine = File.ReadAllLines(inputFileName);
            //using regex to handle wildcards 
            Regex regex = new Regex(searchSeq);
            //need to check if item searched is actually a dna line
            for (int i = 1; i < fileLine.Length;)
            {
                matchSuccess = regex.IsMatch(fileLine[i]);
                MatchCollection mc = regex.Matches(fileLine[i]);

                if (matchSuccess)
                {
                    //regular expression pattern asking for line in the boundary (\b) starting with NR_ and the following 8 char
                    string pattern = @"\bNR_.{8}";

                    //if the patteren searched for is in the file line
                    //using match match collection and matches because there could be more than one match
                    MatchCollection matchCol = Regex.Matches(fileLine[i], pattern);

                    //printing more than one match
                    for (int j = 0; j < matchCol.Count; j++)
                    {
                        Console.WriteLine(matchCol[j].Value);
                    }
                }
                //read only the dna lines
                i = i + 2;
            }
                if(!matchSuccess)
                {
                     Console.WriteLine("The sequences you searched for were not found in the file.");
                }    
        }
    }
}
