using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace ESFA.DC.Summarisation.ReportService.Data.Repository
{
    public class AbstractRepositoryService
    {
        public virtual async Task<IEnumerable<T>> ExecuteSqlWithStringParameterAsync<T>(string connectionString, string parameter, string sql, CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var commandDefinition = new CommandDefinition(sql, new { parameter }, commandTimeout: 600, cancellationToken: cancellationToken);

                return await sqlConnection.QueryAsync<T>(commandDefinition);
            }
        }

        public virtual async Task<IEnumerable<T>> ExecuteSqlWithParameterAsync<T>(string connectionString, object parameter, string sql, CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var commandDefinition = new CommandDefinition(sql, new { parameter }, commandTimeout: 600, cancellationToken: cancellationToken);

                return await sqlConnection.QueryAsync<T>(commandDefinition);
            }
        }


        public virtual async Task<IEnumerable<T>> ExecuteSqlWithParametersAsync<T>(string connectionString, object parameter, string sql, CancellationToken cancellationToken)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var commandDefinition = new CommandDefinition(sql, parameter, commandTimeout: 600, cancellationToken: cancellationToken);

                return await sqlConnection.QueryAsync<T>(commandDefinition);
            }
        }
    }
}
