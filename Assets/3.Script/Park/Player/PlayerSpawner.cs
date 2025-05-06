using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] List<CharacterProfile> datas;

    void OnEnable()
    {
        StartCoroutine(SpawnLogic_Co());
    }

    IEnumerator SpawnLogic_Co(){
        yield return new WaitUntil(() => CharacterSession.I != null);
        
        CharacterProfile profile = CharacterSession.I.currentProfile;

        SpawnCharacter(profile);
    }

    void SpawnCharacter(CharacterProfile profile){
        CharacterControl player = Instantiate(profile.model, transform).GetComponent<CharacterControl>();
        player.characterProfile = profile;
    }
}