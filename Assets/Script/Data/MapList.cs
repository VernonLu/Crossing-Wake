using System.Collections.Generic;
using UnityEngine;

public class MapList
{

    private static List<MapInfo> mapList = new List<MapInfo>();

    public static void Add(MapInfo mapInfo)
    {
        foreach(MapInfo map in mapList)
        {
            if(map.mapID == mapInfo.mapID) { return; }
        }
        mapList.Add(mapInfo);
    }
    public static void Add(string mapID, string mapName)
    {
        foreach (MapInfo mapInfo in mapList)
        {
            if (mapInfo.mapID == mapID) { return; }
        }
        mapList.Add(new MapInfo(mapID, mapName));
    }

    public static void Remove(string mapID)
    {
        foreach (MapInfo mapInfo in mapList)
        {
            if (mapInfo.mapID == mapID)
            {
                mapList.Remove(mapInfo);
            }
        }
    }

    public static int GetMapCount()
    {
        return mapList.Count;
    }

    public static string GetMapID(int index)
    {
        if (index < mapList.Count)
        {
            return mapList[index].mapID;
        }
        return null;
    }

    public static string GetMapName(int index)
    {
        if (index < mapList.Count)
        {
            return mapList[index].mapName;
        }
        return null;
    }

    /// <summary>
    /// 随机获取地图
    /// </summary>
    /// <returns></returns>
    public static string GetRandomMap()
    {
        int count = mapList.Count;
        int index = Random.Range(0, count);
        return mapList[index].mapName;
    }
}