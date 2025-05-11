using System;
using System.Text;

namespace Project1
{

// Quadtree class representing the spatial data structure
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
        /// <param name="x">X coordinate of the rectangle</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width of the rectangle</param>
        /// <param name="height">Height of the rectangle</param>
        public void Insert(int x, int y, int width, int height)
        {
            root = root.Insert(x, y, width, height);
        }

        /// <summary>
        /// Deletes a rectangle at the specified coordinates.
        /// </summary>
        public void Delete(int x, int y)
        {
            root = root.Delete(x, y);
        }


        /// <summary>
        /// Finds and displays a rectangle at the specified location.
        /// </summary>

        public void Find(int x, int y)
        {
            Console.WriteLine(root.Find(x, y));
        }


        /// <summary>
        /// Updates the dimensions of a rectangle at the given coordinates.
        /// </summary>
        public void Update(int x, int y, int width, int height)
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
                    Insert(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                    break;

                case "delete":
                    if (parts.Length != 3)
                        throw new FormatException("Delete requires 2 arguments.");
                    Delete(int.Parse(parts[1]), int.Parse(parts[2]));
                    break;

                case "find":
                    if (parts.Length != 3)
                        throw new FormatException("Find requires 2 arguments.");
                    Find(int.Parse(parts[1]), int.Parse(parts[2]));
                    break;

                case "update":
                    if (parts.Length != 5)
                        throw new FormatException("Update requires 4 arguments.");
                    Update(int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
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
