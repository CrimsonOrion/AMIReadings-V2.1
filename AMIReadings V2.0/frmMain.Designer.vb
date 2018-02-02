<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnOFDExcel = New System.Windows.Forms.Button()
        Me.txtExcelFile = New System.Windows.Forms.TextBox()
        Me.btnConvUpload = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.mstBLPRHIST = New System.Windows.Forms.TabPage()
        Me.dgvBLPRHIST = New System.Windows.Forms.DataGridView()
        Me.mstBLPAXMTR = New System.Windows.Forms.TabPage()
        Me.dgvBLPAXMTR = New System.Windows.Forms.DataGridView()
        Me.mstBLPRDING = New System.Windows.Forms.TabPage()
        Me.dgvBLPRDING = New System.Windows.Forms.DataGridView()
        Me.mstBLPRDING_NEW = New System.Windows.Forms.TabPage()
        Me.dgvBLPRDING_New = New System.Windows.Forms.DataGridView()
        Me.RDAcct = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDSUB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDSERV = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDRDDATE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDREAD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDDEM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDKVR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDEST = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDBOOK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDSEQ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDUSER = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDTIME = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDDETRY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDVAR = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDVEE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabControl1.SuspendLayout()
        Me.mstBLPRHIST.SuspendLayout()
        CType(Me.dgvBLPRHIST, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mstBLPAXMTR.SuspendLayout()
        CType(Me.dgvBLPAXMTR, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mstBLPRDING.SuspendLayout()
        CType(Me.dgvBLPRDING, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mstBLPRDING_NEW.SuspendLayout()
        CType(Me.dgvBLPRDING_New, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 18)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Enter DB2 (.xlsx) file:"
        '
        'btnOFDExcel
        '
        Me.btnOFDExcel.Image = Global.AMIReadings_V2._0.My.Resources.Resources.Folder_icon
        Me.btnOFDExcel.Location = New System.Drawing.Point(690, 6)
        Me.btnOFDExcel.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.btnOFDExcel.Name = "btnOFDExcel"
        Me.btnOFDExcel.Size = New System.Drawing.Size(65, 45)
        Me.btnOFDExcel.TabIndex = 4
        Me.btnOFDExcel.UseVisualStyleBackColor = True
        '
        'txtExcelFile
        '
        Me.txtExcelFile.Location = New System.Drawing.Point(178, 15)
        Me.txtExcelFile.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.txtExcelFile.Name = "txtExcelFile"
        Me.txtExcelFile.Size = New System.Drawing.Size(500, 26)
        Me.txtExcelFile.TabIndex = 3
        '
        'btnConvUpload
        '
        Me.btnConvUpload.Enabled = False
        Me.btnConvUpload.Location = New System.Drawing.Point(178, 52)
        Me.btnConvUpload.Name = "btnConvUpload"
        Me.btnConvUpload.Size = New System.Drawing.Size(145, 32)
        Me.btnConvUpload.TabIndex = 6
        Me.btnConvUpload.Text = "Convert && Upload"
        Me.btnConvUpload.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.mstBLPRHIST)
        Me.TabControl1.Controls.Add(Me.mstBLPAXMTR)
        Me.TabControl1.Controls.Add(Me.mstBLPRDING)
        Me.TabControl1.Controls.Add(Me.mstBLPRDING_NEW)
        Me.TabControl1.Location = New System.Drawing.Point(12, 90)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(743, 491)
        Me.TabControl1.TabIndex = 7
        '
        'mstBLPRHIST
        '
        Me.mstBLPRHIST.Controls.Add(Me.dgvBLPRHIST)
        Me.mstBLPRHIST.Location = New System.Drawing.Point(4, 29)
        Me.mstBLPRHIST.Name = "mstBLPRHIST"
        Me.mstBLPRHIST.Padding = New System.Windows.Forms.Padding(3)
        Me.mstBLPRHIST.Size = New System.Drawing.Size(735, 458)
        Me.mstBLPRHIST.TabIndex = 0
        Me.mstBLPRHIST.Text = "BLPRHIST"
        Me.mstBLPRHIST.UseVisualStyleBackColor = True
        '
        'dgvBLPRHIST
        '
        Me.dgvBLPRHIST.AllowUserToAddRows = False
        Me.dgvBLPRHIST.AllowUserToDeleteRows = False
        Me.dgvBLPRHIST.AllowUserToOrderColumns = True
        Me.dgvBLPRHIST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBLPRHIST.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBLPRHIST.Location = New System.Drawing.Point(3, 3)
        Me.dgvBLPRHIST.Name = "dgvBLPRHIST"
        Me.dgvBLPRHIST.ReadOnly = True
        Me.dgvBLPRHIST.Size = New System.Drawing.Size(729, 452)
        Me.dgvBLPRHIST.TabIndex = 0
        '
        'mstBLPAXMTR
        '
        Me.mstBLPAXMTR.Controls.Add(Me.dgvBLPAXMTR)
        Me.mstBLPAXMTR.Location = New System.Drawing.Point(4, 29)
        Me.mstBLPAXMTR.Name = "mstBLPAXMTR"
        Me.mstBLPAXMTR.Padding = New System.Windows.Forms.Padding(3)
        Me.mstBLPAXMTR.Size = New System.Drawing.Size(735, 458)
        Me.mstBLPAXMTR.TabIndex = 1
        Me.mstBLPAXMTR.Text = "BLPAXMTR"
        Me.mstBLPAXMTR.UseVisualStyleBackColor = True
        '
        'dgvBLPAXMTR
        '
        Me.dgvBLPAXMTR.AllowUserToAddRows = False
        Me.dgvBLPAXMTR.AllowUserToDeleteRows = False
        Me.dgvBLPAXMTR.AllowUserToOrderColumns = True
        Me.dgvBLPAXMTR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBLPAXMTR.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBLPAXMTR.Location = New System.Drawing.Point(3, 3)
        Me.dgvBLPAXMTR.Name = "dgvBLPAXMTR"
        Me.dgvBLPAXMTR.ReadOnly = True
        Me.dgvBLPAXMTR.Size = New System.Drawing.Size(729, 452)
        Me.dgvBLPAXMTR.TabIndex = 1
        '
        'mstBLPRDING
        '
        Me.mstBLPRDING.Controls.Add(Me.dgvBLPRDING)
        Me.mstBLPRDING.Location = New System.Drawing.Point(4, 29)
        Me.mstBLPRDING.Name = "mstBLPRDING"
        Me.mstBLPRDING.Padding = New System.Windows.Forms.Padding(3)
        Me.mstBLPRDING.Size = New System.Drawing.Size(735, 458)
        Me.mstBLPRDING.TabIndex = 2
        Me.mstBLPRDING.Text = "BLPRDING"
        Me.mstBLPRDING.UseVisualStyleBackColor = True
        '
        'dgvBLPRDING
        '
        Me.dgvBLPRDING.AllowUserToAddRows = False
        Me.dgvBLPRDING.AllowUserToDeleteRows = False
        Me.dgvBLPRDING.AllowUserToOrderColumns = True
        Me.dgvBLPRDING.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBLPRDING.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBLPRDING.Location = New System.Drawing.Point(3, 3)
        Me.dgvBLPRDING.Name = "dgvBLPRDING"
        Me.dgvBLPRDING.ReadOnly = True
        Me.dgvBLPRDING.Size = New System.Drawing.Size(729, 452)
        Me.dgvBLPRDING.TabIndex = 1
        '
        'mstBLPRDING_NEW
        '
        Me.mstBLPRDING_NEW.Controls.Add(Me.dgvBLPRDING_New)
        Me.mstBLPRDING_NEW.Location = New System.Drawing.Point(4, 29)
        Me.mstBLPRDING_NEW.Name = "mstBLPRDING_NEW"
        Me.mstBLPRDING_NEW.Padding = New System.Windows.Forms.Padding(3)
        Me.mstBLPRDING_NEW.Size = New System.Drawing.Size(735, 458)
        Me.mstBLPRDING_NEW.TabIndex = 3
        Me.mstBLPRDING_NEW.Text = "BLPRDING_NEW"
        Me.mstBLPRDING_NEW.UseVisualStyleBackColor = True
        '
        'dgvBLPRDING_New
        '
        Me.dgvBLPRDING_New.AllowUserToAddRows = False
        Me.dgvBLPRDING_New.AllowUserToDeleteRows = False
        Me.dgvBLPRDING_New.AllowUserToOrderColumns = True
        Me.dgvBLPRDING_New.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBLPRDING_New.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RDAcct, Me.RDSUB, Me.RDSERV, Me.RDRDDATE, Me.RDREAD, Me.RDDEM, Me.RDKVR, Me.RDEST, Me.RDBOOK, Me.RDSEQ, Me.RDUSER, Me.RDTIME, Me.RDDETRY, Me.RDVAR, Me.RDVEE})
        Me.dgvBLPRDING_New.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvBLPRDING_New.Location = New System.Drawing.Point(3, 3)
        Me.dgvBLPRDING_New.Name = "dgvBLPRDING_New"
        Me.dgvBLPRDING_New.ReadOnly = True
        Me.dgvBLPRDING_New.Size = New System.Drawing.Size(729, 452)
        Me.dgvBLPRDING_New.TabIndex = 2
        '
        'RDAcct
        '
        Me.RDAcct.HeaderText = "Account"
        Me.RDAcct.Name = "RDAcct"
        Me.RDAcct.ReadOnly = True
        '
        'RDSUB
        '
        Me.RDSUB.HeaderText = "Sub"
        Me.RDSUB.Name = "RDSUB"
        Me.RDSUB.ReadOnly = True
        '
        'RDSERV
        '
        Me.RDSERV.HeaderText = "Service"
        Me.RDSERV.Name = "RDSERV"
        Me.RDSERV.ReadOnly = True
        '
        'RDRDDATE
        '
        Me.RDRDDATE.HeaderText = "Read Date"
        Me.RDRDDATE.Name = "RDRDDATE"
        Me.RDRDDATE.ReadOnly = True
        '
        'RDREAD
        '
        Me.RDREAD.HeaderText = "Reading"
        Me.RDREAD.Name = "RDREAD"
        Me.RDREAD.ReadOnly = True
        '
        'RDDEM
        '
        Me.RDDEM.HeaderText = "Demand"
        Me.RDDEM.Name = "RDDEM"
        Me.RDDEM.ReadOnly = True
        '
        'RDKVR
        '
        Me.RDKVR.HeaderText = "KVAR"
        Me.RDKVR.Name = "RDKVR"
        Me.RDKVR.ReadOnly = True
        '
        'RDEST
        '
        Me.RDEST.HeaderText = "RDEST"
        Me.RDEST.Name = "RDEST"
        Me.RDEST.ReadOnly = True
        '
        'RDBOOK
        '
        Me.RDBOOK.HeaderText = "Route#"
        Me.RDBOOK.Name = "RDBOOK"
        Me.RDBOOK.ReadOnly = True
        '
        'RDSEQ
        '
        Me.RDSEQ.HeaderText = "Sequence#"
        Me.RDSEQ.Name = "RDSEQ"
        Me.RDSEQ.ReadOnly = True
        '
        'RDUSER
        '
        Me.RDUSER.HeaderText = "User"
        Me.RDUSER.Name = "RDUSER"
        Me.RDUSER.ReadOnly = True
        '
        'RDTIME
        '
        Me.RDTIME.HeaderText = "Time of Entry"
        Me.RDTIME.Name = "RDTIME"
        Me.RDTIME.ReadOnly = True
        '
        'RDDETRY
        '
        Me.RDDETRY.HeaderText = "Date of Entry"
        Me.RDDETRY.Name = "RDDETRY"
        Me.RDDETRY.ReadOnly = True
        '
        'RDVAR
        '
        Me.RDVAR.HeaderText = "RDVAR"
        Me.RDVAR.Name = "RDVAR"
        Me.RDVAR.ReadOnly = True
        '
        'RDVEE
        '
        Me.RDVEE.HeaderText = "RDVEE"
        Me.RDVEE.Name = "RDVEE"
        Me.RDVEE.ReadOnly = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(765, 593)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnConvUpload)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnOFDExcel)
        Me.Controls.Add(Me.txtExcelFile)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMain"
        Me.Text = "Upload AMI Readings V.2.0"
        Me.TabControl1.ResumeLayout(False)
        Me.mstBLPRHIST.ResumeLayout(False)
        CType(Me.dgvBLPRHIST, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mstBLPAXMTR.ResumeLayout(False)
        CType(Me.dgvBLPAXMTR, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mstBLPRDING.ResumeLayout(False)
        CType(Me.dgvBLPRDING, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mstBLPRDING_NEW.ResumeLayout(False)
        CType(Me.dgvBLPRDING_New, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOFDExcel As System.Windows.Forms.Button
    Friend WithEvents txtExcelFile As System.Windows.Forms.TextBox
    Friend WithEvents btnConvUpload As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents mstBLPRHIST As System.Windows.Forms.TabPage
    Friend WithEvents dgvBLPRHIST As System.Windows.Forms.DataGridView
    Friend WithEvents mstBLPAXMTR As System.Windows.Forms.TabPage
    Friend WithEvents dgvBLPAXMTR As System.Windows.Forms.DataGridView
    Friend WithEvents mstBLPRDING As System.Windows.Forms.TabPage
    Friend WithEvents dgvBLPRDING As System.Windows.Forms.DataGridView
    Friend WithEvents mstBLPRDING_NEW As System.Windows.Forms.TabPage
    Friend WithEvents dgvBLPRDING_New As System.Windows.Forms.DataGridView
    Friend WithEvents RDAcct As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDSUB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDSERV As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDRDDATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDREAD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDDEM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDKVR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDEST As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDBOOK As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDSEQ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDUSER As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDTIME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDDETRY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDVAR As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDVEE As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
