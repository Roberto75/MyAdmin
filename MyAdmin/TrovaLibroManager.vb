Public Class TrovaLibroManager
    Inherits MyManager.Manager
    Public Sub New()
        MyBase.New("mercatino")
    End Sub

    Public Sub New(ByVal connectionName As String)
        MyBase.New(connectionName)
    End Sub

    Public Sub New(ByVal connection As System.Data.Common.DbConnection)
        MyBase.New(connection)
    End Sub


    Private _userToBeImported As Data.DataTable
    Private _annunciToBeImported As Data.DataTable



    Public Function importFromOldDatabse() As Boolean

        'Dim _importUserId As Long
        '_strSQL = "SELECT user_id FROM UTENTI WHERE NOME = '---' and MY_LOGIN = '---'"
        '_importUserId = _executeScalar(_strSQL)



        Dim old As New OldTrovaLibroManager()
        old.openConnection()
        _userToBeImported = old.getUtenti()
        _annunciToBeImported = old.getAnnunci()
        old.closeConnection()



        importUser()
        importAnnunci()




        Return True
    End Function



    Private Function importUser() As Boolean
        Dim manager As New MyManager.UserManager()
        Dim mercatino As New MyManager.MercatinoManager()
        Dim old As New OldTrovaLibroManager()


        manager.openConnection()
        mercatino.openConnection()
        old.openConnection()

        Dim newId As Long
        Dim conta As Long = 0
        Dim dt As Data.DataTable
        Dim check As Long
        For Each row As Data.DataRow In _userToBeImported.Rows

            dt = old.getAnnunciFromEmail(row("email"))

            manager._nome = dt.Rows(0)("nome").ToString.Replace(" ", "")
            manager._cognome = dt.Rows(0)("cognome").ToString.Replace(" ", "")
            manager._email = row("email")

            manager._login = manager.creaLogin(manager._nome, manager._cognome)

            'manager._login = dt.Rows(0)("nome").ToString.Replace(" ", "") & "." & dt.Rows(0)("cognome").ToString.Replace(" ", "")

            manager._login = manager._login.ToLower
            ' manager._indirizzo = Me.indirizzo.Text
            ' manager._numero_civico = Me.numero_civico.Text
            'manager._provincia = row("")
            'manager._citta = Me.citta.Text
            'manager._cap = Me.cap.Text
            'manager._fax = Me.fax.Text
            'manager._http = Me.http.Text
            'manager._sesso = Me.sesso.SelectedValue

            'manager._tipologia = Me.tipologia.SelectedValue

            'manager._photo = False
            'manager._password = Me.mypassword.Text
            'manager._telefono = Me.telefono.Text

            manager._isEnabled = True

            'inserisco sul DB utenti 
            Try
                check = manager.getUserIdFromEmail(manager._email)
            Catch ex As MyManager.ManagerException
                check = 0
            End Try

            If check = 0 Then
                newId = manager.insert()
                'inserisco sul db del mercatino
                mercatino.insertUser(newId, manager._nome, manager._cognome, manager._email, manager._login, -1)
                conta = conta + 1
            End If

            If Not _statusBar Is Nothing Then
                _statusBar.Text = String.Format("Utente {0}/{1}", conta, _userToBeImported.Rows.Count)
                System.Windows.Forms.Application.DoEvents()
            End If
           
        Next

        _strSQL = "UPDATE UTENTI SET DATE_ADDED = NULL"

        mercatino.closeConnection()
        manager.closeConnection()
        old.closeConnection()

        Return True
    End Function



    Private Function importAnnunci() As Boolean
        Dim manager As New MyManager.MercatinoManager()
        Dim userManager As New MyManager.UserManager()

        manager.openConnection()
        userManager.openConnection()

        Dim userId As Long
        Dim annuncioId As Long

        Dim conta As Long = 0
        Dim check As Boolean

        Dim command As System.Data.Common.DbCommand
        
        For Each row As Data.DataRow In _annunciToBeImported.Rows

            Try
                userId = userManager.getUserIdFromEmail(row("email"))
                check = True
            Catch ex As Exception
                check = False
            End Try
            


            'verifico che sia un utenti ANCHE del mercatino ...
            If check AndAlso (Not manager.getUtente(userId) Is Nothing) Then

                Select Case row("tipoAnnuncio").ToString.ToLower
                    Case "vendo"
                        manager._tipoAnnuncio = MyManager.MercatinoManager.tipoAnnuncio.vendo
                    Case "compro"
                        manager._tipoAnnuncio = MyManager.MercatinoManager.tipoAnnuncio.compro
                    Case "scambio"
                        manager._tipoAnnuncio = MyManager.MercatinoManager.tipoAnnuncio.scambio
                    Case Else
                        Throw New MyManager.ManagerException("tipoAnnuncio non gestito:" & row("tipoAnnuncio").ToString.ToLower)
                End Select

                'If IsDBNull(row("prezzo")) OrElse String.IsNullOrEmpty(row("prezzo").ToString.Trim) Then
                '    manager._prezzo = 0
                'Else
                '    Try
                '        manager._prezzo = Decimal.Parse(row("prezzo").ToString.Replace(".", ","))
                '    Catch ex As Exception
                '        manager._prezzo = 0
                '    End Try
                'End If

                If IsDBNull(row("prezzo")) OrElse String.IsNullOrEmpty(row("prezzo")) OrElse Not IsNumeric(row("prezzo")) Then
                    manager._prezzo = 0
                Else
                    manager._prezzo = Decimal.Parse(row("prezzo").ToString.Replace(".", ","))
                End If

                manager._titolo = row("titolo")
                manager._casaEditrice = row("casaEditrice")
                manager._autore = row("autore")
                manager._nota = row("nota")


                Select Case row("genere")
                    Case 1, 2, 3
                        manager._categoriaId = 1060000
                        '1060000:            Narrativa(italiana)
                        '1070000:            Narrativa(straniera)
                    Case 4
                        '1141700 testi universitari - altro
                        manager._categoriaId = 1141700
                    Case 5
                        manager._categoriaId = 1170000
                    Case 6
                        manager._categoriaId = 1130400
                    Case 7
                        manager._categoriaId = 1140200
                    Case 8
                        manager._categoriaId = 1140300
                    Case 9
                        manager._categoriaId = 1140400
                    Case 10
                        manager._categoriaId = 1140500
                    Case 11
                        manager._categoriaId = 1140600
                    Case 12
                        manager._categoriaId = 1140700
                    Case 13
                        manager._categoriaId = 1140800
                    Case 14
                        manager._categoriaId = 1140900
                    Case 15
                        manager._categoriaId = 1141000
                    Case 16
                        manager._categoriaId = 1141100
                    Case 17
                        manager._categoriaId = 1141200
                    Case 18
                        manager._categoriaId = 1141300
                    Case 19
                        manager._categoriaId = 1141400
                    Case 20
                        manager._categoriaId = 1141500
                    Case 21
                        manager._categoriaId = 1141600
                    Case Else
                        Throw New MyManager.ManagerException("categoria non riconosciuta" & row("genere").ToString)
                End Select

                ' Try
                annuncioId = manager.insertAnnuncio(userId)

                command = manager.getConnection.CreateCommand()
                _strSql = "UPDATE ANNUNCIO SET DATE_ADDED = @DATE_ADDED WHERE annuncio_id = " & annuncioId
                _addParameter(command, "@DATE_ADDED", row("DATA"))
                command.CommandText = _strSql
                _executeNoQuery(command)

                conta = conta + 1
                'Catch ex As Exception
                'Dim a As String
                'a = "a"
                'End Try



                If Not _statusBar Is Nothing Then
                    _statusBar.Text = String.Format("Annuncio {0}/{1}", conta, _annunciToBeImported.Rows.Count)
                    System.Windows.Forms.Application.DoEvents()
                End If

            End If

        Next



        manager.closeConnection()
        userManager.closeConnection()


    End Function



End Class
