using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AdventOfCode {

    public abstract class AdventOfCodeSolver {

        private string mySession = PrivateData.MySession;

        private string dataUrl;

        public AdventOfCodeSolver(string dUrl) {
            dataUrl = dUrl;
        }

        public string SolvePartOne() {
            return PartOneSolver();
        }

        public string SolvePartTwo() {
            return PartTwoSolver();
        }

        protected string GetData() {
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
