using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode {
    public static class SolverFactory {
        public static ISolver Create(short day, bool local) {
            switch (day) {
                case 1:
                    return new DayOneSolver(local);
                case 2:
                    return new DayTwoSolver(local);
                case 3:
                    return new DayThreeSolver(local);
				case 4:
					return new DayFourSolver(local);
                case 5:
                    return new DayFiveSolver(local);
            }
            return null;
        }
    }
}
