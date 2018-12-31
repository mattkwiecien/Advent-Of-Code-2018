using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode {
    public class DaySixSolver : SolverBase {

        private List<(int, int)> vectors = new List<(int, int)>();

        public DaySixSolver(bool local) : base(6, local) {
            GetVectorsFromInput();
        }

        protected override string PartOneSolver() {

            int width = 1000, height = 1000;
            //Grid used to mark each location with a unique ID per vector (index of the vector in the master vector array)
            var grid = GetMatrix(width, height, -1);
            //Grid used to mark each location with the smallest taxicab distance
            var distanceGrid = GetMatrix(width, height, GetTaxicabDistance((0, 0), (width, height)));

            SetGridPoints(ref grid, ref distanceGrid, width, height);
            var TaxicabDistancesByVector = GetDistancesFromGrid(grid);
            return TaxicabDistancesByVector.Values.Max().ToString();

        }

        protected override string PartTwoSolver() {

            var totalSize = 0;
            for (var x = 0; x < 1000; x++) {
                for (var y = 0; y < 1000; y++) {
                    var sumOfDistances = 0;
                    vectors.ForEach(v => sumOfDistances += GetTaxicabDistance(v, (y, x)));
                    if (sumOfDistances < 10000) { totalSize += 1; }
                }
            }
            return totalSize.ToString();

        }

        private int GetTaxicabDistance((int, int) p, (int, int) q) {
            return Math.Abs(p.Item1 - q.Item1) + Math.Abs(p.Item2 - q.Item2);
        }

        private Dictionary<int, int> GetDistancesFromGrid(int[,] grid) {
            var counts = new Dictionary<int, int>();
            var boundaries = GetBounds();
            for (var x = boundaries["xstart"]; x <= boundaries["xend"]; x++) {
                for (var y = boundaries["ystart"]; y <= boundaries["yend"]; y++) {
                    if (counts.ContainsKey(grid[x, y])) {
                        counts[grid[x, y]]++;
                    } else {
                        counts.Add(grid[x, y], 1);
                    }
                }
            }
            return counts;
        }

        private void SetGridPoints(ref int[,] grid, ref int[,] distanceGrid, int width, int height) {
            foreach (var vector in vectors) {
                for (var x = 0; x < width; x++) {
                    for (var y = 0; y < height; y++) {
                        var distToPoint = GetTaxicabDistance(vector, (y, x));

                        if (vector.Equals((y, x))) {
                            distanceGrid[x, y] = -1;
                            grid[x, y] = vectors.IndexOf(vector);
                        } else if (distToPoint == distanceGrid[x, y]) {
                            distanceGrid[x, y] = distToPoint;
                            grid[x, y] = -1;
                        } else if (distToPoint < distanceGrid[x, y]) {
                            distanceGrid[x, y] = distToPoint;
                            grid[x, y] = vectors.IndexOf(vector);
                        }
                    }
                }
            }
        }

        private Dictionary<string, int> GetBounds() {
            var boundaries = new Dictionary<string, int>();
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
            return boundaries;
        }

        private void GetVectorsFromInput() {
            foreach (var xy in _myData.Split('\n')) {
                if (xy.Split(',').Length < 2) { continue; }
                vectors.Add((int.Parse(xy.Split(',')[0].Trim()), int.Parse(xy.Split(',')[1].Trim())));
            }
        }

        private int[,] GetMatrix(int width, int height, int defaultVal) {
            var mtx = new int[width, height];
            for (var x = 0; x < width; x++) {
                for (var y = 0; y < height; y++) {
                    mtx[x, y] = defaultVal;
                }
            }
            return mtx;
        }

    }
}
