﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class UserLogIn : MonoBehaviour
{
    public Text userNameInput;
    public Text passwordInput;

    public Image mes;
    //IP地址
    public string host;
    //端口号
    public string port;
    //用户名
    public string userName;
    //密码
    public string password;
    //数据库名称
    public string databaseName;
    //封装好的数据库类
    MySqlAccess mysql;
    // Start is called before the first frame update
    void Start()
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLogInBtnClick() {
        mysql.OpenSql();
        //获取数据
        DataSet ds = mysql.Select("users", new string[] { "user_password","user_id" }, new string[] { "user_name"}, new string[] { "="}, new string[] { userNameInput.text,});
        if (ds != null) {
            DataTable table = ds.Tables[0];
            List<string>data=new List<string>();
            foreach (DataRow row in table.Rows) {
                foreach (DataColumn col in table.Columns) {
                    data.Add(row[col].ToString());
                }
            }
            if (data[0]==passwordInput.text) {
                Debug.Log("登陆成功");
                mes.color = Color.green;
            }
            else {
                Debug.Log("登陆失败");
                mes.color = Color.red;
            }
        }
        else {
            Debug.Log("登陆失败");
            mes.color = Color.red;
        }
        mysql.CloseSql();
    }
}