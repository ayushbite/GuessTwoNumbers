using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathNumberGusserProject.Models
{
    public class GameValueModel
    {
        
        public int ValueOne { get; set; } = 0;
        public int ValueTwo { get; set; } = 0;
        public int Sub { get; set; } = 0;
        public int Sum { get; set; } = 0;
        public int Gridnumber { get; set; } 
        public GameValueModel(int sumrandval,int subrandval,int gridnumber)
        {
         
            
            Sum = sumrandval;
            Sub = subrandval;
            ValueOne = (Sum + Sub) / 2;
            //substuting in first equaltion
            ValueTwo = Sum - ValueOne;
            this.Gridnumber = gridnumber;   

        }
    }
}
