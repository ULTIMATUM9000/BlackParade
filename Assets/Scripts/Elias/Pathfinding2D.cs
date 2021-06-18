using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using YergoScripts.Grid;

namespace YergoScripts.Pathfinding
{
    /// <summary>
    /// Pathfinding Formula to determine the shortest path. Manhattan - Rook Movement, Diagonal - King Movement, Euclidean - Any movement
    /// </summary>
    public enum PathfindingHeuristicType2D
    {
        Manhattan, Diagonal, Euclidean
    }

    public class Route2D
    {
        float _Distance = 0;
        List<Vector2> _Path;

        public float Distance { get => _Distance; set => _Distance = value; }
        public List<Vector2> Path { get => _Path; set => _Path = value; }
    }

    /*
    public class Pathfinding2D
    { 
        class GridCellNode2D
        {
            GridCellNode2D parent;
            GridCell gridCell;
            float gCost = 0;
            float fCost = 0;

            public GridCellNode2D Parent { get => parent; set => parent = value; }
            public GridCell GridCell { get => gridCell; set => gridCell = value; }
            public float GCost { get => gCost; set => gCost = value; }
            public float FCost { get => fCost; set => fCost = value; }
            public Vector2 Position { get => gridCell.WorldPosition; }
        }
        /// <summary>
        /// Manhattan - Rook Movement, Diagonal - King Movement, Euclidean - Any movement
        /// </summary>
        public enum HeuristicFormula
        {
            Manhattan, Diagonal, Euclidean
        }

        Heuristic DetermineHeuristicFormula(HeuristicFormula heuristicFormula)
        {
            Heuristic heuristic = null;

            switch (heuristicFormula)
            {
                case HeuristicFormula.Manhattan:
                    heuristic = ManhattanDistance;
                    break;
                case HeuristicFormula.Diagonal:
                    heuristic = DiagonalDistance;
                    break;
                case HeuristicFormula.Euclidean:
                    break;
            }

            return heuristic;
        }

        delegate float Heuristic(Vector2 start, Vector2 goal);

        float ManhattanDistance(Vector2 current, Vector2 goal)
        {
            return Mathf.Abs(current.x - goal.x) + Mathf.Abs(current.y - goal.y);
        }

        float DiagonalDistance(Vector2 current, Vector2 goal)
        {
            return Mathf.Max(Mathf.Abs(current.x - goal.x), Mathf.Abs(current.x - goal.x));
        }

        List<Vector2> ReconstructPath(GridCellNode2D currentNode)
        {
            List<Vector2> route = new List<Vector2>();

            while (currentNode.Parent != null) // While currentNode's parent is not yet null.
            {
                route.Add(currentNode.GridCell.WorldPosition); // Add curretNode's position to route.
                currentNode = currentNode.Parent; // Move to parent of currentNode
            }

            route.Reverse();
            return route;
        }

        GridCellNode2D FindLowestFScore(Dictionary<GridCell, GridCellNode2D> dictionary)
        {
            string blah = string.Empty;
            GridCellNode2D current = null;

            foreach (GridCellNode2D node in dictionary.Values)
            {
                current = node;
                break;
            }

            foreach (GridCellNode2D node in dictionary.Values)
            {
                if (node.FCost <= current.FCost)
                    current = node;
            }

            return current;
        }

        List<GridCellNode2D> GetNeighbors(Grid2D grid, GridCellNode2D current, HeuristicFormula heuristicFormula)
        {
            List<GridCellNode2D> neighborNodeList = new List<GridCellNode2D>();

            GridCellNode2D neighborNode = null;

            Grid2D.NeighborDirection neighborDirection = new Grid2D.NeighborDirection();
            
            switch (heuristicFormula)
            {
                case HeuristicFormula.Manhattan:
                    neighborDirection = Grid2D.NeighborDirection.Cardinal;        
                    break;

                case HeuristicFormula.Diagonal:
                    neighborDirection = Grid2D.NeighborDirection.CardinalAndOrdinal;
                    break;

                case HeuristicFormula.Euclidean:
                    break;
            }

            foreach(GridCell cell in grid.GetNeighbors(current.GridCell, neighborDirection))
            {
                neighborNode = new GridCellNode2D()
                {
                    GridCell = cell,
                    Parent = current,
                    GCost = current.GCost + Vector2.Distance(current.GridCell.WorldPosition, cell.WorldPosition)
                };

                neighborNodeList.Add(neighborNode);

            }

            return neighborNodeList;
        }

        /*
        public List<Vector2> GenerateRoute(Tilemap tilemap, Vector2Int start, Vector2Int goal, HeuristicFormula heuristicFormula)
        {
            Heuristic heuristic = DetermineHeuristicFormula(heuristicFormula);

            #region Tried Wikipedia 1
            //float tentativeGScore = 0;

            //Node currentNode = new Node();

            //List<Node> openList = new List<Node>();

            //List<Vector2> route = new List<Vector2>();

            //Vector2Int startPosition = new Vector2Int(Mathf.RoundToInt(start.x), 
            //    Mathf.RoundToInt(start.y));

            //Vector2Int goalPosition = new Vector2Int(Mathf.RoundToInt(goal.x),
            //    Mathf.RoundToInt(goal.y));

            //currentNode.position = startPosition;
            //currentNode.fCost = Mathf.Abs(currentNode.position.x - goalPosition.x) +
            //                Mathf.Abs(currentNode.position.y - goalPosition.y);

            //openList.Add(currentNode);

            //for (int t = 0; t < 2; t++)
            //{
            //    currentNode = FindLowestFScore(openList);

            //    if (currentNode.position == goalPosition)
            //        return ReconstructPath(currentNode, route);

            //    openList.Remove(currentNode);

            //    for(int i = 0; i < 4; i++)
            //    {
            //        // Get neightbor
            //        Node neightborNode = new Node();
            //        neightborNode.parent = currentNode;
            //        neightborNode.position = currentNode.position;

            //        if (i == 0) neightborNode.position += Vector2Int.up;
            //        if (i == 1) neightborNode.position += Vector2Int.right;
            //        if (i == 2) neightborNode.position += Vector2Int.down;
            //        if (i == 3) neightborNode.position += Vector2Int.left;
            //        // Get neightbor

            //        tentativeGScore = currentNode.gCost + FindWeightBetweenVector(currentNode.position, neightborNode.position);
            //        Debug.Log(neightborNode.position + ": " + tentativeGScore);
            //        if (neightborNode.position != currentNode.position)
            //        {
            //            neightborNode.gCost = tentativeGScore;
            //            neightborNode.hCost = Mathf.Abs(neightborNode.position.x - goalPosition.x) +
            //                Mathf.Abs(neightborNode.position.y - goalPosition.y);
            //            neightborNode.fCost = neightborNode.gCost + neightborNode.hCost;

            //            // Debug
            //            m_Checked.GetComponent<ObjectNode>().getCost(neightborNode.hCost, neightborNode.gCost, neightborNode.fCost);
            //            Instantiate(m_Checked, (Vector2)neightborNode.position, Quaternion.identity);
            //            // Debug

            //            openList.Add(neightborNode); 
            //            //Debug.Log(i + ": " + node.position + " f: " + node.fCost);
            //        }
            //    }
            //}
            #endregion

            #region Tried Wikipedia 2
            //float tentative_gScore = 0;

            //List<Vector2Int> openSet = new List<Vector2Int>();
            //List<Vector2Int> cameFrom = new List<Vector2Int>();

            //Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
            //Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();

            //Vector2Int goalPosition = new Vector2Int(Mathf.RoundToInt(goal.x), Mathf.RoundToInt(goal.y));
            //Vector2Int current = new Vector2Int(Mathf.RoundToInt(start.x), Mathf.RoundToInt(start.y));

            //gScore.Add(current, 0);
            //fScore.Add(current, Mathf.Abs(current.x - goalPosition.x) + Mathf.Abs(current.y - goalPosition.y));

            //for(int x = 0; x < 2; x++)
            //{
            //    foreach (Vector2Int set in openSet)
            //        current = set;

            //    foreach(Vector2Int set in openSet)
            //    {
            //        if (fScore[current] > fScore[set])
            //            current = set;
            //    }

            //    if (current == goalPosition)
            //        break; // Return Route

            //    openSet.Remove(current);

            //    for(int i = 0; i < 4; i++)
            //    {
            //        Vector2Int neighbor = current;

            //        if (i == 0) neighbor += Vector2Int.up;
            //        if (i == 1) neighbor += Vector2Int.right;
            //        if (i == 2) neighbor += Vector2Int.down;
            //        if (i == 3) neighbor += Vector2Int.left;
            //        Debug.Log(gScore.ContainsKey(neighbor));
            //        //if (gScore.ContainsKey(neighbor))
            //        //    tentative_gScore = gScore[current] + FindWeightBetweenVector(current, neighbor);


            //    }
            //}
            #endregion

            #region Version 3.1 It Works! (Somewhat)
            //float lowestFCost = Mathf.Infinity;

            //Node current = new Node();

            //// Dictionary<Vector2Int, Node> openList = new Dictionary<Vector2Int, Node>();
            //List<Node> openList = new List<Node>();
            //List<Vector2> route = new List<Vector2>();

            //current.position = start;
            //current.gCost = 0;
            //current.fCost = Mathf.Abs(current.position.x - goal.x) + (current.position.y - goal.y);

            ////openList.Add(current.position, current);
            //openList.Add(current);

            //for (int x = 0; x < 10000; x++)
            //{
            //    // Get lowest F cost in openList for current
            //    lowestFCost = Mathf.Infinity;

            //    //foreach (var node in openList)
            //    //{
            //    //    if (lowestFCost > node.Value.fCost)
            //    //    {
            //    //        current.position = node.Key; // Get position
            //    //        lowestFCost = node.Value.fCost;
            //    //    }
            //    //}

            //    foreach(Node n in openList)
            //    {
            //        if(lowestFCost > n.fCost)
            //        {
            //            current = n;
            //            lowestFCost = n.fCost;
            //        }
            //    }
            //    // Get lowest F cost in openList for current

            //    if (current.position == goal)
            //    {
            //        Debug.Log("Has reached");
            //        return ReconstructPath(current, route); // return route
            //    }

            //    //openList.Remove(current.position);
            //    openList.Remove(current);

            //    for (int i = 0; i < 4; i++)
            //    {
            //        Node neighbor = new Node();
            //        neighbor.position = current.position;

            //        if (i == 0) neighbor.position += Vector2Int.up;
            //        if (i == 1) neighbor.position += Vector2Int.right;
            //        if (i == 2) neighbor.position += Vector2Int.down;
            //        if (i == 3) neighbor.position += Vector2Int.left;

            //        //Debug.Log(neighbor.position + ": " + (tilemap.GetTile((Vector3Int)neighbor.position) == null));

            //        if (tilemap.GetTile((Vector3Int)neighbor.position) != null)
            //            continue;

            //        if (current.parent != null ? neighbor.position != current.parent.position : true)
            //        {
            //            neighbor.parent = current;
            //            neighbor.gCost = current.gCost + 1;
            //            neighbor.hCost = Mathf.Abs(neighbor.position.x - goal.x) + Mathf.Abs(neighbor.position.y - goal.y);
            //            neighbor.fCost = neighbor.gCost + neighbor.hCost;

            //            //m_Checked.GetComponent<ObjectNode>().getCost(neighbor.hCost, neighbor.gCost, neighbor.fCost);

            //            //Instantiate(m_Checked, (Vector2)neighbor.position, Quaternion.identity);

            //            openList.Add(neighbor);
            //        }
            //    }
            //}
            #endregion

            #region Version 3.2 Memory Optimized
            //Node current = new Node();

            //List<Node> neighborList = new List<Node>();

            //Dictionary<Vector2Int, Node> openList = new Dictionary<Vector2Int, Node>();
            //Dictionary<Vector2Int, Node> map = new Dictionary<Vector2Int, Node>();

            //current.position = start;
            //current.gCost = 0;
            //current.fCost = Mathf.Abs(start.x - goal.x) + Mathf.Abs(start.y - goal.y);

            //map.Add(current.position, current);
            //openList.Add(current.position, current);

            //for(int x = 0; x < 10000; x++) // while(openList.Count > 0)
            //{
            //    // Find lowest FCost in openList
            //    foreach(Node node in openList.Values) // Get first node inside openList
            //    {
            //        current = node;
            //        break;
            //    }

            //    foreach(Node node in openList.Values)
            //    {
            //        if (node.fCost < current.fCost) // Find lowest FCost in openList
            //            current = node;
            //    }
            //    // Find lowest FCost in openList

            //    if (current.position == goal) // If current has reached goal
            //    {
            //        Debug.Log(x);
            //        return ReconstructPath(current); // return route
            //    }

            //    Debug.Log(current.position);

            //    openList.Remove(current.position); // Remove from to be searched list

            //    // Generate Neighbors
            //    neighborList.Clear();

            //    for(int i = 0; i < 4; i++)
            //    {
            //        Node node = new Node();
            //        node.position = current.position;

            //        if (i == 0) node.position += Vector2Int.up;
            //        if (i == 1) node.position += Vector2Int.right;
            //        if (i == 2) node.position += Vector2Int.down;
            //        if (i == 3) node.position += Vector2Int.left;

            //        if (tilemap.GetTile((Vector3Int)node.position) == null)
            //        {
            //            node.gCost = current.gCost + 1; // Overall distance from start to current + distance from current to neighbor. 
            //            neighborList.Add(node);
            //        }
            //    }
            //    // Generate Neighbors

            //    foreach(Node node in neighborList)
            //    {
            //        if (map.ContainsKey(node.position)) // If another node is already in the map
            //        {
            //            if (node.gCost < map[node.position].gCost)
            //            {
            //                node.parent = current;
            //                node.fCost = node.gCost + Mathf.Abs(node.position.x - goal.x) + Mathf.Abs(node.position.y - goal.y);

            //                map[node.position] = node;

            //                if (openList.ContainsKey(node.position))
            //                    openList[node.position] = node;

            //                m_Checked.GetComponent<ObjectNode>().getCost(0, node.gCost, node.fCost);
            //                Instantiate(m_Checked, (Vector2)node.position, Quaternion.identity);
            //            }
            //        }

            //        else
            //        {
            //            node.parent = current;
            //            node.fCost = node.gCost + Mathf.Abs(node.position.x - goal.x) + Mathf.Abs(node.position.y - goal.y);

            //            map.Add(node.position, node);
            //            openList.Add(node.position, node);

            //            m_Checked.GetComponent<ObjectNode>().getCost(0, node.gCost, node.fCost);
            //            Instantiate(m_Checked, (Vector2)node.position, Quaternion.identity);
            //        }
            //    }
            //}
            #endregion
            // Memory Optimized in storing nodes but not yet that optimized appparently
            // More Reliable pathfinding
            #region Version 3.3 Slight Improvement
            Node current = new Node();

            List<Node> neighborList = new List<Node>();

            Dictionary<Vector2Int, Node> openList = new Dictionary<Vector2Int, Node>();
            Dictionary<Vector2Int, Node> map = new Dictionary<Vector2Int, Node>();
                
            current.position = start;
            current.gCost = 0;
            current.fCost = heuristic(start, goal);

            map.Add(current.position, current);
            openList.Add(current.position, current);

            for (int x = 0; x < 10000; x++) // while(openList.Count > 0)
            {
                // Find lowest FCost in openList
                foreach (Node node in openList.Values)
                {
                    current = node;
                    break;
                }

                foreach (Node node in openList.Values)
                {
                    if (node.fCost <= current.fCost)
                        current = node;
                }
                // Find lowest FCost in openList

                if (current.position == goal) // If current has reached goal
                {
                    Debug.Log(x);
                    return ReconstructPath(current); // return route
                }

                openList.Remove(current.position); // Remove from to be searched list

                // Generate Neighbors
                neighborList.Clear();

                for (int i = 0; i < 4; i++)
                {
                    Node node = new Node();
                    node.position = current.position;

                    if (i == 0) node.position += Vector2Int.up;
                    if (i == 1) node.position += Vector2Int.right;
                    if (i == 2) node.position += Vector2Int.down;
                    if (i == 3) node.position += Vector2Int.left;

                    if (tilemap.GetTile((Vector3Int)node.position) == null) // Neighbor Exception
                    {
                        node.gCost = current.gCost + 1; // Overall distance from start to current + distance from current to neighbor. 
                        neighborList.Add(node);
                    }
                }
                // Generate Neighbors

                foreach (Node node in neighborList)
                {
                    if (map.ContainsKey(node.position))
                    {
                        if (node.gCost < map[node.position].gCost)
                        {
                            node.parent = current;
                            node.fCost = node.gCost + heuristic(node.position, goal);

                            if (openList.ContainsKey(node.position))
                                openList[node.position] = node;

                            else
                                openList.Add(node.position, node);

                            map[node.position] = node;
                        }
                    }

                    else
                    {
                        node.parent = current;
                        node.fCost = node.gCost + heuristic(node.position, goal);

                        openList.Add(node.position, node);

                        map.Add(node.position, node);
                    }
                }
            }
            #endregion

            return ReconstructPath(current);
        }

    public List<Vector2> GenerateRoute(Grid2D grid, Vector2 start, Vector2 goal, HeuristicFormula heuristicFormula)
        {
            #region Version 4.1
            Heuristic heuristic = DetermineHeuristicFormula(heuristicFormula);

            Dictionary<GridCell, GridCellNode2D> closedList = new Dictionary<GridCell, GridCellNode2D>();
            Dictionary<GridCell, GridCellNode2D> openList = new Dictionary<GridCell, GridCellNode2D>();

            List<GridCell> neighborsList = new List<GridCell>();

            GridCellNode2D current = new GridCellNode2D();

            GridCell startCell = grid.GetGridCell(start);
            GridCell goalCell = grid.GetGridCell(goal);

            if (startCell == null)
            {
                Debug.LogError("Pathfinding2D Error: Start Position is not in any Grid Cell!");
                return null;
            }

            if (goalCell == null)
            {
                Debug.LogError("Pathfinding2D Error: Goal Position is not in any Grid Cell!");
                return null;
            }

            current.GridCell = startCell;

            openList.Add(current.GridCell, current);

            for (int i = 0; i < 10000; i++)
            {
                if (openList.Count < 1)
                    return null;

                // Find lowest fcost
                current = FindLowestFScore(openList);

                // Remove current from openList and add to closedList
                openList.Remove(current.GridCell);
                closedList.Add(current.GridCell, current);

                // Check if current is the goal
                if (current.GridCell == goalCell)
                {
                    Debug.Log("Path Found! " + i + " iterations!");
                    return ReconstructPath(current);
                }

                foreach (GridCellNode2D neighborNode in GetNeighbors(grid, current, heuristicFormula))//Loop through each neighbor of the current node
                {
                    // Debug
                    grid.MarkCell(neighborNode.GridCell, Color.red - new Color(0, 0, 0, 0.75f));

                    if (!closedList.ContainsKey(neighborNode.GridCell))
                    {
                        neighborNode.FCost = neighborNode.GCost + heuristic(neighborNode.GridCell.WorldPosition, goal);

                        if (!openList.ContainsKey(neighborNode.GridCell))
                            openList.Add(neighborNode.GridCell, neighborNode);

                        else if (neighborNode.GCost < openList[neighborNode.GridCell].GCost)
                        {
                            openList[neighborNode.GridCell].GCost = neighborNode.GCost;
                            openList[neighborNode.GridCell].Parent = neighborNode.Parent;
                        }
                    }
                }
            }
            #endregion

            return null;
        }
    }
        */
    public class Pathfinding2D
    {
        class GridCellNode
        {
            GridCellNode _Parent;
            GridCell _GridCell;
            float _GCost = 0;
            float _FCost = 0;

