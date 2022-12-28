using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject chesspieces;

    //위치와 플레이어별 기물 할
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;

    void Start()
    {
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXBoard(), cm.GetZBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int z)
    {
        positions[x, z] = null;
    }

    public GameObject GetPosition(int x, int z)
    {
        return positions[x, z];
    }

    public bool PositionOnBoard(int x, int z)
    {
        if (x < 0 || z < 0 || x > positions.GetLength(0) || z >= positions.GetLength(1)) return false;
            return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool isGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if(currentPlayer == "White")
        {
            currentPlayer = "Black";
        }
        else
        {
            currentPlayer = "White";
        }
    }

    public void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }
}
