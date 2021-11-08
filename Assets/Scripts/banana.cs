using UnityEngine;

public class banana : MonoBehaviour
{
    public Collider2D gridArea;
//food for my snake
    private void Start()
    {
        RandomPosition();
    }


    public void RandomPosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }
//change the banana position when I "Collect it"
    private void OnTriggerEnter2D(Collider2D other)
    {
        RandomPosition();
    }

}
