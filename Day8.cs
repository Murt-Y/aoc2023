using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.ComponentModel.DataAnnotations;

namespace aoc2023
{

    class D8
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day8.txt");
        string[] input = text.Split("\n");
        return input;
        }


        public class Coor
        { 
            public string name  { get; set; }
            public string left { get; set; }
            public string right { get; set; }
            public bool start { get; set; }
            public bool end { get; set; }

        
        }

        public int [] Solution()
        {
        int[] result = {0, 0};
        string[] input =ReadMyInput();
        int inputcount = input.Count();
        int result1=0;
        int result2=0;

        string codeinput=input[0];
        int i=2;
        List<Coor> coordinates = new List<Coor>();
        while(i<inputcount){

            string [] inst=input[i].Split('=',' ','(',')',',');
            bool startnode=false;
            bool endnode=false;
            if(inst[0][2]=='A'){
                startnode=true;
            }
            else if(inst[0][2]=='Z'){
                endnode=true;
            }
            coordinates.Add(new Coor{name=inst[0], left=inst[4],right=inst[6], start=startnode, end=endnode});
            i++;
        }
        Coor Left(Coor current){
            Coor ret =coordinates.Find(x=>x.name==current.left);
            return ret;
        }
        Coor Right(Coor current){
            Coor ret =coordinates.Find(x=>x.name==current.right);
            return ret;
        }
        //Part 1
        int steps=1;
        Coor cur =coordinates.Find(x=>x.name=="AAA");
        bool found=false;
        /*
        while(found==false){
        foreach(char c in codeinput){
            if(c=='L'){
                cur=Left(cur);
            }
            else if(c=='R'){
                cur=Right(cur);
            }
            if (cur.name=="ZZZ"){
                found=true;
                break;
            }
            steps ++;
        }
        }
        result1=steps;
        */
        //Part 2
        List<Coor> paths = new List<Coor>();
        foreach (Coor c in coordinates){
            if(c.start==true){
                paths.Add(c);
            }
        }
        found=false;
        int pathcount=paths.Count;
        List<int>shortest=new List<int>();
        int p=0;
        while(p<pathcount){
            shortest.Add(0);
            p++;
        }

            int r=0;
            while(r<pathcount)
            {
            cur=paths[r];
            steps=1;
            found=false;
            while(found==false){
            foreach(char c in codeinput){
            if(c=='L'){
                cur=Left(cur);
            }
            else if(c=='R'){
                cur=Right(cur);

            }
            if (cur.name[2]=='Z'){
                found=true;
                shortest[r]=steps;
            }
            steps++;
            }
            }
            r++;
            }

        static UInt64 gcd(UInt64 a, UInt64 b) 
        { 
            if (a == 0) 
                return b;  
            return gcd(b % a, a);  
        } 
        static UInt64 LeastCM(UInt64 a, UInt64 b) 
         { 
            return (a / gcd(a, b)) * b; 
        } 
        p=1;
        found=false;
        r=1;
        UInt64 lcm=Convert.ToUInt64(shortest[0]);
        while(r<shortest.Count)
        {
            lcm=LeastCM(lcm,Convert.ToUInt64(shortest[r]));
            Console.WriteLine(lcm);
            r++;
        }
        Console.WriteLine("Result is ..."+lcm);
        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}