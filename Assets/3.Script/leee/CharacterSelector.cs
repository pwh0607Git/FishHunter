using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public LobbyUI lobbyUI;
    public int characterIndex;

    public void OnSelect()
    {
        lobbyUI.SelectCharacter(characterIndex);
    }
}
