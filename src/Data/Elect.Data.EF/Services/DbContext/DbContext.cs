namespace Elect.Data.EF.Services.DbContext
{
    public abstract class DbContext : Microsoft.EntityFrameworkCore.DbContext, Elect.Data.EF.Interfaces.DbContext.IDbContext
    {
        protected DbContext()
        {
        }
        protected DbContext(DbContextOptions options) : base(options)
        {
        }
        public DbCommand CreateCommand(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            var connection = Database.GetDbConnection();
            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = text;
            cmd.CommandType = type;
            if (parameters?.Any() == true)
            {
                foreach (var parameter in parameters)
                {
                    var p = cmd.CreateParameter();
                    p.DbType = parameter.DbType;
                    p.ParameterName = parameter.ParameterName;
                    p.Value = parameter.Value;
                    cmd.Parameters.Add(p);
                }
            }
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
            connection.Open();
            return cmd;
        }
        public void ExecuteCommand(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(text, type, parameters);
            cmd.ExecuteReader();
        }
        public List<T> ExecuteCommand<T>(string text, CommandType type = CommandType.Text, params SqlParameter[] parameters)where T : class, new()
        {
            var cmd = CreateCommand(text, type, parameters);
            using (var reader = cmd.ExecuteReader())
            {
                var data = reader.QueryTo<T>();
                return data;
            }
        }
    }
}
