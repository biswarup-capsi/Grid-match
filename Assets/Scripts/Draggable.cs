using UnityEngine;
//using DG.Tweening; // Optional: For jelly bounce (install DOTween via Package Manager if wanted; fallback to simple scale).

public class Draggable : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 offset;
    RaycastHit2D ray;

    Vector3 initialPos;

    private Collider2D myCollider;

    void Start()
    {
        initialPos = transform.position;
        myCollider = GetComponent<Collider2D>();
    }

    private void OnMouseDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
        if (myCollider != null) myCollider.enabled = false;
    }

    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, 0);
    }

    private void OnMouseUp()
    {
        if (myCollider != null) myCollider.enabled = true;

        ray = Physics2D.Raycast(transform.position, Vector3.zero);


        if (ray)
        {
            if (ray.transform.GetComponent<Collider2D>())
            {
                //Debug.Log("Dropped: " + ray.transform.gameObject.name);
                var cell = ray.transform.GetComponent<Collider2D>();
                Debug.Log("Dropped: " + cell.gameObject.name);

                UIController.Instance.AddScore(1);


                GameObject originalToClone = gameObject;
                GameObject clone = Instantiate(originalToClone, cell.transform.position, Quaternion.identity);

                clone.transform.localScale = Vector3.one * 0.8f;

                transform.position = initialPos;
            }
        }
        else
        {
            Debug.Log("No cell found");
            transform.position = initialPos;
        }
    }

}