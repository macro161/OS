using MOS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class JCL : Process
    {
        public static string[] resources = new String[] { "Check program" };
        public static JCL jcl = new JCL(65, "JCL", (int)ProcessState.Blocked, 0);
        public static String output = "";
        public JCL(int priority, String id, int status, int pointer) : base(priority, id, status, pointer, resources) { }

        public override void Run(int pointer)
        {
            if (IsSupervisoryCommandsOk())
            {
                Resources.dynamicRes.add("Program ok");
                Planner.blocked.Add(jcl);
                // jcl.setPointer(0);
                jcl.SetStatus((int)ProcessState.Blocked);
                jcl.SetPriority(jcl.GetPriority() - 1);
                Planner.LineUp();
            }
            else
            {
                Resources.dynamicRes.add("Program not ok");
                Planner.blocked.Add(jcl);
                // jcl.setPointer(0);
                jcl.SetStatus((int)ProcessState.Blocked);
                jcl.SetPriority(jcl.GetPriority() - 1);
            }
        }

        private bool IsSupervisoryCommandsOk()
        {
            throw new NotImplementedException();
        }
    }
}
