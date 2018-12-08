using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode {
    public class DayThreeSolver : SolverBase {

        private List<Sheet> _mySheets = new List<Sheet>();

        public DayThreeSolver() : base(@"https://adventofcode.com/2018/day/3/input") {
            GetSheets();
        }

        protected override string PartOneSolver() {
            var fabric = new short[100, 100];
            foreach(var sheet in _mySheets) {
                sheet.AddToFabric(ref fabric);
            }
            for(var i=0; i<100; i++) {
                for(var j=0; j < 100; j++) {
                    Console.Write(fabric[i, j]);
                }
                Console.Write("\n");
            }
            return "";
        }

        protected override string PartTwoSolver() {
            return "";
        }

        private void GetSheets() {
            var sheets = _myData.Split("\n");

            foreach (var sheet in sheets) {
                var sheetParts = sheet.Split(" ");
                if (string.IsNullOrWhiteSpace(sheetParts[0])) { continue; }
                if (!short.TryParse(sheetParts[0].Replace("#", "").Trim(), out short id)) { continue; }

                var locationParts = sheetParts[2].TrimEnd(':').Split(",");
                var sizeParts = sheetParts[3].Split("x");

                if (!short.TryParse(locationParts[0], out short xCoord)) { continue; }
                if (!short.TryParse(locationParts[1], out short yCoord)) { continue; }

                if (!short.TryParse(sizeParts[0], out short width)) { continue; }
                if (!short.TryParse(locationParts[1], out short height)) { continue; }

                var mySheet = new Sheet(id, (xCoord, yCoord), (width, height));
                _mySheets.Add(mySheet);

            }
        }

        private class Sheet {

            public short Id { get; set; }
            public (short, short) Location { get; set; }
            public (short, short) Size { get; set; }

            public Sheet(short id, (short, short) location, (short, short) size) {
                Id = id;
                Location = location;
                Size = size;
            }

            public void AddToFabric(ref string[,] fabric) {
               for (var x = 0; x < 100; x++) {
                    for (var y = 0; y < 100; y++) {
                        if (x < Location.Item1 || x > Location.Item1 + Size.Item1 || y < Location.Item2 || y > Location.Item2 + Size.Item2) {
                            fabric[x, y] += 0;
                        } else if (fabric[x,y] != "0" || fabric[x, y] != Id.ToString()) {
                            fabric[x, y] = "X";
                        } else {
                            fabric[x, y] = Id;
                        }
                    }
                }
            }

        }
    }
}
