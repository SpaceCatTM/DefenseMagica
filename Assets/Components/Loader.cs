using UnityEngine;

using System;
using System.Collections;
using System.IO;

using Mono.Data.Sqlite;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		String filePath = Path.Combine (Application.dataPath, "StreamingAssets/Databases/data.db");
		
		using (SqliteConnection dbConnection = OpenConnectionFromFile(filePath)) {
			using (SqliteCommand dbCommand = dbConnection.CreateCommand()) {
				dbCommand.CommandText = "select * from user_info";
				
				using (SqliteDataReader dbReader = dbCommand.ExecuteReader()) {
					while (dbReader.Read() == true) {
						GameStorage.PlayerParameter = new PlayerParameter
						{
							Name = dbReader.GetString (dbReader.GetOrdinal("name")),
							Level = dbReader.GetInt32 (dbReader.GetOrdinal("level")),
							Exp = dbReader.GetInt32 (dbReader.GetOrdinal("exp")),
							WeaponItem = dbReader.GetString (dbReader.GetOrdinal("weapon_item")),
							Money = dbReader.GetInt32 (dbReader.GetOrdinal("money"))
						};
					}
				}
			} 
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
		WWW data = new WWW(Path.Combine(Application.streamingAssetsPath, streamAssetPath));
		yield return data;
		
		File.WriteAllBytes(filePath, data.bytes);
	}
}
