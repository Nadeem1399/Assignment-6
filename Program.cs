using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FileHandling
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // Part 1: File and Stream Handling

            // Create a new file
            string filePath = "test.txt";

            // Write some text into the file
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                byte[] textBytes = Encoding.UTF8.GetBytes("Hello, world!");
                await fs.WriteAsync(textBytes, 0, textBytes.Length);
            }

            // Read the contents of the file and display them on the console
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (StreamReader reader = new StreamReader(fs))
            {
                string content = await reader.ReadToEndAsync();
                Console.WriteLine("Content of the file:");
                Console.WriteLine(content);
            }

            // Delete the file
            File.Delete(filePath);

            // Part 2: Asynchronous Operations

            // Perform the above operations asynchronously
            await Task.Run(async () =>
            {
                // Create a new file asynchronously
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    byte[] textBytes = Encoding.UTF8.GetBytes("Hello, world! (Async)");
                    await fs.WriteAsync(textBytes, 0, textBytes.Length);
                }

                // Read the contents of the file asynchronously and display them on the console
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string content = await reader.ReadToEndAsync();
                    Console.WriteLine("Content of the file (Async):");
                    Console.WriteLine(content);
                }

                // Delete the file asynchronously
                File.Delete(filePath);
            });

            // Part 3: Serialization and Deserialization

            // Create an instance of the Person class
            Person person = new Person { Name = "David", Age = 25 };

            // Serialize the instance to a file
            string serializedFilePath = "person.dat";
            using (FileStream fs = new FileStream(serializedFilePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, person);
            }

            // Deserialize the instance from the file
            using (FileStream fs = new FileStream(serializedFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Person deserializedPerson = (Person)formatter.Deserialize(fs);
                Console.WriteLine("Deserialized Person:");
                Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
            }

            // Clean up: Delete the serialized file
            File.Delete(serializedFilePath);
        }
    }
}
