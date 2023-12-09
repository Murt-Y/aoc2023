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

    class D9
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day9.txt");
        string[] input = text.Split("\n");
        return input;
        }




        public int [] Solution()
        {
        int[] result = {0, 0};
        string[] input =ReadMyInput();
        int inputcount = input.Count();
        int result1=0;
        int result2=0;
        //Part 1
        List<int[]> dataset = new List<int[]>();
        foreach (string s in input){
            dataset.Add(Array.ConvertAll(s.Split(' '), int.Parse));
        }
        int Seq(int[] pattern){
            int l=pattern.Length;
            int[] lowpattern= new int[l-1];
            int i=0;
            while(i<l-1){
                lowpattern[i]=pattern[i+1]-pattern[i];
                i++;
            }
            i=0;
            bool allzero=true;
            while(i<l-1)
            {
                if(lowpattern[i]!=0){
                    allzero=false;
                    break;
                }
                i++;
            }
            if (allzero==true){
                return 0;
            }
            else {
                return lowpattern[l-2]+Seq(lowpattern);
            }
        }
        foreach(int[]m in dataset){
            int l= m.Length;
            result1+=m[l-1]+Seq(m);
        }

 


        //Part 2
        int BSeq(int[] pattern){
            int l=pattern.Length;
            int[] lowpattern= new int[l-1];
            int i=0;
            while(i<l-1){
                lowpattern[i]=pattern[i+1]-pattern[i];
                i++;
            }
            i=0;
            bool allzero=true;
            while(i<l-1)
            {
                if(lowpattern[i]!=0){
                    allzero=false;
                    break;
                }
                i++;
            }
            if (allzero==true){
                return 0;
            }
            else {
                return lowpattern[0]-BSeq(lowpattern);
            }
        }
        foreach(int[]m in dataset){
            int l= m.Length;
            result2+=m[0]-BSeq(m);
        }

        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}