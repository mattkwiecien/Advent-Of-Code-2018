using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode {
    public class DaySixSolver : SolverBase {

        private List<(int, int)> vectors = new List<(int, int)>();
        private Dictionary<string, int> boundaries = new Dictionary<string, int>();

        public DaySixSolver(bool local) : base(6, local) {
            GetVectorsFromInput();
        }

        protected override string PartOneSolver() {
            RemoveInfinitePoints();
            var width = 1000;
            var height = 1000;

            var grid = new int[width, height];
            var valueGrid = new int[width, height];
            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    grid[x, y] = -1;
                    valueGrid[x, y] = 100000;
                }
            }

            foreach (var vector in vectors) {
                for (var x = 0; x < width; x++) {
                    for (var y = 0; y < height; y++) {
                        var distToPoint = GetTaxicabDistance(vector, (y, x));

                        if (vector.Equals((y,x))) {
                            valueGrid[x, y] = -1;
                            grid[x, y] = vectors.IndexOf(vector);
                        } else if (distToPoint == valueGrid[x, y]) {
                            valueGrid[x, y] = distToPoint;
                            grid[x, y] = -1;
                        } else if (distToPoint < valueGrid[x, y]) {
                            valueGrid[x, y] = distToPoint;
                            grid[x, y] = vectors.IndexOf(vector);
                        }
                    }
                }
            }

            //displayGrid(grid, width, height);
            var counts = new Dictionary<int, int>();
            for (var x = boundaries["xstart"]; x <= boundaries["xend"]; x++) {
                for (var y = boundaries["ystart"]; y <= boundaries["yend"]; y++) {
                    if (counts.ContainsKey(grid[x, y])) {
                        counts[grid[x, y]]++;
                    } else {
                        counts.Add(grid[x, y], 1);
                    }
                }
            }


            return counts.Values.Max().ToString();
        }
        private void displayGrid(string[,] grid, int width, int height) {
            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    Console.Write(grid[x, y]);
                }
                Console.Write('\n');
            }
        }

        protected override string PartTwoSolver() {
            return "";
        }

        private string getLetterFromVector((int,int) input) {
            if (input.Equals((1, 1))) {
                return "a";
            } else if (input.Equals((1, 6))) {
                return "b";
            } else if (input.Equals((8, 3))) {
                return "c";
            } else if (input.Equals((3, 4))) {
                return "d";
            } else if (input.Equals((5, 5))) {
                return "e";
            } else if (input.Equals((8, 9))) {
                return "f";
            } else {
                return "x";
            }
        }

        private void RemoveInfinitePoints() {
            foreach (var xy in vectors) {
                var xless = vectors.FirstOrDefault(x => x.Item1 < xy.Item1);
                if (xless.Equals(default) && !boundaries.ContainsKey("xstart")) {
                    boundaries.Add("xstart", xy.Item1);
                }
                var xmore = vectors.FirstOrDefault(x => x.Item1 > xy.Item1);
                if (xmore.Equals(default) && !boundaries.ContainsKey("xend")) {
                    boundaries.Add("xend", xy.Item1);
                }
                var yless = vectors.FirstOrDefault(y => y.Item2 < xy.Item2);
                if (yless.Equals(default) && !boundaries.ContainsKey("ystart")) {
                    boundaries.Add("ystart", xy.Item2);
                }
                var ymore = vectors.FirstOrDefault(y => y.Item2 > xy.Item2);
                if (ymore.Equals(default) && !boundaries.ContainsKey("yend")) {
                    boundaries.Add("yend", xy.Item2);
                }
            }
        }

        private void GetVectorsFromInput() {
            //var testData = @"1, 1.1, 6.8, 3.3, 4.5, 5.8, 9";
            //foreach (var xy in testData.Split('.')) {
            foreach (var xy in _myData.Split('\n')) {
                if (xy.Split(',').Length < 2) { continue; }
                vectors.Add((int.Parse(xy.Split(',')[0].Trim()), int.Parse(xy.Split(',')[1].Trim())));
            }
        }

        private int GetTaxicabDistance((int, int) p, (int, int) q) {
            return Math.Abs(p.Item1 - q.Item1) + Math.Abs(p.Item2 - q.Item2);
        }

    }
}
