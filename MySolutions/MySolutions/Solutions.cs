using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Solutions
{
	public int Solution1()
	{
        string initial;

        char first;
        char last;
        int sum = 0;

        foreach (string line in File.ReadLines(@"D:\VSProjects\AoC\MySolutions\MySolutions\input.txt"))
        {

            if (line.Any(c => char.IsDigit(c)))
            {
                //Console.WriteLine(line);
                // Extract digits from the beginning of the line and end
                initial = new string(line.Where(Char.IsDigit).ToArray());

                first = initial.First();
                last = initial.Last();

                int number = int.Parse(first.ToString() + last.ToString());

                sum = sum + number;
            }
        }

        return sum;

    }

    public void Solution2()
    {
        // Check if a file path is provided as a command-line argument

        // Read the content of the file
        string[] lines = File.ReadAllLines((@"D:\VSProjects\AoC\MySolutions\MySolutions\combinations.txt"));

        int p1 = 0;
        int p2 = 0;

        foreach (string line in lines)
        {
            bool ok = true;

            // Split the line into id and events
            string[] parts = line.Split(':');
            string id = parts[0].Trim();
            string eventsLine = parts[1].Trim();

            Dictionary<string, int> V = new Dictionary<string, int>();

            // Iterate through events
            foreach (string eventStr in eventsLine.Split(';'))
            {
                // Iterate through balls in each event
                foreach (string balls in eventStr.Split(','))
                {
                    string[] ballParts = balls.Trim().Split(' ');

                    // Check if ballParts has at least two elements
                    if (ballParts.Length >= 2)
                    {
                        // Combine all parts except the last one to handle numbers with commas
                        string numberString = string.Join(" ", ballParts.Take(ballParts.Length - 1));

                        // Attempt to parse the trimmed string as an integer
                        if (int.TryParse(numberString, out int n))
                        {
                            string color = ballParts.Last();

                            V[color] = Math.Max(V.GetValueOrDefault(color, 0), n);

                            if (n > new Dictionary<string, int> { { "red", 12 }, { "green", 13 }, { "blue", 14 } }.GetValueOrDefault(color, 0))
                            {
                                ok = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Invalid format for number: {numberString}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format in 'ballParts' array");
                    }
                }

            }

            if (ok)
            {
                p1 += int.Parse(id.Split()[^1]);
            }
        }

        Console.WriteLine(p1);
    }

    //private static string[] inputLines;

    public static void Solution3(string[] inputLines)
    {
        
        Dictionary<Coordinates, int> numbers = BuildNumbers(inputLines);
        Dictionary<Coordinates, char> specialCharacters = BuildSpecialCharacters(inputLines);

        Part1(numbers, specialCharacters);
    }

    static Dictionary<Coordinates, int> BuildNumbers(string[] inputLines)
    {
        Dictionary<Coordinates, int> numbers = new Dictionary<Coordinates, int>();
        for (int row = 0; row < inputLines.Length; row++)
        {
            var line = inputLines[row];
            for (int col = 0; col < line.Length; col++)
            {
                if (char.IsDigit(line[col]) && line[col] != '.')
                {
                    Coordinates coordinates = new Coordinates(row, col);
                    StringBuilder b = new StringBuilder();
                    while (col < line.Length && char.IsDigit(line[col]))
                    {
                        b.Append(line[col]);
                        col++;
                    }

                    numbers[coordinates] = int.Parse(b.ToString());
                }
            }
        }
        return numbers;
    }

    static Dictionary<Coordinates, char> BuildSpecialCharacters(string[] inputLines)
    {
        Dictionary<Coordinates, char> spCharacters = new Dictionary<Coordinates, char>();
        for (int row = 0; row < inputLines.Length; row++)
        {
            var line = inputLines[row];
            for (int col = 0; col < line.Length; col++)
            {
                char ch = line[col];
                if (!char.IsDigit(ch) && ch != '.')
                {
                    Coordinates coordinates = new Coordinates(row, col);
                    spCharacters[coordinates] = ch;
                }
            }
        }
        return spCharacters;
    }

    static IEnumerable<Coordinates> GetNumberCoordinates(Coordinates coordinate, Dictionary<Coordinates, int> numbers)
    {
        if (!numbers.ContainsKey(coordinate))
        {
            return Enumerable.Empty<Coordinates>();
        }

        int numberOfDigits = numbers[coordinate].ToString().Length;
        return Enumerable.Range(coordinate.Col, numberOfDigits)
            .Select(col => new Coordinates(coordinate.Row, col));
    }

    static bool NeigbourIsSpecial(Coordinates coordinate, Dictionary<Coordinates, char> specialCharacters)
    {
        Coordinates[] directions = {
            new Coordinates(coordinate.Row, coordinate.Col - 1), //left
            new Coordinates(coordinate.Row, coordinate.Col + 1), // right
            new Coordinates(coordinate.Row - 1, coordinate.Col), // up
            new Coordinates(coordinate.Row + 1, coordinate.Col), // down

            new Coordinates(coordinate.Row - 1, coordinate.Col - 1), // diagonal-up-left
            new Coordinates(coordinate.Row - 1, coordinate.Col + 1), // diagonal-up-right
            new Coordinates(coordinate.Row + 1, coordinate.Col - 1), // diagonal-down-left
            new Coordinates(coordinate.Row + 1, coordinate.Col + 1), // diagonal-right-down
        };

        foreach (Coordinates c in directions)
        {
            if (specialCharacters.ContainsKey(c))
            {
                return true;
            }
        }
        return false;
    }

    static void Part1(Dictionary<Coordinates, int> numbers, Dictionary<Coordinates, char> specialCharacters)
    {
        int accumulativeValue = 0;
        foreach (var (c, number) in numbers)
        {
            IEnumerable<Coordinates> numberCoordinates = GetNumberCoordinates(c, numbers);
            if (numberCoordinates.Any(coord => NeigbourIsSpecial(coord, specialCharacters)))
            {
                accumulativeValue += number;
            }
        }
        Console.WriteLine("Accumulative value: " + accumulativeValue);
    }

    record struct Coordinates(int Row, int Col);
}
