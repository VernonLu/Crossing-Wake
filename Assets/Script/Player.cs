public class Player {

    public string nickName { get; set; }
    public int score { get; set; }

    public Player(string nickName, int score)
    {
        this.nickName = nickName;
        this.score = score;
    }
}
