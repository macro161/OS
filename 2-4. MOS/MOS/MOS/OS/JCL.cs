using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class JCL : Process
    {
        public static List<String> fromMemory = new List<String>();
        public static Dictionary<int, IEnumerable<String>> seperatedPrograms = new Dictionary<int, IEnumerable<String>>();
        public static List<Program> programs = new List<Program>();
        //public static List<string> pvz = new List<string>()
        //{
        //    "program1",
        //    "DATA",
        //    "10",
        //    "10",
        //    "CODE",
        //    "LR00",
        //    "CR01",
        //    "JE80",
        //    "HALT",
        //    "program2",
        //    "DATA",
        //    "10",
        //    "10",
        //    "CODE",
        //    "LR00",
        //    "CR01",
        //    "JE80",
        //    "HALT"
        //};

        public JCL(Kernel kernel, int priority, int status, Guid id, int pointer, Resource[] resources) : base(kernel, priority, status, resources, id, pointer, "JCL") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public override void DecrementPriority()
        {
           
        }

        public override void Run()
        {
            fromMemory = SupervisoryMemory.Memory;
            int counter = 0;

            // seperates programs
            while (true)
            {
                counter++;
                List<String> list = fromMemory.TakeWhile(line => line != "HALT").ToList();
                if (list.Count == 0)
                    break;
                fromMemory.RemoveRange(0, list.Count + 1);
                seperatedPrograms.Add(counter, list);
            }

            // seperates into code and data segments, creates Program list
            foreach (var program in seperatedPrograms)
            {
                var name = (program.Value.TakeWhile(line => line != "DATA").ToArray())[0];
                var dataSeg = program.Value.SkipWhile(line => line != "DATA").Skip(1).TakeWhile(line => line != "CODE").ToList();
                var codeSeg = program.Value.SkipWhile(line => line != "CODE").Skip(1).TakeWhile(line => line != null).ToList();
                Program pr = new Program(name, dataSeg, codeSeg);
                if (name.Length != 0 && name.Length < 25 && dataSeg != null && codeSeg != null && codeSeg.Count != 0
                    && program.Value.Contains("DATA") && program.Value.Contains("CODE")
                    && checkCommands(codeSeg))
                    programs.Add(pr);
            }

            // saves to Supervisory memory
            SupervisoryMemory.ProgramList = programs;

            //    foreach (var program in programs)
            //    {
            //        Debug.WriteLine(program.name);
            //        foreach (var a in program.dataSegment)
            //            Debug.WriteLine($"*{a}");
            //        foreach (var a in program.codeSegment)
            //            Debug.WriteLine($"*{a}");
            //    }
        }

        public static bool checkCommands(List<string> code)
        {
            string[] commands = new string[] { "LR", "SR", "RR", "AD", "SB", "CR", "MU", "DI", "PY", "JU", "JG", "JE", "JL", "SM", "LM", "LO", "PY", "HALT" };
            bool isCorrect = false;

            foreach (string str in code)
            {
                foreach (string command in commands)
                {
                    if (str.Length > 3)
                    {
                        string strCom = str.Substring(0, 2);
                        string strAdd = str.Substring(2, 2);
                        if (strCom == command 
                            && System.Text.RegularExpressions.Regex.IsMatch(strAdd, @"\A\b[0-9a-fA-F]+\b\Z") 
                            && str.Length == 4)
                        {
                            isCorrect = true;
                            break;
                        }
                    }
                }
                if (!isCorrect)
                    return false;
            }
            return true;
        }
    }
}
