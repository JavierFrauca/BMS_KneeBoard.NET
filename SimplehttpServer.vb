Imports System.Net
Imports System.Threading

Public Class SimpleHttpServer
    Private mvarServerPort As String
    Public Property ServerPort() As String
        Get
            Return mvarServerPort
        End Get
        Set(ByVal value As String)
            mvarServerPort = value
        End Set
    End Property
    Private mvarhtml As String = ""
    Public Property Html() As String
        Get
            Return mvarhtml
        End Get
        Set(ByVal value As String)
            mvarhtml = value
        End Set
    End Property
    Private mvarState As Integer = 0
    Public ReadOnly Property State() As Integer
        Get
            Return mvarState
        End Get
    End Property

    Private listener As HttpListener
    Private serverThread As New Thread(AddressOf ServerLoop)

    Public Sub Start()
        Try
            If listener IsNot Nothing Then
                If listener.IsListening = True Then
                    listener.Stop()
                End If
                listener.Close()
            End If
            listener = New HttpListener()
            listener.Prefixes.Add("http://*:" & mvarServerPort & "/") 'Agrega aquí tus prefijos
            listener.Start()

            Console.WriteLine("Servidor iniciado.")
            mvarState = 1
            'Crea un nuevo hilo y ejecuta el servidor en ese hilo
            serverThread.Start()
        Catch ex As Exception
            mvarState = 0
        End Try

    End Sub

    Private Sub ServerLoop()

        While True
            Try
                Dim context As HttpListenerContext = listener.GetContext()
                Dim response As HttpListenerResponse = context.Response
                'Crea una respuesta básica
                Dim buffer As Byte() = System.Text.Encoding.UTF8.GetBytes(mvarhtml)
                'Envía la respuesta al cliente
                response.ContentType = "text/html"
                response.ContentLength64 = buffer.Length
                Dim output As System.IO.Stream = response.OutputStream
                output.Write(buffer, 0, buffer.Length)
                output.Close()
            Catch ex As Exception
                'ignoralo es un error
            End Try
        End While
    End Sub

    Public Sub [Stop]()
        If listener IsNot Nothing Then
            If listener.IsListening = True Then
                listener.[Stop]()
                listener.Close()
                mvarState = 0
            End If
        End If
        If serverThread.IsAlive Then serverThread.Abort()
    End Sub

End Class