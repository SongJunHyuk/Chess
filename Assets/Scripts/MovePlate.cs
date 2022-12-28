using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //보드에서의 위치 
    int matrixX;
    int matrixZ;

    //false면 단순한 기물이동 true면 공격 
    public bool attack = false;

    public void Start()
    {
        if(attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixZ);

            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), reference.GetComponent<Chessman>().GetZBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetXBoard(matrixZ);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        reference.GetComponent<Chessman>().DestroyMovePlates();

    }

    public void SetCoords(int x, int z)
    {
        matrixX = x;
        matrixZ = z;
    }

    public void SetRefenece(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    //public void SetXBoard(int x)
    //{
    //    xBoard = x;
    //}
}
