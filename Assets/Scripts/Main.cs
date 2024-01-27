using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Main : MonoBehaviour
{
    /// <summary>
    /// The prefab to clone for all map blocks.
    /// </summary>
    public GameObject blockPrefab;
    /// <summary>
    /// A list of all created tiles
    /// </summary>
    public List<MapBlock> tiles;
    /// <summary>
    /// The canvas to add tiles to
    /// </summary>
    public GameObject theCanvas;
    /// <summary>
    /// Private counter used by various methods to prevent infinite loops
    /// </summary>
    private int counter = 0;

    /// <summary>
    /// The Gameobject containing the settings menu
    /// </summary>
    public GameObject settingsMenu;
    /// <summary>
    /// The image for the editing Tile/render Image
    /// </summary>
    public Image editingTileImage, renderiMage;
    public TMP_InputField guarGen1, minGen1, nullWeight1, tileSize1;
    /// <summary>
    /// The current tile being edited.
    /// </summary>
    private int currEdit = 1;
    // Start is called before the first frame update
    void Start()
    {
        openCloseSettings(false);
        Screen.SetResolution(1920, 1080, false);
        genTiles();
    }
    /// <summary>
    /// Generates all tiles based off the info in Game Manager.
    /// </summary>
    public void genTiles()
    {
        #region generation
        clearTiles();
        for (int i = 0; i < GameManager.numTiles; i++)
        {
            tiles.Add(Instantiate(blockPrefab, new Vector3(200 * UnityEngine.Random.Range(1, 9), 200 * UnityEngine.Random.Range(1, 5), 0), transform.rotation, theCanvas.transform).GetComponent<MapBlock>());
            tiles[i].setBlock(determineTile());
            counter = 0;
            bool foundEmpty = false;
            while (!foundEmpty)
            {
                counter = 0;
                tiles[i].transform.position = new Vector3(200 * UnityEngine.Random.Range(1, 9), 200 * UnityEngine.Random.Range(1, 5), 0);
                for (int u = 0; u < i; u++)
                {
                    if (GetWorldSapceRect(tiles[i].blockImage.rectTransform).Overlaps(GetWorldSapceRect(tiles[u].blockImage.rectTransform)))
                    {
                        counter++;
                        u = 10000;
                    }
                }
                if (counter == 0)
                {
                    foundEmpty = true;
                }
            }

        }
        for (int i = 0; i < GameManager.guarGen.Length; i++)
        {
            while (countTiles(i) < GameManager.guarNum[i])
            {
                guaranteeTile(i);
            }
        }
        genCorners();
        #endregion
        #region line gen
        for (int i = 0; i < tiles.Count-4; i++)
        {
            int kill = 0;
            GameObject q = Instantiate(blockPrefab, tiles[i].transform.position, transform.rotation, theCanvas.transform);
            float xmod = 200 * UnityEngine.Random.Range(-1, 2);
            float ymod = 200 * UnityEngine.Random.Range(-1, 2);
            q.transform.position = q.transform.position + new Vector3(xmod, ymod);
            while (!checkOverlap(q.GetComponent<MapBlock>()))
            {
                if((q.transform.position.x < 1920 && q.transform.position.x > 0 && q.transform.position.y < 1080 && q.transform.position.y > 0))
                {
                    q.transform.position = q.transform.position + new Vector3(xmod, ymod);
                }
                else
                {
                    if (kill>1000)
                    {
                        Destroy(q);
                        int r = UnityEngine.Random.Range(0, tiles.Count);
                        while(r==i)
                        {
                            r = UnityEngine.Random.Range(0, tiles.Count);
                        }
                        tiles[i].drawLine(tiles[r]);
                        break;
                    }
                    xmod = 200 * UnityEngine.Random.Range(-1, 2);
                    ymod = 200 * UnityEngine.Random.Range(-1, 2);
                    q.transform.position = tiles[i].transform.position + new Vector3(xmod, ymod);
                }
                if(checkOverlap(q.GetComponent<MapBlock>())&&checkOverlap(tiles[i]))
                {
                    xmod = 200 * UnityEngine.Random.Range(-1, 2);
                    ymod = 200 * UnityEngine.Random.Range(-1, 2);
                    q.transform.position = tiles[i].transform.position + new Vector3(xmod, ymod);
                }
            }
            if ((checkOverlap(q.GetComponent<MapBlock>(), true) == tiles[i])||q.transform.position.y<=0)
            {
                int r = UnityEngine.Random.Range(0, tiles.Count);
                while (r == i)
                {
                    r = UnityEngine.Random.Range(0, tiles.Count);
                }
                tiles[i].drawLine(tiles[r]);
            }
            else if (checkOverlap(q.GetComponent<MapBlock>()))
            {
                tiles[i].drawLine(q.GetComponent<MapBlock>());
            }
            Destroy(q);

        }
        int ra = UnityEngine.Random.Range(0, tiles.Count);
        while (ra >= tiles.Count - 4)
        {
            ra = UnityEngine.Random.Range(0, tiles.Count);
        }
        tiles[tiles.Count - 4].drawLine(tiles[tiles.Count - 3], tiles[ra]);
        tiles[tiles.Count - 3].drawLine(tiles[tiles.Count - 2], tiles[ra]);
        tiles[tiles.Count - 2].drawLine(tiles[tiles.Count - 1], tiles[ra]);

        tiles[tiles.Count - 1].drawLine(tiles[tiles.Count - 4], tiles[ra]);
        #endregion
        #region loop
        setLoop();
        #endregion
        string peath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ("MonOtoTopologicalCortext/" + UnityEngine.Random.seed+UnityEngine.Random.Range(0, 1000000) + ".png"));
        if(!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "MonOtoTopologicalCortext/")))
         {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "MonOtoTopologicalCortext/"));
        }
        ScreenCapture.CaptureScreenshot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ("MonOtoTopologicalCortext/" + UnityEngine.Random.seed + UnityEngine.Random.Range(0,1000000) + ".png")));
        if (!Application.isEditor)
        {
            Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length].width, Screen.resolutions[Screen.resolutions.Length].height, false);
        }

    }
    /// <summary>
    /// Clears all tiles on the map
    /// </summary>
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
    /// <summary>
    /// Sets up the arrows on the corners of the map.
    /// </summary>
    public void setLoop()
    {
        int i = UnityEngine.Random.Range(0, 4);
        bool startRed = i < 2 ? true : false;
        if(i%2==0)
        {
            tiles[tiles.Count - 4].setArrow(false, startRed, true);
           tiles[tiles.Count - 3].setArrow(true, !startRed, true);
        }
        else
        {
            tiles[tiles.Count - 4].setArrow(true, startRed, true);
            tiles[tiles.Count - 3].setArrow(false, !startRed, true);
        }
        int ni = UnityEngine.Random.Range(0, 4);
        startRed = ni < 2 ? true : false;
        if (i % 2 == 0)
        {
            tiles[tiles.Count - 2].setArrow(true, startRed, false);
            tiles[tiles.Count - 1].setArrow(false, !startRed, false);
        }
        else
        {
            tiles[tiles.Count - 2].setArrow(false, startRed, false);
            tiles[tiles.Count - 1].setArrow(true, !startRed, false);
        }
    }
    /// <summary>
    /// Generates a specific tile over an existing one.
    /// Will not generate over another guaranteed tile.
    /// </summary>
    /// <param name="id">The id of the tile to generate</param>
    public void guaranteeTile(int id)
    {
        int replacement = UnityEngine.Random.Range(0, tiles.Count);
        while(countTiles(tiles[replacement].id) <= GameManager.guarNum[tiles[replacement].id])
        {
            replacement = UnityEngine.Random.Range(0, tiles.Count);
        }
        tiles[replacement].setBlock(id);
    }
    /// <summary>
    /// Generates the 4 corner tiles
    /// </summary>
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
    /// <summary>
    /// Determines what tile will be generated.
    /// Rerolls if a tile chosen is over the limit.
    /// </summary>
    /// <returns>The id of a valid tile</returns>
    public int determineTile()
    {
        int id = UnityEngine.Random.Range(-GameManager.nullWeight, GameManager.tileNames.Length);
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

    /// <summary>
    /// Counts the number of a specific tile given an id
    /// </summary>
    /// <param name="id">ID of the tile to count</param>
    /// <returns>The number of tiles with the input ID.</returns>
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
    /// Code by lassade on Unity Forums.
    /// Takes in a rect and returns that rect in worldspace
    /// </summary>
    /// <param name="rt">Rect to get in world space</param>
    /// <returns>The rect in world space</returns>
    Rect GetWorldSapceRect(RectTransform rt)
    {
        var r = rt.rect;
        r.center = rt.TransformPoint(r.center);
        r.size = rt.TransformVector(r.size);
        return r;
    }
    /// <summary>
    /// Checks if a tile overlaps another tile.
    /// </summary>
    /// <param name="r">The mapblock to check</param>
    /// <returns>If there is an overlap</returns>
    public bool checkOverlap(MapBlock r)
    {
        for (int u = 0; u < tiles.Count; u++)
        {
            if (GetWorldSapceRect(r.blockImage.rectTransform).Overlaps(GetWorldSapceRect(tiles[u].blockImage.rectTransform)))
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Checks if a tile overlaps another tile and then returns that tile.
    /// Returns the input if no overlap
    /// </summary>
    /// <param name="r">the map block to check</param>
    /// <param name="returnM">Does nothing</param>
    /// <returns>The overlapping map block</returns>
    public MapBlock checkOverlap(MapBlock r, bool returnM)
    {
        for (int u = 0; u < tiles.Count; u++)
        {
            if (GetWorldSapceRect(r.blockImage.rectTransform).Overlaps(GetWorldSapceRect(tiles[u].blockImage.rectTransform)))
            {
                return tiles[u];
            }
        }
        return r;
    }

    /// <summary>
    /// Loads a tile to be edited in settings
    /// </summary>
    /// <param name="id">The id of the tile to edit</param>
    public void loadBlockSettings(int id)
    {
        currEdit = id;
        editingTileImage.sprite = GameManager.tiles[currEdit];
        guarGen1.text = GameManager.guarNum[currEdit].ToString();
        minGen1.text = GameManager.limits[currEdit].ToString();
    }
    /// <summary>
    /// Opens/closes the settings menu
    /// </summary>
    /// <param name="open">If true, the settings menu will be opened.</param>
    public void openCloseSettings(bool open)
    {
        settingsMenu.SetActive(open);
        if(open)
        {
            editingTileImage.sprite = GameManager.tiles[currEdit];
            guarGen1.text = GameManager.guarNum[currEdit].ToString();
            minGen1.text = GameManager.limits[currEdit].ToString();
            nullWeight1.text = GameManager.nullWeight.ToString();
            tileSize1.text = GameManager.numTiles.ToString();
        }
    }

    /// <summary>
    /// Changes the value of a certain variable in Game Manager.
    /// Guaranteed number of tiles will be clamped down to the smallest value it can without going over the max of 15 for all tiles combined.
    /// Guaranteed number will always be less than or equal to the limit.
    /// null weight can't go over 10000
    /// max number of tiles can't go over 32
    /// </summary>
    /// <param name="type">Which variable to change, 0 = the guarNum of active tile, 1 = the limit of active tile, 2 = null weight, 3 = max tiles.</param>
    public void changeValue( int type)
    {
        switch(type)
        {
            case 0:
                int p = calcTotalGuar() - GameManager.guarNum[currEdit];
                int max = 15 - p;
                if(max<0)
                {
                    max = 0;
                }
                GameManager.guarNum[currEdit] = Math.Clamp(int.Parse(guarGen1.text),0,max);
                if (GameManager.guarNum[currEdit]> GameManager.limits[currEdit])
                {
                    GameManager.limits[currEdit] = GameManager.guarNum[currEdit];
                }
                break;
            case 1:
                GameManager.limits[currEdit] = Math.Clamp(int.Parse(minGen1.text),0,32);
                if (GameManager.guarNum[currEdit] > GameManager.limits[currEdit])
                {
                    GameManager.limits[currEdit] = GameManager.guarNum[currEdit];
                }
                break;
            case 2:
                GameManager.nullWeight = Math.Clamp(int.Parse(nullWeight1.text),0,10000);
                break;
            case 3:
                GameManager.numTiles = Math.Clamp(int.Parse(tileSize1.text),0,32);
                break;
        }
    }

    /// <summary>
    /// Calculates the total number of tiles guaranteed to generate
    /// </summary>
    /// <returns>the total number of tiles guaranteed to generate</returns>
    public int calcTotalGuar()
    {
        int r = 0;
        for(int i = 0; i<GameManager.guarNum.Length;i++)
        {
            r += GameManager.guarNum[i];
        }
        return r;
    }

    /// <summary>
    /// Shows or hides the connecting lines between tiles and then takes a new screenshot.
    /// </summary>
    public void toggleLinesLines()
    {
        renderiMage.gameObject.SetActive(!renderiMage.IsActive());
        string peath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ("MonOtoTopologicalCortext/" + UnityEngine.Random.seed + UnityEngine.Random.Range(0, 1000000) + ".png"));
        if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "MonOtoTopologicalCortext/")))
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "MonOtoTopologicalCortext/"));
        }
        ScreenCapture.CaptureScreenshot(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), ("MonOtoTopologicalCortext/" + UnityEngine.Random.seed + UnityEngine.Random.Range(0, 1000000) + ".png")));
    }
}
