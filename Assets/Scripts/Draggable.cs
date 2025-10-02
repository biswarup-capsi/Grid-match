using UnityEngine;
//using DG.Tweening; // Optional: For jelly bounce (install DOTween via Package Manager if wanted; fallback to simple scale).

public class Draggable : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 offset;
    RaycastHit2D ray;

    Vector3 initialPos;

    private Collider2D myCollider;
    private SpriteRenderer sp;
    private int defLayer;

    void Start()
    {
        initialPos = transform.position;
        myCollider = GetComponent<Collider2D>();
        sp = GetComponent<SpriteRenderer>();
        defLayer = sp.sortingOrder;
    }

    private void OnMouseDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePos;
        myCollider.enabled = false;
        sp.sortingOrder = 50; // Bring to front while dragging
    }

    private void OnMouseDrag()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x + offset.x, mousePos.y + offset.y, 0);
    }

    private void OnMouseUp()
    {
        ray = Physics2D.Raycast(transform.position, Vector3.zero);

        if (ray)
        {
            if (ray.transform.GetComponent<Collider2D>())
            {
                Transform obj = ray.transform.gameObject.transform;
                //Debug.Log("Dropped: " + ray.transform.gameObject.name);
                var cell = ray.transform.GetComponent<Collider2D>();
                Debug.Log("Dropped: " + cell.gameObject.name);
          
                UIController.Instance.AddScore(1);


                GameObject originalToClone = gameObject;
                GameObject clone = Instantiate(originalToClone,obj);
                clone.transform.localPosition = Vector3.zero;

                obj.GetComponent<Collider2D>().enabled = false; 

                clone.transform.localScale = Vector3.one * 0.8f;

                transform.position = initialPos;
                sp.sortingOrder = defLayer;
                myCollider.enabled = true;
            }
        }
        else
        {
            Debug.Log("No cell found");
            transform.position = initialPos;
            sp.sortingOrder = defLayer;
            myCollider.enabled = true;
        }
    }

}