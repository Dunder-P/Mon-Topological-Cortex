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
    public static int[] limits = { 0,1,0,1,2,0,0,0,0,0,1};
    /// <summary>
    /// Whether to force a tile to spawn in if none exist after generation
    /// </summary>
    public static bool[] guarGen = { false, true, false, false, false, false, false, false, false, false, false};
    /// <summary>
    /// The color of the tiles
    /// </summary>
    public static Color[] tileCols = { new Color(152, 152, 152, 255), new Color(238, 195, 58), new Color(238, 195, 58), new Color(1154, 0, 245, 255), new Color(75, 132, 223, 255), new Color(140, 0, 2), new Color(0, 238, 11), new Color(254, 0, 238, 255), new Color(254, 1, 5), new Color(204, 123, 7), new Color(13, 58, 67, 255), };
}
