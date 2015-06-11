using System;
using System.Collections.Generic;
using System.Text;

namespace PeanutLibrary.DataBase
{
    public interface IDbHelper : IDbPrivate, IDbPublic, IDbSimple, IDbParameter, IDbProcedure, IDbTransaction
    {
    }
}
