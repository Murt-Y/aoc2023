using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Schema;

namespace aoc2023
{

    class D17
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day17.txt");
        string[] input = text.Split("\n");
        return input;
        }
        public class Coor
        { 

            public int x { get; set; }
            public int y { get; set; }
            public int cost { get; set; }
        }
        public class Node
        { 
            public int index { get; set; }
            public int nodecost { get; set; }
            public int steps { get; set; }
            public int direction { get; set; }

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
        List<Node> nodes = new List<Node>();
        List<Node> nodes2 = new List<Node>();
        List<Node> activenodes = new List<Node>();

        int xc=0;
        int yc=0;
        int xstart=0;
        int ystart=0;
        int xmax=0;
        int ymax=0;


        int[] RetCoordinates(int c){
            int [] retvalue=new int[2];
            retvalue[0]=c%xmax;
            retvalue[1]=(c-retvalue[0])/xmax;
            return retvalue;

        }
        int CalcIndex(int x, int y){
            int retvalue=y*xmax+x;
            return retvalue;

        }
        //Initialize Coordinates
        int i=0;
        foreach(string s in input){
            foreach (char c in s){
                coordinates.Add(new Coor() {x=xc, y=yc, cost=c-'0',});
                xc++;
                i++;
            }
            xmax=xc;
            xc=0;
            yc++;
        }
        ymax=yc;
        int maxindex=coordinates.Count-1;
        /*
        0 ->
        1 V
        2 <-
        3 ^
        */

        void Path(Node nx){
            int c=nx.index;
            int cost=nx.nodecost;
            int steps=nx.steps;
            int dir=nx.direction;
            int []xy=RetCoordinates(c);
            int x=xy[0];
            int y=xy[1];
            
            //Console.WriteLine("Current Node is x: "+x+ " y: "+y+ " direction: " +dir + " cost: "+cost) ;
            //Right
            if(x<xmax-1){
                int stepsr=steps;
                int totalcost=cost+coordinates[c+1].cost;
                if(dir!=0){
                    stepsr=0;
                }
                if(dir!=2 && stepsr<3){
                    int bettern=nodes.FindIndex(n=> n.index==c+1 && n.direction==0 && n.nodecost<=totalcost && n.steps<=stepsr+1);
                    if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c+1, nodecost=totalcost, steps=stepsr+1, direction=0};
                        nodes.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);

                        int[] xytemp=RetCoordinates(c+1);
                        int xtemp=xytemp[0];
                        int ytemp=xytemp[1];

                        //Console.WriteLine("New Node at x:"+xtemp+" y:"+ytemp+"  direction -> cost..."+totalcost);
                    }
                }
            }

            //Left
            if(x>0){
                int stepsl=steps;
                if(dir!=2){
                    stepsl=0;
                }
                int totalcost=cost+coordinates[c-1].cost;
                if(dir!=0 && stepsl<3){
                    int bettern=nodes.FindIndex(n=> n.index==c-1 && n.direction==2 && n.nodecost<=totalcost && n.steps<=stepsl+1);
                    if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c-1, nodecost=totalcost, steps=stepsl+1, direction=2};
                        nodes.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);


                        int[] xytemp=RetCoordinates(c-1);
                        int xtemp=xytemp[0];
                        int ytemp=xytemp[1];

                        //Console.WriteLine("New Node at x:"+xtemp+" y:"+ytemp+"  direction <- cost..."+totalcost);
                    }
                }
            }

            //Up
            if(y>0){
                int stepsu=steps;
                if(dir!=3){
                    stepsu=0;
                }
                int totalcost=cost+coordinates[c-xmax].cost;
                if(dir!=1 && stepsu<3){
                    int bettern=nodes.FindIndex(n=> n.index==c-xmax && n.direction==3 && n.nodecost<=totalcost && n.steps<=stepsu+1);
                    if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c-xmax, nodecost=totalcost, steps=stepsu+1, direction=3};
                        nodes.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);


                        int[] xytemp=RetCoordinates(c-xmax);
                        int xtemp=xytemp[0];
                        int ytemp=xytemp[1];

                        //Console.WriteLine("New Node at x:"+xtemp+" y:"+ytemp+"  direction ^ cost..."+totalcost);
                    }
                }
            }

            //Down
            if(y<ymax-1){
                int stepsd=steps;
                if(dir!=1){
                    stepsd=0;
                }
                int totalcost=cost+coordinates[c+xmax].cost;
                if(dir!=3 && stepsd<3){
                    int bettern=nodes.FindIndex(n=> n.index==c+xmax && n.direction==1 && n.nodecost<=totalcost && n.steps<=stepsd+1);
                    if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c+xmax, nodecost=totalcost, steps=stepsd+1, direction=1};
                        nodes.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);


                        int[] xytemp=RetCoordinates(c+xmax);
                        int xtemp=xytemp[0];
                        int ytemp=xytemp[1];

                        //Console.WriteLine("New Node at x:"+xtemp+" y:"+ytemp+"  direction V cost..."+totalcost);
                    }
                }
            }
            activenodes.Remove(nx);
        }
        Node starting_node=new Node{index=0,nodecost=0,steps=0,direction=-1};
        nodes.Add(starting_node);
        /*
        activenodes.Add(starting_node);
        while(activenodes.Count>0){
            Node nodetocheck=new Node{index=0,nodecost=int.MaxValue,steps=0,direction=-1};
            foreach (Node n in activenodes){
                if (n.nodecost<nodetocheck.nodecost){
                    nodetocheck=n;
                }
            }
            Path(nodetocheck);
            //Console.WriteLine("------------");
        }
        */
        List<Node> finalnodes=nodes.FindAll(n=> n.index==maxindex);
        foreach(Node nn in finalnodes){
        Console.WriteLine("-----"+nn.nodecost);
        }

        result1=0;
        
        Console.WriteLine("      --------------      ");
        i=0;
        while(i<coordinates.Count){
            List<Node>thisvalue=nodes.FindAll(nxx=> nxx.index==i);
            thisvalue.OrderBy(ixx => ixx.nodecost);
            int sx1=int.MaxValue;
            foreach(Node nnn in thisvalue){
                if(nnn.nodecost<sx1){
                    sx1=nnn.nodecost;
                }
            }
            Console.Write(sx1.ToString("D3")+'|');
            if((i+1)%xmax==0){
                Console.WriteLine();
            }
            i++;
        }
        Console.WriteLine("      --------------      ");
