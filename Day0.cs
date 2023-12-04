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

    class D0
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day4.txt");
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


        //Part 2


        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}