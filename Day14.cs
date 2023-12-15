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
        public class Coor
        { 
            public int x  { get; set; }
            public int y { get; set; }
            public char val { get; set; }

        }
    class D14
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day14.txt");
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
        List<Coor> coordinates = new List<Coor>();
        int xmax=0;
        int ymax=0;
        //Set the Max Coordinates
        int xc=0;
        int yc=0;

        int[] RetCoordinates(int c){
            int [] retvalue=new int[2];
            retvalue[0]=c%xmax;
            retvalue[1]=(c-retvalue[0])/xmax;
            return retvalue;

        }
        int CalcIndex(int x, int y){
            int retvalue=y*ymax+x;
            return retvalue;

        }
        void ShiftUp(int c){
            int [] coors=new int[2];
            coors=RetCoordinates(c);
            int x=coors[0];
            int y=coors[1];
            if(y>0){
                char u =coordinates[c-xmax].val;
                if (u=='.'){
                    coordinates[c-xmax].val='O';
                    coordinates[c].val='.';
                    ShiftUp(c-xmax);
                }
            }  
        }
        void ShiftDown(int c){
            int [] coors=new int[2];
            coors=RetCoordinates(c);
            int x=coors[0];
            int y=coors[1];
            if(y<ymax-1){
                char u =coordinates[c+xmax].val;
                if (u=='.'){
                    coordinates[c+xmax].val='O';
                    coordinates[c].val='.';
                    ShiftDown(c+xmax);
                }
            }  
        }
        void ShiftRight(int c){
            int [] coors=new int[2];
            coors=RetCoordinates(c);
            int x=coors[0];
            int y=coors[1];
            if(x<xmax-1){
                char u =coordinates[c+1].val;
                if (u=='.'){
                    coordinates[c+1].val='O';
                    coordinates[c].val='.';
                    ShiftRight(c+1);
                }
            }  
        }
        void ShiftLeft(int c){
            int [] coors=new int[2];
            coors=RetCoordinates(c);
            int x=coors[0];
            int y=coors[1];
            if(x>0){
                char u =coordinates[c-1].val;
                if (u=='.'){
                    coordinates[c-1].val='O';
                    coordinates[c].val='.';
                    ShiftLeft(c-1);
                }
            }  
        }

        int i=0;
        foreach(string s in input){
            foreach (char c in s){
                coordinates.Add(new Coor() {x=xc, y=yc, val=c});
                xc++;
                i++;
            }
            xmax=xc;
            xc=0;
            yc++;
        }
        int[]resultpattern=new int[25];
        int[]resultpattern1000=new int[25];
        bool cyclepattern=false;
        int cyclecheck=0;
        ymax=yc;       
        int tx=0;
        int cyclecount=0;
        while(cyclecount<10000){
            tx=0;
        while(tx<coordinates.Count){
            if(coordinates[tx].val=='O'){
            ShiftUp(tx);
            }
            tx++;
        }
        /*
                tx=0;
        foreach(Coor c in coordinates){
            Console.Write(c.val);
        
            tx++;
            if(tx%xmax==0){
                Console.WriteLine(""); 
            }
        }
        Console.WriteLine("-----------"); 
        */
        tx=0;
        while(tx<coordinates.Count){
            if(coordinates[tx].val=='O'){
            ShiftLeft(tx);
            }
            tx++;
        }
        /*
                tx=0;
        foreach(Coor c in coordinates){
            Console.Write(c.val);
        
            tx++;
            if(tx%xmax==0){
                Console.WriteLine(""); 
            }
        }
        Console.WriteLine("-----------"); 
        */
        tx=0;
        int ty=ymax-1;
        while(ty>=0){
            while(tx<xmax){
            int c1=CalcIndex(tx,ty);
            if(coordinates[c1].val=='O'){
            ShiftDown(c1);
            }
            tx++;
            }
            tx=0;
            ty--;
        }
        /*
                tx=0;
        foreach(Coor c in coordinates){
            Console.Write(c.val);
        
            tx++;
            if(tx%xmax==0){
                Console.WriteLine(""); 
            }
        }
        Console.WriteLine("-----------"); 
        */
        tx=xmax-1;
        ty=0;
        while(tx>=0){
            while(ty<ymax){
            int c1=CalcIndex(tx,ty);
            if(coordinates[c1].val=='O'){
            ShiftRight(c1);
            }
            ty++;
            }
            ty=0;
            tx--;
        }
        /*
                tx=0;
        foreach(Coor c in coordinates){
            Console.Write(c.val);
        
            tx++;
            if(tx%xmax==0){
                Console.WriteLine(""); 
            }
        }
        Console.WriteLine("-----------"); 
        */
        cyclecount++;
        tx=0;
        result2=0;
        foreach(Coor c in coordinates){
            if(c.val=='O'){
            int []coors=RetCoordinates(tx);
            int x=coors[0];
            int y=coors[1];
            result2+= (ymax-y); 
        }
            tx++;
        }
        if(cyclecount>=500 && cyclecount <525){
            resultpattern1000[cyclecount-500]=result2;
        }

        i=0;
        while(i<24){
            resultpattern[i]=resultpattern[i+1];
            i++;
        }
        resultpattern[24]=result2;
        if (resultpattern.SequenceEqual(resultpattern1000)){
            if(cyclepattern==true){
                int repetitive=cyclecount-cyclecheck;
                int finalresult=resultpattern1000[(1000000000-1000)%repetitive];
                Console.WriteLine("Final Result..... "+finalresult);
                break;
            }
            else{
                cyclecheck=cyclecount;
                cyclepattern=true;
            }
        }



        }


        //Part 2


        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}