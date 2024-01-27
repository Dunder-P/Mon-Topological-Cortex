using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    /// <summary>
    /// UNUSED The names to be put on the tiles
    /// </summary>
    public static string[] tileNames = { "", "Start", "Gold", "Shadow Realm", "Shop","Combat","Good","Flamingo","Bad","Teleport", "VS" };
    /// <summary>
    /// A limit on how many can spawn in (ie: reroll if at the limit)
    /// </summary>
    public static int[] limits = { 0,1,0,1,2,0,0,1,0,0,1};
    /// <summary>
    /// UNUSED. Whether to force a tile to spawn in if none exist after generation
    /// </summary>
    public static bool[] guarGen = { false, true, false, false, false, false, false, true, false, false, false};
    /// <summary>
    /// The guaranteed number of tiles to generate for each type of tile
    /// </summary>
    public static int[] guarNum = { 0, 1, 0, 0, 2, 0, 0, 1, 0, 0, 1 };
    /// <summary>
    /// The color of the tiles. NO LONGER USED
    /// </summary>
    public static Color[] tileCols = {
    new Color(152 / 255.0f, 152 / 255.0f, 152 / 255.0f, 1.0f),
    new Color(238 / 255.0f, 195 / 255.0f, 58 / 255.0f, 1.0f),
    new Color(238 / 255.0f, 195 / 255.0f, 58 / 255.0f, 1.0f),
    new Color(154 / 255.0f, 0 / 255.0f, 245 / 255.0f, 1.0f),
    new Color(75 / 255.0f, 132 / 255.0f, 223 / 255.0f, 1.0f),
    new Color(140 / 255.0f, 0 / 255.0f, 2 / 255.0f, 1.0f),
    new Color(0 / 255.0f, 238 / 255.0f, 11 / 255.0f, 1.0f),
    new Color(254 / 255.0f, 0 / 255.0f, 238 / 255.0f, 1.0f),
    new Color(254 / 255.0f, 1 / 255.0f, 5 / 255.0f, 1.0f),
    new Color(204 / 255.0f, 123 / 255.0f, 7 / 255.0f, 1.0f),
    new Color(13 / 255.0f, 58 / 255.0f, 67 / 255.0f, 1.0f)
};
    /// <summary>
    /// The number of tiles to generate not counting corners.
    /// </summary>
    public static int numTiles = 23;
    /// <summary>
    /// The weight of null tiles. (In the current iteration it is essentially Nullweight+1/11+nullweight)
    /// </summary>
    public static int nullWeight = 7;
    /// <summary>
    /// A sprite array of tile assets, in order of ID.
    /// </summary>
    public static Sprite[] tiles = Resources.LoadAll<Sprite>("Tiles/");
}
