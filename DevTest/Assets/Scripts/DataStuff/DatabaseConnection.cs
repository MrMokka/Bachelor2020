using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;

public class DatabaseConnection : MonoBehaviour {




	string connectionString = "Server=tcp:bachelorserver.database.windows.net,1433;" +
		"Initial Catalog=BachelorProject; Persist Security Info=False;" +
		"User ID=writer; Password=SuperSaver!; MultipleActiveResultSets=False;" +
		"Encrypt=True; TrustServerCertificate=False; Connection Timeout=30;";

	private static readonly string connectionStringWriter = "Server=tcp:bachelorserver.database.windows.net,1433;" +
		"Initial Catalog=BachelorProject; Persist Security Info=False;" +
		"User ID=writer; Password=SuperSaver!; MultipleActiveResultSets=False;" +
		"Encrypt=True; TrustServerCertificate=False; Connection Timeout=30;";

	private static readonly string connectionStringReader = "Server=tcp:bachelorserver.database.windows.net,1433;" +
		"Initial Catalog=BachelorProject; Persist Security Info=False;" +
		"User ID=reader; Password=SuperLoader!; MultipleActiveResultSets=True;" +
		"Encrypt=True; TrustServerCertificate=False; Connection Timeout=30;";


	void Start() {
		//List<Question> questions = ReadQuestionsFromDatabase();
		
	}

	/* Code from Anders
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
	*/

	public static List<Question> ReadQuestionsFromDatabase(string categoryName) {
		return ReadQuestionsFromDatabase(new Category { Name = categoryName });
	}

	public static List<Question> ReadQuestionsFromDatabase(int weightFilter) {
		return ReadQuestionsFromDatabase(null, weightFilter);
	}

	public static List<Question> ReadQuestionsFromDatabase(Category categoryFilter = null, int weightFilter = -1) {
		using(SqlConnection connection = new SqlConnection(connectionStringReader)) {
			string sqlString = 
				$"SELECT Q.QuestionId, T.TypeId as TypeId, T.Name as TypeName, C.Name as CategoryName, Q.QuestionText, Q.Weight, Q.Active, Q.Json " +
				$"FROM Question as Q " +
				$"left join Type as T on T.TypeId = Q.TypeId " +
				$"left join Question_Category as QC on QC.QuestionId = Q.QuestionId " +
				$"left join Category as C on QC.CategoryId = C.CategoryId ";

			if(categoryFilter != null) {
				sqlString += $"WHERE C.Name = '{categoryFilter.Name}'";
			}

			if(weightFilter > 0) {
				if(categoryFilter != null)
					sqlString += " AND ";
				else
					sqlString += "WHERE ";
				sqlString += $"Q.Weight = {weightFilter}";
			}

			connection.Open();

			SqlCommand cmd = new SqlCommand(sqlString, connection);

			SqlDataReader reader = cmd.ExecuteReader();
			List<Question> questions = new List<Question>();

			if(reader == null) {
				Debug.LogError("No reader found");
				return questions;
			}

			if(!reader.HasRows) {
				Debug.LogError("No rows found");
			}

			List<int> usedIds = new List<int>();
			while(reader.Read()) {
				int Id = (int)reader["QuestionId"];
				if(usedIds.Contains(Id))
					continue;
				else
					usedIds.Add(Id);
				Question Question = new Question {
					Id = Id,
					Active = (bool)reader["Active"],
					Weight = (int)reader["Weight"],
					QuestionText = reader["QuestionText"].ToString(),
					Type = new Type { Id = (int)reader["TypeId"], Name = reader["TypeName"].ToString() },
					QuestionObject = reader["Json"].ToString()
				};
				List<Category> categoryList = new List<Category>();
				string categoryQuery =
					$"SELECT C.Name as CategoryName, C.CategoryId as CategoryId " +
					$"FROM Question_Category as QC " +
					$"left join Category as C on QC.CategoryId = C.CategoryId " +
					$"WHERE QC.QuestionId = {Question.Id}";

				SqlDataReader dataReader = new SqlCommand(categoryQuery, connection).ExecuteReader();
				if(dataReader != null) {
					while(dataReader.Read()) {
						categoryList.Add(
							new Category {
								Id = (int)dataReader["CategoryId"], 
								Name = dataReader["CategoryName"].ToString()
							}
						);
					}
				}
				Question.CategoryList = categoryList;
				questions.Add(Question);
			}
			return questions;
			/*
			select Q.QuestionId, T.Name as Type, Q.QuestionText, C.Name as Category, Q.Weight, Q.Active, Q.Json from Question as Q
			left join Type as T on T.TypeId = Q.TypeId
			left join Question_Category as QC on QC.QuestionId = Q.QuestionId
			left join Category as C on QC.CategoryId = C.CategoryId
			*/
		}
	}

