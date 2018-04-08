Public Class FormTest

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        test1()
    End Sub



    Private Sub test1()

        Dim manager As New MyManager.ImmobiliareManager()
        manager.openConnection()

        Try
            manager.updateLatitude(17.581079799999998)

        Finally
            manager.closeConnection()

        End Try

      

    End Sub
End Class