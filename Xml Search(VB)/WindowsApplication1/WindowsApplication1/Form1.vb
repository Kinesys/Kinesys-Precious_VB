Imports System.Net 'WebClient 클래스 사용
Imports System.Xml 'XmlDocument, XmlNodeList 클래스 사용
Imports System.IO 'StringReader 클래스 사아

Public Class Form1

    Const headerURL = "https://localhost/"

    Dim Nullable As XmlAttribute

    Private Sub bthSearch_Click(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.lvFile.Items.Clear()

        Dim wc = New WebClient()

        Dim buffer = wc.DownloadString(String.Format( _
            " {0}WebXml.xml ", headerURL))

        wc.Dispose()

        Dim sr As StringReader = New StringReader(buffer)

        Dim doc As XmlDocument = New XmlDocument()

        doc.Load(sr)

        sr.Close()

        Dim forecastNodes As XmlNodeList = _
            doc.SelectNodes(" xml_reply/human/human_entry ")

        For Each node As XmlNode In forecastNodes
            Me.lvFile.Items.Add(New ListViewItem( _
                New String() {GetNodeValue(node, "title")}))
        Next

    End Sub

    Private Function GetNodeValue(ByVal parent As XmlNode, _
            ByVal name As String) As String

        Try

            Dim attr As XmlAttribute = _
                parent.SelectSingleNode(name).Attributes("name")
            If attr.Value <> " " Then
                Return attr.Value
            End If

            Return " "
        Catch
            Return " "
        End Try

    End Function

End Class
