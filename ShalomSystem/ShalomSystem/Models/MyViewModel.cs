using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;

namespace ShalomSystem.Models
{
    public class MyViewModel
    {
        public IEnumerable<User>   LogCredetials { get; set; }
        public IEnumerable<Person> PersonInfo    { get; set; }



      //  public IEnumerable<Animal> Employees { get; set; }




        //cattle model to use 
        public IEnumerable<Cattle> CattleInfo { get; set; }
        public IEnumerable<Cattle> BullsInfo { get; set; }


        public IEnumerable<ChartData> barChart { get; set; }


        public  Person NewPerson { get; set; }
        //
        public Cattle cattleInfo { get; set; }




    }
}