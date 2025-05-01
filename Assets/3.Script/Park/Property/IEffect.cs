using UnityEngine;

public interface IEffect
{
    public abstract void Apply(GameObject player);
}

public class ScoreUpEffect : IEffect
{
    public void Apply(GameObject player)
    {
        
    }
}

public class ScoreDownEffect : IEffect
{
    public void Apply(GameObject player)
    {
        
    }
}

public class HeartUpEffect : IEffect
{
    public void Apply(GameObject player)
    {
        
    }
}

public class HeartDownEffect : IEffect
{
    public void Apply(GameObject player)
    {
        
    }
}

public class InterfereEffect : IEffect
{
    public void Apply(GameObject player)
    {
        
    }
}

public class DamageEffect : IEffect
{
    public void Apply(GameObject player)
    {
        
    }
}