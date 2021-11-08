using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{

    public Vector2 moves = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    
    public int startSize = 3;

    private void Start()
    {
        ResetState();
    }

    private void Update() {
        
        if (this.moves.x != 0f){
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                this.moves = Vector2.up;

            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                this.moves = Vector2.down;
            }
        }
        
        else if (this.moves.y != 0f){
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                this.moves = Vector2.right;

            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.moves = Vector2.left;
            }
        }
    }

    private void FixedUpdate() {
                // for moving to tail with head
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        // for moving to right direction and in the our range
        float x = Mathf.Round(this.transform.position.x) + this.moves.x;
        float y = Mathf.Round(this.transform.position.y) + this.moves.y;

        this.transform.position = new Vector2(x, y);
    }

//Increase the snake after eating
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    public void ResetState()
    {
        this.moves = Vector2.right;
        this.transform.position = Vector3.zero;

        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);

        for (int i = 0; i < this.startSize - 1; i++) {
            Grow();
        }
    }


//If we get food we will grow up
//if we get stuck in a wall (or in my tail) we will be dead
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food") {
            Grow();
        } else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
