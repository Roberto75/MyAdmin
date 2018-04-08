Public Class FormMain
    Inherits MyFormsLibrary.FormMaster
    Implements MyFormsLibrary.IFormMaster

    Public Overrides Function _buildMenuTop() As Boolean Implements MyFormsLibrary.IFormMaster.BuildMenuTop
        Return True
    End Function

    Public Overrides Function _menuTopSubItemClicked(ByVal clickedItem As System.Windows.Forms.ToolStripItem) As Boolean Implements MyFormsLibrary.IFormMaster.MenuTopSubItemClicked
        Return True
    End Function

    Public Overrides Function _buildMenuLeft() As Boolean Implements MyFormsLibrary.IFormMaster.BuildMenuLeft

        Me.UcMenuLeftStack1._init()

        Me.UcMenuLeftStack1._addGroup("www.trova-libro.it")
        Me.UcMenuLeftStack1._addGroup("www.cerco-vendo-casa.it")
        Me.UcMenuLeftStack1._addGroup("MyRuty")


        UcMenuLeftStack1_MyGroupOnClickMenuLeftStack("www.cerco-vendo-casa.it")

        Return True
    End Function

    Private Sub UcMenuLeftStack1_MyGroupOnClickMenuLeftStack(ByVal group As System.String) Handles UcMenuLeftStack1.MyMenuLeftStackGroupOnClick
        UcMenuLeftTreeView1._clear()

        Dim myNode As System.Windows.Forms.TreeNode

        Select group
            Case "www.trova-libro.it"

                myNode = Me.UcMenuLeftTreeView1._addNodeLevel_1("nAnnunci", "Annunci")
                Me.UcMenuLeftTreeView1._addNodeLevel_2("nDelete", "Delete", myNode)
                Me.UcMenuLeftTreeView1._addNodeLevel_2("nImportOLD", "Import from OLD DataBase", myNode)
                Me.UcMenuLeftTreeView1._addNodeLevel_2("nImportFromExcel", "Import from Excel", myNode)

                myNode = Me.UcMenuLeftTreeView1._addNodeLevel_1("nUtenti", "Utenti")
                Me.UcMenuLeftTreeView1._addNodeLevel_2("nCompare", "Compare", myNode)
                Me.UcMenuLeftTreeView1._addNodeLevel_2("nCheck", "Check utenti", myNode)

                myNode = Me.UcMenuLeftTreeView1._addNodeLevel_1("nEmail", "Email")
                Me.UcMenuLeftTreeView1._addNodeLevel_2("nSendInvitation", "Invia codice invito", myNode)
                'Me.UcMenuLeftTreeView1._addNodeLevel_2("nChart1", "Chart1", myNode)
            Case "www.cerco-vendo-casa.it"
                myNode = Me.UcMenuLeftTreeView1._addNodeLevel_1("nEmail", "Email")
                Me.UcMenuLeftTreeView1._addNodeLevel_2("casaEmailAgenzie", "Invito Agenzie", myNode)
                Me.UcMenuLeftTreeView1._addNodeLevel_2("casaEmailPrivati", "Invito Privati", myNode)

            Case "MyRuty"
                myNode = Me.UcMenuLeftTreeView1._addNodeLevel_1("nClickOnce", "Click Once")
                Me.UcMenuLeftTreeView1._addNodeLevel_2("nDeployMyRuty", "MyRuty", myNode)

                myNode = Me.UcMenuLeftTreeView1._addNodeLevel_1("nRegioniProvinceComuni", "RegioniProvinceComuni")

            Case Else
                Throw New MyManager.ManagerException("MenuStack non gestito:" & group)
        End Select

        Me.UcMenuLeftTreeView1._init()




    End Sub


    Private Sub UcMenuLeftTreeView1_OnClickMenuLeft(ByVal node As System.Windows.Forms.TreeNode) Handles UcMenuLeftTreeView1.MyOnClickMenuLeft
        Select Case node.Name
            Case "nCompare"
                Dim f As New FormCompareUtenti
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar)
                Me._addTabPage(f._getTabPage())
            Case "nCheck"
                Dim f As New FormCheckUtenti
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar)
                Me._addTabPage(f._getTabPage())
            Case "nDelete"
                Dim f As New FormDelete()
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar)
                Me._addTabPage(f._getTabPage())
            Case "nSendInvitation"
                Dim f As New FormSendEmailInvitation()
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar)
                Me._addTabPage(f._getTabPage())
            Case "nImportOLD"
                Dim f As New FormImport()
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar)
                Me._addTabPage(f._getTabPage())
            Case "nDeployMyRuty"
                Dim f As New FormMyRuty()
                f._init(Me.MyStatusBar)
                f._setTitolo("ClickOnce", "MyRuty")
                Me._addTabPage(f._getTabPage())
            Case "nImportFromExcel"
                Dim f As New FormImportFromExcel()
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar, Nothing)
                Me._addTabPage(f._getTabPage())
            Case "casaEmailAgenzie"
                Dim f As New FormSendEmail()
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar)
                Me._addTabPage(f._getTabPage())
            Case "nRegioniProvinceComuni"
                Dim f As New FormRegioniProvinceComuni()
                f.Owner = Me
                f._setTitolo(node.Text)
                f._init(Me.MyConnection, Me.MyStatusBar, MyProgressBar)
                Me._addTabPage(f._getTabPage())

        End Select
    End Sub

    Private Sub FormMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _init()
        _displayMenuTopKillProcessExcel()
    End Sub
End Class
