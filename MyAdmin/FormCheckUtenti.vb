Public Class FormCheckUtenti
    Private _userManager As New MyManager.UserManager
    Private _mercatinoManager As New MyManager.MercatinoManager
    
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim utenti As Data.DataTable
        _userManager.openConnection()
        _mercatinoManager.openConnection()

        utenti = _userManager.getUtentiList()

        Dim messaggioLOG As String = ""
        Dim login As String
        Dim loginNew As String



        Dim myEmail As New MyManager.MailManager
        myEmail._MailServer = ""
        myEmail._Subject = "Trova-Libro - Aggiornamento della login"
        myEmail._From("info@trova-libro.it")
        myEmail._Cc("roberto.rutigliano@gmail.com")


        Dim messaggio As String


        For Each user As Data.DataRow In utenti.Rows
            login = user("my_login")
            If Not MyManager.RegularExpressionManager.isValidLogin(login) Then
           
                loginNew = login.Replace(" ", "")

                If MyManager.RegularExpressionManager.isValidLogin(loginNew) Then
                    _userManager._login = loginNew
                    _userManager.updateUser(user("user_id"), loginNew, "")
                    _mercatinoManager.updateUser(user("user_id"), loginNew, "")

                    messaggioLOG &= login & "  - USER_ID: " & user("user_id") & " - EMAIL: " & user("email") & " - NOME: " & user("NOME") & " - COGNOME: " & user("COGNOME") & " - LAST LOGIN: " & user("date_last_login") & " - DATE ADDED: " & user("date_added") & " - NEW LOGIN: " & loginNew & vbCrLf



                    If login.Trim <> loginNew.Trim Then

                    
                        '*** EMAIL ***

                        messaggio = "Gentile utente, <br />"
                        messaggio &= "La sua vecchia login conteneva dei caratteri che non rispettano le policy di sicurezza e di conseguenza è stata aggiornata. <br />"
                        messaggio &= "<p>La sua vecchia login era la seguente: " & login & "</p>"

                        messaggio &= "<p>La sua <b>nuova</b> login è la seguente:  <b>" & loginNew & "</b><p>"

                        messaggio &= "<p>Probabilmente la sua vecchia login conteneva spazi vuoti.</p>" & vbCrLf
                        messaggio &= "<p>La password è rimasta la stessa. In ogni modo può rigenerala utilizzando la funzione ""Genera password"" del menu utente. </p> <br />" & vbCrLf
                        
                        messaggio &= "<p>Per qualsiasi problema o chiarimento ci può contattare al seguente indirizzo: info@trova-libro.it</p>"
                        messaggio &= "<br /><p>Cordiali saluti dallo staff di Trova-libro</p>"

                        myEmail._Body = messaggio

                        myEmail._ToClearField()
                        myEmail._To(user("email"))

                        myEmail.send()
                    End If


                End If
            End If
        Next

        _userManager.closeConnection()
        _mercatinoManager.closeConnection()

        Me.TextBox1.Text = messaggioLOG
    End Sub
End Class