	public static void WriteDataToDatabase(Question Question, string category) {
		using(SqlConnection connection = new SqlConnection(connectionStringWriter)) {
			string insertSQL = $"INSERT INTO Question (TypeId, QuestionText, Active, Weight, Json) $ VALUES(" +
					$" (SELECT Name FROM Type WHERE Type.Name = {Question.Type.Name}), {Question.QuestionText}" +
					$" 1, {Question.Weight}, {Question.QuestionObject}" +
					$")";

			connection.Open();
			int questionId = (int)new SqlCommand(insertSQL, connection).ExecuteScalar();
			print(questionId);

			foreach(Category c in Question.CategoryList) {
				string categorySQL =
					$"INSERT INTO Question_Category (QuestionId, CategoryId) VALUES ( " +
					$"{questionId}, (SELECT CategoryId FROM Category WHERE Category.Name = {c.Name})" +
					$")";
				new SqlCommand(categorySQL, connection).ExecuteNonQuery();
			}

			

			/*
			using(SqlCommand cmd = ) {
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows) {
					typeId = (int)reader["TypeId"];
				} else {
					new SqlCommand($"INSERT INTO Type (Name) VALUES ({typeName})", connection).ExecuteNonQuery();
				}
				reader.Close();
			}
			int categoryId = -1;

			string typeName = Question.Type.Name;
			int typeId = GetRowId(connection, "Type", $"WHERE Type.Name = {typeName}");
			if(typeId == -1) {
				new SqlCommand($"INSERT INTO Type (Name) VALUES ({typeName})", connection).ExecuteNonQuery();
				typeId = GetRowId(connection, "Type", $"WHERE Type.Name = {typeName}");
				if(typeId == -1) {
					Debug.LogError("TypeId not found! Even after inserting!");
					return;
				}
			}

			foreach(Category c in Question.CategoryList) {
				string sqlString = $"IF (NOT EXISTS (SELECT Name FROM Category WHERE Category.Name = {c.Name})" +
					$" INSERT INTO Category (Name) VALUES ({c.Name}) OUTPUT INSERTED.CategoryId)" +
					$" ELSE (SELECT Name FROM Category WHERE Category.Name = {c.Name})";
				categoryId = (int) new SqlCommand(sqlString, connection).ExecuteScalar();
			}

			using(SqlCommand cmd = new SqlCommand($"SELECT * FROM Type where Type.Name = {typeName}", connection)) {
				SqlDataReader reader = cmd.ExecuteReader();
				if(reader.HasRows) {
					typeId = (int) reader["TypeId"];
				} else {
					new SqlCommand($"INSERT INTO Type (Name) VALUES ({typeName})", connection).ExecuteNonQuery();
				}
				reader.Close();
			}
			*/
		}
	}

	private static int GetRowId(SqlConnection connection, string tableName, string whereClause) {
		int id = -1;
		using(SqlCommand cmd = new SqlCommand($"SELECT {tableName}Id FROM {tableName} " + whereClause, connection)) {
			SqlDataReader reader = cmd.ExecuteReader();
			if(reader.HasRows) {
				int.TryParse(reader[$"{tableName}Id"].ToString(), out id);
			}
			reader.Close();
		}
		return id;
	}


}

