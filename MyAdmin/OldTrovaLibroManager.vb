Public Class OldTrovaLibroManager
    Inherits MyManager.Manager

    Public Sub New()
        MyBase.New("OLDmercatino")
    End Sub

    Public Sub New(ByVal connectionName As String)
        MyBase.New(connectionName)
    End Sub

    Public Sub New(ByVal connection As System.Data.Common.DbConnection)
        MyBase.New(connection)
    End Sub


    Public Function getAnnunci() As Data.DataTable
        Dim strSql As String
        strSql = "SELECT * FROM ANNUNCIO WHERE data >= #1/1/2010# AND bCancellato = false"
        Return _fillDataTable(strSql)
    End Function



    Public Function getUtenti() As Data.DataTable
        Dim strSQL As String
        strSQL = "SELECT email, Count(*) AS TOT" & _
                    " FROM Annuncio " & _
                    " WHERE (data >= #1/1/2010#  AND email <> ' ' )" & _
                    " GROUP BY email" & _
                    " Having count(*) > 1" & _
                    " ORDER BY Count(*) DESC"
        Return _fillDataTable(strSQL)
    End Function

    Public Function getAnnunciFromEmail(ByVal email As String) As Data.DataTable
        Dim strSQL As String
        strSQL = "SELECT *" & _
                    " FROM Annuncio " & _
                    " WHERE  email  =  @EMAIL " & _
                    " ORDER BY data DESC "

        Dim command As System.Data.Common.DbCommand
        command = _connection.CreateCommand()
        command.CommandText = strSQL

        Me._addParameter(command, "@EMAIL", email)


        Return _fillDataSet(command).Tables(0)

    End Function


End Class
