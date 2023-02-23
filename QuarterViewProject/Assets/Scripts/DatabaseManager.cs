using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField addName;
    public TMP_InputField searchName;

    private string userId;
    private DatabaseReference dbReference;
    float time;
    int enemyKill;
    int score;
    [SerializeField]
    GameObject addComplete;

    public List<string> nameList = new List<string>();
    public List<float> timeList = new List<float>();
    public List<int> enemyKillList = new List<int>();
    public List<int> scoreList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        userId = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SetData(float time, int enemykill, int score)
    {
        this.time = time;
        this.enemyKill= enemykill;
        this.score = score;
    }

    public void CreateUser()
    {
        User newUser = new User(addName.text, time, enemyKill, score);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userId).SetRawJsonValueAsync(json);
        addComplete.SetActive(true);
    }

    public IEnumerator LoadData()
    {
        nameList.Clear();
        timeList.Clear();    
        enemyKillList.Clear();
        scoreList.Clear();

        var DBTask = dbReference.Child("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if(DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }

        else
        {
            DataSnapshot snapshot = DBTask.Result;

            foreach (DataSnapshot childScanpshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string name = childScanpshot.Child("name").Value.ToString();
                nameList.Add(name);
                float time = float.Parse(childScanpshot.Child("time").Value.ToString());
                timeList.Add(time);
                int enemyKill = int.Parse(childScanpshot.Child("enemyKill").Value.ToString());
                enemyKillList.Add(enemyKill);
                int score = int.Parse(childScanpshot.Child("score").Value.ToString());
                scoreList.Add(score);
            }
        }
    }

    

   
}
