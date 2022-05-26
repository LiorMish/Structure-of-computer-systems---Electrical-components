using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is an example of a testing code that you should run for all the gates that you create

            WireSet s = new WireSet(4);
            s.SetValue(12);

            //Create a gate
            AndGate and = new AndGate();
            Console.WriteLine(and + "");
            //Test that the unit testing works properly
            if (!and.TestGate())
                Console.WriteLine("bugbug");

            //Create a gate
            OrGate or = new OrGate();
            Console.WriteLine(or + "");
            //Test that the unit testing works properly
            if (!or.TestGate())
                Console.WriteLine("bugbug");

            //Create a gate
            XorGate xor = new XorGate();
            Console.WriteLine(xor + "");
            //Test that the unit testing works properly
            if (!xor.TestGate())
                Console.WriteLine("bugbug");

            //Create a gate
            MuxGate mux = new MuxGate();
            Console.WriteLine(mux + "");
            //Test that the unit testing works properly
            if (!mux.TestGate())
                Console.WriteLine("bugbug");

            //Create a gate
            Demux demux = new Demux();
            Console.WriteLine(demux + "");
            //Test that the unit testing works properly
            if (!demux.TestGate())
                Console.WriteLine("bugbug");


            //Create a gate
            MultiBitAndGate multiAnd = new MultiBitAndGate(4);
            Console.WriteLine(multiAnd + "");
            //Test that the unit testing works properly
            if (!multiAnd.TestGate())
                Console.WriteLine("bugbug");

            //Create a gate
            MultiBitOrGate multiOr = new MultiBitOrGate(4);
            Console.WriteLine(multiOr + "");
            //Test that the unit testing works properly
            if (!multiOr.TestGate())
                Console.WriteLine("bugbug");


            //Create a gate
            BitwiseAndGate bitWiseAnd = new BitwiseAndGate(4);
            Console.WriteLine(bitWiseAnd + "");
            //Test that the unit testing works properly
            if (!bitWiseAnd.TestGate())
                Console.WriteLine("bugbug");

            //Create a gate
            BitwiseNotGate bitWiseNot = new BitwiseNotGate(4);
            Console.WriteLine(bitWiseNot + "");
            //Test that the unit testing works properly
            if (!bitWiseNot.TestGate())
                Console.WriteLine("bugbug");

            //Create a gate
            BitwiseOrGate bitWiseOr = new BitwiseOrGate(4);
            Console.WriteLine(bitWiseOr + "");
            //Test that the unit testing works properly
            if (!bitWiseOr.TestGate())
                Console.WriteLine("bugbug");


         

            //Create a gate
            BitwiseDemux bd = new BitwiseDemux(4);
            Console.WriteLine(bd + "");
            //Test that the unit testing works properly
            if (!bd.TestGate())
                Console.WriteLine("bugbug1111");
            



            BitwiseMultiwayMux m = new BitwiseMultiwayMux(7, 3);
            if (!m.TestGate())
                Console.WriteLine("bugbug- multiwat mux");

            BitwiseMultiwayDemux md = new BitwiseMultiwayDemux(4, 4);
            if (!md.TestGate())
                Console.WriteLine("bugbug- multiwat demux");

            MultiBitAdder adder = new MultiBitAdder(2);
            adder.Input1[0].Value = 0;
            adder.Input1[1].Value = 0;
            adder.Input2[0].Value = 0;
            adder.Input2[1].Value = 0;
            adder.Input1[0].Value = 0;

            WireSet w = new WireSet(5);
            w.Set2sComplement(8);
            Console.WriteLine(w + "   8");
            int x = w.Get2sComplement();
            Console.WriteLine(x + "   888");
            w.Set2sComplement(-8);
            Console.WriteLine(w + "    -8");
            int y = w.Get2sComplement();
            Console.WriteLine(y + "    -888");

            ALU a = new ALU(5);

            SingleBitRegister sbr = new SingleBitRegister();
            if (!sbr.TestGate())
                Console.WriteLine(sbr + "debug");

            MultiBitRegister mbr = new MultiBitRegister(4);
            if (!mbr.TestGate())
                Console.WriteLine(mbr + "debug");

            Memory memo = new Memory(2, 6);
            if (!memo.TestGate())
                Console.WriteLine(memo + "debug");





            //Now we ruin the nand gates that are used in all other gates. The gate should not work properly after this.
            /*NAndGate.Corrupt = true;
            if (and.TestGate())
                Console.WriteLine("bugbug");
           */

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
