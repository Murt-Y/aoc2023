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

    class D10
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day10.txt");
        string[] input = text.Split("\n");
        return input;
        }

        public class Coor
        { 
            public int x { get; set; }
            public int y { get; set; }
            public char val { get; set; }
            public int steps { get; set; }
            public bool path{ get; set; }
            public bool mazer{ get; set; }

        }
        List<Coor> coordinates = new List<Coor>();

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

        int[] Right(int x, int y){
            if(x>=xmax-1){
                return null;
            }
            else{
                return new[] {x+1,y};
            }
        }
        int[] Left(int x, int y){
            if(x<=0){
                return null;
            }
            else{
                return new[] {x-1,y};
            }
        }
        int[] Up(int x, int y){
            if(y<=0){
                return null;
            }
            else{
                return new[] {x,y-1};
            }
        }
        int[] Down(int x, int y){
            if(y>=ymax-1){
                return null;
            }
            else{
                return new[] {x,y+1};
            }
        }
        //Directions 0 downtoup  1 uptodown 2 lefttoright 3 righttoleft
        int[] VPipe(int x, int y, int dir){
            int[] newcoor=new []{0,0};
            if (dir==0){
                newcoor=Up(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 0};
                }
            }
            else if (dir==1){
                newcoor=Down(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 1};
                }
            }
        return null;
        }
        int[] HPipe(int x, int y, int dir){
            int[] newcoor=new []{0,0};
            if (dir==2){
                newcoor=Right(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 2};
                }
            }
            else if (dir==3){
                newcoor=Left(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 3};
                }
            }
        return null;
        }
        int[] LPipe(int x, int y, int dir){
            int[] newcoor=new []{0,0};
            if (dir==1){
                newcoor=Right(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 2};
                }
            }
            else if (dir==3){
                newcoor=Up(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 0};
                }
            }
        return null;
        }
        int[] JPipe(int x, int y, int dir){
            int[] newcoor=new []{0,0};
            if (dir==1){
                newcoor=Left(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 3};
                }
            }
            else if (dir==2){
                newcoor=Up(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 0};
                }
            }
        return null;
        }
        int[] SevenPipe(int x, int y, int dir){
            int[] newcoor=new []{0,0};
            if (dir==0){
                newcoor=Left(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 3};
                }
            }
            else if (dir==2){
                newcoor=Down(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 1};
                }
            }
        return null;
        }
        int[] FPipe(int x, int y, int dir){
            int[] newcoor=new []{0,0};
            if (dir==3){
                newcoor=Down(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 1};
                }
            }
            else if (dir==0){
                newcoor=Right(x,y);
                if (newcoor==null){
                    return null;
                }
                else{
                    return new[]{newcoor[0],newcoor[1], 2};
                }
            }
        return null;
        }
        int IndexC(int x, int y){
            return(y*xmax+x);
        }
        int[] Move(int x, int y, int dir){
            int c=IndexC(x,y);
            if (coordinates[c].val=='|'){
                return (VPipe(x,y,dir));
            }
            else if (coordinates[c].val=='-'){
                return (HPipe(x,y,dir));
            }
            else if (coordinates[c].val=='L'){
                return (LPipe(x,y,dir));
            }
            else if (coordinates[c].val=='J'){
                return (JPipe(x,y,dir));
            }
            else if (coordinates[c].val=='7'){
                return (SevenPipe(x,y,dir));
            }
            else if (coordinates[c].val=='F'){
                return (FPipe(x,y,dir));
            }
        return null;
        }

        //Set the Max Coordinates
        int xc=0;
        int yc=0;
        int xstart=0;
        int ystart=0;

        //Initialize Coordinates
        int i=0;
        foreach(string s in input){
            foreach (char c in s){
                if(c=='S'){
                    xstart=xc;
                    ystart=yc;
                }
                coordinates.Add(new Coor() {x=xc, y=yc, val=c, mazer=false});
                xc++;
                i++;
            }
            xmax=xc;
            xc=0;
            yc++;
        }
        ymax=yc;
        bool loop=false;
        string correctpath="";
        //To the Right
        int[] Mover=Move(xstart+1,ystart,2);
        int steps=1;
        while (!loop && Mover!=null){
            steps++;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                Console.WriteLine("Correct Path ...." + (steps+1));
                loop=true;
                correctpath="right";
            }
        }
        //To the Left
        if(!loop){
        Mover=Move(xstart-1,ystart,3);
        steps=1;
        }
        while (!loop && Mover!=null){
            steps++;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                Console.WriteLine("Correct Path ...." + (steps+1));
                loop=true;
                correctpath="left";
            }
        }
        //To the Up
        if(!loop){
        Mover=Move(xstart,ystart-1,0);
        steps=1;
        }
        while (!loop && Mover!=null){
            steps++;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                Console.WriteLine("Correct Path ...." + (steps+1));
                loop=true;
                correctpath="up";
            }
        }
        //To the Down
        if(!loop){
        Mover=Move(xstart,ystart+1,1);
        steps=1;
        }
        while (!loop && Mover!=null){
            steps++;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                Console.WriteLine("Correct Path ...." + (steps+1));
                loop=true;
                correctpath="down";
            }
        }
        coordinates[IndexC(xstart,ystart)].path=true;
        result1=(steps+1)/2;


        //PARTII
        loop=false;
        if (correctpath=="right"){
        //To the Right
        Mover=Move(xstart+1,ystart,2);
        int c=IndexC(xstart+1, ystart);
        coordinates[c].path=true;
        while (!loop){
        c=IndexC(Mover[0], Mover[1]);
        coordinates[c].path=true;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                if(Mover[2]==0){
                    coordinates[IndexC(xstart,ystart)].val='F';
                }
                else if(Mover[2]==1){
                    coordinates[IndexC(xstart,ystart)].val='L';
                }
                else if(Mover[2]==2){
                    coordinates[IndexC(xstart,ystart)].val='-';
                }

                }
        }
        }
        if (correctpath=="left"){
        //To the Left
        Mover=Move(xstart-1,ystart,3);
        int c=IndexC(xstart-1, ystart);
        coordinates[c].path=true;
        while (!loop){
        c=IndexC(Mover[0], Mover[1]);
        coordinates[c].path=true;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                if(Mover[2]==0){
                    coordinates[IndexC(xstart,ystart)].val='7';
                }
                else if(Mover[2]==1){
                    coordinates[IndexC(xstart,ystart)].val='J';
                }
                else if(Mover[2]==3){
                    coordinates[IndexC(xstart,ystart)].val='-';
                }
                }
        }
        }
        if (correctpath=="up"){
        //To the Up
        Mover=Move(xstart,ystart-1,0);
        int c=IndexC(xstart, ystart-1);
        coordinates[c].path=true;
        while (!loop){
        c=IndexC(Mover[0], Mover[1]);
        coordinates[c].path=true;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                if(Mover[2]==3){
                    coordinates[IndexC(xstart,ystart)].val='L';
                }
                else if(Mover[2]==0){
                    coordinates[IndexC(xstart,ystart)].val='|';
                }
                else if(Mover[2]==2){
                    coordinates[IndexC(xstart,ystart)].val='J';
                }
                }
        }
        }
        if (correctpath=="down"){
        //To the Down
        Mover=Move(xstart,ystart+1,1);
        int c=IndexC(xstart, ystart+1);
        coordinates[c].path=true;
        while (!loop){
        c=IndexC(Mover[0], Mover[1]);
        coordinates[c].path=true;
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                if(Mover[2]==3){
                    coordinates[IndexC(xstart,ystart)].val='F';
                }
                else if(Mover[2]==1){
                    coordinates[IndexC(xstart,ystart)].val='|';
                }
                else if(Mover[2]==2){
                    coordinates[IndexC(xstart,ystart)].val='7';
                }

                }
        }
        }
        void PaintUp(int x, int y){
                int[] newpos=Up(x,y);
                if(newpos==null){
                    return;
                }
                else if(coordinates[IndexC(newpos[0],newpos[1])].path==true){
                    return;
                }
                else{
                    coordinates[IndexC(newpos[0],newpos[1])].mazer=true;
                    PaintUp(newpos[0],newpos[1]);
                }
        }
        void PaintDown(int x, int y){
                int[] newpos=Down(x,y);
                if(newpos==null){
                    return;
                }
                else if(coordinates[IndexC(newpos[0],newpos[1])].path==true){
                    return;
                }
                else{
                    coordinates[IndexC(newpos[0],newpos[1])].mazer=true;
                    PaintDown(newpos[0],newpos[1]);
                }
        }
        void PaintRight(int x, int y){
                int[] newpos=Right(x,y);
                if(newpos==null){
                    return;
                }
                else if(coordinates[IndexC(newpos[0],newpos[1])].path==true){
                    return;
                }
                else{
                    coordinates[IndexC(newpos[0],newpos[1])].mazer=true;
                    PaintRight(newpos[0],newpos[1]);
                }
        }
        void PaintLeft(int x, int y){
                int[] newpos=Left(x,y);
                if(newpos==null){
                    return;
                }
                else if(coordinates[IndexC(newpos[0],newpos[1])].path==true){
                    return;
                }
                else{
                    coordinates[IndexC(newpos[0],newpos[1])].mazer=true;
                    PaintLeft(newpos[0],newpos[1]);
                }
        }
        coordinates[IndexC(xstart, ystart)].path=true;
        void PaintMe(int x, int y, int direct){
            int indexof=IndexC(x,y);
            if(direct==1){
                PaintRight(x,y);
                if(coordinates[indexof].val=='J'){
                    PaintDown(x,y);
                }
            }
            else if(direct==0){
                PaintLeft(x,y);
                 if(coordinates[indexof].val=='F'){
                    PaintUp(x,y);
                }
            }
            else if(direct==3){
                PaintDown(x,y);
                 if(coordinates[indexof].val=='L'){
                    PaintLeft(x,y);
                }
            }
            else if(direct==2){
                PaintUp(x,y);
                if(coordinates[indexof].val=='7'){
                    PaintRight(x,y);
                }
            }
        }

        int cx=IndexC(xstart,ystart);
        char st=coordinates[cx].val;
        /*loop=false;
        if(st=='|' || st=='J' || st=='L'){
        Mover=Move(xstart,ystart-1,0);
        PaintMe(xstart,ystart-1,0);
        while (!loop){
            PaintMe(Mover[0],Mover[1],Mover[2]);
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                }
        }
        }
        */
        loop=false;
        if(st=='|' || st=='7' || st=='F'){
        Mover=Move(xstart,ystart+1,1);
        PaintMe(xstart,ystart+1,1);
        while (!loop){
            PaintMe(Mover[0],Mover[1],Mover[2]);
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                }
        }
        }
        /*
        loop=false;
        if(st=='-' || st=='L' || st=='F'){
        Mover=Move(xstart+1,ystart,2);
        PaintMe(xstart+1,ystart,2);
        while (!loop){
            PaintMe(Mover[0],Mover[1],Mover[2]);
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                }
        }
        }
        */
        loop=false;
        if(st=='-' || st=='7' || st=='J'){
        Mover=Move(xstart-1,ystart,3);
        PaintMe(xstart-1,ystart,3);
        while (!loop){
            PaintMe(Mover[0],Mover[1],Mover[2]);
            Mover=Move(Mover[0],Mover[1],Mover[2]);
            if(Mover[0]==xstart && Mover[1]==ystart){
                loop=true;
                }
        }
        }
        /*
        */
        

        /*
        int m=0;
        bool fence=false;
        bool[] maze = new bool[xmax];
        i=0;

        
        while(i<xmax){
            maze[i]=false;
            i++;
        }
        while(m<inputcount){
            int n=0;
            while(n<xmax){
                int c=xmax*m+n;
                if(coordinates[c].val=='-' || coordinates[c].val=='7' || coordinates[c].val=='F'){
                    if (maze[n]==false){
                        maze[n]=true;
                    }
                    else{
                        maze[n]=false;
                    }
                }
                coordinates[c].mazer=maze[n];
                n++;
            }
            n=0;
            m++;
            fence=false;
        }
        */
        foreach(Coor ct in coordinates){
            if(ct.path==false && ct.mazer==true){
                result2++;
            }
        }


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}