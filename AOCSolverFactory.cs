using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode {
    public static class AOCSolverFactory {
        public static AdventOfCodeSolver Create(short day) {
            switch (day) {
                case 1:
                    return new DayOneSolver();
                case 2:
                    return new DayTwoSolver();
            }
            return null;
        }
    }
}
