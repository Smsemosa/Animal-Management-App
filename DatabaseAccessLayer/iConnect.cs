using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseAccessLayer
{
    public interface iConnect
    {
        void OpenConnection();
        void CloseConnection();
 
    }
}
