using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode {
	public class DayThreeSolver : SolverBase {

		private List<Sheet> _mySheets = new List<Sheet>();

		public DayThreeSolver(bool local) : base(3, local) {
			GetSheets();
		}

		protected override string PartOneSolver() {
			var nestedSheets = new List<Sheet>();

			foreach (var sheet in _mySheets) {
				var innerSheets = _mySheets.FindAll(x => sheet.ContainsOther(x));
				if (innerSheets == null || innerSheets.Count == 0) { continue; }
				nestedSheets.AddRange(innerSheets);
			}

			return nestedSheets.Max(x => x.Area).ToString();
		}

		protected override string PartTwoSolver() {
			return "";
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
				if (!short.TryParse(locationParts[1], out short height)) { return null; }

				return new Sheet(id, (xCoord, yCoord), (width, height));
			}
		}

		private partial class Sheet {

			private (short, short) _location;
			private (short, short) _size;

			public short Id { get; set; }
			public int Area => _size.Item1 * _size.Item2;
			public (int, int) XCoords => (_location.Item1 - 1, _location.Item1 - 1 + _size.Item1);
			public (int, int) YCoords => (_location.Item2 - 1, _location.Item2 - 1 + _size.Item2);

			public Sheet(short id, (short, short) location, (short, short) size) {
				_location = location;
				_size = size;
				Id = id;
			}

			public bool ContainsOther(Sheet other) {
				return
					other.Id != this.Id &&
					other.XCoords.Item1 >= this.XCoords.Item1 &&
					other.XCoords.Item2 <= this.XCoords.Item2 &&
					other.YCoords.Item1 >= this.YCoords.Item1 &&
					other.YCoords.Item2 <= this.YCoords.Item2;
			}

		}
	}
}
