using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YergoScripts.Grid
{
    /// <summary>
    /// Cardinal - North, South, East, West.
    /// CardianAndOrdinal - North South, East, West, Northeast, Northwest, ...
    /// </summary>
    public enum GridNeighborType2D
    {
        Cardinal, CardinalAndOrdinal
    }

    public class Grid2D : MonoBehaviour
    {
        [SerializeField] Vector2 m_GridWorldSize;       // Grid Size in World Size
        [SerializeField] float m_CellDiameter = 0;      // Size of each Cell

        [SerializeField] LayerMask m_ObstacleMask;      // Layer for obstacles

        float m_CellRadius = 0;                         // Radius size of each cell


        GridCell[,] m_CellArray;                      // Data container for each Cell

        Vector2Int m_GridSize;                          // Grid size in Array Units

        Dictionary<GridCell, MarkedCell> m_MarkedCellList = new Dictionary<GridCell, MarkedCell>();

        class MarkedCell
        {
            public GridCell gridCell;
            public Color color;
        }

        void Awake()
        {
            m_CellRadius = m_CellDiameter / 2;

            m_GridSize = Vector2Int.RoundToInt(m_GridWorldSize / m_CellDiameter);

            GenerateGrid();
        }

        void GenerateGrid()
        {
            m_CellArray = new GridCell[m_GridSize.x, m_GridSize.y];

            GridCell cell;

            Vector2 bottomLeft = (Vector2)transform.position - m_GridWorldSize / 2;
            Vector2 worldPosition;

            bool isObstacle = false;

            for (int x = 0; x < m_GridSize.x; x++)
            {
                for (int y = 0; y < m_GridSize.y; y++)
                {
                    worldPosition = bottomLeft + new Vector2(x * m_CellDiameter, y * m_CellDiameter);

                    isObstacle = Physics2D.OverlapBox(worldPosition, Vector2.one * (m_CellDiameter - 0.05f), 0, m_ObstacleMask);

                    cell = new GridCell(worldPosition, new Vector3Int(x, y, 0), Vector2.one * m_CellDiameter, isObstacle);

                    m_CellArray[x, y] = cell;
                }
            }
        }

        public GridCell GetGridCell(Vector2 worldPosition)
        {
            if (m_CellArray == null)
                return null;

            foreach (GridCell cell in m_CellArray)
            {
                if (cell.Contains(worldPosition))
                    return cell;
            }

            return null;
        }

        public List<GridCell> GetNeighbors(Vector2 current, GridNeighborType2D neighborDirection)
        {
            GridCell currentCell = null;

            List<GridCell> neighbors = new List<GridCell>();

            Vector2Int gridPosition;

            if (m_CellArray == null)
                return null;

            foreach (GridCell cell in m_CellArray)
            {
                if (cell.Contains(current))
                {
                    currentCell = cell;
                    break;
                }
            }

            if (currentCell == null)
                return null;

            switch (neighborDirection)
            {
                case GridNeighborType2D.Cardinal:
                    for (int i = 0; i < 4; i++)
                    {
                        gridPosition = (Vector2Int)currentCell.GridPosition + Vector2Int.RoundToInt(MathY.DegreeToVector2(90 * i));

                        if (gridPosition.x < 0 || gridPosition.x > m_GridSize.x || gridPosition.y < 0 || gridPosition.y > m_GridSize.y)
                            continue;

                        if (!m_CellArray[gridPosition.x, gridPosition.y].IsObstacle)
                            neighbors.Add(m_CellArray[gridPosition.x, gridPosition.y]);
                    }
                    break;

                case GridNeighborType2D.CardinalAndOrdinal:
                    for (int i = 0; i < 8; i++)
                    {
                        gridPosition = (Vector2Int)currentCell.GridPosition + Vector2Int.RoundToInt(MathY.DegreeToVector2(45 * i));

                        if (gridPosition.x < 0 || gridPosition.x > m_GridSize.x || gridPosition.y < 0 || gridPosition.y > m_GridSize.y)
                            continue;

                        if (!m_CellArray[gridPosition.x, gridPosition.y].IsObstacle)
                            neighbors.Add(m_CellArray[gridPosition.x, gridPosition.y]);
                    }
                    break;
            }

            return neighbors;
        }

        public List<GridCell> GetNeighbors(GridCell current, GridNeighborType2D neighborDirection)
        {
            GridCell currentCell = null;
            List<GridCell> neighbors = new List<GridCell>();

            Vector2Int gridPosition;

            if (current == null)
            {
                Debug.LogError("Grid2D: GetNeighbors' current is null!");
                return null;
            }

            if (m_CellArray == null)
                return null;

            foreach (GridCell cell in m_CellArray)
            {
                if (cell == current)
                {
                    currentCell = cell;
                    break;
                }
            }

            if (currentCell == null)
                return null;

            switch (neighborDirection)
            {
                case GridNeighborType2D.Cardinal:
                    for (int i = 0; i < 4; i++)
                    {
                        gridPosition = (Vector2Int)currentCell.GridPosition + Vector2Int.RoundToInt(MathY.DegreeToVector2(90 * i));

                        if (gridPosition.x < 0 || gridPosition.x >= m_GridSize.x || gridPosition.y < 0 || gridPosition.y >= m_GridSize.y)
                            continue;

                        if (!m_CellArray[gridPosition.x, gridPosition.y].IsObstacle)
                            neighbors.Add(m_CellArray[gridPosition.x, gridPosition.y]);
                    }
                    break;

                case GridNeighborType2D.CardinalAndOrdinal:
                    for (int i = 0; i < 8; i++)
                    {
                        gridPosition = (Vector2Int)currentCell.GridPosition + Vector2Int.RoundToInt(MathY.DegreeToVector2(45 * i));

                        if (gridPosition.x < 0 || gridPosition.x >= m_GridSize.x || gridPosition.y < 0 || gridPosition.y >= m_GridSize.y)
                            continue;

                        if (!m_CellArray[gridPosition.x, gridPosition.y].IsObstacle)
                            neighbors.Add(m_CellArray[gridPosition.x, gridPosition.y]);
                    }
                    break;
            }

            return neighbors;
        }

        public void MarkCell(Vector2 current, Color cellColor)
        {
            foreach (GridCell cell in m_CellArray)
            {
                if (cell.Contains(current))
                {
                    if (m_MarkedCellList.ContainsKey(cell))
                        m_MarkedCellList[cell].color = cellColor;

                    else
                    {
                        m_MarkedCellList.Add(cell, new MarkedCell()
                        {
                            gridCell = cell,
                            color = cellColor
                        });
                    }
                    break;
                }
            }
        }

        public void MarkCell(List<Vector2> current, Color cellColor)
        {
            if (current == null)
            {
                Debug.LogError("Grid2D: MarkCell's current is null!");
                return;
            }

            foreach (Vector2 pos in current)
            {
                foreach (GridCell cell in m_CellArray)
                {
                    if (cell.Contains(pos))
                    {
                        if (m_MarkedCellList.ContainsKey(cell))
                            m_MarkedCellList[cell].color = cellColor;

                        else
                        {
                            m_MarkedCellList.Add(cell, new MarkedCell()
                            {
                                gridCell = cell,
                                color = cellColor
                            });
                        }
                        break;
                    }
                }
            }
        }

        public void MarkCell(GridCell current, Color cellColor)
        {
            foreach (GridCell cell in m_CellArray)
            {
                if (cell == current)
                {
                    if (m_MarkedCellList.ContainsKey(cell))
                        m_MarkedCellList[cell].color = cellColor;

                    else
                    {
                        m_MarkedCellList.Add(cell, new MarkedCell()
                        {
                            gridCell = cell,
                            color = cellColor
                        });
                    }
                    break;
                }
            }
        }

        public void MarkCell(List<GridCell> current, Color cellColor)
        {
            foreach (GridCell checkCell in current)
            {
                foreach (GridCell cell in m_CellArray)
                {
                    if (cell == checkCell)
                    {
                        if (m_MarkedCellList.ContainsKey(cell))
                            m_MarkedCellList[cell].color = cellColor;

                        else
                        {
                            m_MarkedCellList.Add(cell, new MarkedCell()
                            {
                                gridCell = cell,
                                color = cellColor
                            });
                        }
                        break;
                    }
                }
            }
        }

        public void RemoveAllMarkedCells()
        {
            m_MarkedCellList.Clear();
        }

        void OnDrawGizmos()
        {
            m_CellRadius = m_CellDiameter / 2;

            Gizmos.color = Color.black;
            Gizmos.DrawWireCube((Vector2)transform.position - Vector2.one * m_CellRadius, m_GridWorldSize);
            if (m_CellArray != null)
            {
                foreach (GridCell cell in m_CellArray)
                {
                    if (cell.IsObstacle)
                    {
                        Gizmos.color = Color.yellow + new Color(0, 0, 0, -0.75f);
                        Gizmos.DrawCube(cell.WorldPosition, cell.Size);
                    }
                }

                foreach (MarkedCell markedCell in m_MarkedCellList.Values)
                {
                    Gizmos.color = markedCell.color;
                    Gizmos.DrawCube(markedCell.gridCell.WorldPosition, Vector2.one * m_CellDiameter);
                }
            }

            else
                m_GridSize = Vector2Int.RoundToInt(m_GridWorldSize / m_CellDiameter);

            Vector2 bottomLeft = (Vector2)transform.position - m_GridWorldSize / 2 - Vector2.one * m_CellRadius;
            Vector2 to;
            Vector2 from;

            Gizmos.color = Color.black;

            for (int x = 0; x <= m_GridSize.x; x++)
            {
                from = bottomLeft + new Vector2(x * m_CellDiameter, 0);
                to = bottomLeft + new Vector2(x * m_CellDiameter, m_GridWorldSize.y);

                Gizmos.DrawLine(from, to);
            }

            for (int y = 0; y <= m_GridSize.y; y++)
            {
                from = bottomLeft + new Vector2(0, y * m_CellDiameter);
                to = bottomLeft + new Vector2(m_GridWorldSize.x, y * m_CellDiameter);

                Gizmos.DrawLine(from, to);
            }
        }
    }
}