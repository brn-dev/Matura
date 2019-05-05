using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using GraphControl;
using IpGraph.Database;
using IpGraph.Database.Models;

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
            DataContext = this;
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

        private Router GetMin(IReadOnlyCollection<Router> routers, Dictionary<Router, double> dist)
        {
            var minDist = double.PositiveInfinity;
            Router minRouter = null;

            foreach (var router in routers)
            {
                if (dist[router] < minDist)
                {
                    minRouter = router;
                    minDist = dist[router];
                }
            }

            if (minRouter is null)
            {
                return routers.First();
            }

            return minRouter;
        }

        private IEnumerable<(Router, double)> GetNeighbors(Router router)
        {
            var neighbors = new List<(Router, double)>();

            foreach (var conn in router.Endpoint1Connections)
            {
                neighbors.Add((conn.Endpoint2, conn.TransmissionTime));
            }

            foreach (var conn in router.Endpoint2Connections)
            {
                neighbors.Add((conn.Endpoint1, conn.TransmissionTime));
            }

            return neighbors;
        }


        private void Graph_OnEndChanged(object sender, RoutedEventArgs e)
        {
            var routers = RoutingDbContext.Instance.Routers.ToArray();

            var start = routers.Single(r => r.Id.ToString() == Graph.Start.Text);
            var end = routers.Single(r => r.Id.ToString() == Graph.End.Text);

            var q = new List<Router>();

            var dist = new Dictionary<Router, double>();
            var prev = new Dictionary<Router, Router>();

            foreach (var router in routers)
            {
                dist[router] = double.PositiveInfinity;
                prev[router] = null;
                q.Add(router);
            }

            dist[start] = 0;

            while (q.Count > 0)
            {
                var u = GetMin(q, dist);

                q.Remove(u);

                if (u == end)
                {
                    while (u != null)
                    {
                        _nodesById[u.Id].Colors.Add(Colors.Blue);
                        Debug.WriteLine(u.Id);
                        u = prev[u];
                    }

                    return;
                }

                foreach (var (neighbor, distance) in GetNeighbors(u))
                {
                    var alt = dist[u] + distance;
                    if (alt < dist[neighbor])
                    {
                        dist[neighbor] = alt;
                        prev[neighbor] = u;
                    }
                }
            }
        }
    }
}
