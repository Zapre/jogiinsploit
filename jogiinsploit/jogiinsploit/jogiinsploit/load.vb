Imports System.ComponentModel

Public Class load
    Private Sub load_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Timer2.Start()
        Timer3.Start()
        Timer4.Start()
        Timer5.Start()
        Timer6.Start()
        Timer7.Start()
        Timer8.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Value = ProgressBar1.Value + 1
        'space just too make it look nice and neat and pretend there is 1 line of extra code
        If ProgressBar1.Value = ProgressBar1.Maximum Then
            ProgressBar1.Value = 100
            Timer1.Stop()
            Timer2.Start()
            Timer3.Start()
            Timer4.Start()
            Timer5.Start()
            Timer6.Start()
            Timer7.Start()
            Timer8.Start()
            main.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If ProgressBar1.Value = 13 Then
            Label2.Text = "Injecting XD]# Into Datajase..."
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If ProgressBar1.Value = 21 Then
            Label2.Text = "Using Avast Decompiler Scripts..."
        End If
    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        If ProgressBar1.Value = 35 Then
            Label2.Text = "Hacking FBI..."
        End If
    End Sub

    Private Sub Timer5_Tick(sender As Object, e As EventArgs) Handles Timer5.Tick
        If ProgressBar1.Value = 50 Then
            Label2.Text = "Converting Code To 'English'..."
        End If
    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        If ProgressBar1.Value = 67 Then
            Label2.Text = "Using Anti Mrexy-Ware WAF Scripts..."
        End If
    End Sub

    Private Sub Timer7_Tick(sender As Object, e As EventArgs) Handles Timer7.Tick
        If ProgressBar1.Value = 83 Then
            Label2.Text = "DLL Importing..."
        End If
    End Sub

    Private Sub Timer8_Tick(sender As Object, e As EventArgs) Handles Timer8.Tick
        If ProgressBar1.Value = 100 Then
            Label2.Text = "Launching..."
        End If
    End Sub

    Private Sub load_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Timer1.Stop()
        Timer2.Stop()
        Timer3.Stop()
        Timer4.Stop()
        Timer5.Stop()
        Timer6.Stop()
        Timer7.Stop()
        Timer8.Stop()
    End Sub
End Class