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
                case 6:
                    return new DaySixSolver(local);
                case 7:
                    throw new NotImplementedException();
                case 8:
                    throw new NotImplementedException();
                case 9:
                    throw new NotImplementedException();
                case 10:
                    throw new NotImplementedException();
                case 11:
                    throw new NotImplementedException();
                case 12:
                    throw new NotImplementedException();
                case 13:
                    throw new NotImplementedException();
                case 14:
                    throw new NotImplementedException();
                case 15:
                    throw new NotImplementedException();
                case 16:
                    throw new NotImplementedException();
                case 17:
                    throw new NotImplementedException();
                case 18:
                    throw new NotImplementedException();
                case 19:
                    throw new NotImplementedException();
                case 20:
                    throw new NotImplementedException();
                case 21:
                    throw new NotImplementedException();
                case 22:
                    throw new NotImplementedException();
                case 23:
                    throw new NotImplementedException();
                case 24:
                    throw new NotImplementedException();
                case 25:
                    throw new NotImplementedException();
            }
            return null;
        }
    }
}
