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

    class D7
    {
                public class Hand
        { 
            public int rank { get; set; }
            public int value { get; set; }
            public int bid { get; set; }
            public int cardno { get; set; }
            public string list { get; set; }
            public double tiebreaker { get; set; }

        }
        string[] ReadMyInput()
        {
        string text = File.ReadAllText(@"./input/Day7.txt");
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

        int l=0;
        List<Hand> hands = new List<Hand>(inputcount);
        while(l<inputcount){
        hands.Add(new Hand() {rank=0, value=0, bid=0, cardno=l});        
        l++;
        }

        int Value(char card)
        {
            if(card-'0'>=0 && card-'0'<=9){
                return card-'0';
            }
            else if(card=='T')
            {
                return 10;
            }
            else if(card=='J')
            {
                return 11;
            }
            else if(card=='Q')
            {
                return 12;
            }
            else if(card=='K')
            {
                return 13;
            }
            else if(card=='A')
            {
                return 14;
            }
            return 0;
        }
        int Value2(char card)
        {
            if(card-'0'>=0 && card-'0'<=9){
                return card-'0';
            }
            else if(card=='T')
            {
                return 10;
            }
            else if(card=='J')
            {
                return 1;
            }
            else if(card=='Q')
            {
                return 12;
            }
            else if(card=='K')
            {
                return 13;
            }
            else if(card=='A')
            {
                return 14;
            }
            return 0;
        }
        
        int Power(int x)
        {
            int returnv =1;
            if(x==0)
            {
                return 0;
            }
            else{
                while(x>0)
                {
                    returnv*=10;
                    x--;
                }
            }
            return returnv;
        }
        //Part 1
        int curhand=0;
        foreach(string s in input)
        {
            List<int> cards = new List<int>(13);

            int i=0;
            while(i<15)
            {
                cards.Add(0);
                i++;
            }
            string [] cr=s.Split(" ");
            
            hands[curhand].bid=Convert.ToInt32(cr[1]);
            hands[curhand].list=(cr[0]);
            double tiebreakervalue=0;
            int z=4;

            foreach(char c in cr[0])
            {
                int k=Value(c);
                cards[k]+= 1;
                float t = (float)k;
                tiebreakervalue+=t*Math.Pow(14, z);
                z--;
            }
            hands[curhand].tiebreaker=tiebreakervalue;
            int rank=0;
            foreach(int x in cards){
                rank+=Power(x);
            }
            hands[curhand].value=rank;
            curhand++;
        }
           
        List<Hand> sortedhands = hands.OrderBy(o=>o.value).ToList();
        int r=0;
        bool change=true;
        int changecount=0;

        while(change ==true && r<inputcount){
            if(r+1<inputcount && sortedhands[r].value==sortedhands[r+1].value)
            {
                if(sortedhands[r].tiebreaker>sortedhands[r+1].tiebreaker){
                    Hand temp=sortedhands[r];
                    sortedhands[r]=sortedhands[r+1];
                    sortedhands[r+1]=temp;
                    changecount++;
                }
            }


            r++;
            if(r==inputcount && changecount==0)
            {
                change=false;
            }
            else if(r==inputcount && changecount>0)
            {
                changecount=0;
                r=0;
            }
        }
        int a =1;
        foreach(Hand h in sortedhands)
        {
            result1 +=a*h.bid;
            a++;
        }
        //Part 2
        curhand=0;
        foreach(string s in input)
        {
            List<int> cards = new List<int>(13);

            int i=0;
            while(i<15)
            {
                cards.Add(0);
                i++;
            }
            string [] cr=s.Split(" ");
            
            hands[curhand].bid=Convert.ToInt32(cr[1]);
            hands[curhand].list=(cr[0]);
            double tiebreakervalue=0;
            int z=4;

            foreach(char c in cr[0])
            {
                int k=Value2(c);
                cards[k]+= 1;
                float t = (float)k;
                tiebreakervalue+=t*Math.Pow(14, z);
                z--;
            }
            hands[curhand].tiebreaker=tiebreakervalue;
            int rank=0;
            int jokercount=cards[1];
            cards.RemoveAt(0);
            cards.RemoveAt(0);
            cards.Sort();

            if(jokercount>0)
            {
                if(cards[12]==4){
                    cards[12]++;
                }
                else if(cards[12]==3){
                    if(jokercount==2){
                        cards[12]=5;
                    }
                    else{
                        cards[12]=4;
                    }
                }
                else if(cards[12]==2){
                    if(jokercount==3){
                        cards[12]=5;
                    }
                    else if(jokercount==2){
                        cards[12]=4;
                    }
                    else{
                        cards[12]=3;
                    }
                }
                else if(cards[12]==1){
                    if(jokercount==4){
                        cards[12]=5;
                    }
                    else if(jokercount==3){
                        cards[12]=4;
                    }
                    else if(jokercount==2){
                        cards[12]=3;
                    }
                    else{
                        cards[12]=2;
                    }
                }
                else if(cards[12]==0){
                        cards[12]=5;
                }
            }
            
            foreach(int x in cards){
                rank+=Power(x);
            }
            hands[curhand].value=rank;
            curhand++;
        }
        List<Hand> sortedhands2 = hands.OrderBy(o=>o.value).ToList();
        r=0;
        bool change2=true;
        int changecount2=0;

        while(change2 ==true && r<inputcount){
            if(r+1<inputcount && sortedhands2[r].value==sortedhands2[r+1].value)
            {
                if(sortedhands2[r].tiebreaker>sortedhands2[r+1].tiebreaker){
                    Hand temp=sortedhands2[r];
                    sortedhands2[r]=sortedhands2[r+1];
                    sortedhands2[r+1]=temp;
                    changecount2++;
                }
            }


            r++;
            if(r==inputcount && changecount2==0)
            {
                change=false;
            }
            else if(r==inputcount && changecount2>0)
            {
                changecount2=0;
                r=0;
            }
        }
        a =1;
        foreach(Hand h in sortedhands2)
        {
            result2 +=a*h.bid;
            a++;
        }
        //Results


        result[0]=result1;
        result[1]=result2;
        return result;
        }



    }
}