            public GridCellNode Parent { get => _Parent; set => _Parent = value; }
            public GridCell GridCell { get => _GridCell; set => _GridCell = value; }
            public float GCost { get => _GCost; set => _GCost = value; }
            public float FCost { get => _FCost; set => _FCost = value; }
            public Vector2 Position { get => _GridCell.WorldPosition; }
        }

        delegate float Heuristic(Vector2 current, Vector2 goal);

        #region Heuristic Formula
        static float ManhattanDistance(Vector2 current, Vector2 goal) // Rook Movement
        {
            return Mathf.Abs(current.x - goal.x) + Mathf.Abs(current.y - goal.y);
        }

        static float DiagonalDistance(Vector2 current, Vector2 goal) // King Movement
        {
            return Mathf.Max(Mathf.Abs(current.x - goal.x), Mathf.Abs(current.y - goal.y));
        }

        static float EuclideanDistance(Vector2 current, Vector2 goal) // Any Direction (Almost same as Diagonal Distance but different)
        {
            return Mathf.Sqrt(Mathf.Pow(current.x - goal.x, 2) + Mathf.Pow(current.y - goal.y, 2));
        }
        #endregion

        #region Pathfinding Functions
        /// <summary>
        /// Return heuristic forumula.
        /// </summary>
        /// <param name="heuristicFormula"></param>
        /// <returns></returns>
        static Heuristic DetermineHeuristicFormula(PathfindingHeuristicType2D heuristicFormula)
        {
            Heuristic heuristic = null;
            
            switch (heuristicFormula)
            {
                case PathfindingHeuristicType2D.Manhattan:
                    heuristic = ManhattanDistance;
                    break;
                case PathfindingHeuristicType2D.Diagonal:
                    heuristic = DiagonalDistance;
                    break;
                case PathfindingHeuristicType2D.Euclidean:
                    heuristic = EuclideanDistance;
                    break;
            }

            return heuristic;
        }
        
