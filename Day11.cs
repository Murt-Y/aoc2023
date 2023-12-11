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

    class D11
    {
        public class Galaxy
        { 
            public int x { get; set; }
            public int y { get; set; }
        }
        List<string> coordinates = new List<string>();
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day11.txt");
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
        int xmax=input[0].Length;
        int ymax=inputcount;
        int i=0;
        foreach(string s in input){
            coordinates.Add(s);
        }
        /*
        void ExpadLine(int line){
                coordinates.Insert(line,coordinates[line]);
            }
        void ExpandRow(int line){
                int k=0;
                while(k<ymax){
                    coordinates[k]=coordinates[k].Insert(line, ".");
                    k++;
                }
            }
        
        void CheckH(){
            i=0;
            bool alldots=true;
            while(i<xmax){
                int k=0;
                List<char> verticalline =new List<char>();
                while(k<ymax){
                    verticalline.Add(coordinates[k][i]);
                    k++;
                }
                if(verticalline.Contains('#')){
                    alldots=false;
                }
                else{
                    ExpandRow(i);
                    i++;
                    xmax++;
                }
                i++;
            }
        }
        
        void CheckV(){
            i=0;
            bool alldots=true;
            while(i<ymax){
                if(coordinates[i].Contains('#')){
                    alldots=false;
                }
                else{
                    ExpadLine(i);
                    i++;
                    ymax++;
                }
                i++;
            }
        }
        CheckV();
        CheckH();
        */

        List<int> voidrows=new List<int>();
        List<int> voidcols=new List<int>();
        void CheckH(){
            i=0;
            bool alldots=true;
            while(i<xmax){
                int k=0;
                List<char> verticalline =new List<char>();
                while(k<ymax){
                    verticalline.Add(coordinates[k][i]);
                    k++;
                }
                if(verticalline.Contains('#')){
                    alldots=false;
                }
                else{
                    voidcols.Add(i);
                }
                i++;
            }
        }
        
        void CheckV(){
            i=0;
            bool alldots=true;
            while(i<ymax){
                if(coordinates[i].Contains('#')){
                    alldots=false;
                }
                else{
                    voidrows.Add(i);
                }
                i++;
            }
        }
        CheckV();
        CheckH();


        i=0;
        int k=0;
        List<Galaxy>galaxies =new List<Galaxy>();
        while(i<ymax){
            while(k<xmax){
                if(coordinates[i][k]=='#'){
                    galaxies.Add(new Galaxy{x=k, y=i});
                }
                k++;
            }
            k=0;
            i++;
        }
        int totaldist=0;
        UInt64 totaldist2=0;

        foreach(Galaxy g in galaxies){
            foreach(Galaxy h in galaxies){
                int xdist=Math.Abs(h.x-g.x);
                int ydist=Math.Abs(h.y-g.y);
                int xvoid=0;
                int yvoid=0;
                int xs=g.x;
                int xb=h.x;
                if(h.x<=g.x){
                    xs=h.x;
                    xb=g.x;
                }
                int ys=g.y;
                int yb=h.y;
                if(h.y<=g.y){
                    ys=h.y;
                    yb=g.y;
                }
                foreach(int v in voidcols){
                    if(xs<v && v<xb){
                        xvoid++;
                    }
                }
                foreach(int v in voidrows){
                    if(ys<v && v<yb){
                        yvoid++;
                    }
                }
                totaldist2+=Convert.ToUInt64(xdist+ydist+(xvoid*(1000000-1))+(yvoid)*(1000000-1));
                totaldist+=xdist+ydist+(xvoid)+(yvoid);
            }
        }
        result1=totaldist/2;
        Console.WriteLine("Part 2 answer is ...... :   "+totaldist2/2);
        //Part 2


        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}