/*
                Console.WriteLine("      --------------      ");
        i=0;
        while(i<coordinates.Count){
            Console.Write(coordinates[i].cost.ToString("D3")+'|');
            if((i+1)%xmax==0){
                Console.WriteLine();
            }
            i++;
        }
        Console.WriteLine("      --------------      ");
*/ 

        //Part 2
        void Path2(Node nx){
            int c=nx.index;
            int cost=nx.nodecost;
            int steps=nx.steps;
            int dir=nx.direction;
            int []xy=RetCoordinates(c);
            int x=xy[0];
            int y=xy[1];
            
            //Console.WriteLine("Current Node is x: "+x+ " y: "+y+ " direction: " +dir + " cost: "+cost) ;
            //Right
            if(x<xmax-4 && dir!=2 && dir!=0){
                int stogo=xmax-x-1;
                if(stogo>10){
                    stogo=10;
                }
                int totalcost=cost;
                int stepsr=steps;
                i=1;
                while(i<stogo+1){            
                    totalcost+=coordinates[c+i].cost;
                    if(i>3){
                        int bettern=nodes2.FindIndex(n=> n.index==c+i && n.direction==0 && n.nodecost<=totalcost);
                        if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c+i, nodecost=totalcost, steps=stepsr+1, direction=0};
                        nodes2.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);
                        }
                    }
                        i++;
                }
            }
         
            //Left
            if(x>3 && dir!=0 && dir!=2){
                int stogo=x;
                if(stogo>10){
                    stogo=10;
                }
                int totalcost=cost;
                int stepsl=steps;
                i=1;
                while(i<stogo+1){            
                    totalcost+=coordinates[c-i].cost;
                    if(i>3){
                        int bettern=nodes2.FindIndex(n=> n.index==c-i && n.direction==2 && n.nodecost<=totalcost);
                        if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c-i, nodecost=totalcost, steps=stepsl+1, direction=2};
                        nodes2.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);
                        }
                    }
                        i++;
                }
            }
            
            if(y>3 && dir!=1 && dir!=3){
                int stogo=y;
                if(stogo>10){
                    stogo=10;
                }
                int totalcost=cost;
                int stepsu=steps;
                i=1;
                while(i<stogo+1){            
                    totalcost+=coordinates[c-(i*xmax)].cost;
                    if(i>3){
                        int bettern=nodes2.FindIndex(n=> n.index==c-(i*xmax) && n.direction==3 && n.nodecost<=totalcost);
                        if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c-(i*xmax), nodecost=totalcost, steps=stepsu+1, direction=3};
                        nodes2.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);
                        }
                    }
                        i++;
                }
            }
            
            //Down
            if(y<ymax-4 && dir!=3 && dir!=1){
                int stogo=ymax-y-1;
                if(stogo>10){
                    stogo=10;
                }
                int totalcost=cost;
                int stepsd=steps;
                i=1;
                while(i<stogo+1){            
                    totalcost+=coordinates[c+(i*xmax)].cost;
                    if(i>3){
                        int bettern=nodes2.FindIndex(n=> n.index==c+(i*xmax) && n.direction==1 && n.nodecost<=totalcost);
                        if(bettern==-1){
                        Node new_node_to_add=new Node(){index=c+(i*xmax), nodecost=totalcost, steps=stepsd+1, direction=1};
                        nodes2.Add(new_node_to_add);
                        activenodes.Add(new_node_to_add);
                        }
                    }
                        i++;
                }
            }
                       
            activenodes.Remove(nx);


        }


        nodes2.Add(starting_node);
        activenodes.Add(starting_node);
        while(activenodes.Count>0){
            Node nodetocheck=new Node{index=0,nodecost=int.MaxValue,steps=0,direction=-1};
            foreach (Node n in activenodes){
                if (n.nodecost<nodetocheck.nodecost){
                    nodetocheck=n;
                }
            }
            Path2(nodetocheck);
            //Console.WriteLine("------------");
        }
        Console.WriteLine("      --------------      ");
        i=0;
        while(i<coordinates.Count){
            List<Node>thisvalue=nodes2.FindAll(nxx=> nxx.index==i);
            thisvalue.OrderBy(ixx => ixx.nodecost);
            int sx1=int.MaxValue;
            foreach(Node nnn in thisvalue){
                if(nnn.nodecost<sx1){
                    sx1=nnn.nodecost;
                }
            }
            Console.Write(sx1.ToString("D3")+'|');
            if((i+1)%xmax==0){
                Console.WriteLine();
            }
            i++;
        }
        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}