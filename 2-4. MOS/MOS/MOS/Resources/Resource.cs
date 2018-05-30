using MOS.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.Enums;

namespace MOS.Resources
{
    public class Resource
    {
        Resource resource;

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string Name { get; private set; }
        public Process Creator { get; private set; }
        public List<Process> Awaiters { get; set; }
        public Kernel Kernel { get; private set; }
        public Guid Id { get; private set; }
        public List<ResourceElement> Elements { get; set; }

        public Resource(Kernel kernel, string name, Process creator)
        {
            Kernel = kernel;
            Name = name;
            Creator = creator;
            Awaiters = new List<Process>();
            Id = Guid.NewGuid();
            Elements = new List<ResourceElement>();
        }

        public Resource()
        {
        }

        public void AskForResource(Process process)
        {
            Log.Info(process.Name + " requested " + this.Name + " resource");
            process.Status = (int)ProcessState.Blocked;
            Kernel.blocked.Add(process);
            Awaiters.Add(process);
        }

        public void ReleaseResource(ResourceElement resElement)
        {
            Elements.Add(resElement);
        }

        public void ReleaseResource()
        {
            Kernel.staticResources[this] = true;
        }

        public void CreateResource(Kernel kernel, string name, Process creator, bool isStatic)
        {
            resource.Kernel = kernel;
            resource.Name = name;
            resource.Creator = creator;
            resource.Id = Guid.NewGuid();
            
            resource.Creator.Resources.Add(resource); 
            
            
            if (isStatic == true)
            {
                kernel.staticResources.Add(resource,true);
            }
            else {
                kernel.dynamicResources.Add(resource);
            }
        }


        public void DeleteResource()
        {
            Creator.Resources.Remove(this);
            Elements = null;

            foreach (Process awaiter in Awaiters)
            {
                awaiter.ActivateProcess();
            }

            if (Kernel.dynamicResources.Contains(this))
            {
                Kernel.dynamicResources.Remove(this);
            }
            else {
                Kernel.staticResources.Remove(this);
            }

            
        }

   
    }
}
