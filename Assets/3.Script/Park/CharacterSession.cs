
public class CharacterSession : BehaviourSingleton<CharacterSession>
{
    protected override bool IsDontDestroy() => true;

    public CharacterProfile currentProfile;
    public string playerNickname {get; set;}
}