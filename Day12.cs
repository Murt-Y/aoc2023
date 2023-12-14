using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Security.Permissions;
using System.Data;
using System.Xml.Schema;

namespace aoc2023
{

    class D12
    {
        List<string> coordinates = new List<string>();
        List<int[]> springs = new List<int[]>();
        List<string> coordinates2 = new List<string>();
        List<int[]> springs2 = new List<int[]>();
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day12.txt");
        string[] input = text.Split("\n");
        return input;
        }
        
        public int [] Solution()
        {
        int[] result = {0, 0};
        string[] input =ReadMyInput();
        int inputcount = input.Count();
        UInt128 result1=0;
        UInt128 result2=0;
        Dictionary<string,UInt128> passkey =  new Dictionary<string,UInt128>(); 

        //Part 1
        int xmax=input[0].Length;
        int ymax=inputcount;
        int i=0;
        foreach(string s in input){
            string[] code=s.Split(" ");
            int d=code[0].IndexOf("..");
            while(d>0){
                code[0]=code[0].Remove(d,1);
                d=code[0].IndexOf("..");
            }
            coordinates.Add(code[0]);
            string[]numinput=code[1].Split(",");
            int[] nums =new int[numinput.Length];
            int z=0;
            foreach(string x in numinput){
                nums[z]=Convert.ToInt32(x);
                z++;
            }
            springs.Add(nums);
        }

        //SADECE 2 INPUT VEREREK YAP.# geldiyse noktaya kadar git ve bak dotsa bir sonrakine geç. doğru çıkanı dictionarye ekle;
        UInt128 Check(string codex, int[] row){
            string entry=codex;
            foreach(int s in row){
                entry+=Convert.ToString(s);
            }
            UInt128 total=0;
            UInt128 restemp=0;

            if(passkey.TryGetValue(entry, out restemp)){
                total+=restemp;
            }
            else{
            char x=codex[0];
            
            if(x=='#'){
            restemp=Dash(codex, row);
            total+=restemp;
            passkey.Add(entry,restemp);
            }
            else if(x=='.'){
            restemp=Dot(codex, row);
            total+=restemp;
            passkey.Add(entry,restemp);
            }
            else if(x=='?'){
            restemp=Dash(codex, row);
            restemp+=Dot(codex, row);
            total+=restemp;
            passkey.Add(entry,restemp);
            }
        
        }
        return total;
        }
        
        UInt128 Dash(string codex, int[] row){
        int d=codex.IndexOf('.');
        int nextrow=row[0];
        if (d==-1){
            if(codex.Length<nextrow){
                return 0;
            }
        }
        else{
        string group=codex.Substring(0,d);
        if(group.Length<nextrow){
            return 0;
        }
        }
        codex=codex.Substring(nextrow);
        if(row.Length==1){
            if(codex.Contains('#')){
                return 0;
            }
            else{
                return 1;
            }
        }       
        if(codex=="" && row.Length>=1){
            return 0;
        }
        if(codex[0]=='#'){
            return 0;
        }
        
        row=row.Skip(1).ToArray();

        return Dot(codex,row);

        }
       
        UInt128 Dot(string codex, int[] row){
            codex=codex.Substring(1);
            if(codex=="" && row.Length>=1){
            return 0;
        }

            return Check(codex,row);
            
        }

        int r=0;
        while (r<coordinates.Count){
            string codex=coordinates[r];
            result1+=Check(codex, springs[r]);
            r++;
            passkey.Clear();
        }

        UInt128 t1=result1;

        //Part 2
        foreach(string s in coordinates){
            coordinates2.Add(s+"?"+s+"?"+s+"?"+s+"?"+s);
        }
        foreach(int[] s in springs){
            int[] temp =s.Concat(s).ToArray();
            temp=temp.Concat(s).ToArray();
            temp=temp.Concat(s).ToArray();
            temp=temp.Concat(s).ToArray();
            springs2.Add(temp);            
        }
        
        //Results
        Console.WriteLine("Result 1 is .... :" +result1);
        r=0;
        while (r<coordinates2.Count){
            string codex=coordinates2[r];
            int[] patx=new int[0];
            result2+=Check(codex, springs2[r]);
            r++;
            passkey.Clear();
            Console.WriteLine("Line....."+r+"...."+result2);
        }
    

        Console.WriteLine("Result 1 is .... :" +result1);
        Console.WriteLine("Result 2 is .... :" +result2);
        result[0]=0;
        result[1]=0;
        return result;
        }
    }
}