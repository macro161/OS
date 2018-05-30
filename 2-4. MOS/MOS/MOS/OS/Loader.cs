using MOS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.RealMachine;

namespace MOS.OS
{
    public class Loader : Process
    {
        public MemoryInfoResourceElement Element { get; set; }
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Loader(Kernel kernel, Process father, int priority, int status, Guid id, int pointer, List<Resource> resources)  { }

        public Loader()
        {
        }

        public override void DecrementPriority()
        {
        }

        public override void Run()
        {
            Log.Info("Loader process is running.");

            switch (Pointer)
            {
                case 0:
                    Pointer = 1;
                    Kernel.dynamicResources.First(res => res.Name == "LOADERPACKET").AskForResource(this);
                    break;
                case 1:
                    Pointer = 2;
                    Kernel.staticResources.First(res => res.Key.Name == "CHAN4").Key.AskForResource(this);
                    break;
                case 2:
                    Pointer = 3;
                    string[,] data = new string[16,16];
                    int x = 0;
                    int y = 0;
                    Program program = HardDisk.ProgramList.First(o => o.name == Element.Value);
                    foreach (string dataSeg in program.dataSegment)
                    {
                        data[x, y] = dataSeg;
                        y++;
                        if (y > 15)
                        {
                            y = 0;
                            x++;
                        }
                    }

                    x = 8;
                    y = 0;

                    foreach (string codeSeg in program.codeSegment)
                    {
                        data[x, y] = codeSeg;
                        y++;
                        if (y > 15)
                        {
                            y = 0;
                            x++;
                        }
                    }

                    int[] tracks = new int[16];
                    int ptr = Element.Ptr.ToHex();

                    for (int i = 0; i < 16; i++)
                    {
                        tracks[i] = RealMachine.RealMachine.memory.IntAt(ptr, i);
                    }

                    string[] dataToSend = new string[16];

                    for (int i = 0; i < 16; i++)
                    {
                        for (int j = 0; j < 16 ; j++)
                        {
                            dataToSend[j] = data[i, j];
                        }

                        ChannelsDevice.XCHG(tracks[i], dataToSend);
                    }
                    Log.Info("Task in memory.");
                    Kernel.dynamicResources.First(res => res.Name == "FROMLOADER").ReleaseResource(new ResourceElement(receiver : Element.Sender));
                    break;
                case 3:
                    Pointer = 0;
                    Kernel.staticResources.First(res => res.Key.Name == "CHAN4").Key.ReleaseResource();
                    break;

                    
            }
        }
    }
}
