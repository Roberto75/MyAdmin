Public Class FormMyRuty
    Inherits MyFormsLibrary.FormClickOnce


    Public Overloads Function _init(ByRef statusBar As Windows.Forms.ToolStripStatusLabel) As Boolean
        MyBase._init(statusBar, "MyRuty", "MyRuty.exe", _
                     "D:\CQS\public\implementazione\FrontEnd\AdminCreditoLab\testsign.pfx", _
                     "Cr3d1t0S1gnit", "http://www.trova-libro.it/", "", Framework.Framework40)
        Return True
    End Function

    Protected Overrides Function _getServersToDeploy() As Boolean
        Dim s As New List(Of MyControlsLibrary.CmbItem)
        Dim i As New MyControlsLibrary.CmbItem
        i.Label = ""
        i.Value = "http://www.trova-libro.it/"
        s.Add(i)

        Me._serverToDeploy = s
    End Function

    Public Overrides Function _updateFileConfig(ByVal item As MyControlsLibrary.CmbItem) As Boolean
        Return True
    End Function


End Class