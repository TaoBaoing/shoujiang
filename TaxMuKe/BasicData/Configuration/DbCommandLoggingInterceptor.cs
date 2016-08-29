using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;
using log4net;


namespace BasicData
{
    public class DbCommandLoggingInterceptor : DbCommandInterceptor
    {
        protected ILog log = LogManager.GetLogger("log4net");
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {     
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            Loging(command, interceptionContext.Exception);
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            Loging(command, interceptionContext.Exception);
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            Loging(command, interceptionContext.Exception);
            base.ReaderExecuted(command, interceptionContext);
        }

        private void Loging(DbCommand command, Exception exception)
        {
            if (exception != null)
                log.Error(string.Format("Error:{0} Command:{1}", exception.ToString(), command.CommandText));
            else
            {
                string logMsg = command.CommandText.Replace("[", "").Replace("]", "");
                command.Parameters.Cast<DbParameter>().ToList().ForEach((i) => { logMsg = logMsg.Replace(i.ParameterName.Contains("@") ? i.ParameterName : "@" + i.ParameterName, "'" + i.Value.ToString() + "'"); });
                log.Info(string.Format("Elapsed:{0} Command:{1} ", _stopwatch.Elapsed, logMsg));
            }
        }
    }
}