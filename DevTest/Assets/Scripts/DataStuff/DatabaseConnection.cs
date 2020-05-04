using System;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;

public class DatabaseConnection : MonoBehaviour {


	private static readonly string connectionStringWriter = "Server=tcp:bachelorserver.database.windows.net,1433;" +
		"Initial Catalog=BachelorProject; Persist Security Info=False;" +
		"User ID=read_write; Password=LeseSkrive!; MultipleActiveResultSets=False;" +
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

	#region Get Category
	public static List<Category> GetCategories() {
		return GetCategories(-1);
	}

	public static List<Category> GetCategories(int questionId) {
		List<Category> categoryList = new List<Category>();
		string categoryQuery = "";
		
		if(questionId != -1) {
			categoryQuery += $"SELECT C.Name as CategoryName, C.CategoryId as CategoryId " +
			$"FROM Question_Category as QC " +
			$"left join Category as C on QC.CategoryId = C.CategoryId " +
			$"WHERE QC.QuestionId = {questionId}";
		} else {
			categoryQuery += $"SELECT C.Name as CategoryName, C.CategoryId as CategoryId " +
			$"FROM Category as C ";
		}

		using(SqlConnection connection = new SqlConnection(connectionStringReader)) {
			connection.Open();
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
		}
		return categoryList;
	}
	#endregion

	#region Get Type
	public static List<Type> GetTypes() {
		return GetTypes(-1);
	}

	public static List<Type> GetTypes(int typeId) {
		List<Type> categoryList = new List<Type>();
		string typeQuery = "";

		if(typeId != -1) {
			typeQuery += $"SELECT T.Name as TypeName, T.TypeId as TypeId " +
			$"FROM Type as T " +
			$"WHERE T.TypeId = {typeId}";
		} else {
			typeQuery += $"SELECT T.Name as TypeName, T.TypeId as TypeId " +
			$"FROM Type as T ";
		}

		using(SqlConnection connection = new SqlConnection(connectionStringReader)) {
			connection.Open();
			SqlDataReader dataReader = new SqlCommand(typeQuery, connection).ExecuteReader();
			if(dataReader != null) {
				while(dataReader.Read()) {
					categoryList.Add(
						new Type {
							Id = (int)dataReader["TypeId"],
							Name = dataReader["TypeName"].ToString()
						}
					);
				}
			}
		}
		return categoryList;
	}

	#endregion

	#region Get Question

	public class ReadQuestionOptions {
		public int Number = 100;
		public List<Category> CategoryFilter = null;
		public List<int> WeightFilter = null;
		public bool IsActive = false;
		public bool RandomOrder = true;
	};

