Public Class FormCompareUtenti


    Private _userManager As New MyManager.UserManager
    Private _mercatinoManager As New MyManager.MercatinoManager





    Private Sub MyButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton1.Click
        '
        Dim utenti As Data.DataTable
        Dim utentiMercatino As Data.DataTable


        _userManager.openConnection()
        _mercatinoManager.openConnection()


        utenti = _userManager.getUtentiList()
        utentiMercatino = _mercatinoManager.getUtentiList()

        Dim usrMercatinoId As Long
        Dim userId As Long

        Dim detail As Data.DataTable

        Dim messaggio As String = ""

        Dim found As Boolean

        messaggio = "Utenti in MERCATINO non censiti in UTENTI" & vbCrLf

        For Each usrMercatino As Data.DataRow In utentiMercatino.Rows
            usrMercatinoId = usrMercatino("user_id")


            'cerco l'utente del mercatino in UTENTI
            found = False
            For Each user As Data.DataRow In utenti.Rows
                If usrMercatinoId = user("user_id") Then
                    found = True
                    Exit For
                End If
            Next

            If Not found Then

                detail = _mercatinoManager.getUtente(usrMercatinoId)

                messaggio &= usrMercatinoId & ";" & detail.Rows(0)("nome") & ";" & detail.Rows(0)("cognome") & ";" & detail.Rows(0)("my_login") & ";" & detail.Rows(0)("email") & ";" & detail.Rows(0)("date_added") & vbCrLf
            End If

        Next



        messaggio &= "Utenti in UTENTI non censiti in MERCATINO" & vbCrLf



        For Each user As Data.DataRow In utenti.Rows
            userId = user("user_id")


            'cerco l'utente del mercatino in UTENTI
            found = False
            For Each usrMercatino As Data.DataRow In utentiMercatino.Rows
                If userId = usrMercatino("user_id") Then
                    found = True
                    Exit For
                End If
            Next

            If Not found Then

                detail = _userManager.getUtente(userId).Tables(0)


                _mercatinoManager.insertUser(userId, detail.Rows(0)("nome"), detail.Rows(0)("cognome"), detail.Rows(0)("email"), detail.Rows(0)("my_login"), -1)

                messaggio &= userId & ";" & detail.Rows(0)("nome") & ";" & detail.Rows(0)("cognome") & ";" & detail.Rows(0)("my_login") & ";" & detail.Rows(0)("email") & ";" & detail.Rows(0)("date_added") & vbCrLf
            End If

        Next







        Me.TextBox1.Text = messaggio



    End Sub
End Class