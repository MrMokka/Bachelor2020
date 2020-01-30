using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;

public class DatabaseConnection : MonoBehaviour {




	string connectionString = "Server=tcp:bachelorserver.database.windows.net,1433;" +
		"Initial Catalog=BachelorProject; Persist Security Info=False;" +
		"User ID=sartaren; Password=Kukkqpp0; MultipleActiveResultSets=False;" +
		"Encrypt=True; TrustServerCertificate=False; Connection Timeout=30;";

	void Start() {

		Connect();
	}

	void Connect() {

		using(SqlConnection conn = new SqlConnection(connectionString)) {
			using(SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", conn)) {
				conn.Open();
				print("Open complete");
				var rs = cmd.ExecuteReader();
				if(rs != null) {
					while(rs.Read()) {
						var t = rs["firstName"];
						print(t);
					}
				}
			}
		}

	}



}

