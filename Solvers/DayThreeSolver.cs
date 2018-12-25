using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
	public class DayThreeSolver : SolverBase {

		private List<Sheet> _mySheets = new List<Sheet>();
		private Dictionary<(int, int), int> _sheetPoints = new Dictionary<(int, int), int>();

		public DayThreeSolver(bool local) : base(3, local) {
			GetSheets();
			MapAllSheetPoints();
		}

		protected override string PartOneSolver() {
			var moreThanOne = _sheetPoints.Count(x => x.Value == 0);
			return moreThanOne.ToString();
		}

		protected override string PartTwoSolver() {
			_mySheets.ForEach(x => x.FindOverlap(_sheetPoints));
			var noOverlap = _mySheets.FirstOrDefault(x => !x.HasOverlap);
			return noOverlap.Id.ToString();
		}

		private void MapAllSheetPoints() {
			_mySheets.ForEach(x => x.PopulateMasterSheet(ref _sheetPoints));
		}

		private void GetSheets() {
			var sheets = _myData.Split("\n");

			foreach (var sheetData in sheets) {
				var newSheet = Sheet.FromString(sheetData);
				if (newSheet == null) { continue; }
				_mySheets.Add(newSheet);
			}
		}

		private partial class Sheet {
			public static Sheet FromString(string sheetData) {
				var sheetParts = sheetData.Split(" ");
				if (string.IsNullOrWhiteSpace(sheetParts[0])) { return null; }
				if (!short.TryParse(sheetParts[0].Replace("#", "").Trim(), out short id)) { return null; }

				var locationParts = sheetParts[2].TrimEnd(':').Split(",");
				var sizeParts = sheetParts[3].Split("x");

				if (!short.TryParse(locationParts[0], out short xCoord)) { return null; }
				if (!short.TryParse(locationParts[1], out short yCoord)) { return null; }

				if (!short.TryParse(sizeParts[0], out short width)) { return null; }
				if (!short.TryParse(sizeParts[1], out short height)) { return null; }

				return new Sheet(id, (xCoord, yCoord), (width, height));
			}
		}

		private partial class Sheet {

			private (short, short) _location;
			private (short, short) _size;

			public short Id { get; set; }
			public (int, int) XCoords => (_location.Item1, _location.Item1 + _size.Item1);
			public (int, int) YCoords => (_location.Item2, _location.Item2 + _size.Item2);
			public bool HasOverlap { get; set; }

			public Sheet(short id, (short, short) location, (short, short) size) {
				_location = location;
				_size = size;
				Id = id;
			}

			public void PopulateMasterSheet(ref Dictionary<(int, int), int> allPoints) {
				for (var x = this.XCoords.Item1; x < this.XCoords.Item2; x++) {
					for (var y = this.YCoords.Item1; y < this.YCoords.Item2; y++) {
						if (allPoints.ContainsKey((x, y))) {
							allPoints[(x, y)] = 0;
						} else {
							allPoints.Add((x, y), this.Id);
						}
					}
				}
			}

			public void FindOverlap(Dictionary<(int,int), int> allPoints) {
				for (var x = this.XCoords.Item1; x < this.XCoords.Item2; x++) {
					for (var y = this.YCoords.Item1; y < this.YCoords.Item2; y++) {
						if (!allPoints.ContainsKey((x,y)) || allPoints[(x,y)] == this.Id) { continue;  }
						this.HasOverlap = true;
					}
				}
			}

		}
	}
}
