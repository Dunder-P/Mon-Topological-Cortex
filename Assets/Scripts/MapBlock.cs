using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapBlock : MonoBehaviour
{
    public TextMeshProUGUI blockText, leftText, rightText;
    public Image blockImage;
    public int id = 0;
    public Color redCol, blueCol;
    private Vector3[] poindts = new Vector3[2];
    public bool drawnLine = false;
    private LineRenderer myLine;
    // Start is called before the first frame update
    void Awake()
    {
        myLine = GetComponent<LineRenderer>();
    }
    public void setBlock(int idN)
    {
        blockImage.sprite = GameManager.tiles[idN];
        blockText.text = GameManager.tileNames[idN];
        id = idN;
    }
    public void drawLine(MapBlock second)
    {
        myLine.positionCount = 2;
        Vector3[] points = { this.transform.position, second.transform.position };
        poindts[0] = this.transform.position;
        poindts[1] = second.transform.position;
        myLine.SetPositions(points);
        drawnLine = true;
    }
    public void drawLine(MapBlock second, MapBlock third)
    {
        if(Random.Range(0,5)>=2)//small chance to ignore the third
        {
            drawLine(second);
            return;
        }    
        myLine.positionCount = 3;
        Vector3[] points = { this.transform.position, second.transform.position, third.transform.position };
        poindts[0] = this.transform.position;
        poindts[1] = second.transform.position;
        myLine.SetPositions(points);
        drawnLine = true;
    }
    public void setArrow(bool right, bool red, bool ouet)
    {
        string txt = right ? "->" : "<-";
        if(!ouet)
        {
            rightText.gameObject.SetActive(true);
            rightText.color = red ? redCol : blueCol;
            rightText.text = txt;
        }
        else
        {
            leftText.gameObject.SetActive(true);
            leftText.color = red ? redCol : blueCol;
            leftText.text = txt;
        }


    }
    // Update is called once per frame
    void Update()
    {
        if (drawnLine)
        {
            myLine.SetPositions(poindts);
        }
    }
}
