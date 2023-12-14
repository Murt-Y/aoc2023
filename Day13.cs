using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
public static class Extension
{
    public static List<string>GetClone(this List<string> source)
    {
        return source.GetRange(0, source.Count);
    }
}
namespace aoc2023
{

    class D13
    {
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day13.txt");
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
        List<int> scores=new List<int>();
        List<int> scores2=new List<int>();

        List<string>RowView(List<string> blk){
            int ymax=blk.Count;
            int xmax=blk[0].Length;
            List<string> rowblock=new List<string>();
            int zx=0;
            while(zx<xmax){
                string rowt =string.Empty;
                int zy=0;
                while(zy<ymax){
                    rowt+=blk[zy][zx];
                    zy++;
                }
                rowblock.Add(rowt);
                zy=0;
                zx++;
            }
            return rowblock;
        }

        int CheckH(List<string> blk){
            int ymax=blk.Count;
            int xmax=blk[0].Length;
            int z=1;
            while(z<blk.Count){
                if(blk[z]==blk[z-1]){
                    int tx=1;
                    bool alleq=true;
                    while(z-tx-1>=0 && z+tx<ymax){
                        if(blk[z+tx]!=blk[z-tx-1])
                        {
                            alleq=false;
                            break;
                        }
                        tx++;
                    }
                    if(alleq==true){
                        return (z);
                    }
                }
                z++;
            }
            return -1;
        }
        int CheckHi(List<string> blk, int scoretocheck){
            int ymax=blk.Count;
            int xmax=blk[0].Length;
            int z=1;
            if(z==scoretocheck){
                    z++;
                }
            while(z<blk.Count){
                if(blk[z]==blk[z-1]){
                    int tx=1;
                    bool alleq=true;
                    while(z-tx-1>=0 && z+tx<ymax){
                        if(blk[z+tx]!=blk[z-tx-1])
                        {
                            alleq=false;
                            break;
                        }
                        tx++;
                    }
                    if(alleq==true){
                        return (z);
                    }
                }
                z++;
                if(z==scoretocheck){
                    z++;
                }
            }
            return -1;
        }

        int CheckV(List<string> blk){
            blk=RowView(blk);
            int ymax=blk.Count;
            int xmax=blk[0].Length;
            int z=1;
            while(z<blk.Count){
                if(blk[z]==blk[z-1]){
                    int tx=1;
                    bool alleq=true;
                    while(z-tx-1>=0 && z+tx<ymax){
                        if(blk[z+tx]!=blk[z-tx-1])
                        {
                            alleq=false;
                            break;
                        }
                        tx++;
                    }
                    if(alleq==true){
                        return (z);
                    }
                }
                z++;
            }
            return -1;
        }
        int CheckVi(List<string> blk, int scoretocheck){
            blk=RowView(blk);
            int ymax=blk.Count;
            int xmax=blk[0].Length;
            int z=1;
            if(z==scoretocheck){
                    z++;
                }
            while(z<blk.Count){
                if(blk[z]==blk[z-1]){
                    int tx=1;
                    bool alleq=true;
                    while(z-tx-1>=0 && z+tx<ymax){
                        if(blk[z+tx]!=blk[z-tx-1])
                        {
                            alleq=false;
                            break;
                        }
                        tx++;
                    }
                    if(alleq==true){
                        return (z);
                    }
                }
                z++;
                if(z==scoretocheck){
                    z++;
                }
            }
            return -1;
        }

        int i=0;
        List<string> block=new List<string>();
        while (i<inputcount){
            int x=input[i].Length;
            
            if(x!=0){
                block.Add(input[i]);
            }
            if (x==0 || i==inputcount-1){
                int inp1=CheckH(block);
                if(inp1!=-1){
                    scores.Add(inp1*100);
                }
                else{
                    inp1=CheckV(block);
                    scores.Add(inp1);
                }
                block.Clear();
            }

            i++;
        }
        foreach(int x in scores){
            result1+=x;
        }

        //Part 2
        void ChangeView(List<string> blk, int blkcount){
            List<string>tempblk=blk.GetClone();
            int ymax=blk.Count;
            int xmax=blk[0].Length;
            int k=0;
            int l=0;

            void ChangeSmudge(){
                if(blk[l][k]=='#'){
                    blk[l]=blk[l].Remove(k,1).Insert(k,".");
                }
                else{
                    blk[l]=blk[l].Remove(k,1).Insert(k,"#");
                }
                if(k<xmax-1){
                    k++;
                }
                else{
                    k=0;
                    l++;
                }
                }
            

                bool correctsmudge=false;
                while(correctsmudge==false){

                blk=tempblk.GetClone();
                ChangeSmudge();

                int inp1=CheckHi(blk, scores[blkcount]/100) ;
                if(inp1!=-1 && (inp1*100)!=scores[blkcount]){
                    scores2.Add(inp1*100);
                    correctsmudge=true;
                    
                }
                inp1=CheckVi(blk, scores[blkcount] );
                if(correctsmudge==false && inp1!=-1 && inp1!=scores[blkcount]){
                    scores2.Add(inp1);
                    correctsmudge=true;
                }
                }
            
        }

        block.Clear();
        i=0;
        int blockcount=0;
        while (i<inputcount){
            int x=input[i].Length;
            
            if(x!=0){
                block.Add(input[i]);
            }
            if (x==0 || i==inputcount-1){
                ChangeView(block, blockcount);
                block.Clear();
                blockcount++;

            }

            i++;
        }
        foreach(int x in scores2){
            result2+=x;
        }

        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}