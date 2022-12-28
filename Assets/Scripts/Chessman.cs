using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    //참조 
    public GameObject controller;
    public GameObject movePlate;

    //위치
    private int xBoard = -1;
    private int yBoard = -1;
    private int zBoard = -1;

    private string player;

    public Sprite Black_Queen, Black_Knight, Black_bishop, Black_King, Black_Rook, Black_Pawn;
    public Sprite White_Queen, White_Knight, White_bishop, White_King, White_Rook, White_Pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch(this.name)
        {
            case "Black_Queen": this.GetComponent<SpriteRenderer>().sprite = Black_Queen;break;
            case "Black_Knight": this.GetComponent<SpriteRenderer>().sprite = Black_Knight; break;
            case "Black_bishop": this.GetComponent<SpriteRenderer>().sprite = Black_bishop; break;
            case "Black_King": this.GetComponent<SpriteRenderer>().sprite = Black_King; break;
            case "Black_Rook": this.GetComponent<SpriteRenderer>().sprite = Black_Rook; break;
            case "Black_Pawn": this.GetComponent<SpriteRenderer>().sprite = Black_Pawn; break;

            case "White_Queen": this.GetComponent<SpriteRenderer>().sprite = White_Queen; break;
            case "White_Knight": this.GetComponent<SpriteRenderer>().sprite = White_Knight; break;
            case "White_bishop": this.GetComponent<SpriteRenderer>().sprite = White_bishop; break;
            case "White_King": this.GetComponent<SpriteRenderer>().sprite = White_King; break;
            case "White_Rook": this.GetComponent<SpriteRenderer>().sprite = White_Rook; break;
            case "White_Pawn": this.GetComponent<SpriteRenderer>().sprite = White_Pawn; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;
        float z = zBoard;

        x *= 0.66f;
        y *= 0.66f;
        z *= 0.66f;

        x += -2.3f;
        y += -2.3f;
        z += 0.66f;

        this.transform.position = new Vector3(x, y, z);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public int GetZBoard()
    {
        return zBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    public void SetZBoard(int z)
    {
        zBoard = z;
    }

    private void OnMouseUp()
    {
        if(!controller.GetComponent<Game>().isGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();
            InitiateMovePlates();
        }
        
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for(int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    //기물이
    public void InitiateMovePlates()
    {
        switch(this.name)
        {
            case "Black_Queen":
            case "White_QUeen":
                LineMovePlan(1, 0);
                LineMovePlan(0, 1);
                LineMovePlan(1, 1);
                LineMovePlan(-1, 0);
                LineMovePlan(0, 1);
                LineMovePlan(-1, -1);
                LineMovePlan(-1, 1);
                LineMovePlan(1, -1);
                break;
            case "Black_Knight":
            case "White_Knight":
                LMovePlate();//나이트 이동 
                break;

            case "Black_Bishop":
            case "White_Bishop":
                LineMovePlan(1, 1);
                LineMovePlan(1, -1);
                LineMovePlan(-1, 1);
                LineMovePlan(-1, -1);
                break;
            case "Black_King":
            case "White_King":
                SurroundMovePlate();
                break;
            case "Black_Rook":
            case "White_Rook":
                LineMovePlan(1, 0);
                LineMovePlan(0, 1);
                LineMovePlan(-1, 0);
                LineMovePlan(0, -1);
                break;
            case "Black_Pawn":
                PawnMovePlate(xBoard, zBoard - 1);
                break;
            case "White_Pawn":
                PawnMovePlate(xBoard, zBoard + 1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int zIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int z = zBoard + zIncrement;

        while(sc.PositionOnBoard(x, z) && sc.GetPosition(x, z) == null)
        {
            MovePlateSpawn(x, z);
            x += xIncrement;
            z += zIncrement;
        }

        if(sc.PositionOnBoard(x, z) && sc.GetPosition(x, z).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, z);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, zBoard + 2);
        PointMovePlate(xBoard - 1, zBoard + 2);
        PointMovePlate(xBoard + 2, zBoard + 1);
        PointMovePlate(xBoard - 2, zBoard - 1);
        PointMovePlate(xBoard + 1, zBoard - 2);
        PointMovePlate(xBoard - 1, zBoard - 2);
        PointMovePlate(xBoard - 2, zBoard + 1);
        PointMovePlate(xBoard - 2, zBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, zBoard + 1);
        PointMovePlate(xBoard, zBoard - 1);
        PointMovePlate(xBoard - 1, zBoard - 1);
        PointMovePlate(xBoard - 1, zBoard - 0);
        PointMovePlate(xBoard - 1, zBoard + 1);
        PointMovePlate(xBoard + 1, zBoard - 1);
        PointMovePlate(xBoard + 1, zBoard - 0);
        PointMovePlate(xBoard + 1, zBoard + 1);
    }

    public void PointMovePlate(int x, int z)
    {
        Game sc = controller.GetComponent<Game>();
        if(sc.PositionOnBoard(x, z))
        {
            GameObject cp = sc.GetPosition(x, z);
            if(cp == null)
            {
                MovePlateSpawn(x, z);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, z);
            }
        }
    }

    public void PawnMovePlate(int x, int z)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, z))
        {
            if (sc.GetPosition(x, z) == null)
            {
                MovePlateSpawn(x, z);
            }

            if(sc.PositionOnBoard(x + 1, z) && sc.GetPosition(x + 1, z) != null &&
                sc.GetPosition(x + 1, z).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, z);
            }

            if (sc.PositionOnBoard(x - 1, z) && sc.GetPosition(x - 1, z) != null &&
                sc.GetPosition(x - 1, z).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, z);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixZ)
    {
        float x = matrixX;
        float z = matrixZ;

        x *= 0.66f;
        z *= 0.66f;

        x += -2.3f;
        z += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, z, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetRefenece(gameObject);
        mpScript.SetCoords(matrixX, matrixZ);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixZ, bool isAttack = false)
    {
        float x = matrixX;
        float z = matrixZ;

        x *= 0.66f;
        z *= 0.66f;

        x += -2.3f;
        z += -2.3f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, z, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetRefenece(gameObject);
        mpScript.SetCoords(matrixX, matrixZ);
    }
    //일자로 이동 
    public void LineMovePlan(int x, int z)
    {

    }
}
