using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public static GridSpawner Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [Header("Grid Settings")]
    public GameObject cellPrefab1;      // Prefab to spawn
    public GameObject cellPrefab2;      // Prefab to spawn
    public GameObject cellPrefab3;      // Prefab to spawn
    
    private GameObject cellPrefab;      // Selected prefab

    public Transform gridParent;       // Parent container
    public float cellSpacing = 1.2f;   // Space between cells
    private int gridSize = 2;         // Initial grid size (2x2)
    private int currentLevel;

    void Start()
    {
        currentLevel = UIController.Instance.Level;
        GenerateGrid(currentLevel); // Start with level 1
    }

    private void Update()
    {
        UIController.Instance.LevelText.text = "Level: " + currentLevel;
    }

    public void NextLevel()
    {
        currentLevel++;
        GenerateGrid(currentLevel);
    }

    public void GenerateGrid(int level)
    {
        // Clear old grid
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // Determine grid size
        gridSize = level % 2 == 0 ? ++level : gridSize;

        float offset = (gridSize - 1) * cellSpacing / 2f;

        // Spawn grid
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Pick a random prefab for each cell
                int index = Random.Range(1, 4);
                switch (index)
                {
                    case 1:
                        cellPrefab = cellPrefab1;
                        break;
                    case 2:
                        cellPrefab = cellPrefab2;
                        break;
                    case 3:
                        cellPrefab = cellPrefab3;
                        break;
                }

                Vector3 pos = new Vector3(x * cellSpacing - offset, y * cellSpacing - offset, 0);
                Instantiate(cellPrefab, pos, Quaternion.identity, gridParent);
            }
        }
    }

    public void ResetGrid()
    {
        // Optionally reset the current level or set to 1
        currentLevel = UIController.Instance.Level;

        // Destroy old grid
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // Regenerate grid for the current level
        GenerateGrid(currentLevel);
    }



    public void CheckWin()
    {
        foreach (Transform child in gridParent)
        {
            Cell cell = child.GetComponent<Cell>();
            if (cell != null && !cell.IsOccupied)
            {
                return;
            }
        }

        // All cells are occupied
        Debug.Log("WIN!");
        UIController.Instance.PauseGame();
        NextLevel();
        UIController.Instance.ShowLevelPanel();
    }

}
