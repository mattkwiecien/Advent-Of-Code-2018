using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode {
    public class DaySevenSolver : SolverBase {

        private List<Step> instructions = new List<Step>();

        public DaySevenSolver(bool local) : base(7, local) {
            CreateInstructions();
        }

        protected override string PartOneSolver() {
            string order = "";

            while (instructions.Count > 0) {
                instructions = instructions.OrderBy(x => x.Parent).ToList();

                foreach (var s in instructions) {
                    //If this step's parent is no one's child, it's a top level step that can be executed.
                    if (instructions.FirstOrDefault(i => i.ID == s.Parent) == null) {
                        order += s.Parent;
                        //If there's only 1 step left, then we can execute that step's parent as well as that step itself.
                        if (instructions.Count == 1) { order += s.ID; }
                        //Once we execute this step, remove all other steps that specify it as a dependency.
                        instructions.RemoveAll(x => x.Parent == s.Parent);
                        break;
                    }
                }
            }

            return order;
        }

        protected override string PartTwoSolver() {
            return "";
        }

        private void CreateInstructions() {
            foreach (var xy in _myData.Split('\n')) {
                if (string.IsNullOrWhiteSpace(xy)) { continue; }
                var child = new Step(xy.Split(" ")[7], xy.Split(" ")[1]);
                instructions.Add(child);
            }
        }

        private class Step {
            public string ID { get; set; }
            public string Parent { get; set; }

            public Step(string id, string parent = null) {
                this.ID = id;
                this.Parent = parent;
            }
        }

    }
}
