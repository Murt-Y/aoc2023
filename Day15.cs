using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Runtime.Serialization;

namespace aoc2023
{

    class D15
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day15.txt");
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
        List<string[]> boxes=new List<string[]>(256);
        int k=0;
        while(k<256){
            string[]s1=new string[] {""};
            boxes.Add(s1);
            k++;
        }
        //Part 1
        int Hash(char c, int current){
            int value = current+c;
            value*=17;
            value=value%256;
            return value;
        }

        string[] codes =input[0].Split(",");
        int currentv=0;
        foreach(string x in codes){
            char[] div={'-','='};
            string[]inst=x.Split(div);
            string label=inst[0];
            char operation='.';
            string lens=string.Empty;
            if(x.Contains('-')){
             operation='-';
            }
            else{
                operation='='; 
            }
            if(inst.Length==2){
            lens=inst[1];
            }

            int box=0;
            currentv=0;
            foreach(char c in label){
                currentv=Hash(c, currentv);
            }
            box=currentv;
            bool exist=false;
            if(operation=='='){
                int i=0;
                if(boxes[box]!=null){
                    foreach(string b in boxes[box]){
                        string[]bl=b.Split(" ");
                    if(bl[0] ==label){
                        boxes[box][i]=label+" "+lens;;
                        exist=true;
                    }
                    i++;
                    }
                }
                if(exist==false){
                    var temp=boxes[box];
                    int bsize=0;
                    if(temp!=null){
                        bsize=temp.Length;
                    }
                    Array.Resize(ref temp, bsize+1);
                    temp[bsize]=label+" "+lens;
                    boxes[box]=temp;
                }
                
            }
            if(operation=='-'){
                int i=0;
                if(boxes[box]!=null){
                    foreach(string b in boxes[box]){
                        string[]bl=b.Split(" ");
                    if(bl[0] == label){
                        var foos = new List<string>(boxes[box]);
                        foos.RemoveAt(i);
                        boxes[box]= foos.ToArray();
                    }
                    i++;
                    }
                }
            }
            
            
            currentv=0;
        }
        int boxno=0;
        UInt64 res2=0;
        foreach(string[] b in boxes){
            int slot=0;
            foreach(string s in b){
                if(s!=""){
                    string[]pl=s.Split(" ");
                    int lensno=Convert.ToInt32(pl[1]);
                    UInt64 focal=Convert.ToUInt64((boxno+1)*slot*lensno);
                    res2+=Convert.ToUInt64(focal);

                }

                
                slot++;
            }

            boxno++;
        }

        Console.WriteLine("Result 2 ......"+res2);
        //Part 2


        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}