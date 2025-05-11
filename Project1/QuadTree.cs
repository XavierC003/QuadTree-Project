using System;
using System.IO;
using System.Text;

namespace Project1
{
    /// <summary>
    /// Quadtree class representing the spatial data structure.
    /// </summary>
    public class Quadtree
    {
        private Node root;

        /// <summary>
        /// Initializes the quadtree with a 100x100 root leaf node.
        /// </summary>
        public Quadtree()
        {
            root = new LeafNode(-50, -50, 100, 100);
        }

        /// <summary>
        /// Inserts a rectangle into the quadtree.
        /// </summary>
        public void Insert(double x, double y, double width, double height)
        {
            root = root.Insert(x, y, width, height);
        }

        /// <summary>
        /// Deletes a rectangle at the specified coordinates.
        /// </summary>
        public void Delete(double x, double y)
        {
            root = root.Delete(x, y);
        }

        /// <summary>
        /// Finds and displays a rectangle at the specified location.
        /// </summary>
        public void Find(double x, double y)
        {
            Console.WriteLine(root.Find(x, y));
        }

        /// <summary>
        /// Updates the dimensions of a rectangle at the given coordinates.
        /// </summary>
        public void Update(double x, double y, double width, double height)
        {
            root = root.Update(x, y, width, height);
        }

        /// <summary>
        /// Prints the entire quadtree hierarchy.
        /// </summary>
        public void Dump()
        {
            var sb = new StringBuilder();
            root.Dump(sb, 0);
            Console.Write(sb.ToString());
        }

        /// <summary>
        /// Reads and executes commands from a given .cmmd file.
        /// </summary>
        /// <param name="filePath">Path to the command file</param>
        /// <exception cref="FileNotFoundException">Thrown if the file does not exist</exception>
        /// <exception cref="FormatException">Thrown if any command has invalid numbers</exception>
        /// <exception cref="Exception">Thrown for unknown or malformed commands</exception>
        public void ProcessCommands(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Command file not found.");

                foreach (var line in File.ReadLines(filePath))
                {
                    string cleanLine = line.Trim();
                    if (cleanLine.EndsWith(";"))
                        cleanLine = cleanLine[..^1];

                    var parts = cleanLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    switch (parts[0].ToLower())
                    {
                        case "insert":
                            if (parts.Length != 5)
                                throw new FormatException("Insert requires 4 arguments.");
                            Insert(double.Parse(parts[1]), double.Parse(parts[2]),
                                   double.Parse(parts[3]), double.Parse(parts[4]));
                            break;

                        case "delete":
                            if (parts.Length != 3)
                                throw new FormatException("Delete requires 2 arguments.");
                            Delete(double.Parse(parts[1]), double.Parse(parts[2]));
                            break;

                        case "find":
                            if (parts.Length != 3)
                                throw new FormatException("Find requires 2 arguments.");
                            Find(double.Parse(parts[1]), double.Parse(parts[2]));
                            break;

                        case "update":
                            if (parts.Length != 5)
                                throw new FormatException("Update requires 4 arguments.");
                            Update(double.Parse(parts[1]), double.Parse(parts[2]),
                                   double.Parse(parts[3]), double.Parse(parts[4]));
                            break;

                        case "dump":
                            Dump();
                            break;

                        default:
                            throw new Exception($"Unknown command: {parts[0]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing commands: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}