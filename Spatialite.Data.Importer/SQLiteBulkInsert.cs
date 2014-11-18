namespace Spatialite.Data.Importer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Sqlite bulk insert.
    /// Link: http://procbits.com/2009/09/08/sqlite-bulk-insert
    /// </summary>
    public class SqliteBulkInsert
    {
        #region Constants

        /// <summary>The param delim.</summary>
        private const string ParamDelim = ":";

        #endregion

        #region Fields

        /// <summary>The begin insert text.</summary>
        private readonly string beginInsertText;

        /// <summary>The conn.</summary>
        private readonly SQLiteConnection conn;

        /// <summary>The parameters.</summary>
        private readonly IDictionary<string, SQLiteParameter> parameters = new Dictionary<string, SQLiteParameter>();

        /// <summary>The table name.</summary>
        private readonly string tableName;

        /// <summary>The cmd.</summary>
        private SQLiteCommand cmd;

        /// <summary>The counter.</summary>
        private uint counter;

        /// <summary>The txn.</summary>
        private SQLiteTransaction txn;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="SqliteBulkInsert"/> class.</summary>
        /// <param name="connection">The db connection.</param>
        /// <param name="tableName">The table name.</param>
        public SqliteBulkInsert(SQLiteConnection connection, string tableName)
        {
            this.CommitMax = 10000;
            this.AllowBulkInsert = true;
            this.conn = connection;
            this.tableName = tableName;

            StringBuilder query = new StringBuilder(255);
            query.AppendFormat("INSERT INTO [{0}] (", tableName);
            this.beginInsertText = query.ToString();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets a value indicating whether allow bulk insert.</summary>
        public bool AllowBulkInsert { get; set; }

        /// <summary>Gets the command text.</summary>
        /// <exception cref="SQLiteException">You must add at least one parameter.</exception>
        public string CommandText
        {
            get
            {
                if (this.parameters.Count < 1)
                {
                    throw new SQLiteException("You must add at least one parameter.");
                }

                StringBuilder sb = new StringBuilder(255);
                sb.Append(this.beginInsertText);

                foreach (string param in this.parameters.Keys)
                {
                    sb.AppendFormat("[{0}], ", param);
                }

                sb.Remove(sb.Length - 2, 2);
                sb.Append(") VALUES (");

                foreach (string param in this.parameters.Keys)
                {
                    sb.AppendFormat("{0}{1}, ", ParamDelim, param);
                }

                sb.Remove(sb.Length - 2, 2);
                sb.Append(")");

                return sb.ToString();
            }
        }

        /// <summary>Gets or sets the commit max.</summary>
        [CLSCompliant(false)]
        public uint CommitMax { get; set; }

        /// <summary>Gets the param delimiter.</summary>
        public string ParamDelimiter
        {
            get
            {
                return ParamDelim;
            }
        }

        /// <summary>Gets the table name.</summary>
        public string TableName
        {
            get
            {
                return this.tableName;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Add parameter.</summary>
        /// <param name="name">Name of parameter.</param>
        /// <param name="type">Type of parameter.</param>
        public void AddParameter(string name, DbType type)
        {
            string parameterName = string.Format("{0}{1}", ParamDelim, name);
            SQLiteParameter param = new SQLiteParameter(parameterName, type);
            this.parameters.Add(name, param);
        }

        /// <summary>Flush transaction.</summary>
        /// <exception cref="Exception">Could not commit transaction. See InnerException for more details.</exception>
        public void Flush()
        {
            try
            {
                if (this.txn != null)
                {
                    this.txn.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not commit transaction. See InnerException for more details", ex);
            }
            finally
            {
                if (this.txn != null)
                {
                    this.txn.Dispose();
                }

                this.txn = null;
                this.counter = 0;
            }
        }

        /// <summary>The insert.</summary>
        /// <param name="paramValues">The param values.</param>
        /// <exception cref="Exception">The values array count must be equal to the count of the number of parameters.</exception>
        public void Insert(object[] paramValues)
        {
            if (paramValues.Length != this.parameters.Count)
            {
                throw new Exception("The values array count must be equal to the count of the number of parameters.");
            }

            this.counter++;

            if (this.counter == 1)
            {
                if (this.AllowBulkInsert)
                {
                    this.txn = this.conn.BeginTransaction();
                }

                this.cmd = this.conn.CreateCommand();
                foreach (SQLiteParameter par in this.parameters.Values)
                {
                    this.cmd.Parameters.Add(par);
                }

                this.cmd.CommandText = this.CommandText;
            }

            int i = 0;
            foreach (SQLiteParameter par in this.parameters.Values)
            {
                par.Value = paramValues[i];
                i++;
            }

            this.cmd.ExecuteNonQuery();

            if (this.counter == this.CommitMax)
            {
                try
                {
                    if (this.txn != null)
                    {
                        this.txn.Commit();
                    }
                }
                catch (Exception)
                {
                    Debug.Print("Exception");
                }
                finally
                {
                    if (this.txn != null)
                    {
                        this.txn.Dispose();
                        this.txn = null;
                    }

                    this.counter = 0;
                }
            }
        }

        #endregion
    }
}