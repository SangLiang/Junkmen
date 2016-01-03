using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public float turnDelay = 0.1f;

    public static GameManager instance = null;

    public BoardManager boardScript;
    private int level = 3;
    private List<Enemy> enemies;
    private bool enemiesMoving;


    public int playerFoodInts = 100;
    [HideInInspector]public bool playerTurn = true;//在使用[HideInInspector]时，即使是publc，也不会显示到面板上

    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance!=this){
            Destroy(gameObject);
        }


		enemies = new List<Enemy>();
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();


    }

    void InitGame() {
		enemies.Clear();
        boardScript.SetupScene(level);
    }


    public void GameOver() {
        enabled = false;
    }

	// Update is called once per frame
	void Update () {
	
	}


	IEnumerator MoveEnemies(){
		enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if(enemies.Count == 0){
            yield return new WaitForSeconds(turnDelay);
        }
	}


}
