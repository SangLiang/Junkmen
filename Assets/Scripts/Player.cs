using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Player : MoveObject
{

    public int wallDamage = 1;
    public int pointPerFood = 10;
    public int pointPerSoda = 20;
    public Text foodText;

    public float restartLevelDelay = 1f;
    private Animator animator;

    private int food;

    // Use this for initialization
    protected override void Start()
    {
        //重写MoveObject父类中的Start方法

        foodText.text = "Food " + food;  


        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodInts;
        base.Start();

    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodInts = food;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playerTurn)
        {
            return;
        }

        int horizontal = 0;
        int vertical = 0;


        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0) {
            AttemptMove<Wall>(horizontal, vertical);
        }

    }




    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        foodText.text = "Food " + food;
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        CheckGameOver();
        GameManager.instance.playerTurn = false;
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Exit") {
            Invoke("ReStart",restartLevelDelay);
            enabled = false;
        }else if(col.tag=="Food"){
            food += pointPerFood;
            foodText.text = "+"+pointPerFood+"Food " + food;
            col.gameObject.SetActive(false);
        }else if(col.tag=="Soda"){
            food += pointPerSoda;
            foodText.text = "+"+pointPerSoda+"Food " + food;
            col.gameObject.SetActive(false);
        }

    }



    protected override void OnCantMove<T>(T component) {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerAttack");
    }

    private void ReStart() {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void loseFood(int loss) {
        animator.SetTrigger("playerHurts");
        food -= loss;
        foodText.text = "-" + loss + "Food " + food;
        CheckGameOver();
    }




    private void CheckGameOver()
    {
        if (food <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

}
