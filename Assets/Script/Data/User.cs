using System.Collections.Generic;

public class User {

    private static User instance;
    public static User Instance
    {
        get
        {
            if (null == instance) { instance = new User(); }
            return instance;
        }
    }
    private User()
    {
        aircrafts = new List<AircraftInfo>();
    }

    public string userID { get; set; }
    public string userName { get; set; }
    public string nickname { get; set; }
    public int score { get; set; }
    public int cash { get; set; }
    public string team { get; set; }
    public AircraftInfo currentAircraft { get; set; }
    public List<AircraftInfo> aircrafts;

}