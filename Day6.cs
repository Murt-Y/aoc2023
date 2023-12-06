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

    class D6
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day6.txt");
        string[] input = text.Split("\n");
        return input;
        }




        public int [] Solution()
        {
        int[] result = {0, 0};
        string[] input =ReadMyInput();
        int result1=1;
        int result2=0;

        List<string> time = new List<string>(input[0].Split(" "));
        List<string> dist = new List<string>(input[1].Split(" "));
        time.RemoveAll(s => string.IsNullOrWhiteSpace(s));
        dist.RemoveAll(s => string.IsNullOrWhiteSpace(s));
        
        //Part 1
        int beatcount=0;
        int c=time.Count;
        int i=0;
        UInt64 Dist(UInt64 speed, UInt64 time){
            UInt64 remaingtime=time-speed;
            return remaingtime*speed;
        }
        while(i<c)
        {
            UInt64 s=0;
            while(s<Convert.ToUInt64(time[i])){
            if(Dist(s, Convert.ToUInt64(time[i]))>Convert.ToUInt64(dist[i])){
                beatcount++;
            }
            s++;
            }
            result1*=beatcount;
            beatcount=0;
            i++;
        }
        


        //Part 2
        
        string t="";
        foreach (string s in time)
        {
            t+=s;
        }
        string d="";
        foreach (string s in dist)
        {
            d+=s;
        }
        UInt64 t2=Convert.ToUInt64(t);
        UInt64 d2=Convert.ToUInt64(d);

            UInt64 sp=0;
            beatcount=0;
            while(sp<t2){
            if(Dist(sp, t2)>d2){
                beatcount++;
            }
            sp++;
            }
            result2=beatcount;

        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}