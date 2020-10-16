using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BracketsInFile
{
    class Program
    {
        static void Main(string[] args)
        {
            bool verificationFlag = true;
            string path;

            do
            {
                Console.Write("Enter path of the file: ");
                path = Console.ReadLine();

                FileInfo file = new FileInfo(path); // "FilesToCheck/Correct.txt"

                if (file.Exists && file.Directory.Name == "FilesToCheck")
                    verificationFlag = false;

            } while (verificationFlag);

            string result = CheckBracketsMatch(path);
            Console.WriteLine(result);

            WriteResult(result + "\n");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static string CheckBracketsMatch(string path)
        {
            string result;

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string text = sr.ReadToEnd();
                    if (text == "")
                    {
                        result = "The code file is empty.";
                        return result;
                    }

                    char[] array = text.ToCharArray();
                    int left = 0;
                    int right = 0;

                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i] == '{')                        
                            left++;                        
                        else if (array[i] == '}')
                            right++;
                    }

                    if (left > right)
                    {
                        result = $"{left - right} unclosed left brackets were found.";
                    }
                    else if (right > left)
                    {
                        result = $"{right - left} needless right brackets were found.";
                    }
                    else
                    {
                        result = "The number of left and right brackets is the same.";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = String.Format($"{e.ToString()} exception occurred when trying to open the file.");
            }
            
            return result;
        }

        static void WriteResult(string result)
        {
            using (FileStream fs = new FileStream("FilesToCheck/Log.txt", FileMode.Append))
            {
                byte[] array = Encoding.Default.GetBytes(result);
                fs.Write(array, 0, array.Length);                
            }
            Console.WriteLine("The result of operation was written to the Log file.");                
        }
    }
}