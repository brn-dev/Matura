using System.Data.Linq;
using System.Data.SQLite;
using IpGraph.Database.Models;

namespace IpGraph.Database
{
    internal class RoutingDbContext
    {
        private const string ConnectionString = "DbLinqProvider=Sqlite;Data Source=Routing.db";

        private readonly DataContext _context;

        private RoutingDbContext()
        {
            var connection = new SQLiteConnection(ConnectionString);
            _context = new DataContext(connection);
        }

        public Table<Router> Routers => _context.GetTable<Router>();

        public Table<Connection> Connections => _context.GetTable<Connection>();

        private static RoutingDbContext _instance;

        public static RoutingDbContext Instance => _instance ?? (_instance = new RoutingDbContext());
    }
}
