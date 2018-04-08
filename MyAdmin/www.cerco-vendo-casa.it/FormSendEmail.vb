Public Class FormSendEmail


    Private _http As String = "http://www.cerco-vendo-casa.it/"
    Public _applicationName As String = "Cerco-Vendo-Casa"


    Public Overrides Function _init(ByRef connection As System.Data.Common.DbConnection, ByRef statusBar As System.Windows.Forms.ToolStripStatusLabel, ByRef progressBar As System.Windows.Forms.ToolStripProgressBar) As Boolean
        MyBase._init(connection, statusBar, progressBar)

        Me.TextBox1.Text = "inserire l'elenco dei destinatari nel seguente fomato:" & vbCrLf & _
            "email;nome"

        Return True
    End Function



    Private Sub MyButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton1.Click
        'Dim myService As New Google.GData.Spreadsheets.SpreadsheetsService("MyRuty-MyApplication-1")
        'myService.setUserCredentials("roberto.rutigliano@gmail.com", "r0b3rt0$1980")


        'Dim query As New Google.GData.Spreadsheets.SpreadsheetQuery("https://spreadsheets.google.com/feeds/spreadsheets/private/full/t_jWlNz8I0pHiKo03ZkwL-A")
        ''Dim query As New Google.GData.Spreadsheets.SpreadsheetQuery()
        'Dim feed As Google.GData.Spreadsheets.SpreadsheetFeed
        'Dim workSheet As Google.GData.Spreadsheets.WorksheetEntry
        'Dim spreadSheet As Google.GData.Spreadsheets.SpreadsheetEntry




        'feed = myService.Query(query)

        'For Each spreadSheet In feed.Entries

        '    Console.WriteLine(spreadSheet.Title.Text)

        'Next

        'For Each entry As Google.GData.Client.AtomEntry In spreadSheet.Worksheets.Entries

        '    Console.WriteLine(entry.Title.Text)

        '    If entry.Title.Text = "Agenzie" Then
        '        workSheet = entry
        '        Exit For
        '    End If

        'Next


        ''Dim queryCell As New Google.GData.Spreadsheets.CellQuery(workSheet.CellFeedLink)
        ' ''=QUERY(A:D;"SELECT B WHERE D = '' limit 10 ")
        ''queryCell.Range = "A:D"
        ''queryCell.Query = "SELECT B WHERE D = ''"
        ''Dim feddCell As Google.GData.Spreadsheets.CellFeed
        ''feddCell = myService.Query(queryCell)


        ''For Each i As Google.GData.Client.AtomEntry In feddCell.Entries

        ''    Console.WriteLine(i.Title.Text)
        ''Next



        ''Dim l As Google.GData.Spreadsheets.ListFeed = workSheet.Links.FindService (Google.GData.Spreadsheets.Links

        ''Dim queryList As New Google.GData.Spreadsheets.ListQuery(workSheet.CellFeedLink, workSheet.Id.AbsoluteUri, "private", "")
        ''Dim queryList As New Google.GData.Spreadsheets.ListQuery(workSheet.li)
        ''queryList.SpreadsheetQuery = "SELECT B WHERE D = ''"

        ''Dim feedList As Google.GData.Spreadsheets.ListFeed
        ''feedList = myService.Query(queryList)


        ''For Each i As Google.GData.Spreadsheets.ListEntry In feedList.Entries

        ''    Console.WriteLine(i.Title.Text)
        ''Next


    End Sub

    Private Sub MyButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton2.Click
        If System.Windows.Forms.MessageBox.Show("Confermare l'invio delle email?", My.Application.Info.ProductName, System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK Then
            If sendEmail() Then
                System.Windows.Forms.MessageBox.Show("Invio email completato con successo", My.Application.Info.ProductName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Question)
            End If
        End If
    End Sub



    Private Function sendEmail() As Boolean


        If String.IsNullOrEmpty(Me.TextBox1.Text) Then
            Return False
        End If

        Dim messaggio As String

        '*** invio email ***
        Dim myEmail As New MyManager.MailManager

        myEmail._Subject = "Cerco Vendo Casa - nuovo portale di ricerca immobili da oggi on line"
        myEmail._From("info@cerco-vendo-casa.it")

        messaggio = "Gentile utente,"
        messaggio &= "<p>Cerco-Vendo-Casa.it è un nuovo portale che fornisce uno strumento gratuito ed efficace per chi ha necessità di promuovere immobili in internet.</p>" & vbCrLf
        messaggio &= "<p>Attraverso una semplice procedura di registrazione, infatti, è possibile inserire un  annuncio immobiliare nel sito di Cerco-Vendo-Casa.it corredandolo di foto e geolocalizzazione tramite google-maps.</p>" & vbCrLf
        messaggio &= "<p>In questo modo  l’immobile sarà visibile a migliaia di possibili utenti interessati che visiteranno il sito.</p>" & vbCrLf
        messaggio &= "<p>Cerco-Vendo-Casa.it è ottimizzato in modo che gli annunci presenti nel proprio data-base siano facilmente accessibili anche tramite richieste sui più comuni motori di ricerca (Google, Bing).</p>" & vbCrLf
        messaggio &= "<p>Cerco-Vendo-Casa.it, inoltre, garantisce la completa riservatezza dei dati sensibili per tutelare i suoi utenti da eventuali utilizzi da parte di software automatici che possano sfruttarli indebitamente.</p>"
        messaggio &= "<p>Ti invitiamo quindi a visitare il nostro portale <a href=""" & _http & """>" & _http & "</a> certi che i nostri servizi ti soddisferanno e ti invitiamo a contattarci a info@cerco-vendo-casa.it per qualsiasi suggerimento ed eventuale richiesta o curiosità.</p>"

        messaggio &= "<br />"

        messaggio &= getFirma()

        myEmail._Body = messaggio

        Dim destinatario As String
        Dim email As String
        Dim contaRigheLette As Long = 1
        Dim contaEmailInviate As Long = 0

        Dim temp() As String

        For Each line As String In Me.TextBox1.Lines

            'temp = line.Split(",")
            ' temp = line
            ' If temp.Length = 3 Then
            'destinatario = line.Split(",")(0)
            ' email = temp(2)


            If Not String.IsNullOrEmpty(line) Then

                email = line.Trim


                If MyManager.RegularExpressionManager.isValidEmail(email) Then
                    'messaggio = String.Format("Gentile {0} {1}, " & vbCrLf, nome, cognome)

                    myEmail._ToClearField()
                    myEmail._To(email)
                    'myEmail._To("roberto.rutigliano@gmail.com")
                    myEmail.send()

                    contaEmailInviate = contaEmailInviate + 1
                Else

                End If


            End If
            _statusBarUpdate(String.Format("Email inviate {0:N0}/{1:N0}", contaEmailInviate, contaRigheLette))
            System.Windows.Forms.Application.DoEvents()
            contaRigheLette = contaRigheLette + 1
            System.Threading.Thread.Sleep(300)
        Next
       

        Return True
    End Function


    Public Overridable Function getFirma() As String
        Dim risultato As String = ""

        risultato = "<br />Cordiali saluti dallo staff di " & _applicationName & "." & vbCrLf & _
              "<br><br><br> " & _applicationName & vbCrLf & _
              "<br><a href=""" & _http & """>" & _http & "</a>" & vbCrLf
        Return risultato
    End Function

End Class