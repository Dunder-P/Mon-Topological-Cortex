using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapBlock : MonoBehaviour
{
    public TextMeshProUGUI blockText;
    public Image blockImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setBlock(int id)
    {
        blockImage.color = GameManager.tileCols[id];
        blockText.text = GameManager.tileNames[id];
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
