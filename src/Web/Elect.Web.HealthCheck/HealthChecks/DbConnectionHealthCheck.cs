namespace Elect.Web.HealthCheck.HealthChecks
{
    public class DbConnectionHealthCheck : IHealthCheck
    {
        public string ConnectionString { get; }
        public DbConnectionHealthCheck(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
                }
            }
            return HealthCheckResult.Healthy();
        }
    }
}
