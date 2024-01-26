using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapBlock : MonoBehaviour
{
    public TextMeshProUGUI blockText;
    public Image blockImage;
    public int id = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setBlock(int idN)
    {
        blockImage.color = GameManager.tileCols[idN];
        blockText.text = GameManager.tileNames[idN];
        id = idN;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
