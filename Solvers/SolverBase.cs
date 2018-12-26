using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AdventOfCode {

    public abstract class SolverBase : ISolver {

        private string _mySession = Solvers.PrivateData.SessionToken;

		private string _dataUrl = @"https://adventofcode.com/2018/day/{0}/input";
		private string _localFile = @"C:\Users\mkwiecien\Source\Repos\Advent-Of-Code-2018\LocalInput\day{0}.txt";

        protected string _myData;

        public SolverBase(short solveDay, bool local) {
            _myData = GetData(solveDay, local);
        }

        public string SolvePartOne() {
            return PartOneSolver();
        }

        public string SolvePartTwo() {
            return PartTwoSolver();
        }

        protected string GetData(short solveDay, bool local) {
			return local ? LoadFromFile(solveDay) : LoadFromWeb(solveDay);
        }

		private string LoadFromFile(short solveDay) {
			var fileLocation = string.Format(_localFile, solveDay);
			return File.ReadAllText(fileLocation);
		}

		private string LoadFromWeb(short solveDay) {
			var dataUrl = string.Format(_dataUrl, solveDay);
			var req = WebRequest.Create(dataUrl);
			req.Method = "GET";
			req.Headers.Add($"cookie: session={_mySession}");
			var resp = req.GetResponse();

			string content;
			using (var ms = resp.GetResponseStream()) {
				using (var reader = new StreamReader(ms)) {
					content = reader.ReadToEnd();
				}
			}
			return content;
		}


        protected abstract string PartOneSolver();
        protected abstract string PartTwoSolver();

    }
}
