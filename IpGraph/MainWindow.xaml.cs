using System.Collections.Generic;
using System.Windows;
using GraphControl;
using IpGraph.Database;

namespace IpGraph
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IDictionary<int, Node> _nodesById;

        public MainWindow()
        {
            InitializeComponent();
            InitDatabaseData();
        }

        private void InitDatabaseData()
        {
            _nodesById = new Dictionary<int, Node>();
            foreach (var router in RoutingDbContext.Instance.Routers)
            {
                var node = new Node
                {
                    Text = router.Id.ToString()
                };
                Graph.Nodes.Add(node);
                _nodesById[router.Id] = node;
            }

            foreach (var connection in RoutingDbContext.Instance.Connections)
            {
                var node1 = _nodesById[connection.Endpoint1.Id];
                var node2 = _nodesById[connection.Endpoint2.Id];
                var edge = new Edge
                {
                    First = node1,
                    Second = node2,
                    Text = connection.Id.ToString()
                };
                Graph.Edges.Add(edge);
            }
        }
    }
}
