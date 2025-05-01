public class PropertyFactory : BehaviourSingleton<PropertyFactory>
{
    protected override bool IsDontDestroy() => false;                   // 인게임 내부에만 존재하면 된다.
}
