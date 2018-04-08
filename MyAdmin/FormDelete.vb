Public Class FormDelete
    Inherits MyFormsLibrary.FormBaseDetail_3

    Private _manager As MyManager.MercatinoManager



    Private Sub MyButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton1.Click
        _manager = New MyManager.MercatinoManager()
        _manager.openConnection()
        Try
            _manager.deleteALLAnnunci()
        Finally
            _manager.closeConnection()
        End Try
        Windows.Forms.MessageBox.Show("Cancellazione conclusa con successo", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)

    End Sub

    Private Sub MyButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton2.Click
        _manager = New MyManager.MercatinoManager()
        _manager.openConnection()
        Try
            _manager.deleteALLAnnunci()
            _manager.deleteALLUtenti()
        Finally
            _manager.closeConnection()
        End Try


        Dim user As New MyManager.UserManager()
        user.openConnection()
        Try
            user.deleteALLUsersAndLogs()

        Finally
            user.closeConnection()
        End Try

        Windows.Forms.MessageBox.Show("Cancellazione conclusa con successo", My.Application.Info.ProductName, Windows.Forms.MessageBoxButtons.OK, Windows.Forms.MessageBoxIcon.Information)


    End Sub

    Private Sub MyButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyButton3.Click

        Dim userId As Long = -1

        Dim user As New MyManager.UserManager()
        user.openConnection()
        Try
            If Not String.IsNullOrEmpty(Me.TextBox2.Text) Then
                userId = user.getUtente(Me.TextBox2.Text).Tables(0).Rows(0)("USER_ID")

                If userId <> Me.TextBox2.Text Then
                    Exit Sub
                End If

            Else
                userId = user.getUserIdFromEmail(Me.TextBox1.Text)
            End If

            user.deleteUserAndLogs(userId)
        Finally
            user.closeConnection()
        End Try

        If userId <> -1 Then

            _manager = New MyManager.MercatinoManager()
            _manager.openConnection()
            Try
                _manager.deleteAnnunciByUserId(userId, "")
                _manager.deleteUser(userId)
            Finally
                _manager.closeConnection()
            End Try


        End If

    End Sub
End Class