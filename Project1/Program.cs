namespace Project1;

// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;


//Main program class
class Program
{
    static void Main(string[] args)
    {
        // Create new quadtree
        Quadtree qt = new Quadtree();
        try
        {
            if (args.Length > 0)
            {
                qt.ProcessCommands(args[0]);
            }
            else
            {
                Console.WriteLine("No command file provided.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
