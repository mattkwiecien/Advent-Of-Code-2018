using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode {
    public class DayTwoSolver : SolverBase {

        private List<string> _idList = new List<string>();

        public DayTwoSolver(bool local) : base(2, local) {
            _idList = new List<string>(_myData.Split("\n"));
        }

        protected override string PartOneSolver() {

            var dupes = 0;
            var trips = 0;

            foreach (var id in _idList) {
                var letterCounts = new Dictionary<char, short>();
                foreach (var let in id) {
                    if (letterCounts.ContainsKey(let)) {
                        letterCounts[let] += 1;
                    } else {
                        letterCounts.Add(let, 1);
                    }
                }
                dupes += letterCounts.ContainsValue(2) ? 1 : 0;
                trips += letterCounts.ContainsValue(3) ? 1 : 0;
            }

            return (dupes * trips).ToString();

        }

        protected override string PartTwoSolver() {

            for (var first = 0; first < _idList.Count; first++) {
                var id = _idList[first];

                for (var charIndex = 0; charIndex < id.Length; charIndex++) {
                    for (var second = first + 1; second < _idList.Count - 1; second++) {
                        var match = _idList[second];
                        var trimmedId = id.Remove(charIndex, 1);
                        var trimmedMatch = match.Remove(charIndex, 1);

                        if (trimmedId == trimmedMatch) { return trimmedMatch; }
                    }
                }
            }

            return "";
        }

    }
}
