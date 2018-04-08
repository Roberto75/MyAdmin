Public Class FormOutlook

    Private _manager As MyManager.OutlookManager


    Private Sub btnOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFile.Click
        Dim OpenFileDialog1 As New Windows.Forms.OpenFileDialog
        OpenFileDialog1.Filter = "Outlook (*.pst)|*.pst;"

        OpenFileDialog1.InitialDirectory = Application.ExecutablePath
        OpenFileDialog1.Title = "Caricamento File"
        'OpenFileDialog1.FileName = "source.xls"

        If (OpenFileDialog1.ShowDialog(tabPageMain) = Windows.Forms.DialogResult.OK) Then

            'If Windows.Forms.MessageBox.Show("Procedere con il caricamento del file?", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OKCancel, Windows.Forms.MessageBoxIcon.Question) <> Windows.Forms.DialogResult.OK Then
            '         Exit Sub
            '    End If
            Me.txtPathSource.Text = OpenFileDialog1.FileName
            Application.DoEvents()
        End If




    End Sub

    Private Sub MyButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton1.Click
        _manager = New MyManager.OutlookManager()


        '        _manager.getAllContact()

        _manager.importFile()



    End Sub
End Class