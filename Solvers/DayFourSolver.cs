using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode {
	public class DayFourSolver : SolverBase {

		private List<GuardRecord> _guardRecords = new List<GuardRecord>();

		public DayFourSolver(bool local) : base(4, local) {
			var records = _myData.Split("\n");

			foreach (var record in records) {
				var gRec = GuardRecord.FromRecord(record);
				if (gRec == null) { continue; }
				_guardRecords.Add(gRec);
			}
			_guardRecords = _guardRecords.OrderBy(x => x.Dt).ToList();
			foreach(var rec in _guardRecords) {
				System.Console.WriteLine($"{rec.Dt}: {rec.Desc}");
			}
		}

		protected override string PartOneSolver() {
			var SleepJournal = new Dictionary<int, List<DateTime>>();
			var currentGuardID = 0;
			foreach (var gRecord in _guardRecords) {
				if (gRecord.IsShiftRecord) {
					currentGuardID = gRecord.GuardID;
					if (!SleepJournal.ContainsKey(currentGuardID)) {
						SleepJournal.Add(currentGuardID, new List<DateTime>());
					}
					continue;
				}
				SleepJournal[currentGuardID].Add(gRecord.Dt);
			}

			var MaxGuardSleep = new Dictionary<int, int>();
			foreach (var kvp in SleepJournal) {
				MaxGuardSleep.Add(kvp.Key, 0);
				if (kvp.Value.Count == 0) { continue; }

				for (var i = 0; i < kvp.Value.Count; i = i + 2) {
					var minutes = kvp.Value[i + 1] - kvp.Value[i];
					MaxGuardSleep[kvp.Key] += int.Parse(minutes.TotalMinutes.ToString());
				}
			}
			var sleeper = MaxGuardSleep.OrderByDescending(x => x.Value).ToList().First();
			var sleepSchedule = SleepJournal[sleeper.Key];

			var minuteCount = new Dictionary<int, int>();
			for (var j = 0; j < sleepSchedule.Count; j = j + 2){
				//for
			}
			return "";		
		}

		protected override string PartTwoSolver() {
			return "";
		}

		private partial class GuardRecord {
			public DateTime Dt { get; set; }
			public string Desc { get; set; }
			public bool IsShiftRecord => Desc.Contains('#');
			public int GuardID => IsShiftRecord ? int.Parse(Desc.Split(" ")[1].Trim('#')) : 0;
		}

		private partial class GuardRecord {
			public static GuardRecord FromRecord(string record) {
				var gRecord = new GuardRecord();
				//[1518-04-13 00:03] Guard #613 begins shift
				var recParts = record.Split("]");
				if (recParts.Length < 2) { return null; }

				gRecord.Desc = recParts[1].Trim(' ');
				gRecord.Dt = DateTime.Parse(recParts[0].Trim('['));
				return gRecord;
			}
		}
	}

}
