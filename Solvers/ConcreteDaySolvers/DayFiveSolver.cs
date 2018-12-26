using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode {
    public class DayFiveSolver : SolverBase {

        private string _polymerChain = "";

        public DayFiveSolver(bool local) : base(5, local) {
            _polymerChain = _myData.Trim('\n');
        }

        protected override string PartOneSolver() {
            var myStack = new Stack<char>();
            foreach (var unit in _polymerChain) {
                myStack.TryPeek(out char lastUnit);
                if (OppositePolarity(lastUnit, unit)) {
                    myStack.Pop();
                } else {
                    myStack.Push(unit);
                }
            }
            return myStack.Count.ToString();
        }

        private bool OppositePolarity(char a, char b) {
            if (a.ToString().ToUpper() != b.ToString().ToUpper()) { return false; }
            if (char.IsUpper(a) && char.IsUpper(b)) { return false; }
            if (char.IsLower(a) && char.IsLower(b)) { return false; }
            return true;
        }

        protected override string PartTwoSolver() {
            return "";
        }

    }

}
