Public Class asd
    Private Sub close_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()
        Timer2.Start()
        Timer3.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Value = ProgressBar1.Value + 3
        If ProgressBar1.Value = 100 Then
            Timer2.Stop()
            Timer3.Stop()
            Application.Exit()
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If ProgressBar1.Value = 26 Then
            Label2.Text = "turning wlan0 monitor mode off..."
        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        If ProgressBar1.Value = 70 Then
            Label2.Text = "Saving Configs..."
        End If
    End Sub
End Class