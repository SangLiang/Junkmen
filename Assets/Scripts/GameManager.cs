using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour {

    public float turnDelay = 0.1f;
    public float levelStartDelay = 2f;
    public static GameManager instance = null;

    public BoardManager boardScript;



    private Text levelText;
    private GameObject levelImage;
    private bool doingSetup;
    private int level = 1;
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


    private void OnLevelWasLoaded( int index) {
        level++;
        InitGame();
    }

    void InitGame() {
        doingSetup = true;
        levelImage = GameObject.Find("levelImage");
        levelText = GameObject.Find("levelText").GetComponent<Text>();
        
        levelText.text = "Day "+level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage",levelStartDelay);
       

		enemies.Clear();
        boardScript.SetupScene(level);
    }

    private void HideLevelImage() {
        levelImage.SetActive(false);
        doingSetup = false;
    
    }



    public void GameOver() {
        levelText.text = "After"+level+"Days,you dead";
        levelImage.SetActive(true);
        enabled = false;
    }

	// Update is called once per frame
	void Update () {
        if (playerTurn || enemiesMoving||doingSetup)
            return;

        StartCoroutine(MoveEnemies());
        
	}

    public void AddEnemyToList(Enemy script){
        enemies.Add(script);
    }


	IEnumerator MoveEnemies(){
		enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if(enemies.Count == 0){
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < enemies.Count;i++ )
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playerTurn = true;
        enemiesMoving = false;

	}


}
