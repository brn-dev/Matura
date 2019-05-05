using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace IpGraph.Database.Models
{
    [Table]
    internal class Connection
    {
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public double TransmissionTime { get; set; }

        [Column(Name = "Endpoint1")] private int? _endpoint1Id;

        private EntityRef<Router> _endpoint1;

        [Association(
            IsForeignKey = true, 
            Storage = nameof(_endpoint1), 
            ThisKey = "_endpoint1Id")]
        public Router Endpoint1
        {
            get => _endpoint1.Entity;
            set => _endpoint1.Entity = value;
        }

        [Column(Name = "Endpoint2")] private int? _endpoint2Id;

        private EntityRef<Router> _endpoint2;

        [Association(
            IsForeignKey = true, 
            Storage = nameof(_endpoint2), 
            ThisKey = "_endpoint2Id")]
        public Router Endpoint2
        {
            get => _endpoint2.Entity;
            set => _endpoint2.Entity = value;
        }
    }
}
