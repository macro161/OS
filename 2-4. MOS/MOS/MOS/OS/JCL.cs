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

        public JCL(Kernel kernel, int priority, int status, Guid id, int pointer, Resource[] resources) : base(kernel, priority, status, resources, id, pointer, "JCL") { }

        public override void AddResource(Resource resource)
        {
            throw new NotImplementedException();
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
                if (name.Length != 0 && name.Length < 25 && dataSeg != null && codeSeg != null && codeSeg.Count != 0)
                    programs.Add(pr);
            }

            // saves to Supervisory memory
            SupervisoryMemory.ProgramList = programs;
        }
    }
}
