using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexingFile
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length == 1 || args.Length == 0 || args.Length > 2)
			{
				Console.WriteLine("Please enter the correct number of parameters.");
				Environment.Exit(0);
			}
			else
			{
				try
				{
					string inputFileName = args[0];
					string outputFileName = args[1];
					string projectFolder;
					FileInfo[] file;
					FileStream fileOutputStream;

					DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory());
					projectFolder = directoryInfo.Parent.FullName;
					directoryInfo = new DirectoryInfo(projectFolder);
					file = directoryInfo.GetFiles(inputFileName);

                    Console.WriteLine("The project folder: {0}", projectFolder);
                    Console.WriteLine(Path.Combine(projectFolder, outputFileName));

					fileOutputStream = File.Create(Path.Combine(projectFolder,outputFileName));

					try
					{
						SearchingFile(file[0].FullName, fileOutputStream);

					}
					catch
					{

						if (file.Length == 0)
						{
							Console.WriteLine("The file is not found. Please enter the correct file.");
							Environment.Exit(0);
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}

			}
		}
		public static void SearchingFile(string inputFile, FileStream outputFileStream)
		{
			byte[] writeBytes = new byte[11];
			byte[] offsetBytes;
			byte[] newLine;

			// position of pointer in 16S.fasta file
			long offset;
			int i;


			FileStream fileRead = new FileStream(inputFile, FileMode.Open, FileAccess.Read);


			while ((i = fileRead.ReadByte()) != -1)
			{
				//put the position of > in the offset
				offset = fileRead.Position - 1;
				if ((char)i == '>')
				{
					// read bytes staring at position to 11 with no offset then put inside writeBytes
					fileRead.Read(writeBytes, 0, 11);
					//writing in index file the values inside writeBytes till the length of the array
					outputFileStream.Write(writeBytes, 0, writeBytes.Length);

					// writing space in index file
					outputFileStream.WriteByte((byte)' ');
					//converting position of '>' into bytes
					offsetBytes = Encoding.ASCII.GetBytes(offset.ToString());
					//writing in index file the position
					outputFileStream.Write(offsetBytes, 0, offsetBytes.Length);

					//new line character converted to bytes
					newLine = Encoding.ASCII.GetBytes(Environment.NewLine);
					//writing new line in index file
					outputFileStream.Write(newLine, 0, newLine.Length);

				}
			}

			outputFileStream.Close();
			fileRead.Close();
		}
	}
}