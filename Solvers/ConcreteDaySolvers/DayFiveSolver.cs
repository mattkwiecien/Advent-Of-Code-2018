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
            return ReducePolymer(_polymerChain).Count.ToString();
        }

        protected override string PartTwoSolver() {
            var shortestLength = _polymerChain.Length;
            var alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (var letter in alphabet) {
                var newPolymer = _polymerChain.Replace(letter.ToString(), "");
                newPolymer = newPolymer.Replace(letter.ToString().ToUpper(), "");
                var result = ReducePolymer(newPolymer);
                if (result.Count < shortestLength) { shortestLength = result.Count; }
            }

            return shortestLength.ToString();
        }

        private Stack<char> ReducePolymer(string polymer) {
            var myStack = new Stack<char>();
            foreach (var unit in polymer) {
                myStack.TryPeek(out char lastUnit);
                if (OppositePolarity(lastUnit, unit)) {
                    myStack.Pop();
                } else {
                    myStack.Push(unit);
                }
            }
            return myStack;
        }

        private bool OppositePolarity(char a, char b) {
            if (a.ToString().ToUpper() != b.ToString().ToUpper()) { return false; }
            if (char.IsUpper(a) && char.IsUpper(b)) { return false; }
            if (char.IsLower(a) && char.IsLower(b)) { return false; }
            return true;
        }

    }
}
