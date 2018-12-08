using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode {
    public class DayOneSolver : SolverBase {

        private List<int> _myFrequencies = new List<int>();

        public DayOneSolver(bool local) : base(1, local) {
            GetFrequencies();
        }

        protected override string PartOneSolver() {
            int ret = 0;
            _myFrequencies.ForEach(x => ret += x);
            return ret.ToString();
        }

        protected override string PartTwoSolver() {
            var freqCount = new Dictionary<int, int>();
            var cnt = 0;
            freqCount.Add(0, 1);

            while (true) {
                foreach (var num in _myFrequencies) {
                    cnt += num;

                    if (freqCount.ContainsKey(cnt)) {
                        return cnt.ToString();
                    } 

                    freqCount.Add(cnt, 1);
                }
            }
        }

        private void GetFrequencies() {
            var strs = _myData.Split("\n");

            foreach (var i in strs) {
                if (!int.TryParse(i.Trim(), out int thisNum)) { continue; }
                _myFrequencies.Add(thisNum);
            }
        }
    }
}
