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
        clearTiles();
        for (int i = 0; i < GameManager.numTiles; i++)
        {
            tiles.Add(Instantiate(blockPrefab, new Vector3(200*Random.Range(1, 9), 200*Random.Range(1, 5), 0), transform.rotation, theCanvas.transform).GetComponent<MapBlock>());
            tiles[i].setBlock(determineTile());
            counter = 0;
            bool foundEmpty = false;
            while(!foundEmpty)
            {
                counter = 0;
                tiles[i].transform.position = new Vector3(200 * Random.Range(1, 9), 200 * Random.Range(1, 5), 0);
                for (int u = 0; u < i; u++)
                {
                    if (GetWorldSapceRect(tiles[i].blockImage.rectTransform).Overlaps(GetWorldSapceRect(tiles[u].blockImage.rectTransform)))
                    {
                        counter++;
                        u = 10000;
                    }
                }
                if(counter==0)
                {
                    foundEmpty = true;
                }
            }

        }
        for (int i = 0; i < GameManager.guarGen.Length; i++)
        {
            while (countTiles(i)< GameManager.guarNum[i])
            {
                guaranteeTile(i);
            }
        }
        genCorners();
    }
    public void clearTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            Destroy(tiles[i].gameObject);
        }
        tiles = new List<MapBlock>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void guaranteeTile(int id)
    {
        int replacement = Random.Range(0, tiles.Count);
        while(countTiles(tiles[replacement].id) <= GameManager.guarNum[tiles[replacement].id])
        {
            replacement = Random.Range(0, tiles.Count);
        }
        tiles[replacement].setBlock(id);
    }
    public void genCorners()
    {
        int i = tiles.Count;
        tiles.Add(Instantiate(blockPrefab, new Vector3(75, 50, 0), transform.rotation, theCanvas.transform).GetComponent<MapBlock>());
        tiles[i].setBlock(determineTile());
        i++;
        tiles.Add(Instantiate(blockPrefab, new Vector3(75, 1000, 0), transform.rotation, theCanvas.transform).GetComponent<MapBlock>());
        tiles[i].setBlock(determineTile());
        i++;
        tiles.Add(Instantiate(blockPrefab, new Vector3(1850, 1000, 0), transform.rotation, theCanvas.transform).GetComponent<MapBlock>());
        tiles[i].setBlock(determineTile());
        i++;
        tiles.Add(Instantiate(blockPrefab, new Vector3(1850, 50, 0), transform.rotation, theCanvas.transform).GetComponent<MapBlock>());
        tiles[i].setBlock(determineTile());
    }
    public int determineTile()
    {
        int id = Random.Range(-GameManager.nullWeight, GameManager.tileNames.Length);
        if(id<1)
        {
            return 0;
        }
        int p = countTiles(id);
        if(p>=GameManager.limits[id]&&GameManager.limits[id]!= 0)
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
