Imports Microsoft.SqlServer
Imports MySql.Data.MySqlClient

Public Class Form1

    Public mysql_con As New MySqlConnection
    Public mysql_string As String
    Public data_set As New DataSet
    Public mysql_comm As MySqlCommand
    Public mysql_reader As MySqlDataReader
    Public mysql_adapter As MySqlDataAdapter



    Function PopulateTable(ByVal string_qry As String, ByVal con As New )

    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            mysql_con = New MySqlConnection()
            mysql_con.ConnectionString = "server=127.0.0.1;user id=root;password=;database=inventory_db"

            If mysql_con.State = ConnectionState.Closed Then
                mysql_con.Open()
                MsgBox("database connected!")
            End If


            mysql_string = "SELECT * FROM tlbitems"
            mysql_comm.Connection = mysql_con
            mysql_comm.CommandText = mysql_string
            mysql_adapter.SelectCommand = mysql_comm
            mysql_adapter.Fill(data_set)
            dg_view_item.DataSource = data_set



        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            mysql_con.Close()
            mysql_adapter.Dispose()
        End Try




    End Sub
End Class
