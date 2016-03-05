using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace POCReportApp.Classes
{
    public class DAL
    {

           public enum criteriaType { OperationsDataCriteria }

    
        public static DataSet FetchDataSet(string command, object[] parms, int? timeout = null)
        {
            DataSet dResult = RunThread2(command, parms, timeout);
            return dResult;
        }

        public static DataSet FetchDataSet(string command, object[] parms,  int? timeout = null)
        {
            DataSet dResult = RunThread2(command, parms, timeout);
            return dResult;
        }

     

        public static DataTable FetchDataTable(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            DataTable dResult = RunThread(ConnectionString, CommandText, commType, Parameters);
            return dResult != null ? dResult : null;
        }

        public static DataTable FetchDataTable(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters, drGeneric_String_String gi)
        {
            DataTable dResult = RunThread(ConnectionString, CommandText, commType, Parameters, gi);
            return dResult != null ? dResult : null;
        }
        
        public static DataSet FetchDataSet(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            DataSet dResult = RunThread2(ConnectionString, CommandText, commType, Parameters);
            return dResult != null ? dResult : null;
        }

        public static DataRow FetchDataRow(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            DataTable dResult = RunThread(ConnectionString, CommandText, commType, Parameters);
            return dResult != null ? dResult.Rows.Count > 0 ? dResult.Rows[0] : null : null;
        }

        public static DataRow FetchDataRow(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters, drGeneric_String_String gi)
        {
            DataTable dResult = RunThread(ConnectionString, CommandText, commType, Parameters, gi);
            return dResult != null ? dResult.Rows.Count > 0 ? dResult.Rows[0] : null : null;
        }

        public static DataTable FetchDataTable(string command, object[] parms, drGeneric_String_String globalInputs, int? timeout = null)
        {
            DataResult dResult = RunThread(command, parms, globalInputs, timeout);
            return dResult != null ? dResult.DataTable : null;

        }

        public static DataRow FetchDataRow(string command, object[] parms, int? timeout = null)
        {
            DataResult dResult = RunThread(command, parms, timeout);

            if (dResult == null || dResult.DataTable == null || dResult.DataTable.Rows.Count == 0)
                return null;

            return dResult.DataTable.Rows[0];
        }

        public static bool ExecuteProcedure(string command, object[] parms, int? timeout = null)
        {
            return RunThread3(command, parms, timeout);
        }

        public static void ExecuteProcedure(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            RunThread3(ConnectionString, CommandText, commType, Parameters);
        }

        public static DataResult InsertRecord(DataCriteria criteria)
        {
            return CreateRecord(criteria);
        }

        public static DataResult RemoveRecord(DataCriteria criteria)
        {
            return DeleteRecord(criteria);
        }

        #region Multi-Threading



        public delegate DataTable AsynchDataTable(string ConnectionString, string CommandText, CommandType commtype, SqlParameterCollection Parameters);
        public delegate DataSet AsynchDataSet(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters);
        public delegate DataSet AsynchDataSet2(string command, object[] parms,  int? timeout = null);
        public delegate bool AsynchProcedure(string command, object[] parms,  int? timeout = null);
        public delegate void AsynchProcedure2(string ConnectionString, string CommandText, CommandType commtype, SqlParameterCollection Parameters);


     

        private static DataTable RunThread(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            // Create the delegate.
            AsynchDataTable caller = new AsynchDataTable(GetDataTable);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(ConnectionString, CommandText, commType, Parameters, GetGlobalInputs(), null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // Call EndInvoke to retrieve the results.
            DataTable dResult = caller.EndInvoke(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            caller = null;
            result = null;

            return dResult;
        }

        private static DataTable RunThread(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            // Create the delegate.
            AsynchDataTable caller = new AsynchDataTable(GetDataTable);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(ConnectionString, CommandText, commType, Parameters, gi, null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // Call EndInvoke to retrieve the results.
            DataTable dResult = caller.EndInvoke(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            caller = null;
            result = null;

            return dResult;
        }

        private static DataSet RunThread2(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            // Create the delegate.
            AsynchDataSet caller = new AsynchDataSet(GetDataSet);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(ConnectionString, CommandText, commType, Parameters, GetGlobalInputs(), null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // Call EndInvoke to retrieve the results.
            DataSet dResult = caller.EndInvoke(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            caller = null;
            result = null;

            return dResult;
        }

        private static DataSet RunThread2(string command, object[] parms, int? timeout = null)
        {
            // Create the delegate.
            AsynchDataSet2 caller = new AsynchDataSet2(GetDataSet);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(command, parms, GetGlobalInputs(), timeout, null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // Call EndInvoke to retrieve the results.
            DataSet dResult = caller.EndInvoke(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            caller = null;
            result = null;
            return dResult;

        }

        private static DataSet RunThread2(string command, object[] parms, drGeneric_String_String gi, int? timeout = null)
        {
            // Create the delegate.
            AsynchDataSet2 caller = new AsynchDataSet2(GetDataSet);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(command, parms, gi, timeout, null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // Call EndInvoke to retrieve the results.
            DataSet dResult = caller.EndInvoke(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            caller = null;
            result = null;
            return dResult;

        }

        private static bool RunThread3(string command, object[] parms, int? timeout = null)
        {
            // Create the delegate.
            AsynchProcedure caller = new AsynchProcedure(ExecuteProcedure2);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(command, parms, GetGlobalInputs(), timeout, null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // Call EndInvoke to retrieve the results.
            bool dResult = caller.EndInvoke(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            caller = null;
            result = null;

            return dResult;
        }

        private static void RunThread3(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters)
        {
            // Create the delegate.
            AsynchProcedure2 caller = new AsynchProcedure2(ExecuteProcedure2);

            // Initiate the asychronous call.
            IAsyncResult result = caller.BeginInvoke(ConnectionString, CommandText, commType, Parameters, GetGlobalInputs(), null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            // Call EndInvoke to retrieve the results.
            caller.EndInvoke(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();
            caller = null;
            result = null;
        }

        private static drGeneric_String_String GetGlobalInputs()
        {
            drGeneric_String_String globalInputs;
            if (System.Web.HttpContext.Current == null || System.Web.HttpContext.Current.Session == null)
            {
                globalInputs = null;
            }
            else
            {
                globalInputs = (drGeneric_String_String)System.Web.HttpContext.Current.Session["GlobalInputs"];
            }

            return globalInputs;
        }

        private static DataResult GetDataResult(string command, object[] parms, drGeneric_String_String gi, int? timeout = null)
        {
            var criteria = GetDataCriteria(criteriaType.OperationsDataCriteria);
            if (timeout.HasValue) criteria.TemporaryTimeout = timeout;
            criteria.InputParameters = ThreadSafeDetermineParms(command, parms, gi);
            criteria.ReadCommand = command;

            DataResult result;
            if (AppSettings.IsLoggingEnabled)
            {
                Log(command, parms, true);
                result = FetchAll(criteria);
                Log(command, parms, false);
            }
            else
            {
                result = FetchAll(criteria);
            }

            return result;
        }

        private static DataResult GetDataResult2(DataCriteria criteria)
        {
            DataResult result;
            criteria.TemporaryTimeout = 60;

            if (AppSettings.IsLoggingEnabled)
            {
                Log(criteria.ToString(), new object[0], true);
                result = FetchAll(criteria);
                Log(criteria.ToString(), new object[0], false);
            }
            else
            {
                result = FetchAll(criteria);
            }
            return result;
        }

        private static DataSet GetDataSet(string command, object[] parms, drGeneric_String_String gi, int? timeout = null)
        {
            var criteria = GetDataCriteria(criteriaType.OperationsDataCriteria);
            if (timeout.HasValue) criteria.TemporaryTimeout = timeout;
            criteria.InputParameters = ThreadSafeDetermineParms(command, parms, gi);
            criteria.ReadCommand = command;

            DataSet result;
            if (AppSettings.IsLoggingEnabled)
            {
                Log(command, parms, true);
                result = FetchDataSet(criteria);
                Log(command, parms, false);
            }
            else
            {
                result = FetchDataSet(criteria);
            }

            return result;
        }

        private static SqlParameterCollection ThreadSafeDetermineParms(string command, SqlParameterCollection parms, drGeneric_String_String globalInputs)
        {
            if (NonUpdatedProcedures.Instance.IsProcedureUpdated(command))
            {

                //******* 20121029 DHB Start code changes. 
                //if (AppSettings.GlobalInputs == null)
                //{
                //    throw new ApplicationException("The procedure " + command + " has been flagged as being updated to accept GlobalInputs but the call to FetchDataTable did not include GlobalInputs. Please modify your call. Contact Wesley or Michael for further info.");
                //}
                //******* 20121029 DHB Stop code changes. 
                if (!parms.Contains("GlobalInputs"))
                {
                    SqlParameter param = new SqlParameter("GlobalInputs", SqlDbType.Structured);
                    param.Value = globalInputs.ToDataTable();
                    parms.Add(param);
                }
                return parms;
            }

            return parms;
        }

        private static object[] ThreadSafeDetermineParms(string command, object[] parms, drGeneric_String_String globalInputs)
        {
            if (NonUpdatedProcedures.Instance.IsProcedureUpdated(command))
            {

                if (globalInputs == null)
                {
                    throw new ApplicationException("The procedure " + command + " has been flagged as being updated to accept GlobalInputs but the call to FetchDataTable did not include GlobalInputs. Please modify your call. Contact Wesley or Michael for further info.");
                }

                var new_parms = new object[parms.Length + 1];
                parms.CopyTo(new_parms, 1);
                new_parms[0] = globalInputs.ToSql();
                return new_parms;
            }

            return parms;
        }

        private static bool ExecuteProcedure2(string command, object[] parms, drGeneric_String_String gi, int? timeout = null)
        {
            var criteria = GetDataCriteria(criteriaType.OperationsDataCriteria);
            if (timeout.HasValue) criteria.TemporaryTimeout = timeout;
            criteria.UpdateCommand = command;
            criteria.InputParameters = ThreadSafeDetermineParms(command, parms, gi);

            if (AppSettings.IsLoggingEnabled)
            {
                
                Log(command, parms, true);
                UpdateRecord(criteria);
                Log(command, parms, false);
            }
            else
            {
                UpdateRecord(criteria);
            }

            return true;
        }

        private static void ExecuteProcedure2(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters, drGeneric_String_String gi)
        {
            using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString))
            {
                conn.Open();
                using (SqlCommand comm = new SqlCommand(CommandText, conn))
                {
                    Parameters = ThreadSafeDetermineParms(comm.CommandText, Parameters, gi);
                    comm.CommandTimeout = 60;
                    comm.CommandType = commType;

                    foreach (SqlParameter parm in Parameters)
                    {
                        comm.Parameters.AddWithValue(parm.ParameterName, parm.Value);
                        comm.Parameters[parm.ParameterName].TypeName = parm.TypeName;
                        comm.Parameters[parm.ParameterName].SqlDbType = parm.SqlDbType;
                    }

                    if (AppSettings.IsLoggingEnabled)
                    {
                        Log(comm.CommandText, Parameters, true);
                        comm.ExecuteNonQuery();
                        Log(comm.CommandText, Parameters, false);
                    }
                    else
                    {
                        comm.ExecuteNonQuery();
                    }
                }
            }
        }

        private static DataTable GetDataTable(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters, drGeneric_String_String gi)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(string.IsNullOrEmpty(ConnectionString) ?AppSettings.ConnectionString:ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand(CommandText, conn))
                {
                    Parameters = ThreadSafeDetermineParms(comm.CommandText, Parameters, gi);
                    comm.CommandTimeout = 60;
                    comm.CommandType = commType;

                    foreach (SqlParameter parm in Parameters)
                    {
                        comm.Parameters.AddWithValue(parm.ParameterName, parm.Value);
                        comm.Parameters[parm.ParameterName].TypeName = parm.TypeName;
                        comm.Parameters[parm.ParameterName].SqlDbType = parm.SqlDbType;
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(comm))
                    {
                        if (AppSettings.IsLoggingEnabled)
                        {
                            Log(comm.CommandText, Parameters, true);
                            da.Fill(dt);
                            Log(comm.CommandText, Parameters, false);
                        }
                        else
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }

            return dt;
        }

        private static DataSet GetDataSet(string ConnectionString, string CommandText, CommandType commType, SqlParameterCollection Parameters, drGeneric_String_String gi)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(string.IsNullOrEmpty(ConnectionString) ? AppSettings.ConnectionString : ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand(CommandText, conn))
                {
                    Parameters = ThreadSafeDetermineParms(comm.CommandText, Parameters, gi);
                    comm.CommandTimeout = 60;
                    comm.CommandType = commType;

                    foreach (SqlParameter parm in Parameters)
                    {
                        comm.Parameters.AddWithValue(parm.ParameterName, parm.Value);
                        comm.Parameters[parm.ParameterName].TypeName = parm.TypeName;
                        comm.Parameters[parm.ParameterName].SqlDbType = parm.SqlDbType;
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(comm))
                    {
                        if (AppSettings.IsLoggingEnabled)
                        {
                            Log(comm.CommandText, Parameters, true);
                            da.Fill(ds);
                            Log(comm.CommandText, Parameters, false);
                        }
                        else
                        {
                            da.Fill(ds);
                        }
                    }
                }
            }

            return ds;
        }

        public static SqlParameter GetParmFromTable(SQLTableValueInput sqlTable, string ParmName)
        {
            SqlParameter parm = new SqlParameter();
            parm.Value = sqlTable.Data;
            parm.TypeName = sqlTable.TypeName;
            parm.SqlDbType = SqlDbType.Structured;
            parm.ParameterName = ParmName;
            return parm;
        }

        private static void Log(string command, object[] parms, bool isStart)
        {
            string sqlParms = string.Empty;

            if (parms != null)
            {
                for (int i = 0; i < parms.Length; i++)
                {
                    if (i < parms.Length - 1)
                    { sqlParms += parms[i] == null ? string.Empty : parms[i].ToString() + ", "; }
                    else
                    { sqlParms += parms[i] == null ? string.Empty : parms[i].ToString(); }
                }
            }

            Log(command, sqlParms, isStart);
        }

        private static void Log(string command, SqlParameterCollection parms, bool isStart)
        {
            string sqlParms = string.Empty;

            if (parms != null)
            {
                for (int i = 0; i < parms.Count; i++)
                {
                    if (parms[i].Direction == ParameterDirection.Input || parms[i].Direction == ParameterDirection.InputOutput)
                    {
                        if (i < parms.Count - 1)
                        { sqlParms += parms[i] == null ? string.Empty : parms[i].Value + ", "; }
                        else
                        { sqlParms += parms[i] == null ? string.Empty : parms[i].Value; }
                    }
                }
            }

            Log(command, sqlParms, isStart);
        }

        private static void Log(string command, string parms, bool isStart)
        {
            var logger = "Thinkgate.SPs." + command + "." + AppSettings.AppVirtualPath + "->" + MethodBase.GetCurrentMethod().Name;

            if (isStart)
            { ThinkgateEventSource.Log.SQLDatabaseStart(logger, "exec " + command + " " + parms, command); }
            else
            { ThinkgateEventSource.Log.SQLDatabaseEnd(logger, "end " + command + " " + parms, command); }
        }

        #endregion
    }

    }
