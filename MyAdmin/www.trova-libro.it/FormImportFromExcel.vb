Public Class FormImportFromExcel

    Dim _excelManager As MyManager.ExcelManager
    Dim _manager As MyManager.MercatinoManager
    Dim _categorieManager As MyManager.CategorieManager
    Private _objExcel As Microsoft.Office.Interop.Excel.Application



    Public Overloads Function _init(ByRef connection As System.Data.Common.DbConnection _
        , ByRef statusBar As System.Windows.Forms.ToolStripStatusLabel _
        , ByRef progressBar As System.Windows.Forms.ToolStripProgressBar _
        , ByRef objExcel As Microsoft.Office.Interop.Excel.Application) As Boolean

        MyBase._init(connection, statusBar, progressBar)

        Me.SplitContainer1.FixedPanel = FixedPanel.Panel1

        If objExcel Is Nothing Then
            objExcel = New Microsoft.Office.Interop.Excel.Application
        End If

        _objExcel = objExcel

        _excelManager = New MyManager.ExcelManager(_connection, _objExcel)
        _manager = New MyManager.MercatinoManager(_connection)
        _categorieManager = New MyManager.CategorieManager(_connection)



        '********
        Me.txtLogin.Text = "sferr"
        Me.txtEmail.Text = "solelibri@katamail.com"
        Me.txtProvincia.Text = "PR"


        Return True
    End Function

    Public Overrides Function _OnTabClosing() As Boolean
        If Not _objExcel Is Nothing Then
            _objExcel.Quit()
        End If
        Return MyBase._OnTabClosing()
    End Function

    Public Overrides Function _importFileSource(ByVal pathInputFile As String) As Boolean
        Me._pathInputFile = pathInputFile
        Return True
    End Function

    Private Sub MyButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton1.Click

        Me._ErrorProvider.SetError(Me.txtPathSource, "")
        If String.IsNullOrEmpty(Me._pathInputFile) Then
            Me._ErrorProvider.SetError(Me.txtPathSource, "Selezionare un file Excel da importare")
            Exit Sub
        End If

        Me._ErrorProvider.SetError(Me.txtLogin, "")
        If String.IsNullOrEmpty(Me.txtLogin.Text) Then
            Me._ErrorProvider.SetError(Me.txtLogin, "Digitare la login")
            Exit Sub
        End If

        Me._ErrorProvider.SetError(Me.txtEmail, "")
        If String.IsNullOrEmpty(Me.txtEmail.Text) Then
            Me._ErrorProvider.SetError(Me.txtEmail, "Digitare l'email")
            Exit Sub
        End If


        Me._ErrorProvider.SetError(Me.txtProvincia, "")
        If String.IsNullOrEmpty(Me.txtProvincia.Text) Then
            Me._ErrorProvider.SetError(Me.txtProvincia, "Digitare la provincia")
            Exit Sub
        End If

        'verifico login + email
        Dim userId As Long
        userId = _manager.getUserIdFromLoginAndEmail(Me.txtLogin.Text, Me.txtEmail.Text)

        If userId = -1 Then
            Windows.Forms.MessageBox.Show("Impossibile determinare un utente. Verificare login ed email.", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            Exit Sub
        End If




        If executeImport(True, userId) Then
            If Windows.Forms.MessageBox.Show("Verifica dei dati conclusa con successo." & vbCrLf & "Proseguire con la procedura di caricamento?", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.OK Then
                _excelManager._closeFileExcel()
                Exit Sub
            End If

            executeImport(False, userId)



            '22/02/2012
            'SIMULAZIONE DI 16000 LIBRI
            For i As Int16 = 1 To 16
                executeImport(False, userId)
            Next


            _excelManager._closeFileExcel()
        Else
            Windows.Forms.MessageBox.Show("Verifica dei dati conclusa con errori", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Error)
            _excelManager._showExcel()
        End If



    End Sub


    Private Function executeImport(ByVal MODE_TEST As Boolean, ByVal userId As Long) As Boolean
        _excelManager._openFileExcel(Me._pathInputFile)



        Dim i As Long = 2
        Dim esito As Boolean = True
        Try

            Me._progressBar.Style = ProgressBarStyle.Marquee

            Dim indice As Long

            With _excelManager
                ._workSheet = ._objExcel.Worksheets(2)

                Do While Not String.IsNullOrEmpty(._workSheet.Cells(i, "B").Value)
                    ._cell = ._workSheet.Cells(i, "B")
                    Console.WriteLine(._cell.Value)


                    If MODE_TEST Then
                        ._cell = ._workSheet.Cells(i, "B")
                        Try
                            Me.decodeTipoAnnuncio(._cell.Value)
                        Catch ex As MyManager.ManagerException
                            ._cell.Interior.Color = RGB(255, 0, 0)
                            esito = False
                        End Try


                        ._cell = ._workSheet.Cells(i, "C")
                        If _categorieManager.decodeCategoria(._cell.Value) = -1 Then
                            ._cell.Interior.Color = RGB(255, 0, 0)
                            esito = False
                        Else
                            Dim debugg As Boolean
                            debugg = True
                            ._cell.Interior.Color = RGB(255, 255, 255)
                        End If


                        'PREZZO è obbligatorio!!! 
                        ._cell = ._workSheet.Cells(i, "H")
                        If formatPrezzo(._cell.Value) = -1 Then
                            ._cell.Interior.Color = RGB(255, 0, 0)
                            esito = False
                        End If

                        Me._statusBarUpdate(String.Format("Check annuncio° {0:N0}", i - 1))
                    Else
                        _manager._tipoAnnuncio = Me.decodeTipoAnnuncio(._workSheet.Cells(i, "B").value)
                        _manager._titolo = ._workSheet.Cells(i, "D").value
                        _manager._autore = ._workSheet.Cells(i, "E").value
                        _manager._casaEditrice = ._workSheet.Cells(i, "F").value
                        _manager._isbn = ._workSheet.Cells(i, "G").value
                        _manager._prezzo = formatPrezzo(._workSheet.Cells(i, "H").value)
                        _manager._nota = ._workSheet.Cells(i, "I").value
                        _manager._categoriaId = _categorieManager.decodeCategoria(._workSheet.Cells(i, "C").value)
                        _manager._regione = "Emilia-Romagna"
                        _manager._provincia = "Parma"


                        _manager.insertAnnuncio(userId)

                        Me._statusBarUpdate(String.Format("Insert annuncio° {0:N0}", i - 1))
                    End If
                    i = i + 1
                Loop
            End With


            If Not MODE_TEST Then
                Windows.Forms.MessageBox.Show("Caricamento di " & (i - 2) & " movimenti concluso con successo.", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)
                Me._statusBarUpdate("")
            End If

        Finally
            Me._progressBar.Style = ProgressBarStyle.Blocks

        End Try

        Return esito

    End Function



    Private Function formatPrezzo(ByVal value As String) As Decimal
        If Not String.IsNullOrEmpty(value) Then
            Dim temp As String
            temp = value.Trim
            temp = temp.Replace(".", ",")
            '22/09/2010 la funzione IsNumeric mi considera valido anche il simbolo dell'EURO
            temp = temp.Replace("€", "")
            temp = temp.Replace("$", "")
            temp = temp.Replace("£", "")

            Return Decimal.Parse(temp)
        Else
            Return -1
        End If
    End Function
    
    Private Function decodeTipoAnnuncio(ByVal value As String) As MyManager.MercatinoManager.TipoAnnuncio
        Dim temp As String

        temp = value.ToLower

        Select Case temp
            Case "vendo"
                Return MyManager.MercatinoManager.TipoAnnuncio.Vendo
            Case "compro"
                Return MyManager.MercatinoManager.TipoAnnuncio.Compro
            Case "scambio"
                Return MyManager.MercatinoManager.TipoAnnuncio.Scambio
            Case Else
                Throw New MyManager.ManagerException("Tipo annuncio NON riconosciuto: " & value)
        End Select


    End Function



End Class