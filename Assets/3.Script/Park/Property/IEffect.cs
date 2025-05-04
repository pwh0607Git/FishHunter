using UnityEngine;

public interface IEffect
{
    public abstract void Apply(PlayerEventListener player);
}

public class ScoreUpEffect : IEffect
{
    private int amount;
    public ScoreUpEffect(int amount){
        this.amount = amount;
    }
    public void Apply(PlayerEventListener player)
    {
        player.ApplyScoreUp(amount);
    }
}

public class ScoreDownEffect : IEffect
{
    private int amount;
    public ScoreDownEffect(int amount){
        this.amount = amount;
    }

    public void Apply(PlayerEventListener player)
    {
        player.ApplyScoreDown(amount);  
    }
}

public class HeartUpEffect : IEffect
{
    public void Apply(PlayerEventListener player)
    {
        player.ApplyHeartUp();
    }
}

public class HeartDownEffect : IEffect
{
    public void Apply(PlayerEventListener player)
    {
        player.ApplyHeartDown();
    }
}

public class InterfereEffect : IEffect
{
    // private int durtion;
    public void Apply(PlayerEventListener player)
    {
        player.ApplyInterfere();
    }
}