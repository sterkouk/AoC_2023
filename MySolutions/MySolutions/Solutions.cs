using System;

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

}
