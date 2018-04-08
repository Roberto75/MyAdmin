Public Class FormSendEmailInvitation
    Private _http As String = "http://www.trova-libro.it/"
    Public _applicationName As String = "Trova-Libro"

    Private Sub MyButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton1.Click
        Me.sendEmail()
    End Sub


    Public Overridable Function getFirma() As String
        Dim risultato As String = ""
   
        risultato = "<br />Cordiali saluti dallo staff di " & _applicationName & "." & vbCrLf & _
              "<br><br><br> " & _applicationName & vbCrLf & _
              "<br><a href=""" & _http & """>" & _http & "</a>" & vbCrLf
        Return risultato
    End Function


    Private Sub sendEmail()

        '*** invio email ***
        'ricavo i dati delle persone a cuin iviare le email

        Dim usersManager As New MyManager.UserManager()
        usersManager.openConnection()


        Dim dt As Data.DataTable
        dt = usersManager.getUtentiList()
        Dim count As Long = 1

        Dim userId As Long
        Dim email As String
        Dim nome, cognome, login As String
        Dim messaggio As String = ""

        Dim myEmail As New MyManager.MailManager
        myEmail._MailServer = ""
        myEmail._Subject = "Trova-Libro NEWS"
        myEmail._From("info@trova-libro.it")


        Try

            For Each row As Data.DataRow In dt.Rows

                userId = row("user_id")
                email = row("email")

                'DEBUGG
                '  email = "roberto.rutigliano@gmail.com"

                login = row("my_login")

                If IsDBNull(row("nome")) Then
                    nome = ""
                Else
                    nome = row("nome")
                End If


                If IsDBNull(row("cognome")) Then
                    cognome = ""
                Else
                    cognome = row("cognome")
                End If



                messaggio = String.Format("Gentile {0} {1}, " & vbCrLf, nome, cognome)
                messaggio &= "<p>Abbiamo aggiunto la possibilià di inserire delle <b>immagini</b> agli annunci.</p>" & vbCrLf
                messaggio &= "<p>Accedi al menu ""I miei annunci"" e clicca sull'annuncio di interesse. </p>" & vbCrLf
                messaggio &= "<p>A questo punto clicca sulla sezione ""Photo"" ed esegui l'upload delle immagini. </p>" & vbCrLf
                messaggio &= "<br />"
                messaggio &= "<p>Per qualsiasi tipo di problema scrivici a webmaster@trova-libro.it</p>"

                messaggio &= getFirma()


                myEmail._Body = messaggio
                myEmail._ToClearField()
                myEmail._To(email)

                myEmail.send()



                _statusBarUpdate(String.Format("Record {0:N0}/{1:N0}", count, dt.Rows.Count))
                System.Windows.Forms.Application.DoEvents()
                count = count + 1
            Next
        Finally
            usersManager.closeConnection()
        End Try


    End Sub


    Private Sub sendEmailInvitation()

        '*** invio email ***
        'ricavo i dati delle persone a cuin iviare le email

        Dim usersManager As New MyManager.UserManager()
        usersManager.openConnection()


        Dim dt As Data.DataTable
        dt = usersManager.getUtentiList()



        Dim codiceAttivazione As String
        Dim userId As Long
        Dim email As String
        Dim nome, cognome, login As String
        Dim messaggio As String = ""

        Dim myEmail As New MyManager.MailManager
        myEmail._MailServer = ""
        myEmail._Subject = "Trova-Libro NEWS"
        myEmail._From("info@trova-libro.it")


        Try

            For Each row As Data.DataRow In dt.Rows

                userId = row("user_id")
                email = row("email")

                'DEBUGG
                email = "roberto.rutigliano@gmail.com"

                login = row("my_login")

                If IsDBNull(row("nome")) Then
                    nome = ""
                Else
                    nome = row("nome")
                End If


                If IsDBNull(row("cognome")) Then
                    cognome = ""
                Else
                    cognome = row("cognome")
                End If


                codiceAttivazione = usersManager.getNewCodiceAttivazione(userId)



                messaggio &= "" & vbCrLf
                messaggio = String.Format("Gentile {0} {1}, " & vbCrLf, nome, cognome)
                messaggio &= "Siamo lieti di comuncarvi la pubblicazione del nuovo sito di Trova-Libro. <br />" & vbCrLf
                messaggio &= "Oltre alla nuova veste grafica, sono state aggiunte e corrente alcune funzionalità tra cui la cancellazione degli annunci. <br />" & vbCrLf
                messaggio &= "Per rispetto delle norme sulla sicurezza abbiamo introdotto una piccola procedura di registrazione. <br />" & vbCrLf
                messaggio &= "I suoi vecchi annunci sono stati automaticamente inseriri nel nuovo sistema e avrà modo di modificarli o cancellartli in qualsisi momento. <br />" & vbCrLf
                messaggio &= "Per rendere attivo il tuo account devi completare la procedura di registrazione al seguente indirizzo <br />" & vbCrLf


                'messaggio &= "<a href=""" & _http & "utenti/activeAccount.aspx?uid=" & Guid.NewGuid.ToString & """ traget=""_blank"">Attiva account</a> <br />" & vbCrLf

                'messaggio &= "copia e incolla il seguente indirizzo nel tuo browser:"
                messaggio &= "" & _http & "utenti/activeAccount.aspx?uid=" & codiceAttivazione

                messaggio &= "<p>Se hai incontrato problemi durante la procedura di registrazione scrivici a info@trova-libro.it</p>"

                myEmail._Body = messaggio
                myEmail._ToClearField()
                myEmail._To(email)

                myEmail.send()

            Next
        Finally
            usersManager.closeConnection()
        End Try

    End Sub






End Class