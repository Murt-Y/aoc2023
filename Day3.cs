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

    class D3
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day3.txt");
        string[] input = text.Split("\n");
        return input;
        }

        public class Coor
        { 
            public int x { get; set; }
            public int y { get; set; }
            public char val { get; set; }
            public bool isol { get; set; }
        }
        List<Coor> coordinates = new List<Coor>();
        List<int> gears = new List<int>();


        public int [] Solution()
        {
        int[] result = {0, 0};
        string[] input =ReadMyInput();
        int inputcount = input.Count();
        int result1=0;
        int result2=0;
        //Part 1
        int xmax=0;
        int ymax=0;

            bool Check(int x, int y){
            if(coordinates[y*xmax+x].val=='.')
            {
                return false;
            }
            return true;
        }
        //Checking the Neighbour Coordinates for values
        bool CheckN(int x, int y){
            //CheckLeft
            if(x>0)
            {   
                if(coordinates[y*xmax+x-1].val-'0'>=0 && coordinates[y*xmax+x-1].val-'0'<=9)
                    {
                        if(coordinates[y*xmax+x-1].isol==true){
                            return true;
                        }
                    }

                else if(Check(x-1, y)){
                    return true;
                }
            }

            //CheckRight
            if(x<xmax-1)
            {   
                if(coordinates[y*xmax+x+1].val-'0'>=0 && coordinates[y*xmax+x+1].val-'0'<=9)
                    {
                        if(CheckN(x+1,y)){
                            return true;
                        }
                    }
                else if(Check(x+1, y)){
                    return true;
                }
            }
            //CheckUp
            if(y>0)
            {
                if(Check(x, y-1)){
                    return true;
                }
            }
            //CheckUpRight
            if(y>0 && x<xmax-1)
            {
                if(Check(x+1, y-1)){
                    return true;
                }
            }
            //CheckUpLeft
            if(y>0 && x>0)
            {
                if(Check(x-1, y-1)){
                    return true;
                }
            }
            //CheckDown
            if(y<ymax-1)
            {
                if(Check(x, y+1)){
                    return true;
                }
            }
            //CheckDownRight
            if(y<ymax-1 && x<xmax-1)
            {
                if(Check(x+1 , y+1)){
                    return true;
                }
            }
            //CheckDownLeft
            if(y<ymax-1 && x>0)
            {
                if(Check(x-1, y+1)){
                    return true;
                }
            }



            return false;
        }
        
        //Set the Max Coordinates
        int xc=0;
        int yc=0;

        //Initialize Coordinates
        int i=0;
        foreach(string s in input){
            foreach (char c in s){
                
                coordinates.Add(new Coor() {x=xc, y=yc, val=c});
                xc++;
                if(c=='*'){
                    gears.Add(i);
                }
                i++;
            }
            xmax=xc;
            xc=0;
            yc++;
        }
        ymax=yc;
        
        //Run through the coordinates to check neighbours
        foreach(Coor c in coordinates)
        {
            if((c.val-'0'>=0 && c.val-'0'<=9) && CheckN(c.x, c.y)){
                c.isol=true;
            };
        }
        
        //returns the values to right to form numbers
        string ValR(int x, int y)
        {   
            string ret="";
            if(x==xmax-1 || (coordinates[y*xmax+x+1].val-'0'<0 || coordinates[y*xmax+x+1].val-'0'>9))
                {
                    return Convert.ToString(coordinates[y*xmax+x].val) ;
                }
            else {
                ret= ValR(x+1,y);
                ret=coordinates[y*xmax+x].val+ret;
                return ret;
            }
        }

        //Check the numbers if touching check the right and add to the value
        foreach(Coor c in coordinates)
        {
            if(c.isol == true){
                //the number we are checking is the first digit of a number series so, the x is 0 or there is no number on left
                if(c.x==0 || coordinates[(c.y*xmax+c.x)-1].isol != true){
                    result1+=Convert.ToInt32(ValR(c.x, c.y));
                }
            }

        }


        //PARTII

        bool IsNumber(char c)
        {
            if(c-'0'>=0 && c-'0'<=9){
                return true;
            }
            else
            {
                return false;
            }
        }
        string CheckB(int x, int y)
        {
            if(x>0 && IsNumber(coordinates[y*xmax+x-1].val)){
                return CheckB(x-1,y);
            }
            return ValR(x,y);
        }
        int CheckGn(int x, int y){
            List<string> neighbours = new List<string>();
            //CheckLeft
            if(x>0)
            {   
            if(IsNumber(coordinates[y*xmax+x-1].val)){
                neighbours.Add(CheckB(x-1,y));
            }
            }

            //CheckRight
            if(x<xmax-1)
            {   
            if(IsNumber(coordinates[y*xmax+x+1].val)){
                neighbours.Add(CheckB(x+1,y));
            }
            }

            //CheckUp(if up not number check up right and left otherwise all is one number)
            if(y>0)
            {   
            if(IsNumber(coordinates[(y-1)*xmax+x].val)){
                neighbours.Add(CheckB(x,y-1));
            }
            else{
                //CheckUpLeft
                if(x>0){
                    if(IsNumber(coordinates[(y-1)*xmax+x-1].val)){
                    neighbours.Add(CheckB(x-1,y-1));
                }
                }
                //CheckUpRight
                if(x<xmax-1){
                    if(IsNumber(coordinates[(y-1)*xmax+x+1].val)){
                    neighbours.Add(CheckB(x+1,y-1));
                }
            } 
            }
            }

            //CheckDown(if down not number check down right and left otherwise all is one number)
            if(y<ymax-1)
            {   
            if(IsNumber(coordinates[(y+1)*xmax+x].val)){
                neighbours.Add(CheckB(x,y+1));
            }
            else{
                //CheckDownLeft
                if(x>0){
                    if(IsNumber(coordinates[(y+1)*xmax+x-1].val)){
                    neighbours.Add(CheckB(x-1,y+1));
                }
                }
                //CheckDownRight
                if(x<xmax-1){
                    if(IsNumber(coordinates[(y+1)*xmax+x+1].val)){
                    neighbours.Add(CheckB(x+1,y+1));
                }
            } 
            }
            }
            int gearratio=0;
            if(neighbours.Count>=2){
                gearratio=1;
                foreach(string m in neighbours)
                {
                    gearratio*=Convert.ToInt32(m);
                }

            }
            return gearratio;
            }


        foreach (int ind in gears){
            
            result2 += CheckGn(coordinates[ind].x,coordinates[ind].y);
        }


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}