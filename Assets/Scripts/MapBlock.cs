using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapBlock : MonoBehaviour
{
    /// <summary>
    /// Block text goes unused right now, but if you modify this for custom tiles, this will show the tile's name on the tile.
    /// </summary>
    public TextMeshProUGUI blockText, leftText, rightText;
    /// <summary>
    /// The image of the map block
    /// </summary>
    public Image blockImage;
    /// <summary>
    /// The id of the tile this map block represents
    /// </summary>
    public int id = 0;
    /// <summary>
    /// The red and blue color for the arrows
    /// </summary>
    public Color redCol, blueCol;
    /// <summary>
    /// The points to draw the connected lines across
    /// </summary>
    private Vector3[] poindts = new Vector3[2];
    /// <summary>
    /// Whether or not the block has it's line set up yet or not
    /// </summary>
    public bool drawnLine = false;
    private LineRenderer myLine;
    // Start is called before the first frame update
    void Awake()
    {
        myLine = GetComponent<LineRenderer>();
    }
    /// <summary>
    /// Initialize the map block based on an ID.
    /// </summary>
    /// <param name="idN">The ID to initalize the map block as</param>
    public void setBlock(int idN)
    {
        blockImage.sprite = GameManager.tiles[idN];
        blockText.text = GameManager.tileNames[idN];
        id = idN;
    }
    /// <summary>
    /// Draws/Sets Up the connective line for this map block
    /// </summary>
    /// <param name="second">The map block that will be drawn to</param>
    public void drawLine(MapBlock second)
    {
        myLine.positionCount = 2;
        Vector3[] points = { this.transform.position, second.transform.position };
        poindts[0] = this.transform.position;
        poindts[1] = second.transform.position;
        myLine.SetPositions(points);
        drawnLine = true;
    }
    /// <summary>
    /// Draws/Sets Up the connective line for this map block.
    /// Intended for the corner pieces so they can bridge to the center.
    /// there is a small chance for a corner piece to not draw to the center.
    /// </summary>
    /// <param name="second">The first map block that will be drawn to</param>
    /// <param name="third">The second map block that will be drawn to</param>
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
    /// <summary>
    /// Sets the arrows on the map block.
    /// Intended for corner pieces.
    /// </summary>
    /// <param name="right">If the arrow faces right. If false it will face left.</param>
    /// <param name="red">If the arrow is red. If false the error will be blue.</param>
    /// <param name="ouet">If the block is on the left hand side, when true the left text will be set, when false the right text will be set.</param>
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
