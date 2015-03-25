using UnityEngine;

using System;
using System.Collections;
using System.IO;

using Mono.Data;
using Mono.Data.Sqlite;

public class DatabaseTest : MonoBehaviour {

	// Use this for initialization
	void Start ()  {
		String filePath = Path.Combine(Application.persistentDataPath, "test.db"); 

		try
		{
			StartCoroutine(LoadFileFromStreamAssets (filePath, "Databases/test.db"));

			DebugConsole.Log (filePath);
		
			using (SqliteConnection dbConnection = OpenConnectionFromFile(filePath)) {
				using (SqliteCommand dbCommand = dbConnection.CreateCommand()) {
					dbCommand.CommandText = "select * from MultiThreadedTest";
					
					using (SqliteDataReader dbReader = dbCommand.ExecuteReader()) {
						while (dbReader.Read() == true) {
							GameObject.Find ("Touch Test").transform.Rotate (new Vector3(dbReader.GetInt32 (0), 0));
							Debug.Log (dbReader.GetInt32(0));
						}
					}
				} 
			}
		}
		catch (Exception e)
		{
			DebugConsole.Log ("Error " + e.ToString ());
		}
	}

	// Update is called once per frame
	void Update () {
		return;
	}

	SqliteConnection OpenConnectionFromFile(string filePath) {
		SqliteConnectionStringBuilder dbConnectionBuilder = new SqliteConnectionStringBuilder(); 
		dbConnectionBuilder.Add("Data Source", filePath);

		SqliteConnection dbConnection = new SqliteConnection(dbConnectionBuilder.ToString());
		dbConnection.Open();

		return dbConnection;
	}

	IEnumerator LoadFileFromStreamAssets(String filePath, String streamAssetPath) {
		DebugConsole.Log (Path.Combine(Application.streamingAssetsPath, streamAssetPath));

		WWW data = new WWW(Path.Combine(Application.streamingAssetsPath, streamAssetPath));
		yield return data;

		File.WriteAllBytes(filePath, data.bytes);
	}
}
	