        /// <summary>
        /// Compiles every GridCellNode's parents and returns the path it took to reach the goal.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        static Route2D ReconstructPath(GridCellNode currentNode)
        {
            float distance = currentNode.GCost;
            List<Vector2> route = new List<Vector2>();

            while (currentNode.Parent != null) // While currentNode's parent is not yet null.
            {
                route.Add(currentNode.GridCell.WorldPosition); // Add curretNode's position to route.
                currentNode = currentNode.Parent; // Move to parent of currentNode
            }

            route.Reverse();

            return new Route2D()
            {
                Distance = distance,
                Path = route
            };
        }

        /// <summary>
        /// Searches for the lowest FCost among the GridCellNodes.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        static GridCellNode FindLowestFCost(Dictionary<GridCell, GridCellNode> dictionary)
        {
            GridCellNode current = null;

            foreach (GridCellNode node in dictionary.Values) // Get any node in the dictionary.
            {
                current = node;
                break;
            }

            foreach (GridCellNode node in dictionary.Values) // Compare each node to find the lowest FCost
            {
                if (node.FCost <= current.FCost) // Apparently <= finds the shortest route faster.
                    current = node;
            }

            return current;
        }

        /// <summary>
        /// Returns the neighbors of the current node and its cell.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="current"></param>
        /// <param name="allowDiagonal"></param>
        /// <returns></returns>
        static List<GridCellNode> GetNeighbors(Grid2D grid, GridCellNode current, bool allowDiagonal)
        {
            List<GridCellNode> neighborNodeList = new List<GridCellNode>();

            GridCellNode neighborNode = null;

            foreach (GridCell cell in grid.GetNeighbors(current.GridCell, allowDiagonal ? GridNeighborType2D.CardinalAndOrdinal : GridNeighborType2D.Cardinal))
            {
                neighborNode = new GridCellNode()
                {
                    GridCell = cell,
                    Parent = current,
                    GCost = current.GCost + Vector2.Distance(current.GridCell.WorldPosition, cell.WorldPosition) // Distance between the current grid cell and the neighbor grid cell.
                };

                neighborNodeList.Add(neighborNode);
            }

            return neighborNodeList;
        }
        #endregion
        
