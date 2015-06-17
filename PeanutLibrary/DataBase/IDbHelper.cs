using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public interface IDbHelper : IDbPublic, IDbSimple, IDbParameter, IDbProcedure, IDbTransaction
    {
    }
}
