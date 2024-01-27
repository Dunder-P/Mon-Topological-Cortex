using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    /// <summary>
    /// The names to be put on the tiles
    /// </summary>
    public static string[] tileNames = { "", "Start", "Gold", "Shadow Realm", "Shop","Combat","Good","Flamingo","Bad","Teleport", "VS" };
    /// <summary>
    /// A limit on how many can spawn in (ie: reroll if at the limit)
    /// </summary>
    public static int[] limits = { 0,1,0,1,2,0,0,1,0,0,1};
    /// <summary>
    /// Whether to force a tile to spawn in if none exist after generation
    /// </summary>
    public static bool[] guarGen = { false, true, false, false, false, false, false, true, false, false, false};
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
    public static int totalGuar = 5;
    public static int numTiles = 23;
    public static int nullWeight = 7;
    public static Sprite[] tiles = Resources.LoadAll<Sprite>("Tiles/");
}