        /// <summary>
        /// Generate shortest route. Return Route2D when route is found, null if not. 
        /// </summary>
        /// <param name="Version">4.1</param>
        /// <returns></returns>
        public static Route2D GenerateRoute(Grid2D grid, Vector2 start, Vector2 goal, PathfindingHeuristicType2D heuristicFormula, bool allowDiagonal = false)
        {
            Heuristic heuristic = DetermineHeuristicFormula(heuristicFormula);

            Dictionary<GridCell, GridCellNode> closedList = new Dictionary<GridCell, GridCellNode>();
            Dictionary<GridCell, GridCellNode> openList = new Dictionary<GridCell, GridCellNode>();

            List<GridCell> neighborsList = new List<GridCell>();

            GridCellNode current = new GridCellNode();

            GridCell startCell = grid.GetGridCell(start);
            GridCell goalCell = grid.GetGridCell(goal);

            if (startCell == null)
            {
                Debug.LogError("Pathfinding2D Error: Start Position is not in any Grid Cell!");
                return null;
            }

            if (goalCell == null)
            {
                Debug.LogError("Pathfinding2D Error: Goal Position is not in any Grid Cell!");
                return null;
            }

            if (startCell.IsObstacle)
            {
                Debug.LogError("Pathfinding2D Error: Start Position is not accessible!");
                return null;
            }

            if (startCell.IsObstacle)
            {
                Debug.LogError("Pathfinding2D Error: Goal Position is not accessible!");
                return null;
            }

            current.GridCell = startCell;

            openList.Add(current.GridCell, current);

            for (int i = 0; i < 10000; i++) // Limit of 10,000 search opportunities
            {
                if (openList.Count < 1) // If there are no more path to go, return null
                    return null;

                // Find lowest fcost
                current = FindLowestFCost(openList);

                // Remove current from openList and add to closedList
                openList.Remove(current.GridCell);
                closedList.Add(current.GridCell, current);

                // Check if current is the goal
                if (current.GridCell == goalCell)
                {
                    //Debug.Log("Path Found! " + i + " iterations!");
                    return ReconstructPath(current);
                }

                foreach (GridCellNode neighborNode in GetNeighbors(grid, current, allowDiagonal))//Loop through each neighbor of the current node
                {
                    // Debug
                    grid.MarkCell(neighborNode.GridCell, Color.red - new Color(0, 0, 0, 0.75f));

                    if (!closedList.ContainsKey(neighborNode.GridCell))
                    {
                        neighborNode.FCost = neighborNode.GCost + heuristic(neighborNode.GridCell.WorldPosition, goal);

                        if (!openList.ContainsKey(neighborNode.GridCell))
                            openList.Add(neighborNode.GridCell, neighborNode);

                        else if (neighborNode.GCost < openList[neighborNode.GridCell].GCost)
                        {
                            openList[neighborNode.GridCell].GCost = neighborNode.GCost;
                            openList[neighborNode.GridCell].Parent = neighborNode.Parent;
                        }
                    }
                }
            }

            return null;
        }
    }
}