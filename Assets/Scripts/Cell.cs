using UnityEngine;

public class Cell : MonoBehaviour
{
    private bool isOccupied = false;   // Internal flag

    public bool IsOccupied => isOccupied; // Read-only accessor

    // Called when a draggable piece is placed here
    public void OccupyCell(GameObject placedObject)
    {
        if (isOccupied) return;

        if (!isOccupied && placedObject.name.Contains(gameObject.name.Replace("Cell_", "")))
        {
            isOccupied = true;
            GetComponent<Collider2D>().enabled = false;
            GridSpawner.Instance.CheckWin();
        }
        else
        {
            UIController.Instance.GameOver();
        }
    }
}
