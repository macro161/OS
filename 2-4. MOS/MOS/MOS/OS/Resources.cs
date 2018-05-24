using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OS
{
    class Resources
    {
        public static List<string> dynamicResources = new List<string>();
        public static Dictionary<string, bool> staticResources = new Dictionary<string, bool>();

        public static void InitStaticResources()
        {
            staticResources.Add("supervisor memory", true);
            staticResources.Add("mos end", true);
            staticResources.Add("output stream", true);
            staticResources.Add("external memory", true);
            staticResources.Add("1chan", true);
            staticResources.Add("2chan", true);
            staticResources.Add("3chan", true);
            staticResources.Add("4chan", true);
            staticResources.Add("user memory", true);
            staticResources.Add("loader packet", true);
        }

        public static void AddDynamicResource(string name)
        {
            dynamicResources.Add(name);
        }

        public static void DeleteDynamicResource(string name)
        {
            dynamicResources.Remove(name);
        }

        public static void setStatic(string name, bool state)
        {
            staticResources[name] = state;
        }

        public static bool CheckDynamicResources(string name)
        {
            if (dynamicResources.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckStaticResources(string name)
        {
            if (staticResources[name])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckIfResourceExists(string name)
        {
            if (CheckStaticResources(name) || CheckDynamicResources(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

+        public static void TakeResource(string name)
        {
            if (CheckStaticResources(name) == true)
            {
                setStatic(name, false);
            }
            if (CheckDynamicResources(name) == true)
            {
                DeleteDynamicResource(name);
            }
        }

    }
}