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
}
