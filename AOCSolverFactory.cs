using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode {
    public static class SolverFactory {
        public static ISolver Create(short day) {
            switch (day) {
                case 1:
                    return new DayOneSolver();
                case 2:
                    return new DayTwoSolver();
                case 3:
                    return new DayThreeSolver();
            }
            return null;
        }
    }
}
