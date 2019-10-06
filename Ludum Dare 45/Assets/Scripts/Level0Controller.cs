using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level0Controller : MonoBehaviour
{
    public List<GameObject> EnemyList;

    private void Awake()
    {
        if(EnemyList.Count == 0)
        {
            Debug.LogError("No enemies in list! Level will complete instantly");
        }
    }

    private void FixedUpdate()
    {
        for(var i = EnemyList.Count-1; i >= 0; i--)
        {
            if(!EnemyList[i])
            {
                EnemyList.RemoveAt(i);
            }
        }

        if(EnemyList.Count == 0)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
