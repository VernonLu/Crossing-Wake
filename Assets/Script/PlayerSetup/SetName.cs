using Photon;

public class SetName : PunBehaviour {
    //Set player's name 
    [PunRPC]
    public void SetPlayerName(string teamNum)
    {
        transform.name = "Player " + teamNum;
    }
}
