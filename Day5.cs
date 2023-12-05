using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace aoc2023
{

    class D5
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day5.txt");
        string[] input = text.Split("\n\n");
        return input;
        }




        public int [] Solution()
        {
        int[] result = {0, 0};
        string[] input =ReadMyInput();
        int inputcount = input.Count();
        int result1=0;
        int result2=0;
        int i=0;

        string[] seeds= input[0].Split(" ");
        string [] seedtosoil =input[1].Split("\n");
        string [] soiltofert =input[2].Split("\n");
        string [] ferttowater =input[3].Split("\n");
        string [] watertolight =input[4].Split("\n");
        string [] lighttotemp =input[5].Split("\n");
        string [] temptohum =input[6].Split("\n");
        string [] humtoloc =input[7].Split("\n");

        ulong StoS(UInt64 s)
        {
            foreach (string k in seedtosoil){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=source && s<=source+range){
                    return (s+dest-source);
                }
            }

            return s;
        }
        UInt64 StoF(UInt64 s)
        {
            foreach (string k in soiltofert){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=source && s<=source+range){
                    return (s+dest-source);
                }
            }

            return s;
        }
        UInt64 FtoW(UInt64 s)
        {
            foreach (string k in ferttowater){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=source && s<=source+range){
                    return (s+dest-source);
                }
            }

            return s;
        }
        UInt64 WtoL(UInt64 s)
        {
            foreach (string k in watertolight){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=source && s<=source+range){
                    return (s+dest-source);
                }
            }

            return s;
        }
        UInt64 LtoT(UInt64 s)
        {
            foreach (string k in lighttotemp){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=source && s<=source+range){
                    return (s+dest-source);
                }
            }

            return s;
        }
        UInt64 TtoH(UInt64 s)
        {
            foreach (string k in temptohum){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=source && s<=source+range){
                    return (s+dest-source);
                }
            }

            return s;
        }
        UInt64 HtoL(UInt64 s)
        {
            foreach (string k in humtoloc){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=source && s<=source+range){
                    return (s+dest-source);
                }
            }

            return s;
        }
        UInt64 loc =UInt64.MaxValue;

        //Part 1
        foreach(string s in seeds)
        {
            UInt64 p=Convert.ToUInt64(s);
            UInt64 l= HtoL(TtoH(LtoT(WtoL(FtoW(StoF(StoS(p)))))));
            if(l<loc)
            {
                loc=l;
            }
        }
        

        Console.WriteLine("Result for Part 1   : " + loc);
        
        //Part 2
        int no=seeds.Length;
        UInt64 loc2 =UInt64.MaxValue;

        ulong SoiltoSeed(UInt64 s)
        {
            foreach (string k in seedtosoil){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=dest && s<=dest+range){
                    return (s-dest+source);
                }
            }

            return s;
        }
        UInt64 FerttoSoil(UInt64 s)
        {
            foreach (string k in soiltofert){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=dest && s<=dest+range){
                    return (s-dest+source);
                }
            }

            return s;
        }

        UInt64 WatertoFert(UInt64 s)
        {
            foreach (string k in ferttowater){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=dest && s<=dest+range){
                    return (s-dest+source);
                }
            }

            return s;
        }
        UInt64 LighttoWater(UInt64 s)
        {
            foreach (string k in watertolight){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=dest && s<=dest+range){
                    return (s-dest+source);
                }
            }

            return s;
        }
        UInt64 TemptoLight(UInt64 s)
        {
            foreach (string k in lighttotemp){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=dest && s<=dest+range){
                    return (s-dest+source);
                }
            }

            return s;
        }
        UInt64 HumtoTemp(UInt64 s)
        {
            foreach (string k in temptohum){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=dest && s<=dest+range){
                    return (s-dest+source);
                }
            }

            return s;
        }
        UInt64 LoctoHum(UInt64 s)
        {
            foreach (string k in humtoloc){
                string[] t =k.Split(" ");
                UInt64 dest=Convert.ToUInt64(t[0]);
                UInt64 source=Convert.ToUInt64(t[1]);
                UInt64 range=Convert.ToUInt64(t[2]);

                if (s>=dest && s<=dest+range){
                    return (s-dest+source);
                }
            }

            return s;
        }


        loc2=0;
        bool findresult=false;
        while(!findresult)
        {
            UInt64 targetseed=SoiltoSeed(FerttoSoil(WatertoFert(LighttoWater(TemptoLight(HumtoTemp(LoctoHum(loc2)))))));
            i=0;
            while(i<no)
            {
                UInt64 k=Convert.ToUInt64(seeds[i+1]);
                UInt64 zo=Convert.ToUInt64(seeds[i]);
                if(targetseed>=zo && targetseed<=zo+k)
                {
                    Console.WriteLine("Result for Part 2   : " + loc2);
                    findresult=true;

                }

                i++;
                i++;
            }

            loc2++;
        }


        /*
        while(i<no)
        {
            UInt64 k=Convert.ToUInt64(seeds[i+1]);
            UInt64 z=k;
            UInt64 zo=Convert.ToUInt64(seeds[i]);
            while(k>0)
            {
                UInt64 l= HtoL(TtoH(LtoT(WtoL(FtoW(StoF(StoS(zo+z-k)))))));
                k--;
            if(l<loc2)
            {
                loc2=l;
            }
            }

            i++;
            i++;
        }
        */
        
        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}