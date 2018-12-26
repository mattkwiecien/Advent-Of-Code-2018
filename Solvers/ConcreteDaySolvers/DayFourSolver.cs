using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode {
    public class DayFourSolver : SolverBase {

        private List<Guard> _guards = new List<Guard>();

        public DayFourSolver(bool local) : base(4, local) {
            var records = _myData.Split("\n");
            var allRecords = CreateGuardRecords(records);
            CreateGuardList(allRecords);
        }

        private void CreateGuardList(List<GuardRecord> allRecords) {
            _guards = new List<Guard>();

            Guard currentGuard = null;
            foreach (var gRecord in allRecords) {
                if (!gRecord.IsShiftRecord && currentGuard != null) {
                    currentGuard.SleepJournal.Add(gRecord);
                    continue;
                }

                var existingRecord = _guards.FirstOrDefault(x => x.GuardId == gRecord.GuardID);
                if(existingRecord != null) {
                    currentGuard = existingRecord;
                } else {
                    currentGuard = new Guard(gRecord.GuardID);
                    _guards.Add(currentGuard);
                }          
            }
        }

        private List<GuardRecord> CreateGuardRecords(string[] records) {
            var allRecords = new List<GuardRecord>();
            foreach (var record in records) {
                var gRec = GuardRecord.FromRecord(record);
                if (gRec == null) { continue; }
                allRecords.Add(gRec);
            }
            return allRecords.OrderBy(x => x.Dt).ToList();
        }

        protected override string PartOneSolver() {
            var sleeper = _guards.OrderByDescending(x => x.TotalMinutesSlept).FirstOrDefault();
            return (sleeper.GuardId * sleeper.MinuteMostAsleep).ToString();
        }

        protected override string PartTwoSolver() {
            return "";
        }

        private class Guard {
            public int GuardId { get; set; }
            public List<GuardRecord> SleepJournal { get; set; }
            public int TotalMinutesSlept => GetMinutesAsleep();
            public int MinuteMostAsleep => GetMinuteMostAsleep();

            public Guard(int guardId) {
                this.GuardId = guardId;
                SleepJournal = new List<GuardRecord>();
            }

            private int GetMinutesAsleep() {
                var cnt = 0;
                if (SleepJournal.Count == 0) { return cnt; }

                for (var i = 0; i < SleepJournal.Count; i = i + 2) {
                    var span = SleepJournal[i + 1].Dt - SleepJournal[i].Dt;
                    cnt += int.Parse(span.TotalMinutes.ToString());
                }

                return cnt;
            }

            private int GetMinuteMostAsleep() {
                var minuteCount = new Dictionary<int, int>();

                for (var j = 0; j < this.SleepJournal.Count; j = j + 2) {
                    var span = (this.SleepJournal[j + 1].Dt - this.SleepJournal[j].Dt).TotalMinutes;
                    for (var i = this.SleepJournal[j].Dt.Minute; i < this.SleepJournal[j].Dt.Minute + span; i++) {
                        if (minuteCount.ContainsKey(i)) {
                            minuteCount[i]++;
                        } else {
                            minuteCount.Add(i, 1);
                        }
                    }
                }

                return minuteCount.OrderByDescending(x => x.Value).FirstOrDefault().Key;
            }
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
