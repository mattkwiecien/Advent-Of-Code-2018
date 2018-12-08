using System;

namespace AdventOfCode {
    public class AdventOfCode2018 {

        public static void Main(string[] args) {

            Console.WriteLine("Enter an Advent of Code day to solve (1-25): ");
            do {
                if (short.TryParse(Console.ReadKey(true).KeyChar.ToString(), out short day) && day > 0 && day < 26) {
					Console.WriteLine("Solve this by pulling data from the web? (y/n): ");
					do {
						var usrInput = Console.ReadKey(true).KeyChar.ToString().ToLower();
						if (usrInput == "y" || usrInput=="n") {
							Solve(day, usrInput == "n");
							break;
						} else {
							Console.WriteLine("Invalid input, please enter y or n.");
						}
					} while (true);
					break;
                } else {
                    Console.WriteLine("Invalid input, please enter a number between 1 and 25: ");
                }
            } while (true);

        }

        public static void Solve(short day, bool local) {

            var Solver = SolverFactory.Create(day, local);
            Console.WriteLine($"The Advent of Code answer to day {day} part 1 is: {Solver.SolvePartOne()}");
            Console.WriteLine($"The Advent of Code answer to day {day} part 2 is: {Solver.SolvePartTwo()}");
            Console.Write("Press ENTER to exit.");
            Console.ReadLine();

        }
    }
}
