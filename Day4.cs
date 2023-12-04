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

    class D4
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

        List<int> cardcount = new List<int>();
        int i =0;
        cardcount.Add(0);
        while(i<inputcount)
        {
            cardcount.Add(1);
            i++;
        }
        //Part 1
        int k=1;
        foreach (string s in input)
        {
            string [] cards = s.Split(':' , '|');
            cards[1]=cards[1].Trim();
            cards[2]=cards[2].Trim();
            List<string> winning = new List<string>(cards[1].Split(" "));
            List<string> numbers = new List<string>(cards[2].Split(" "));
            winning.RemoveAll(s => string.IsNullOrWhiteSpace(s));
            numbers.RemoveAll(s => string.IsNullOrWhiteSpace(s));

            int nowinning=0;

            foreach (string w in winning){
                if (numbers.Contains(w))
                {
                    nowinning++;
                }
            }
            if(nowinning>0){
            result1+=Convert.ToInt32(Math.Pow(2, nowinning-1));
            }


            //Part 2
            int a=0;
            while(a<nowinning){
                cardcount[k+a+1]=cardcount[k+a+1]+cardcount[k];
                a++;
            }

        k++;
        }

        foreach(int t in cardcount)
        {
            result2+=t;
        }

        //Part 2


        //Results

        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}