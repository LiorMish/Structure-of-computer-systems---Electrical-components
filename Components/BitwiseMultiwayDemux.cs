using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a demux with k outputs, each output with n wires. The input also has n wires.

    class BitwiseMultiwayDemux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        //your code here
        private BitwiseDemux[] bitwiseDemuxes;

        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            //your code here
            bitwiseDemuxes = new BitwiseDemux[(int)Math.Pow(2, cControlBits) - 1];

            for (int i = 0; i < bitwiseDemuxes.Length; i++)
                bitwiseDemuxes[i] = new BitwiseDemux(Size);



            int controlsCounter = 0;
            int demuxesCounter = bitwiseDemuxes.Length - 1;
            int demuxesPerLevel = bitwiseDemuxes.Length / 2 + 1;

            while (controlsCounter < cControlBits)
            {
                for (int i = 0; i < demuxesPerLevel; i++)
                {
                    bitwiseDemuxes[demuxesCounter].ConnectControl(Control[controlsCounter]);
                    demuxesCounter--;
                }
                demuxesPerLevel = demuxesPerLevel / 2;
                controlsCounter++;
            }

            demuxesCounter = 0;
            bitwiseDemuxes[0].ConnectInput(Input);
            for (int i = 1; i < bitwiseDemuxes.Length; i = i + 2)
            {
                bitwiseDemuxes[i].ConnectInput(bitwiseDemuxes[demuxesCounter].Output1);
                bitwiseDemuxes[i + 1].ConnectInput(bitwiseDemuxes[demuxesCounter].Output2);
                demuxesCounter++;
            }

            demuxesCounter = bitwiseDemuxes.Length - 1;
            for (int i = Outputs.Length - 1; i >= 0; i = i - 2)
            {
                Outputs[i].ConnectInput(bitwiseDemuxes[demuxesCounter].Output2);
                Outputs[i - 1].ConnectInput(bitwiseDemuxes[demuxesCounter].Output1);
                demuxesCounter--;
            }

        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }




        public override bool TestGate()
        {

            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
            }

            for (int i = 0; i < Size; i++)
            {
                if (Outputs[0][i].Value != 1)
                {
                    return false;
                }
            }


            Control[0].Value = 1;

            for (int i = 0; i < Size; i++)
            {
                if (Outputs[1][i].Value != 1)
                {
                    return false;
                }
            }

            Control[0].Value = 0;

            for (int i = 0; i < Size; i++)
            {
                Input[i].Value = 1;
            }

            for (int i = 0; i < Control.Size; i++)
            {
                Control[i].Value = 1;
            }

            for (int i = 0; i < Size; i++)
            {
                if (Outputs[Outputs.Length - 1][i].Value != 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

