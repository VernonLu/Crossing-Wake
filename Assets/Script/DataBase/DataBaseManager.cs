using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataBaseManager : MonoBehaviour{

    public static string server = "101.132.66.118";
    public static string port = "3306";
    public static string database = "crossing_wake";
    public static string user = "unity";
    public static string pwd = "1225";
    private static string connectionStr;
    private static string sqlStr;
    private static MySqlConnection connection;
    private static MySqlCommand command;
    
    /// <summary>
    /// 打开数据库连接
    /// </summary>
    public static void Open()
    {
        connectionStr = "Server=" + server
            + ";Port=" + port
            + ";Database=" + database
            + ";Uid=" + user
            + ";Pwd=" + pwd;
        try
        {
            connection = new MySqlConnection(connectionStr);
            connection.Open();
        }
        catch (Exception e)
        {
            Debug.Log("Unable to open connection" + e.Message);
        }
    }
    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public static void Close()
    {
        if (null != connection)
        {
            if (!connection.State.ToString().Equals("Closed"))
            {
                connection.Close();
                Debug.Log("Close");
            }
        }
        connection.Dispose();
    }

    /// <summary>
    /// 获取地图信息
    /// </summary>
    public static void GetMapList()
    {
        sqlStr = "SELECT * FROM map;";
        command = new MySqlCommand(sqlStr, connection);
        MySqlDataReader reader = command.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                Debug.Log("Get Map info");
                if (reader.HasRows)
                {
                    MapInfo map = new MapInfo(reader.GetString(0), reader.GetString(1));
                    Debug.Log("Map ID:" + reader[0] + "Map Name:" + reader[1]);
                    MapList.Add(map);
                }
            }
            reader.Close();
        }
        catch(Exception e)
        {
            Debug.Log("Failed to get map list" + e.Message);
        }
        finally
        {
            reader.Close();
            command.Dispose();
        }
    }

    /// <summary>
    /// 在数据库中查询是否有用户名、密码匹配用户，如果有获取用户信息
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public static bool Login(string username, string password)
    {
        bool result = false;
        user = null;

        //获取用户信息
        sqlStr = "SELECT * FROM user_info WHERE user_name='" + username + "' AND user_pwd='" + password + "';";
        command = new MySqlCommand(sqlStr, connection);
        MySqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.Read())
            {
                Debug.Log("Get user info");
                User.Instance.userID = (string)reader[0];
                Debug.Log("User ID " + reader[0]);
                User.Instance.userName = username;
                Debug.Log("Username " + reader[1]);
                User.Instance.nickname = (string)reader[3];
                Debug.Log("Nickname " + reader[2]);
                User.Instance.score = (int)reader[4];
                Debug.Log("Score " + reader[3]);
                User.Instance.cash = (int)reader[5];
                Debug.Log("Cash " + reader[4]);
                result = true;
                Debug.Log("Read success");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Unable to read data" + e.Message);
        }
        finally
        {
            reader.Close();
            command.Dispose();
        }
        return result;
    }

    /// <summary>
    /// 获取玩家已拥有的飞机
    /// </summary>
    public static void GetOwnership()
    {
        sqlStr = "SELECT * FROM ownership WHERE user_id='" + User.Instance.userID + "';";
        command = new MySqlCommand(sqlStr, connection);
        MySqlDataReader reader = command.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                Debug.Log("Get Aircraft info");
                string aircraftID = (string)reader[1];
                Debug.Log(aircraftID);
                AircraftInfo aircraft = new AircraftInfo(aircraftID);
                User.Instance.aircrafts.Add(aircraft);
            }
        }
        catch(Exception e)
        {
            Debug.Log("Failed to get ownership" + e.Message);
        }
        finally
        {
            reader.Close();
            command.Dispose();
        }
    }

    /// <summary>
    /// 获取飞机数据
    /// </summary>
    /// <param name="aircraftID"></param>
    /// <returns></returns>
    public static void GetUserAircrafts(int aircraftIndex)
    {
        sqlStr = "SELECT * FROM aircraft WHERE aircraft_id='" + User.Instance.aircrafts[aircraftIndex].aircraftID + "';";
        command = new MySqlCommand(sqlStr, connection);
        MySqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.Read())
            {
                Debug.Log("Get Aircraft info");
                Debug.Log("aircraft id " + User.Instance.aircrafts[aircraftIndex].aircraftID);
                User.Instance.aircrafts[aircraftIndex].aircraftName = (string)reader[1];
                Debug.Log("aircraft name" + reader[1]);
                User.Instance.aircrafts[aircraftIndex].speed = (int)reader[2];
                Debug.Log("speed" + reader[2]);
                User.Instance.aircrafts[aircraftIndex].motility = (int)reader[3];
                Debug.Log("motility" + reader[3]);
                User.Instance.aircrafts[aircraftIndex].cost = (int)reader[4];
                Debug.Log("cost" + reader[4]);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            reader.Close();
            command.Dispose();
        }
    }

    /// <summary>
    /// 获取飞机数据
    /// </summary>
    /// <param name="aircraftID"></param>
    /// <returns></returns>
    public static AircraftInfo GetAircraftInfo(string aircraftID)
    {
        sqlStr = "SELECT * FROM aircraft WHERE aircraft_id='" + aircraftID + "';";
        command = new MySqlCommand(sqlStr, connection);
        MySqlDataReader reader = command.ExecuteReader();
        AircraftInfo aircraft = null;
        try
        {
            if (reader.Read())
            {
                aircraft = new AircraftInfo(aircraftID);
                aircraft.aircraftName = (string)reader[1];
                aircraft.speed = (int)reader[2];
                aircraft.motility = (int)reader[3];
                aircraft.cost = (int)reader[4];
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            reader.Close();
            command.Dispose();
        }
        return aircraft;
    }

    /// <summary>
    /// 更新用户数据
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="score"></param>
    /// <param name="cash"></param>
    public static void UpdateScoreAndCash(string userID, int score, int cash)
    {
        sqlStr = string.Format("UPDATE user_info SET score={0}，cash={1} WHERE user_id='{2}';", score, cash, userID);
        command = new MySqlCommand(sqlStr, connection);
        int result = command.ExecuteNonQuery();
        Debug.Log("Update score " + score + "and cash " + cash + (result == 1 ? " Success" : " Fail"));
        command.Dispose();
    }

    /// <summary>
    /// 玩家购买飞机
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="aircraftID"></param>
    public static void InsertOwnership(string userID, string aircraftID)
    {
        sqlStr = string.Format("INSERT INTO ownership VALUES('{0}','{1}');", userID, aircraftID);
        command = new MySqlCommand(sqlStr, connection);
        int result = command.ExecuteNonQuery();
        Debug.Log("Insert onwership "+"User ID " + userID + " aircraftID " + aircraftID+ (result == 1 ? " Success" : " Fail"));
        command.Dispose();
    }

    /// <summary>
    /// 获取当前连接状态
    /// </summary>
    /// <returns></returns>
    public static string GetState()
    {
        if (null != connection) return connection.State.ToString();
        return "No Connection";
    }
}
