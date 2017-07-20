using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;
using System.Web.Mvc;

namespace ShalomSystem.Models
{
    public class PersonModel:Person
    {

        public PersonModel()
            : base()
        {

        }


        



        public static List<Person> p;
        public static List<Person> GetPersonDetails(string Uname,string Upassword,bool access)
        {
            List<Person> personInformation = new List<Person>(1);
            p = new List<Person>();

            //access Dataaccess model
            //query database for data 



            p.Add(new Person(204, "Stervanos", "Semosa", DateTime.Today));


            //personInformation.Add(new Person(204, "Stervanos", "Semosa", DateTime.Today));

            return p;
        }
        public static int GetPersonID(string Uname, string Upassword, bool access)
        {
            //access data access to get id 

            int id=0;
            return id;
        }
    }
}