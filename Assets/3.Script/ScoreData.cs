using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LocalScoreManager
{
    private static string FilePath => Path.Combine(Application.persistentDataPath, "scores.json");

    public static void SaveScore(string nickname, int score)
    {
        ScoreData data = LoadScoreData();  // 기존 데이터 불러오기
        data.scores.Add(new ScoreEntry { nickname = nickname, score = score });  // 새 점수 추가

        string json = JsonUtility.ToJson(data, true);  // 예쁘게 출력되는 JSON
        File.WriteAllText(FilePath, json);             // 파일 저장

        Debug.Log("Saved to: " + FilePath);            // 저장 위치 확인 로그
    }

    public static ScoreData LoadScoreData()
    {
        if (!File.Exists(FilePath))
        {
            return new ScoreData(); // 빈 리스트로 시작
        }

        string json = File.ReadAllText(FilePath);
        return JsonUtility.FromJson<ScoreData>(json);
    }

    public static List<ScoreEntry> GetSortedScoresDescending()
    {
        var data = LoadScoreData();
        data.scores.Sort((a, b) => b.score.CompareTo(a.score)); // 높은 점수 먼저
        return data.scores;
    }
}


[System.Serializable]
public class ScoreEntry
{
    public string nickname;
    public int score;
}

[System.Serializable]
public class ScoreData
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}