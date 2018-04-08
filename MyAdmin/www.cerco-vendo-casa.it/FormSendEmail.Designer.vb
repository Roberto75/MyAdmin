<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormSendEmail
    Inherits MyFormsLibrary.FormBaseDetail_3


    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormSendEmail))
        Me.MyButton1 = New MyControlsLibrary.MyButton()
        Me.MyButton2 = New MyControlsLibrary.MyButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TabPage1.SuspendLayout()
        Me.TabControlDetail.SuspendLayout()
        Me.panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me._ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPageMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBox1)
        Me.TabPage1.Size = New System.Drawing.Size(698, 169)
        '
        'TabControlDetail
        '
        Me.TabControlDetail.Size = New System.Drawing.Size(706, 195)
        '
        'panel1
        '
        Me.panel1.Controls.Add(Me.MyButton2)
        Me.panel1.Controls.Add(Me.MyButton1)
        Me.panel1.Size = New System.Drawing.Size(706, 122)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Size = New System.Drawing.Size(706, 346)
        Me.SplitContainer1.SplitterDistance = 147
        '
        'tabPageMain
        '
        Me.tabPageMain.Size = New System.Drawing.Size(712, 377)
        '
        'MyButton1
        '
        Me.MyButton1.BackColor = System.Drawing.Color.Transparent
        Me.MyButton1.FlatAppearance.BorderSize = 0
        Me.MyButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.MyButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.MyButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MyButton1.Image = CType(resources.GetObject("MyButton1.Image"), System.Drawing.Image)
        Me.MyButton1.Location = New System.Drawing.Point(5, 3)
        Me.MyButton1.MyType = MyControlsLibrary.MyButton.ButtonType.btnExecute
        Me.MyButton1.Name = "MyButton1"
        Me.MyButton1.Size = New System.Drawing.Size(156, 30)
        Me.MyButton1.TabIndex = 0
        Me.MyButton1.Text = "Google Doc"
        Me.MyButton1.UseVisualStyleBackColor = False
        '
        'MyButton2
        '
        Me.MyButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyButton2.BackColor = System.Drawing.Color.Transparent
        Me.MyButton2.FlatAppearance.BorderSize = 0
        Me.MyButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.MyButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.MyButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MyButton2.Image = CType(resources.GetObject("MyButton2.Image"), System.Drawing.Image)
        Me.MyButton2.Location = New System.Drawing.Point(545, 3)
        Me.MyButton2.MyType = MyControlsLibrary.MyButton.ButtonType.btnExecute
        Me.MyButton2.Name = "MyButton2"
        Me.MyButton2.Size = New System.Drawing.Size(156, 30)
        Me.MyButton2.TabIndex = 1
        Me.MyButton2.Text = "Esegui"
        Me.MyButton2.UseVisualStyleBackColor = False
        '
        'TextBox1
        '
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Location = New System.Drawing.Point(3, 3)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBox1.Size = New System.Drawing.Size(692, 163)
        Me.TextBox1.TabIndex = 0
        '
        'FormSendEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(720, 403)
        Me.Name = "FormSendEmail"
        Me.Text = "FormSendEmail"
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabControlDetail.ResumeLayout(False)
        Me.panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me._ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPageMain.ResumeLayout(False)
        Me.tabPageMain.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MyButton1 As MyControlsLibrary.MyButton
    Friend WithEvents MyButton2 As MyControlsLibrary.MyButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
