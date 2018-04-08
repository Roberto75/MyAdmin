Public Class FormRegioniProvinceComuni

  
    


    Public Overrides Function _importFileSource(pathInputFile As String) As Boolean

        checkRegioniProvinceComuni()
        Windows.Forms.MessageBox.Show("Operazione conclusa con successo.", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
        Return True
    End Function

    Private Function checkRegioniProvinceComuni()
        Dim manager As New MyManager.ImmobiliareManager
        Dim managerRegioneProvinciaComune As New MyManager.RegioniProvinceComuniManager("RegioniProvinceComuni")

        Dim strSql As String
        Dim dtProvince As Data.DataTable
        Dim dtProvincia As Data.DataTable

        Dim dtComuni As Data.DataTable
        Dim dtComune As Data.DataTable

        Try
            '05/03/2012 Devo aggiornare gli id in base alla descrizione

            manager.openConnection()


            strSql = "select distinct provincia from annuncio"
            dtProvince = manager._fillDataTable(strSql)
        
            For Each row As Data.DataRow In dtProvince.Rows
                dtProvincia = managerRegioneProvinciaComune.getProvinciaByValore(row("provincia"))
                If dtProvincia.Rows.Count = 1 Then
                    ' Console.WriteLine("Provincia: " & dtProvincia.Rows(0)("ID"))
                    strSql = "UPDATE ANNUNCIO SET PROVINCIA_ID = '" & dtProvincia.Rows(0)("ID") & "' WHERE UCASE(PROVINCIA)= '" & dtProvincia.Rows(0)("VALORE").ToString().ToUpper.Replace("'", "''") & "'"
                    manager._executeNoQuery(strSql)
                Else
                    Console.WriteLine("Errore nel recupero della provincia: " & row("provicia"))
                End If
            Next




            strSql = "select distinct comune from annuncio"
            dtComuni = manager._fillDataTable(strSql)
       
            For Each row As Data.DataRow In dtComuni.Rows
                dtComune = managerRegioneProvinciaComune.getComuneByValore(row("comune"))
                If dtComune.Rows.Count = 1 Then
                    ' Console.WriteLine("Comune: " & dtComune.Rows(0)("ID"))
                    strSql = "UPDATE ANNUNCIO SET COMUNE_ID = '" & dtComune.Rows(0)("ID") & "' WHERE UCASE(COMUNE)= '" & dtComune.Rows(0)("VALORE").ToString().ToUpper.Replace("'", "''") & "'"
                    manager._executeNoQuery(strSql)
                Else
                    Console.WriteLine("Errore nel recupero del comune: " & row("comune"))
                End If
            Next

        Finally
            manager.closeConnection()
            managerRegioneProvinciaComune.closeConnection()
        End Try


        Return True
    End Function




End Class