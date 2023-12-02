using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace aoc2023
{

    class D2
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day2.txt");
        string[] input = text.Split("\n");
        return input;

        }
        List<string> games = new List<string>();
        public bool Check(int game){
            string [] l=games[game].Split(";");
            foreach(string t in l)
            {
                string[] m=t.Split(",");
                int g=0;
                int r=0;
                int b=0;
                foreach( string s in m){
                    string [] nos =s.Split(" ");
                    if(nos[2]=="green"){
                        g+=Convert.ToInt32(nos[1]);
                    }
                    else if(nos[2]=="blue"){
                        b+=Convert.ToInt32(nos[1]);
                    }
                    else if(nos[2]=="red"){
                        r+=Convert.ToInt32(nos[1]);
                    }
                }
                if(g>13 || b>14 ||r>12){
                    return false;
                }
            }
            return true;
        }

        public int CheckM(int game){
            int gmin=0;
            int rmin=0;
            int bmin=0;
            string [] l=games[game].Split(";");
            foreach(string t in l)
            {
                string[] m=t.Split(",");
                int g=0;
                int r=0;
                int b=0;
                foreach( string s in m){
                    string [] nos =s.Split(" ");
                    if(nos[2]=="green"){
                        g+=Convert.ToInt32(nos[1]);
                    }
                    else if(nos[2]=="blue"){
                        b+=Convert.ToInt32(nos[1]);
                    }
                    else if(nos[2]=="red"){
                        r+=Convert.ToInt32(nos[1]);
                    }
                }
                if(g>gmin){
                    gmin=g;
                }
                if(b>bmin){
                    bmin=b;
                }
                if(r>rmin){
                    rmin=r;
                }
            }
            return gmin*rmin*bmin;
        }
        public int [] Solution()
        {
        int[] result = {0, 0};
        string[] input =ReadMyInput();
        int inputcount = input.Count();
        int result1=0;
        int result2=0;
        //Part 1
        

         foreach (string s in input)
        {
                    string[]t=s.Split(":");
                    games.Add(t[1]);

        }
        int k=0; 
        foreach(string x in games){
        if(Check(k)){
            result1+=k+1;
        }
        k++;
        }

        result[0]=result1;
        

        
        // Part II
        k=0; 
        foreach(string x in games){

            result2+=CheckM(k);
            k++;
        }
        
        result[1]=result2;
        return result;
        }



    }
}