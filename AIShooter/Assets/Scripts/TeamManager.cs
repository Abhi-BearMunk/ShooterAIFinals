using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {

    public GameObject team1Prefab;
    public GameObject team2Prefab;
    public int teamSize;

    public List<Transform> team1SpawnPoints = new List<Transform>();
    public List<Transform> team2SpawnPoints = new List<Transform>();

    // Use this for initialization
    void Start () {
        StartMatch();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartMatch()
    {
        Team team1 = team1Prefab.GetComponent<Team>();
        Team team2 = team2Prefab.GetComponent<Team>();

        for(int i = 0; i < teamSize; i++)
        {
            Instantiate(team1.playerPrefabs[i], team1SpawnPoints[i].position, Quaternion.LookRotation(-team1SpawnPoints[i].position, Vector3.up));
            Instantiate(team2.playerPrefabs[i], team2SpawnPoints[i].position, Quaternion.LookRotation(-team2SpawnPoints[i].position, Vector3.up));
        }
    }
}
