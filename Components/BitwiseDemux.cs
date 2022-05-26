using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }

        //your code here
        private Demux[] demuxes;

        public BitwiseDemux(int iSize)
        {
            Size = iSize;
            Control = new Wire();
            Input = new WireSet(Size);

            //your code here
            Output1 = new WireSet(Size);
            Output2 = new WireSet(Size);
            demuxes = new Demux[Size];         

            for (int i = 0; i < iSize; i++)
            {
                demuxes[i] = new Demux();
                demuxes[i].ConnectControl(Control);
                demuxes[i].ConnectInput(Input[i]);
                
                Output1[i].ConnectInput(demuxes[i].Output1);
                Output2[i].ConnectInput(demuxes[i].Output2);
            }
                

        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        public override bool TestGate()
        {
            //Set all wires to 0 and control 0
            Control.Value = 0;
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 0;
                if (Output1[i].Value != 0 || Output2[i].Value != 0)
                    return false;

            }
           
            
            //Set all wires to 0 and control 1
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 0;
                Control.Value = 1;
                if (Output1[i].Value != 0 || Output2[i].Value != 0)
                    return false;
            }

            //Set all wires to 1 and control 0
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
                Control.Value = 0;
                if (Output1[i].Value != 1 || Output2[i].Value != 0)
                    return false;
            }
            //Set all wires to 1 and control 1
            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
                Control.Value = 1;
                if (Output1[i].Value != 0 || Output2[i].Value != 1)
                    return false;
            }
            return true;
        }
    }
}
