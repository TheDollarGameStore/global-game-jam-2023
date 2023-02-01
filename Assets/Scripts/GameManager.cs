using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject darkCanal; // prefab to instantiate for each grid cell
    [SerializeField] private GameObject lightCanal; // prefab to instantiate for each grid cell
    [SerializeField] private int gridSize = 10; // size of the grid, number of cells on each side
    [SerializeField] private float cellSize = 16f; // size of each cell, in units

    [HideInInspector] public GameObject[,] grid;

    [HideInInspector] private Segment[,] segmentsGrid;

    [SerializeField] private GameObject piecePrefab;

    [HideInInspector] public Piece currentPiece;

    public Player player;

    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        GenerateGrid();
        NextPiece();
    }

    void NextPiece()
    {
        currentPiece = Instantiate(piecePrefab, player.transform.position, Quaternion.identity).GetComponent<Piece>();
    }

    public void GenerateGrid()
    {
        grid = new GameObject[gridSize, gridSize];
        segmentsGrid = new Segment[gridSize, gridSize];

        Vector3 startPos = new Vector3(-cellSize * (gridSize - 1) / 2f, cellSize * (gridSize - 1) / 2f, 0f);
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Vector3 cellPosition = startPos + new Vector3(x * cellSize, -y * cellSize, 0f);
                grid[y, x] = Instantiate(x % 2 == 1 ? darkCanal : lightCanal, cellPosition, Quaternion.identity);
            }
        }
    }

    public void PlacePiece()
    {
        int pieceHeight = currentPiece.segments.Count;

        if (!SpaceCheck())
        {
            return;
        }

        ShiftPiecesInColumn(pieceHeight);

        currentPiece.transform.position += (Vector3)Vector2.down * 27f;

        for (int i = 0; i < pieceHeight; i++)
        {
            currentPiece.segments[i].transform.parent = null;
            segmentsGrid[i, player.gridPosX] = currentPiece.segments[i];
        }
        Destroy(currentPiece.gameObject);
        NextPiece();
        RefreshSegmentLinks();
    }

    private bool SpaceCheck()
    {
        int space = gridSize;

        for (int y = gridSize - 1; y >= 0; y--)
        {
            if (segmentsGrid[y, player.gridPosX] != null)
            {
                space = gridSize - (y + 1);
                break;
            }
        }


        return space >= currentPiece.segments.Count;
    }

    private void ShiftPiecesInColumn(int spaceNeeded)
    {
        for (int y = gridSize - 1; y >= 0; y--)
        {
            if (segmentsGrid[y, player.gridPosX] != null)
            {
                segmentsGrid[y + spaceNeeded, player.gridPosX] = segmentsGrid[y, player.gridPosX];
                segmentsGrid[y, player.gridPosX] = null;
            }
        }
    }

    private void Update()
    {
        LerpSegments();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Harvest();
        }
    }

    void LerpSegments()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (segmentsGrid[y, x] != null)
                {
                    segmentsGrid[y, x].transform.position = Vector2.Lerp(segmentsGrid[y, x].transform.position, grid[y, x].transform.position, 15f * Time.deltaTime);
                }
            }
        }
    }

    void RefreshSegmentLinks()
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (segmentsGrid[y, x] != null)
                {
                    //Check Top occupied
                    if (y - 1 >= 0 && segmentsGrid[y - 1, x] != null)
                    {
                        //If bottom occupied too
                        if (y + 1 < gridSize && segmentsGrid[y + 1, x] != null)
                        {
                            segmentsGrid[y, x].SetSegment(1);
                        }
                        else //Else only top is occupied
                        {
                            segmentsGrid[y, x].SetSegment(2);
                        }
                    }
                    else //Top not occupied
                    {
                        //If bottom occupied
                        if (y + 1 < gridSize && segmentsGrid[y + 1, x] != null)
                        {
                            segmentsGrid[y, x].SetSegment(0);
                        }
                        else //Bottom and top open
                        {
                            segmentsGrid[y, x].SetSegment(3);
                        }
                    }
                }
            }
        }
    }

    void Harvest()
    {
        Segment[,] segmentsGridDuplicate = (Segment[,])segmentsGrid.Clone();

        List<List<Segment>> groups = new List<List<Segment>>(); // list of lists of Segment objects

        // Loop through the segments array
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                Segment currentSegment = segmentsGridDuplicate[y, x];

                // Skip this iteration if the current segment has already been processed
                if (currentSegment == null)
                {
                    continue;
                }

                // Search for segments with the same color as the current segment
                List<Segment> group = new List<Segment> { currentSegment };
                SearchForGroup(currentSegment.color, x, y, group, segmentsGridDuplicate);

                // Add the group to the list of groups if it contains 3 or more segments
                if (group.Count >= 3)
                {
                    groups.Add(group);
                }
            }
        }

        if (groups.Count > 0)
        {
            ProcessMatches(groups);
        }
    }

    void ProcessMatches(List<List<Segment>> groups)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            for (int j = 0; j < groups[i].Count; j++)
            {
                groups[i][j].matched = true;
                Vector2 gridPos = GetPosOnGrid(groups[i][j]);
                Destroy(groups[i][j].gameObject, 1f);
                segmentsGrid[(int)gridPos.y, (int)gridPos.x] = null;
            }
        }

        Invoke("CompressGrid", 1f);
    }

    private void SearchForGroup(SegmentColor color, int x, int y, List<Segment> group, Segment[,] segmentsGridDuplicate)
    {
        // Check if the current position is within the bounds of the segments array
        if (x < 0 || x >= gridSize || y < 0 || y >= gridSize)
        {
            return;
        }

        // Get the segment at the current position
        Segment segment = segmentsGridDuplicate[y, x];

        // Skip this iteration if the current segment has already been processed or its color does not match the search color
        if (segment == null || segment.color != color)
        {
            return;
        }

        // Add the current segment to the group and set it to null in the segments array to prevent it from being processed again
        if (!group.Contains(segment))
        {
            group.Add(segment);
        }
        segmentsGridDuplicate[y, x] = null;

        // Search in the up, down, left, and right directions
        SearchForGroup(color, x + 1, y, group, segmentsGridDuplicate);
        SearchForGroup(color, x - 1, y, group, segmentsGridDuplicate);
        SearchForGroup(color, x, y + 1, group, segmentsGridDuplicate);
        SearchForGroup(color, x, y - 1, group, segmentsGridDuplicate);
    }

    Vector2 GetPosOnGrid(Segment segment)
    {
        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                if (segmentsGrid[y, x] == segment)
                {
                    return new Vector2(x, y);
                }
            }
        }

        return new Vector2(-1f, -1f);
    }

    void CompressGrid()
    {
        // Keep looping until there are no more spaces left on the y axis
        bool hasSpace = true;
        while (hasSpace)
        {
            hasSpace = false;

            // Loop through the segmentsGrid array
            for (int y = 1; y < segmentsGrid.GetLength(0); y++)
            {
                for (int x = 0; x < segmentsGrid.GetLength(1); x++)
                {
                    // If the current segment is not null and the one above it is null, shift it up
                    if (segmentsGrid[y, x] != null && segmentsGrid[y - 1, x] == null)
                    {
                        segmentsGrid[y - 1, x] = segmentsGrid[y, x];
                        segmentsGrid[y, x] = null;
                        hasSpace = true;
                    }
                }
            }
        }

        RefreshSegmentLinks();
    }
}
