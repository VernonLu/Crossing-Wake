using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftInfo
{
    public AircraftInfo(string aircraftID)
    {
        this.aircraftID = aircraftID;
    }
    public string aircraftID { get; set; }
    public string aircraftName { get; set; }
    public int speed { get; set; }
    public int motility { get; set; }
    public int cost { get; set; }

}
