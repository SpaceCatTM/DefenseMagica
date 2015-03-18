using UnityEngine;

using System;
using System.Collections;
using System.Data.SQLite;

public class DatabaseTest : MonoBehaviour {

	// Use this for initialization
	void Start ()  {
		string filePath;

		using (SQLiteConnection dbConnection = OpenConnectionFromFile(filePath)) {
			using (SQLiteCommand dbCommand = dbConnection.CreateCommand()) {
				dbCommand.CommandType = System.Data.CommandType.Text;
				dbCommand.CommandText = "select * from MultiThreadedTest";
				
				using (SQLiteDataReader dbReader = dbCommand.ExecuteReader()) {
					while (dbReader.Read() == true) {
						GameObject.Find ("Touch Test").transform.Rotate (new Vector3(dbReader.GetInt32 (0), 0));
						Debug.Log (dbReader.GetInt32(0));
					}
				}
			} 
		}
	}

	// Update is called once per frame
	void Update () {
		return;
	}

	SQLiteConnection OpenConnectionFromFile(string filePath) {
		SQLiteConnectionStringBuilder dbConnectionBuilder = new SQLiteConnectionStringBuilder(); 
		dbConnectionBuilder.Add("Data Source", filePath);

		SQLiteConnection dbConnection = new SQLiteConnection(dbConnectionBuilder.ToString());
		dbConnection.Open();

		return dbConnection;
	}

}
	