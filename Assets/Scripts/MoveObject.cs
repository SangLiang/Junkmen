using UnityEngine;
using System.Collections;

public abstract class MoveObject : MonoBehaviour
{


    public float moveTime = 0.1f;
    public LayerMask blockLayer;


    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;
    private float inverseMoveTime;

   
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1 / moveTime;
    }


    protected bool Move( int xDir,int yDir, out RaycastHit2D hit) {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir,yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockLayer);
        boxCollider.enabled = true;


        if(hit.transform==null){
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }






    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqlRemainDistance = (transform.position - end).sqrMagnitude;//取两者差值长度的平方
        while (sqlRemainDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.deltaTime);
            rb2d.MovePosition(newPosition);
            sqlRemainDistance = (transform.position - end).sqrMagnitude;
            yield return null;

        }

    }



    protected virtual void AttemptMove<T>(int xDir, int yDir) 
        where T:Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        if(hit.transform==null)
            return;
       
        T hitComponent = hit.transform.GetComponent<T>();

        if(!canMove&&hitComponent!=null){
            OnCantMove(hitComponent);
        
        }
    
    }

    protected abstract void OnCantMove<T>(T component)//抽象类
        where T : Component;

}
