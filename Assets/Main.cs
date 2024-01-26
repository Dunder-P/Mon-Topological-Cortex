using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
    public GameObject blockPrefab;
    public List<MapBlock> tiles;
    public GameObject theCanvas;
    public List<int> addedTiles;
    private int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        genTiles();
    }

    public void genTiles()
    {
        for (int i = 0; i < GameManager.numTiles; i++)
        {
            tiles.Add(Instantiate(blockPrefab, new Vector3(Random.Range(100, 1820), Random.Range(100, 980), 0), transform.rotation, theCanvas.transform).GetComponent<MapBlock>());
            tiles[i].setBlock(determineTile());
            counter = 0;
            for (int u = 0; u<i;u++)
            {
                counter = 0;
                while (GetWorldSapceRect(tiles[i].blockImage.rectTransform).Overlaps(GetWorldSapceRect(tiles[u].blockImage.rectTransform))&&counter<10)
                {
                    tiles[i].transform.position = new Vector3(Random.Range(100, 1820), Random.Range(100, 980), 0);
                    counter++;
                }
            }
        }
        for (int i = 0; i < GameManager.guarGen.Length; i++)
        {
            if (GameManager.guarGen[i]&&countTiles(i)<1)
            {
                guaranteeTile(i);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void guaranteeTile(int id)
    {
        int replacement = Random.Range(0, tiles.Count);
        if(GameManager.guarGen[tiles[replacement].id]&&countTiles(tiles[replacement].id) ==1)
        {
            counter++;
            if(counter>90)
            {
                return;
            }
            guaranteeTile(id);
            return;
        }
        else
        {
            tiles[replacement].setBlock(id);
        }
    }
    public int determineTile()
    {
        int id = Random.Range(-GameManager.nullWeight, GameManager.tileNames.Length);
        if(id<1)
        {
            return 0;
        }
        int p = countTiles(id);
        if(p>GameManager.limits[id]&&GameManager.limits[id]!= 0)
        {
            return determineTile();
        }
        return id;
    }

    public int countTiles(int id)
    {
        int count = 0;
        for(int i =0;i<tiles.Count;i++)
        {
            if(tiles[i].id == id)
            {
                count++;
            }
        }
        return count;
    }
    /// <summary>
    /// Code by lassade on Unity Forums
    /// </summary>
    /// <param name="rt"></param>
    /// <returns></returns>
    Rect GetWorldSapceRect(RectTransform rt)
    {
        var r = rt.rect;
        r.center = rt.TransformPoint(r.center);
        r.size = rt.TransformVector(r.size);
        return r;
    }
}
