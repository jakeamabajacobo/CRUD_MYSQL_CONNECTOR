Imports Microsoft.SqlServer
Imports MySql.Data.MySqlClient

Public Class Form1

    Dim mysql_con As New MySqlConnection
    Dim mysql_string As String
    Dim data_table As New DataTable
    Dim mysql_comm As New MySqlCommand
    Dim mysql_reader As MySqlDataReader
    Dim mysql_adapter As New MySqlDataAdapter

    Function dg_view_item_CellClick()

        Me.Text = dg_view_item.CurrentRow.Cells(0).Value
        txt_itemname.Text = dg_view_item.CurrentRow.Cells(1).Value
        txt_itemdescription.Text = dg_view_item.CurrentRow.Cells(2).Value
        txt_qty.Text = dg_view_item.CurrentRow.Cells(3).Value
        txt_price.Text = dg_view_item.CurrentRow.Cells(4).Value

    End Function

    Function PopulateTable(ByVal string_qry As String, ByVal con As MySqlConnection) As DataTable

        Try
            con.ConnectionString = "server=127.0.0.1;user id=root;password=;database=item_db"
            mysql_string = "SELECT * FROM tblitems"

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            mysql_comm.Connection = con
            mysql_comm.CommandText = string_qry
            mysql_adapter.SelectCommand = mysql_comm
            mysql_adapter.Fill(data_table)
            Return data_table
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            mysql_con.Close()
        End Try

    End Function


    Private Function DbRequest(ByVal string_query As String, ByVal sql_con As MySqlConnection)

        Try
            sql_con.ConnectionString = "server=127.0.0.1;user id=root;password=;database=item_db"

            If mysql_con.State = ConnectionState.Closed Then
                sql_con.Open()
            End If
            mysql_comm.Connection = sql_con
            mysql_comm.CommandText = string_query
            mysql_adapter.SelectCommand = mysql_comm
            mysql_comm.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            mysql_con.Close()
        End Try

    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dg_view_item.DataSource = PopulateTable("SELECT * FROM tblitems", mysql_con)
    End Sub



    Private Sub dg_view_item_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_view_item.CellClick
        dg_view_item_CellClick()
    End Sub

    Private Sub dg_view_item_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dg_view_item.CellContentClick
        dg_view_item_CellClick()
    End Sub

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click

        DbRequest("INSERT INTO tblitems(ITEMNAME,ITEMDESCRIPTION,QTY,PRICE) VALUES ('" & txt_itemname.Text & "', '" & txt_itemdescription.Text & "','" & txt_qty.Text & "','" & txt_price.Text & "')", mysql_con)
        ClearTable()
        ClearTxtBox()
        dg_view_item.DataSource = PopulateTable("SELECT * FROM tblitems", mysql_con)
    End Sub

    Function ClearTxtBox()
        txt_itemname.Clear()
        txt_itemdescription.Clear()
        txt_price.Clear()
        txt_qty.Clear()
    End Function



    Function ClearTable()
        dg_view_item.DataSource = Nothing
        data_table.Clear()
        dg_view_item.Refresh()
    End Function



    Private Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        DbRequest("UPDATE tblitems SET ITEMNAME='" & txt_itemname.Text & "',ITEMDESCRIPTION='" & txt_itemdescription.Text & "',QTY='" & txt_qty.Text & "',PRICE='" & txt_price.Text & "'  where ID='" & Me.Text & "'", mysql_con)
        ClearTable()
        ClearTxtBox()
        dg_view_item.DataSource = PopulateTable("SELECT * FROM tblitems", mysql_con)
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As EventArgs) Handles btn_delete.Click
        DbRequest("DELETE FROM tblitems where ID='" & Me.Text & "'", mysql_con)
        ClearTable()
        ClearTxtBox()
        dg_view_item.DataSource = PopulateTable("SELECT * FROM tblitems", mysql_con)
    End Sub
End Class
