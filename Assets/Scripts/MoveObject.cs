using UnityEngine;
using System.Collections;

public abstract class MoveObject : MonoBehaviour {


    public float moveTime = 0.1f;
    public LayerMask blockLayer;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;
    private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        //rb2d = G
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
