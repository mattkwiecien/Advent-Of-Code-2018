using System;

namespace AdventOfCode {
    class AdventOfCode2018 {
        static void Main(string[] args) {
            Console.WriteLine("Press any key to continue: ");
            Console.ReadKey();

            var AOCSolver = AOCSolverFactory.Create(1);
            Console.WriteLine($"The answer to day one, part 1 is: {AOCSolver.SolvePartOne()}");
            Console.WriteLine($"The answer to day one, part 2 is: {AOCSolver.SolvePartTwo()}");

            Console.ReadKey();
        }
    }
}
