Imports System.IO
Imports System.Text
Imports System.Net
Imports System.ComponentModel
Imports BMS_http_KneeBoard.My
Imports System.Net.WebRequestMethods
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Collections.Specialized.BitVector32
Imports System.Runtime.Remoting.Messaging

Public Class Form1
    Public LastFileDateBriefing As Date
    Public LastFileDateINI As Date
    Public StrBriefingSubFolder As String = "\User\Briefings\"
    Public StrConfigSubFolder As String = "\User\Config\"
    Dim Server As New SimpleHttpServer
    Dim Colores(20) As Color
    Dim IntColor As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.Reload()
        Colores(0) = Color.Green
        Colores(1) = Color.Gray
        Colores(2) = Color.Fuchsia
        Colores(3) = Color.Aqua
        Colores(4) = Color.Beige
        Colores(5) = Color.Brown
        Colores(6) = Color.Coral
        Colores(7) = Color.AliceBlue
        Colores(8) = Color.Gold
        Colores(9) = Color.LemonChiffon
        Colores(10) = Color.MediumVioletRed
        Colores(11) = Color.MistyRose
        Colores(12) = Color.DarkKhaki
        Colores(13) = Color.DarkSalmon
        Colores(14) = Color.Honeydew
        Colores(15) = Color.GreenYellow
        Colores(16) = Color.PaleVioletRed
        Colores(17) = Color.Pink
        Colores(18) = Color.Plum
        Colores(19) = Color.Teal

        If My.Settings.PuertoHTTP.Trim = "" Then My.Settings.PuertoHTTP = "8080"
        If My.Settings.RutaBMS.Trim = "" Then My.Settings.RutaBMS = "C:\Falcon BMS 4.37"
        My.Settings.Save()
        Me.TextBox1.Text = My.Settings.PuertoHTTP
        Me.TextBoxMainFolder.Text = My.Settings.RutaBMS
        Timer1.Interval = 10000 ' Establecer el intervalo en 1 segundo
        Timer1.Enabled = True ' Iniciar la ejecución del código
        Server.ServerPort = My.Settings.PuertoHTTP
        Server.Html = "No se ha generado contenido"
        Server.Start()
        CheckServerStatus()
        If Server.State = 1 Then Call MakeBriefing()
    End Sub
    Private Sub CheckServerStatus()
        If Server.State = 0 Then
            Me.TextBox1.Enabled = True
            Me.Button2.Enabled = True
            Me.Button1.Enabled = False
            LabelServerStatus.Text = "Servidor fuera de linea"
        Else
            Me.TextBox1.Enabled = False
            Me.Button2.Enabled = False
            Me.Button1.Enabled = True
            LabelServerStatus.Text = "Servidor On-Line"
        End If
    End Sub
    Private Sub ButtonClose_Click(sender As Object, e As EventArgs) Handles ButtonClose.Click
        Me.Close()
    End Sub
    Private Sub FillIndex(ByRef html As StringBuilder, Section As String)
        IntColor += 1
        html.Append("<div style=""background-color: " & ColorTranslator.ToHtml(Colores(IntColor)) & """;"" class=""panel"" onclick=""goto('" & Section.Replace(":", "").ToUpper & "')"">")
        html.Append("<p>")
        html.Append(Section.Replace(":", "").ToUpper)
        html.Append("</p>")
        html.Append("</div>")
    End Sub
    Private Function GetTeatherName(MtrBriefing() As String) As String
        Dim Section As String = ""
        For Each StrLinea As String In MtrBriefing
            Select Case StrLinea
                Case "Comm Ladder:"
                    Section = Trim(StrLinea)
                Case Else
                    Select Case Section
                        Case "Comm Ladder:"
                            'localizar el nombre de las 3 bases implicadas:
                            Dim MtrSplitBase() As String
                            MtrSplitBase = Split(StrLinea, vbTab)
                            If MtrSplitBase.Length > 1 Then
                                Select Case MtrSplitBase(1)
                                    Case "Dep Tower:"
                                        'ahora localiza la base, esto tiene truco, uso la funcion que localiza el mapa, me dara el path
                                        GetTeatherName = FindFolder(My.Settings.RutaBMS, Replace(MtrSplitBase(2), " Tower", "")).ToUpper
                                        GetTeatherName = Mid(GetTeatherName, 1, GetTeatherName.IndexOf("\DOCS\"))
                                        If GetTeatherName.IndexOf("\DATA\") < 0 Then
                                            Return "KOREA"
                                        Else
                                            Return GetTeatherName
                                        End If
                                End Select
                            End If
                    End Select
            End Select
        Next
        Return ""
    End Function
    Private Function ProcesarFicheroBriefing(Fichero As String) As String
        IntColor = -1
        Dim htmlall As New StringBuilder
        Dim htmlindex As New StringBuilder
        Dim htmlbody As New StringBuilder

        htmlbody.Append("<table>")
        htmlbody.Append("<tr><td>")

        Dim BlnInTable As Boolean = False
        Dim Section As String = ""
        Dim MemLine As String = ""
        Dim BaseDEP As String = ""
        Dim BaseARR As String = ""
        Dim BaseALT As String = ""
        Dim StartLon As Single = 0
        Dim StartLat As Single = 0
        If System.IO.File.Exists(Fichero) = False Then Return "No se localizó el fichero"
        Dim MtrBriefingFile() As String = System.IO.File.ReadAllLines(Fichero)
        Dim TeatherName As String = GetTeatherName(MtrBriefingFile)
        If TeatherName.ToUpper.IndexOf("Aegean".ToUpper) >= 0 Then
            StartLat = 33.41
            StartLon = 21.55
        ElseIf TeatherName.ToUpper.IndexOf("BALKANS".ToUpper) >= 0 Then
            StartLat = 36.62498
            StartLon = 10.99996
        ElseIf TeatherName.ToUpper.IndexOf("Central Europe".ToUpper) >= 0 Then
            StartLat = 48
            StartLon = 2
        ElseIf TeatherName.ToUpper.IndexOf("EMF".ToUpper) >= 0 Then
            StartLat = 28.79372
            StartLon = 17.49486
        ElseIf TeatherName.ToUpper.IndexOf("Iberia".ToUpper) >= 0 Then
            StartLat = 25
            StartLon = 0
        ElseIf TeatherName.ToUpper.IndexOf("Ikaros".ToUpper) >= 0 Then
            StartLat = 33.07186
            StartLon = 19.5069
        ElseIf TeatherName.ToUpper.IndexOf("Israel".ToUpper) >= 0 Then
            StartLat = 25.625
            StartLon = 29.56
        ElseIf TeatherName.ToUpper.IndexOf("KOREA".ToUpper) >= 0 Then
            StartLat = 33.84376
            StartLon = 123
        ElseIf TeatherName.ToUpper.IndexOf("Kuwait".ToUpper) >= 0 Then
            StartLat = 25.09686
            StartLon = 42.11698
        ElseIf TeatherName.ToUpper.IndexOf("Mideast128".ToUpper) >= 0 Then
            StartLat = 20.4937
            StartLon = 37.3761
        ElseIf TeatherName.ToUpper.IndexOf("Nevada".ToUpper) >= 0 Then
            StartLat = 36.62943
            StartLon = 11.0056
        ElseIf TeatherName.ToUpper.IndexOf("Nordic".ToUpper) >= 0 Then
            StartLat = 63.12
            StartLon = 9.8735
        ElseIf TeatherName.ToUpper.IndexOf("Panama".ToUpper) >= 0 Then
            StartLat = 3.41
            StartLon = 73.819
        ElseIf TeatherName.ToUpper.IndexOf("RedFlag".ToUpper) >= 0 Then
            StartLat = 33.84376
            StartLon = 123
        Else
            StartLat = 0
            StartLon = 0
        End If

        For Each StrLinea As String In MtrBriefingFile
            StrLinea = Trim(StrLinea)
            If Mid(StrLinea, 1, 10) = "----------" Then StrLinea = "" 'quita la linea de inicio
            If Mid(StrLinea, 1, 1) = vbTab Then StrLinea = Mid(StrLinea, 2) 'quita el tabulador inicial
            If InStr(1, StrLinea, "Package Elements:") = 1 Then StrLinea = "Package Elements:" 'filtra esta columna a un titulo
            If InStr(1, StrLinea, "BRIEFING RECORD") = 1 Then
                htmlbody.Append("<h1>" & StrLinea & "</h1>")
                StrLinea = ""
            End If
            Select Case StrLinea
                Case "" 'linea en blanco, no hace nada
                Case "Mission Overview:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Situation:"
                    'antes de situation, meter los aeropuertos
                    FillIndex(htmlindex, "Airfields:")
                    htmlbody.Append("[AIRFIELDS]")
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Pilot Roster:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Package Elements:"
                    MemLine = ""
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Threat Analysis:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Air-to-Air Threats:"
                    FillSubTitle(htmlbody, BlnInTable, StrLinea)
                Case "Surface-To-Air Threats:"
                    FillSubTitle(htmlbody, BlnInTable, StrLinea)
                Case "Steerpoints:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Comm Ladder:"
                    'es la seccion justo despues de los sterpoint, por lo que aproveco y agrego los stp de precisio
                    Call FillWPNTarget(htmlbody, htmlindex, BlnInTable, StartLat, StartLon)
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Iff"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Ordnance:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Weather:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Support:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Rules of Engagement:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Emergency Procedures:"
                    Section = StrLinea
                    FillIndex(htmlindex, Section)
                    FillTitle(htmlbody, BlnInTable, StrLinea)
                Case "Distress call:"
                    FillSubTitle(htmlbody, BlnInTable, StrLinea)
                Case "Combat Search and Rescue:"
                    FillSubTitle(htmlbody, BlnInTable, StrLinea)
                Case "Alternate airfield:"
                    FillSubTitle(htmlbody, BlnInTable, StrLinea)
                Case "Good Luck!"
                Case "END_OF_BRIEFING"
                Case Else
                    Select Case Section
                        Case "Mission Overview:"
                            FillLI(htmlbody, BlnInTable, StrLinea)
                        Case "Situation:"
                            FillParagraph(htmlbody, BlnInTable, StrLinea)
                        Case "Threat Analysis:"
                            FillParagraph(htmlbody, BlnInTable, StrLinea)
                        Case "Package Elements:"
                            'FILTRADOS ESPECIFICIOS
                            If InStr(1, StrLinea, "Callsign:") > 0 Then
                                StrLinea = StrLinea & vbTab & "T/O:" & vbTab & "Push:" & vbTab & "Tgt:" & vbTab & "IFF:"
                                FillTable(htmlbody, BlnInTable, StrLinea)
                            ElseIf MemLine = "" Then
                                StrLinea = Replace(StrLinea, "T/O:", "")
                                StrLinea = Replace(StrLinea, "Push:", "")
                                StrLinea = Replace(StrLinea, "Tgt:", "")
                                StrLinea = Replace(StrLinea, "IFF:", "")
                                MemLine = Mid(StrLinea, 2)
                            ElseIf MemLine <> "" Then
                                StrLinea = MemLine & vbTab & Mid(StrLinea, 3)
                                MemLine = ""
                                FillTable(htmlbody, BlnInTable, StrLinea)
                            End If
                        Case "Rules of Engagement:"
                            FillParagraph(htmlbody, BlnInTable, StrLinea)
                        Case "Ordnance:"
                            'armamento que lleva cada vuelo
                            If InStr(1, StrLinea, "Callsign:") > 0 Then
                                StrLinea = StrLinea
                                FillTable(htmlbody, BlnInTable, StrLinea)
                            ElseIf Mid(StrLinea, 1, 1) <> " " Then
                                StrLinea = vbTab & StrLinea
                                FillTable(htmlbody, BlnInTable, StrLinea)
                            Else
                                If Mid(StrLinea, 1, 1) = " " Then StrLinea = Mid(StrLinea, 3)
                                FillTH(htmlbody, BlnInTable, StrLinea)
                            End If
                        Case "Emergency Procedures:"
                            FillParagraph(htmlbody, BlnInTable, StrLinea)
                        Case "Good Luck!", "END_OF_BRIEFING"
                        Case "Comm Ladder:"
                            'localizar el nombre de las 3 bases implicadas:
                            Dim MtrSplitBase() As String
                            MtrSplitBase = Split(StrLinea, vbTab)
                            Select Case MtrSplitBase(0)
                                Case "Dep Tower:"
                                    BaseDEP = Replace(MtrSplitBase(1), " Tower", "")
                                Case "Arr Tower:"
                                    BaseARR = Replace(MtrSplitBase(1), " Tower", "")
                                Case "Alt Tower:"
                                    BaseALT = Replace(MtrSplitBase(1), " Tower", "")
                            End Select
                            FillTable(htmlbody, BlnInTable, StrLinea)
                        Case Else
                            FillTable(htmlbody, BlnInTable, StrLinea)
                    End Select

            End Select
        Next
        EndSections(htmlbody, BlnInTable)
        htmlbody.Append("</td></tr>")


        IncludeAirFields(htmlbody, htmlindex, BaseDEP, BaseARR, BaseALT)




        htmlbody.Append("</table>")
        htmlall.Append("<html>")
        htmlall.Append("<head>")
        htmlall.Append("<style>")
        htmlall.Append("#index {width:10%;height:100%;overflow:hidden;float:left;}")
        htmlall.Append("#index div:hover {cursor: pointer;}")
        htmlall.Append("#index p {word-wrap: break-word;padding:0;margin:0;min-height: 35px;}")
        htmlall.Append(".panel{border: 1px solid black;}")
        htmlall.Append("#data {width:90%;height:100%;overflow-y:scroll;float:left;}")
        htmlall.Append("table {width:100%; border-collapse: collapse;}")
        htmlall.Append("td {border: 1px solid black;padding: 0px;margin:0px; text-align: left;}")
        htmlall.Append("body {font-family: Courier; margin:0px;padding:0px;}")
        htmlall.Append("</style>")
        htmlall.Append("<script>")
        htmlall.Append("function goto(idtr){")
        htmlall.Append("event.preventDefault();")
        htmlall.Append("var fila = document.getElementById(idtr);")
        htmlall.Append("fila.scrollIntoView();")
        htmlall.Append("}")
        htmlall.Append("</script>")
        htmlall.Append("</head>")
        htmlall.Append("<body>")
        htmlall.Append("<div id=""index"">" & htmlindex.ToString & "</div><div id=""data"">" & htmlbody.ToString & "</div>")
        htmlall.Append("</body>")
        htmlall.Append("</html>")
        Return htmlall.ToString
    End Function
    Private Sub FillWPNTarget(ByRef html As StringBuilder, ByRef htmlindex As StringBuilder, ByRef BlnInTable As Boolean, StarLat As Single, StarLon As Single)
        'la ruta del fichero ini del piloto, ojo puede tener varios nics
        Dim StrFichero As String = LocalizaFicheroINI()
        If StrFichero.Length > 0 Then
            Dim MtrFichero() As String = System.IO.File.ReadAllLines(StrFichero)
            Dim BlnInSection As Boolean = False
            Dim StrLinea As String
            Dim BlnTieneSTP As Boolean = False
            For Each linea As String In MtrFichero
                If linea.Trim.ToUpper = "[STPT]" Then
                    BlnInSection = True
                Else
                    If BlnInSection = True Then
                        Dim MtrClaveValor() As String = Split(linea, "=")
                        If MtrClaveValor(0).IndexOf("wpntarget_") = 0 Then
                            If MtrClaveValor(1) = "0.000000, 0.000000, 0.000000, -1, Not set" Then
                            Else
                                If BlnTieneSTP = False Then
                                    FillIndex(htmlindex, "Targets:")
                                    FillTitle(html, BlnInTable, "Targets:")
                                    StrLinea = "NUMBER" & vbTab & "TARGET" & "X" & vbTab & "Y" & vbTab & "Z" & vbTab & " "
                                    FillTable(html, BlnInTable, StrLinea)
                                    BlnTieneSTP = True
                                End If
                                Dim MtrData() As String = Split(MtrClaveValor(1), ",", 5)
                                Dim Lat As String = ""
                                Dim Lon As String = ""
                                If StarLat <> 0 And StarLon <> 0 Then
                                    Call ConvertCoordinates(StarLat, StarLon, Val(MtrData(0)), Val(MtrData(1)), Lat, Lon)
                                    StrLinea = MtrClaveValor(0).Substring(10) & vbTab & MtrData(4) & vbTab & Lat & vbTab & Lon & vbTab & MtrData(2) & vbTab & MtrData(3)
                                Else
                                    StrLinea = MtrClaveValor(0).Substring(10) & vbTab & MtrData(4) & vbTab & MtrData(0) & vbTab & MtrData(1) & vbTab & MtrData(2) & vbTab & MtrData(3)
                                End If
                                'StrLinea = MtrClaveValor(0).Substring(10) &  & Replace(MtrClaveValor(1), " ", vbTab, 1, 4)
                                FillTable(html, BlnInTable, StrLinea)
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    Private Function LocalizaFicheroINI() As String
        Dim Carpeta As String = My.Settings.RutaBMS & StrConfigSubFolder
        Dim exclusiones() As String = {"3DUISETTINGS.INI", "PHONEBKN.INI"}
        ' Obtener una lista de todos los archivos en la carpeta
        Dim archivos() As String = Directory.GetFiles(Carpeta, "*.ini")
        ' Iterar sobre la lista de archivos y encontrar el archivo INI más reciente que no esté en la lista de exclusiones
        Dim archivoINI As String = ""
        Dim fechaMasReciente As DateTime = DateTime.MinValue
        For Each archivo As String In archivos
            ' Verificar si el archivo está en la lista de exclusiones
            If Array.IndexOf(exclusiones, Path.GetFileName(archivo).ToUpper) = -1 Then
                ' Obtener la fecha de modificación del archivo
                Dim fechaModificacion As DateTime = System.IO.File.GetLastWriteTime(archivo)
                ' Verificar si la fecha de modificación del archivo es más reciente que la fecha más reciente que se ha encontrado hasta el momento
                If fechaModificacion > fechaMasReciente Then
                    ' Establecer este archivo como el archivo más reciente hasta ahora
                    archivoINI = archivo
                    fechaMasReciente = fechaModificacion
                End If
            End If
        Next
        Return archivoINI
    End Function
    Private Sub IncludeAirFields(ByRef html As StringBuilder, htmlindex As StringBuilder, BaseDEP As String, BaseARR As String, BaseALT As String)
        Dim SubHTML As New StringBuilder
        SubHTML.Append("<tr><td>")
        SubHTML.Append("<h2>Airfields:</h2>")
        SubHTML.Append("<li>" & BaseDEP & "</li>")
        Call LocalizaFicheroBase("Departure Airbase", BaseDEP, html, htmlindex)
        If BaseARR <> BaseDEP Then
            SubHTML.Append("<li>" & BaseARR & "</li>")
            Call LocalizaFicheroBase("Recovery Airbase", BaseARR, html, htmlindex)
        End If
        If BaseALT <> BaseDEP And BaseALT <> BaseARR Then
            SubHTML.Append("<li>" & BaseALT & "</li>")
            Call LocalizaFicheroBase("Alternate Airbase", BaseALT, html, htmlindex)
        End If
        SubHTML.Append("</tr></td>")
        html.Replace("[AIRFIELDS]", SubHTML.ToString)
    End Sub
    Private Sub EndSections(ByRef html As StringBuilder, ByRef BlnInTable As Boolean)
        If BlnInTable = True Then
            html.Append("</table>")
            BlnInTable = False
        End If
    End Sub
    Private Sub FillLI(ByRef html As StringBuilder, ByRef BlnInTable As Boolean, strlinea As String)
        EndSections(html, BlnInTable)
        html.Append("<li>" & strlinea & "</li>")
    End Sub
    Private Sub FillSubTitle(ByRef html As StringBuilder, ByRef BlnInTable As Boolean, strlinea As String)
        EndSections(html, BlnInTable)
        html.Append("<h3>" & strlinea & "</h3>")
    End Sub
    Private Sub FillTitle(ByRef html As StringBuilder, ByRef BlnInTable As Boolean, strlinea As String)
        EndSections(html, BlnInTable)
        html.Append("</td></tr>")
        html.Append("<tr id=""" & strlinea.Replace(":", "").ToUpper & """><td>")
        html.Append("<h2 style=""background-color:" & ColorTranslator.ToHtml(Colores(IntColor)) & """>" & strlinea & "</h2>")
    End Sub
    Private Sub FillParagraph(ByRef html As StringBuilder, ByRef BlnInTable As Boolean, strlinea As String)
        EndSections(html, BlnInTable)
        html.Append("<p>" & strlinea & "</p>")
    End Sub
    Private Sub FillTH(ByRef html As StringBuilder, ByRef BlnInTable As Boolean, strlinea As String)
        Dim MtrLinea() As String
        strlinea = Replace(strlinea, vbTab & vbTab, vbTab)
        strlinea = Replace(strlinea, vbTab & vbTab, vbTab)
        MtrLinea = Split(strlinea, vbTab)
        html.Append("<tr>")
        For i = 0 To UBound(MtrLinea)
            html.Append("<th>" & MtrLinea(i) & "</th>")
        Next i
        html.Append("</tr>")
        BlnInTable = True
        Return
    End Sub

    Private Sub FillTable(ByRef html As StringBuilder, ByRef BlnInTable As Boolean, strlinea As String)
        Dim MtrLinea() As String
        strlinea = Replace(strlinea, vbTab & vbTab, vbTab)
        strlinea = Replace(strlinea, vbTab & vbTab, vbTab)
        MtrLinea = Split(strlinea, vbTab)
        If BlnInTable = False Then
            html.Append("<table>")
            html.Append("<tr>")
            For i = 0 To UBound(MtrLinea)
                html.Append("<th>" & MtrLinea(i) & "</th>")
            Next i
            html.Append("</tr>")
            BlnInTable = True
        Else
            html.Append("<tr>")
            For i = 0 To UBound(MtrLinea)
                html.Append("<td>" & MtrLinea(i) & "</td>")
            Next i
            html.Append("</tr>")
        End If
        Return
    End Sub
    Private Sub LocalizaFicheroBase(TipoDeBase As String, Base As String, ByRef html As StringBuilder, ByRef htmlindex As StringBuilder)
        'busca dentro de la carpeta de bms una carpeta que se llame como el nombre de la base
        Dim StrFolder As String
        Dim StrFile As String
        StrFolder = FindFolder(My.Settings.RutaBMS, Base)
        html.Append("<tr id=""" & Base.ToUpper & """><td>")
        If StrFolder <> "" Then
            Call FillIndex(htmlindex, Base)
            html.Append("<h2  style=""background-color:" & ColorTranslator.ToHtml(Colores(IntColor)) & """>" & TipoDeBase & " " & Base & "</h2>")
            StrFile = Dir(StrFolder & "\*.png", vbArchive)
            Do Until StrFile = ""
                html.Append("<img width=""100%"" src=""data:image/png;base64," & GetBase64(StrFolder & "\" & StrFile) & """ alt=""" & StrFile & """>")
                StrFile = Dir()
            Loop
        Else
            Call FillIndex(htmlindex, Base)
            html.Append("<h2 style=""background-color:" & ColorTranslator.ToHtml(Colores(IntColor)) & """>Airfield " & Base & " not allocated</h2>")
        End If
        html.Append("</tr></td>")
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Call MakeBriefing()
    End Sub
    Private Sub MakeBriefing()
        Timer1.Enabled = False
        Try
            '---------------------------------------------------------------------------------
            'el ini es el datacard, si se ha modificado, fuerza el redibujado del html
            '---------------------------------------------------------------------------------
            Dim StrIniFile As String = LocalizaFicheroINI()
            If StrIniFile.Length > 0 Then
                If LastFileDateINI <> FileDateTime(StrIniFile) Then
                    LastFileDateBriefing = Nothing
                End If
            End If
            '---------------------------------------------------------------------------------
            Dim StrFicheroBriefing As String = My.Settings.RutaBMS & StrBriefingSubFolder & "briefing.txt"
            If LastFileDateBriefing <> FileDateTime(StrFicheroBriefing) Then
                'si se ha modificado algun fichero se actualiza el html
                LastFileDateBriefing = FileDateTime(StrFicheroBriefing)
                Server.Html = ProcesarFicheroBriefing(StrFicheroBriefing)
                LabelLastBriefing.Text = LastFileDateBriefing.ToString("yyyy-MM-dd HH:mm:ss")
            End If
            CheckServerStatus()
        Catch ex As Exception
            LabelLastBriefing.Text = "Error generando briefing, " & Err.Description
            LastFileDateBriefing = Nothing
        End Try
        Timer1.Enabled = True
    End Sub
    Private Function FindFolder(ByVal folderPath As String, ByVal folderName As String) As String
        ' Crear una instancia de DirectoryInfo para la carpeta principal
        Dim folderInfo As New DirectoryInfo(folderPath)

        ' Comprobar si el nombre de la carpeta actual coincide con el nombre buscado
        If folderInfo.Name = folderName Then
            FindFolder = folderInfo.FullName
            Exit Function
        End If

        ' Recorrer todas las subcarpetas de la carpeta actual
        For Each subfolderInfo In folderInfo.GetDirectories()
            ' Llamar recursivamente a la función para buscar en la subcarpeta actual
            FindFolder = FindFolder(subfolderInfo.FullName, folderName)
            If FindFolder <> "" Then Exit Function ' Si se encuentra la carpeta, salir del bucle
        Next

        ' Si no se encuentra la carpeta en esta carpeta ni en sus subcarpetas, devolver una cadena vacía
        FindFolder = ""
    End Function


    Private Function GetBase64(Fichero As String) As String
        Dim bytes As Byte()
        Using fs As New FileStream(Fichero, FileMode.Open, FileAccess.Read)
            bytes = New Byte(fs.Length - 1) {}
            fs.Read(bytes, 0, bytes.Length)
        End Using

        ' Convertir el arreglo de bytes en una cadena Base64
        Return Convert.ToBase64String(bytes)
    End Function

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If Server.State = 1 Then Server.Stop()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        My.Settings.PuertoHTTP = TextBox1.Text
        My.Settings.Save()
        If Server.State = 1 Then
            Server.Stop()
            Server.ServerPort = My.Settings.PuertoHTTP
            Server.Start()
        Else
            Server.ServerPort = My.Settings.PuertoHTTP
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim folderBrowserDialog1 As New FolderBrowserDialog()
        'Muestra el cuadro de diálogo para seleccionar una carpeta
        Dim result As DialogResult = folderBrowserDialog1.ShowDialog()
        'Si el usuario selecciona una carpeta, muestra la ruta de la carpeta en un cuadro de texto
        If result = DialogResult.OK Then
            Me.TextBoxMainFolder.Text = folderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub TextBoxMainFolder_TextChanged(sender As Object, e As EventArgs) Handles TextBoxMainFolder.TextChanged
        My.Settings.RutaBMS = Me.TextBoxMainFolder.Text
        My.Settings.Save()
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Server.Stop()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button2.Enabled = False
        Server.Start()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MakeBriefing()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        SaveFileDialog1.Filter = "Archivos HTML (*.html)|*.html|Todos los archivos (*.*)|*.*"
        SaveFileDialog1.Title = "Guardar archivo HTML"
        SaveFileDialog1.FileName = "briefing.html"
        ' Muestra el cuadro de diálogo para guardar el archivo
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Guarda el contenido de la variable en el archivo seleccionado
            Using sw As New StreamWriter(SaveFileDialog1.FileName)
                sw.Write(Server.Html)
            End Using
        End If
    End Sub

    Const RTD As Single = 57.29578F
    Const DTR As Single = 0.01745329F
    Const EARTH_RADIUS_NM As Single = 3443.92236F 'Mean Equatorial Radius
    Const EARTH_RADIUS_FT As Single = 2.09257E+7F 'Mean Equatorial Radius
    Const NM_PER_MINUTE As Single = 1.00018F 'Nautical Mile per Minute of Latitude (Or Longitude at Equator)
    Const MINUTE_PER_NM As Single = 0.9982F 'Minutes of Latitude (or Longitude at Equator) per NM
    Const FT_PER_MINUTE As Single = 6087.03125F 'Feet per Minute of Latitude (Or Longitude at Equator)
    Const MINUTE_PER_FT As Single = 0.000164283F 'Minutes of Latitude (or Longitude at Equator) per foot
    Const FT_PER_DEGREE As Single = FT_PER_MINUTE * 60.0F

    Sub ConvertCoordinates(ByVal FALCON_ORIGIN_LAT As Single, ByVal FALCON_ORIGIN_LONG As Single, ByVal X As Double, ByVal Y As Double, ByRef LatSalida As String, ByRef LonSalida As String)
        Dim lat As Single = 0
        Dim latd As Integer = 0
        Dim latm As Single = 0
        Dim lon As Single = 0
        Dim lond As Integer = 0
        Dim lonm As Single = 0
        lat = (FALCON_ORIGIN_LAT * FT_PER_DEGREE + X) / EARTH_RADIUS_FT
        lat = R2D(lat)
        latd = Fix(lat)
        latm = (lat - latd) * 60

        Dim cosLat As Single = CSng(Math.Cos(D2R(lat)))
        lon = ((FALCON_ORIGIN_LONG * DTR * EARTH_RADIUS_FT * cosLat) + Y) / (EARTH_RADIUS_FT * cosLat)
        lon = R2D(lon)
        lond = Fix(lon)
        lonm = (lon - lond) * 60

        LatSalida = latd & "º " & latm & "'"
        LonSalida = lond & "º " & lonm & "'"
    End Sub

    Private Function R2D(ByVal radians As Single) As Single
        Return radians * RTD
    End Function

    Private Function D2R(ByVal degrees As Single) As Single
        Return degrees * DTR
    End Function

End Class