	public static List<Question> ReadQuestionsFromDatabase(ReadQuestionOptions options) {
		using(SqlConnection connection = new SqlConnection(connectionStringReader)) {
			string sqlString =
				$"SELECT TOP {options.Number} Q.QuestionId, T.TypeId as TypeId, T.Name as TypeName, C.Name as CategoryName, " +
				$"Q.QuestionText, Q.Weight, Q.Active, Q.Json " +
				$"FROM Question as Q " +
				$"left join Type as T on T.TypeId = Q.TypeId " +
				$"left join Question_Category as QC on QC.QuestionId = Q.QuestionId " +
				$"left join Category as C on QC.CategoryId = C.CategoryId ";

			if(options.IsActive)
				sqlString += $"WHERE Q.Active = 1";
			else
				sqlString += $"WHERE 1 = 1"; //Used for placing WHERE correctly at first, plus failsafe if no filters needed

			//Builds: " AND (Q.Weight = '1' OR Q.Weight = '4' OR Q.Weight = '398475')
			if(options.WeightFilter != null && options.WeightFilter[0] != -1) {
				sqlString += $" AND (";
				for(int i = 0; i < options.WeightFilter.Count; i++) {
					sqlString += $"Q.Weight = {options.WeightFilter[i]}";
					if(i != options.WeightFilter.Count - 1)
						sqlString += $" OR ";
				}
				sqlString += $")";
			}

			//Builds: " AND (C.Name = 'Filter1' OR C.Name = 'Filter2' OR C.Name = 'Filter3')
			if(options.CategoryFilter != null && options.CategoryFilter[0].Name != "All") {
				sqlString += $" AND (";
				for(int i = 0; i < options.CategoryFilter.Count; i++) {
					sqlString += $"C.Name = '{options.CategoryFilter[i].Name}'";
					if(i != options.CategoryFilter.Count - 1)
						sqlString += $" OR ";
				}
				sqlString += $")";
			}

			if(options.RandomOrder)
				sqlString += " ORDER BY NEWID()";

			connection.Open();

			SqlCommand cmd = new SqlCommand(sqlString, connection);

			SqlDataReader reader = cmd.ExecuteReader();
			List<Question> questions = new List<Question>();

			if(reader == null) {
				Debug.LogError("No reader found");
				return questions;
			}

			if(!reader.HasRows) {
				Debug.LogWarning("No rows found");
				return questions;
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
					Active = (int)reader["Active"],
					Weight = (int)reader["Weight"],
					QuestionText = reader["QuestionText"].ToString(),
					Type = new Type { Id = (int)reader["TypeId"], Name = reader["TypeName"].ToString() },
					QuestionObject = reader["Json"].ToString()
				};
				Question.CategoryList = GetCategories(Question.Id);
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

	#endregion

	#region Write question to database

	public static void WriteQuestionToDatabase(Question question) {
		using(SqlConnection connection = new SqlConnection(connectionStringWriter)) {
			string insertSQL = $"INSERT INTO question (TypeId, questionText, Active, Weight, Json)" +
					$" OUTPUT inserted.questionId VALUES(" +
					$" (SELECT TypeId FROM Type WHERE Type.Name = '{question.Type.Name}'), '{question.QuestionText}'," +
					$" '{question.Active}', '{question.Weight}', '{question.QuestionObject}')";
				
			connection.Open();
			int questionId = (int)new SqlCommand(insertSQL, connection).ExecuteScalar();

			foreach(Category c in question.CategoryList) {
				string categorySQL =
					$"INSERT INTO question_Category (questionId, CategoryId) VALUES ( " +
					$"{questionId}, (SELECT CategoryId FROM Category WHERE Category.Name = '{c.Name}'))";
				new SqlCommand(categorySQL, connection).ExecuteNonQuery();
			}

			#region comments
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
			#endregion
		}
	}

	#endregion

	#region Write category to database

	public static bool WriteCategoryToDatabase(Category category) {
		using(SqlConnection connection = new SqlConnection(connectionStringWriter)) {
			string insertSQL =
					$"IF NOT EXISTS (SELECT * FROM Category WHERE Category.Name = '{category.Name}') " +
					$"INSERT INTO Category (Name) VALUES ('{category.Name}')";

			try {
				connection.Open();
				int rows = new SqlCommand(insertSQL, connection).ExecuteNonQuery();
				if(rows > 0)
					return true;
				return false;
			} catch(Exception e) {
				Debug.Log(e);
				return false;
			}
		}
	}

	#endregion

	#region Update question in database

	public static bool UpdateQuestionInDatabase(Question question) {
		int updatedRow = -1;
		using(SqlConnection connection = new SqlConnection(connectionStringWriter)) {
			connection.Open();

			//Update question_category link entity
			string sqlClearCategoryLink = $"DELETE FROM Question_Category WHERE QuestionId = {question.Id}";
			new SqlCommand(sqlClearCategoryLink, connection).ExecuteNonQuery();
			foreach(Category c in question.CategoryList) {
				string sqlCategoryLink =
					$"INSERT INTO question_Category (questionId, CategoryId) VALUES ( " +
					$"{question.Id}, (SELECT CategoryId FROM Category WHERE Category.Name = '{c.Name}'))";
				new SqlCommand(sqlCategoryLink, connection).ExecuteNonQuery();
			}

			string sqlUpdateQuestion = $"UPDATE Question" +
				$" SET QuestionText = '{question.QuestionText}', Active = {question.Active}," +
				$" Weight = {question.Weight}, Json = '{question.QuestionObject}'" +
				$" WHERE QuestionId = {question.Id}";

			updatedRow = new SqlCommand(sqlUpdateQuestion, connection).ExecuteNonQuery();
			
			

			/*
			string insertSQL = $"INSERT INTO question (TypeId, questionText, Active, Weight, Json)" +
					$" OUTPUT inserted.questionId VALUES(" +
					$" (SELECT TypeId FROM Type WHERE Type.Name = '{question.Type.Name}'), '{question.QuestionText}'," +
					$" '{question.Active}', '{question.Weight}', '{question.QuestionObject}')";

			
			int questionId = (int)new SqlCommand(insertSQL, connection).ExecuteScalar();
			print(questionId);
			*/

		}

		if(updatedRow <= 0)
			return false;
		return true;
	}

	#endregion

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

