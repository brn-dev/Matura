using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace IpGraph.Database.Models
{
    [Table]
    internal class Router
    {

        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Country { get; set; }

        [Column]
        public string Region { get; set; }

        [Column]
        public string City { get; set; }

        private readonly EntitySet<Connection> _endpoint1Connections = new EntitySet<Connection>();

        [Association(
            Storage = nameof(_endpoint1Connections), 
            OtherKey = "Endpoint1", 
            ThisKey = "Id")]
        public ICollection<Connection> Endpoint1Connections
        {
            get => _endpoint1Connections;
            set => _endpoint1Connections.Assign(value);
        }

        private readonly EntitySet<Connection> _endpoint2Connections = new EntitySet<Connection>();

        [Association(
            Storage = nameof(_endpoint2Connections), 
            OtherKey = "Endpoint2", 
            ThisKey = "Id")]
        public ICollection<Connection> Endpoint2Connections
        {
            get => _endpoint2Connections;
            set => _endpoint2Connections.Assign(value);
        }

    }
}
