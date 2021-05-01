using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers
{

    public interface ISQLHelper
    {
        Task<DataTable> GetDataFromStoredProc(
            string procName,
            Dictionary<string,string> procParams
        );
    }

    public class SQLHelper : ISQLHelper
    {
        private readonly IErrorHelper _errorHelper;
        private readonly GNBCContext _context;
        public SQLHelper(IErrorHelper errorHelper, GNBCContext context)
        {
            _errorHelper = errorHelper;
            _context = context;
        }

        public async Task<DataTable> GetDataFromStoredProc(
            string procName,
            Dictionary<string,string> procParams
        )
        {
            var resTable = new DataTable();
            try
            {
                Task<DataTable> dTable = Task<DataTable>.Factory.StartNew(()=>{
                    try
                    {
                        using(var command = _context.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = procName;
                            command.CommandType = CommandType.StoredProcedure;
                            _context.Database.OpenConnection();

                            foreach (KeyValuePair<string, string> param in procParams)
                            {
                                command.Parameters.Add(param);
                            }

                            //Update to SQL adapter later!
                            using(var result =  command.ExecuteReader())
                            {
                                if(result.HasRows)
                                {
                                    resTable.Load(result);
                                }
                                else
                                {
                                    _errorHelper.CreateError(400, "No data was found");
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        _errorHelper.CreateError(500, "An Error has ocurred: " + ex.Message);
                    }
                    
                    return resTable;
                });
                return await dTable;
            }
            catch(Exception ex)
            {
                _errorHelper.CreateError(500, "An Error has ocurred: " + ex.Message);
            }

            return resTable;
        }
    }
}