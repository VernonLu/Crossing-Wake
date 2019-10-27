public class MapInfo {
    public string mapID { get; set; }

    public string mapName { get; set; }

    public MapInfo(string mapID, string mapName)
    {
        this.mapID = mapID;
        this.mapName = mapName;
    }
}
