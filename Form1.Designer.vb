<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.LabelMainFolder = New System.Windows.Forms.Label()
        Me.ButtonClose = New System.Windows.Forms.Button()
        Me.TextBoxMainFolder = New System.Windows.Forms.TextBox()
        Me.TimerServerStatus = New System.Windows.Forms.Timer(Me.components)
        Me.LabelLastBriefing = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxServerPort = New System.Windows.Forms.TextBox()
        Me.LabelServerStatus = New System.Windows.Forms.Label()
        Me.ButtonServerStop = New System.Windows.Forms.Button()
        Me.ButtonServerStart = New System.Windows.Forms.Button()
        Me.ButtonSelectFolder = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ButtonUpdate = New System.Windows.Forms.Button()
        Me.ButtonExport = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelMainFolder
        '
        Me.LabelMainFolder.AutoSize = True
        Me.LabelMainFolder.Location = New System.Drawing.Point(16, 58)
        Me.LabelMainFolder.Name = "LabelMainFolder"
        Me.LabelMainFolder.Size = New System.Drawing.Size(117, 13)
        Me.LabelMainFolder.TabIndex = 0
        Me.LabelMainFolder.Text = "Carpeta de falcon BMS"
        '
        'ButtonClose
        '
        Me.ButtonClose.Location = New System.Drawing.Point(594, 294)
        Me.ButtonClose.Name = "ButtonClose"
        Me.ButtonClose.Size = New System.Drawing.Size(75, 34)
        Me.ButtonClose.TabIndex = 1
        Me.ButtonClose.Text = "Cerrar"
        Me.ButtonClose.UseVisualStyleBackColor = True
        '
        'TextBoxMainFolder
        '
        Me.TextBoxMainFolder.Location = New System.Drawing.Point(16, 76)
        Me.TextBoxMainFolder.Name = "TextBoxMainFolder"
        Me.TextBoxMainFolder.Size = New System.Drawing.Size(365, 20)
        Me.TextBoxMainFolder.TabIndex = 3
        '
        'TimerServerStatus
        '
        '
        'LabelLastBriefing
        '
        Me.LabelLastBriefing.Location = New System.Drawing.Point(16, 137)
        Me.LabelLastBriefing.Name = "LabelLastBriefing"
        Me.LabelLastBriefing.Size = New System.Drawing.Size(653, 154)
        Me.LabelLastBriefing.TabIndex = 4
        Me.LabelLastBriefing.Text = "No se ha generado"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(429, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Puerto http"
        '
        'TextBoxServerPort
        '
        Me.TextBoxServerPort.Location = New System.Drawing.Point(415, 78)
        Me.TextBoxServerPort.Name = "TextBoxServerPort"
        Me.TextBoxServerPort.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxServerPort.TabIndex = 6
        Me.TextBoxServerPort.Text = "8080"
        Me.TextBoxServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LabelServerStatus
        '
        Me.LabelServerStatus.Location = New System.Drawing.Point(415, 101)
        Me.LabelServerStatus.Name = "LabelServerStatus"
        Me.LabelServerStatus.Size = New System.Drawing.Size(253, 36)
        Me.LabelServerStatus.TabIndex = 7
        Me.LabelServerStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ButtonServerStop
        '
        Me.ButtonServerStop.Location = New System.Drawing.Point(594, 64)
        Me.ButtonServerStop.Name = "ButtonServerStop"
        Me.ButtonServerStop.Size = New System.Drawing.Size(74, 34)
        Me.ButtonServerStop.TabIndex = 8
        Me.ButtonServerStop.Text = "Stop"
        Me.ButtonServerStop.UseVisualStyleBackColor = True
        '
        'ButtonServerStart
        '
        Me.ButtonServerStart.Location = New System.Drawing.Point(513, 64)
        Me.ButtonServerStart.Name = "ButtonServerStart"
        Me.ButtonServerStart.Size = New System.Drawing.Size(75, 34)
        Me.ButtonServerStart.TabIndex = 9
        Me.ButtonServerStart.Text = "Start"
        Me.ButtonServerStart.UseVisualStyleBackColor = True
        '
        'ButtonSelectFolder
        '
        Me.ButtonSelectFolder.Location = New System.Drawing.Point(387, 76)
        Me.ButtonSelectFolder.Name = "ButtonSelectFolder"
        Me.ButtonSelectFolder.Size = New System.Drawing.Size(22, 23)
        Me.ButtonSelectFolder.TabIndex = 11
        Me.ButtonSelectFolder.Text = "..."
        Me.ButtonSelectFolder.UseVisualStyleBackColor = True
        '
        'ButtonUpdate
        '
        Me.ButtonUpdate.Location = New System.Drawing.Point(432, 294)
        Me.ButtonUpdate.Name = "ButtonUpdate"
        Me.ButtonUpdate.Size = New System.Drawing.Size(75, 34)
        Me.ButtonUpdate.TabIndex = 12
        Me.ButtonUpdate.Text = "Actualizar"
        Me.ButtonUpdate.UseVisualStyleBackColor = True
        '
        'ButtonExport
        '
        Me.ButtonExport.Location = New System.Drawing.Point(513, 294)
        Me.ButtonExport.Name = "ButtonExport"
        Me.ButtonExport.Size = New System.Drawing.Size(75, 34)
        Me.ButtonExport.TabIndex = 13
        Me.ButtonExport.Text = "Exportar"
        Me.ButtonExport.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(681, 50)
        Me.PictureBox1.TabIndex = 16
        Me.PictureBox1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(681, 340)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ButtonExport)
        Me.Controls.Add(Me.ButtonUpdate)
        Me.Controls.Add(Me.ButtonSelectFolder)
        Me.Controls.Add(Me.ButtonServerStart)
        Me.Controls.Add(Me.ButtonServerStop)
        Me.Controls.Add(Me.LabelServerStatus)
        Me.Controls.Add(Me.TextBoxServerPort)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelLastBriefing)
        Me.Controls.Add(Me.TextBoxMainFolder)
        Me.Controls.Add(Me.ButtonClose)
        Me.Controls.Add(Me.LabelMainFolder)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.Text = "BMS http KneeBoard"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelMainFolder As Label
    Friend WithEvents ButtonClose As Button
    Friend WithEvents TextBoxMainFolder As TextBox
    Friend WithEvents TimerServerStatus As Timer
    Friend WithEvents LabelLastBriefing As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxServerPort As TextBox
    Friend WithEvents LabelServerStatus As Label
    Friend WithEvents ButtonServerStop As Button
    Friend WithEvents ButtonServerStart As Button
    Friend WithEvents ButtonSelectFolder As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ButtonUpdate As Button
    Friend WithEvents ButtonExport As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents PictureBox1 As PictureBox
End Class
