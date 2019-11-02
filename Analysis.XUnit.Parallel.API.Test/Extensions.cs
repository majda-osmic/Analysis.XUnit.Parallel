using Analysis.XUnit.Parallel.API.Model;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Analysis.XUnit.Parallel.API.Test
{
    public static class Extensions
    {
        private static object _customerContextLock = new object();
        public static CustomerDbContext InitializeEmptyContext(this CustomerDbContext context)
        {
            lock (_customerContextLock)
            {
                if (!context.Customers.Any())
                {
                    context.Customers.Add(new Customer
                    {
                        ID = 1,
                        FirstName = "John",
                        LastName = "Doe"
                    });

                    context.Customers.Add(new Customer
                    {
                        ID = 2,
                        FirstName = "Jane",
                        LastName = "Doe"
                    });

                    context.Customers.Add(new Customer
                    {
                        ID = 3,
                        FirstName = "Max",
                        LastName = "Mustermann"
                    });

                    context.SaveChanges();
                }
            }

            return context;
        }

        public static async Task<T> DeserializeContent<T>(this HttpResponseMessage message) =>
           await JsonSerializer.DeserializeAsync<T>(await message.Content.ReadAsStreamAsync().ConfigureAwait(false),
           new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).ConfigureAwait(false);
    }
}
