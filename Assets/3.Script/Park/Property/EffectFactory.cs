using System.Collections.Generic;

public static class EffectFactory
{
    public static List<IEffect> CreateEffects(PropData data){
        List<IEffect> resEffects = new();
        
        for(int i=0; i<data.effects.Count; i++){
            switch(data.effects[i]){
                case EffectType.SCOREUP : {
                    resEffects.Add(new ScoreUpEffect());
                    break;
                }
                case EffectType.SCOREDOWN : {
                    resEffects.Add(new ScoreDownEffect());
                    break;
                }
                case EffectType.HEARTUP : {
                    resEffects.Add(new HeartUpEffect());          
                    break;
                }
                case EffectType.HEARTDOWN : {
                    resEffects.Add(new HeartDownEffect());
                    break;
                }
                case EffectType.INTERFERE : {
                    resEffects.Add(new InterfereEffect());
                    break;
                }
            }
        }

        return resEffects;
    }   
}