using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AdventOfCode {

    public abstract class SolverBase : ISolver {

        private string mySession = PrivateData.MySession;
        
        protected string _myData;

        public SolverBase(string dUrl) {
            _myData = GetData(dUrl);
        }

        public string SolvePartOne() {
            return PartOneSolver();
        }

        public string SolvePartTwo() {
            return PartTwoSolver();
        }

        protected string GetData(string dataUrl) {
            var req = WebRequest.Create(dataUrl);
            req.Method = "GET";
            req.Headers.Add($"cookie: session={mySession}");
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
