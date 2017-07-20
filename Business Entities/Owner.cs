using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
   public  class Owner:Person
    {
       private int oId;

        #region properties
        public int OId
        {
            get { return oId; }
            set { oId = value; }
        }
        #endregion
        public Owner(int oId,int pId,string pName,string pSurname, DateTime dob)
           : base( pId, pName, pSurname,  dob)
        {
          this.oId = oId;

        }
        public Owner()
        {

        }
    }
}
