using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using Workers.Abstractions;

namespace Workers.SqlClientNewer
{
    public class Worker : IWorker
    {
        public async Task<string> RunAsync()
        {
            using var conn = new SqlConnection("Server=port077;Database=GWS-Training;User Id=gws;Password=gws;Encrypt=false");
            using var cmd = new SqlCommand("SELECT COUNT(*) FROM GWS_REQUEST", conn);

            string? res = null;
            try
            {
                await conn.OpenAsync();
                res = (await cmd.ExecuteScalarAsync()).ToString();
            }
            catch (Exception ex)
            {
                res = $"({ex.GetType()}) {ex.Message} {ex.StackTrace}";
            }

            return res;
        }
    }
}
