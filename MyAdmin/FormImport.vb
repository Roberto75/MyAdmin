Public Class FormImport
    Inherits MyFormsLibrary.FormBaseDetail_3

    Private _manager As TrovaLibroManager


    Private Sub MyButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton1.Click
        _manager = New TrovaLibroManager()


        _manager._setStatusBar(CType(Me.Owner, FormMain).MyStatusBar)
        _manager.openConnection()
        If Not _manager.importFromOldDatabse() Then
            Windows.Forms.MessageBox.Show("Importazione conclusa con ERRORI", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Hand)
            Exit Sub
        End If

        If Windows.Forms.MessageBox.Show("Importazione conclusa con successo. Inviare l'email di notifica?", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information) <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If


    End Sub

    Private Sub MyButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class