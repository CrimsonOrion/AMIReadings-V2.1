Imports System.Data.OleDb
Imports System.IO
Imports System.Data.SqlClient

' Version 1.0 - Added basic functionality.  Imported CUDTACCR/BLPRHIST from .xlsx from DB2 as well as CUDTACCR/BLPRDING.  Copies the AMI readings that have not been read by ITRON to the new
'               BLPRDING file, then upload the file to AS/400.
' Version 1.1 - Made the deletion of duplicates faster by ordering the new BLPRDING by account, sub, and service.  Then checking the line immediately above it for doubles instead of checking
'               the whole list for each entry.  Also, took made KW Readings 0 for non-Demand accounts.
' Version 1.11 - Made the Rate codes Strings instead of Doubles to accomidate W0x for water accounts.
' Version 1.2 - Attempt to add readings for BLPAXMTR.  (UNFINISHED)
' Version 2.0 - Complete rewrite of original AMI Readings (retired at v1.2).  Instead of BLPRDING being the primary table and building everything off of that and data from BLPRHIST,
'               BLPRHIST is the primary and BLPAXMTR is edited from that data and a new BLPRDING is created using data from BLPRHIST and BLPRDING.
' Version 2.01 - Made change so if the acct number is higher in BLPRHIST than in the last record of BLPRDING, copy rest of BLPRHIST to BLPRDING_New.  ALSO: Delete all custom .csv upon completion
'               AND any exit rather than just completing program as well as clearing the tables and dgvBLPRDING_NEW datagrid view.
' Version 2.1 - Added ability to import water readings for Harker's Island and Pine Knoll Shores to BLPRDING.  Also added a message box that appears when the import is finished successfully.
' Version 2.11 - 3/1/16 - Changed the sheet name from AMI Readings by Reading Cycle to AMI Readings by Date for Readin so I could import the new queries that are seperated by Reading Cycle.
'               This makes life MUCH easier since we won't have to manually select the routes.  Less risk of errors.
' Version 2.12 - 4/13/16 - Changed the table and data grid view to clear when Convert and Combine is pressed instead of when the open file dialog is opened.  This will prevent duplicates in the dgv
'                should the user error out before the cleanup takes place and has to re-upload the same file.
' Version 2.13 - 1/12/18 - Edited EditBLPRIST(): changed KW Reading by dividing reading by Multiplier (index 17).  Remove Multiplier value afterwards.

Public Class frmMain

#Region "Class Variables"

    Const Quote = """"                  'To add the ""s in the .csv fields that are null
    Dim errorCode As Boolean = False    'Checks to see if there's an error in the .xlsx file from DB2.  If so, switch to "True" and attempt to re-run the conversion
    Dim dsAMIReadings As DataSet = New DataSet("AMIReadings")   'The dataset that the tables will use

    'The 3 data tables we'll be working with.
    Dim tblBLPRHIST As DataTable = dsAMIReadings.Tables.Add("BLPRHIST")
    Dim tblBLPAXMTR As DataTable = dsAMIReadings.Tables.Add("BLPAXMTR")
    Dim tblBLPRDING As DataTable = dsAMIReadings.Tables.Add("BLPRDING")

#End Region

#Region "Form Events"

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'If the form gets closed before it finished, run the file cleanup to remove all temp files.
        CleanUp()
    End Sub

#End Region

#Region "Buttons"

    Private Sub btnOFDExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOFDExcel.Click

        'This handles the Open File Dialog for the .xlsx file from DB2

        Dim OFDExcel As New OpenFileDialog      'Create the open file dialog object

        'Set the default options for the open file dialog object
        With OFDExcel
            .Filter = "DB2 Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*"     'The filters the open file dialog uses
            .FilterIndex = 1                                                    'Which filter is the default
            .RestoreDirectory = False                                           'Does the filter keep the previously used directory as a setting or always go back to default?
            .Multiselect = False                                                'Can you select more than one file?
        End With

        'Once the user selects the file, do these things:
        If OFDExcel.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtExcelFile.Text = OFDExcel.FileName                               'Set the filename in the open file dialog to txtExcelFile textbox
            btnConvUpload.Enabled = True                                        'Enable the Convert and Upload button.
        End If

        '***EDITTED 4/13/16 to happen whenever btnConvUpload is pressed.  This happened to Elizabeth when she errored out of a reading pull and the dgv didn't clear on the next run, causing duplicate readings.
        'tblBLPAXMTR.Clear()
        'tblBLPRDING.Clear()
        'tblBLPRHIST.Clear()
        'dgvBLPRDING_New.Rows.Clear()

    End Sub

    Private Sub btnConvUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConvUpload.Click

        Try

            'Clear the tables and data grid view
            tblBLPAXMTR.Clear()
            tblBLPRDING.Clear()
            tblBLPRHIST.Clear()
            dgvBLPRDING_New.Rows.Clear()

            'Start downloading the present BLPRDING file
            LoadProcess("\\Cc-fs1\ss\AMIReadings\FromBLPRDING.dtf")
            'LoadProcess("\\carteret.ncemcs.com\dfs\users\jim\CC400\FromBLPRDING.dtfx")

            'Start downloading the present BLPAXMTR file
            LoadProcess("\\Cc-fs1\ss\AMIReadings\FromBLPAXMTR.dtf")
            'LoadProcess("\\carteret.ncemcs.com\dfs\users\jim\CC400\FromBLPAXMTR.dtfx")

            'Note that the conversion and Combination process has started.
            MsgBox("Convert and Combine has started")

            'Pull the Modified BLPRHIST Excel file and put in the tblBLPRHIST table and display the table in dgvBLPRHIST  ***EDITTED 3/1/2016 to accomidate new sheet name****
            'FillDataGridView(".xlsx", txtExcelFile.Text, "SELECT * FROM [AMI Readings by Reading Cycle$]", tblBLPRHIST, dgvBLPRHIST, "yes", "ACCOUNT, SUB, SERVICE")
            FillDataGridView(".xlsx", txtExcelFile.Text, "SELECT * FROM [AMI Readings by Date for Readin$]", tblBLPRHIST, dgvBLPRHIST, "yes", "ACCOUNT, SUB, SERVICE")

            'Pull the BLPRDING CSV file and put in the tblBLPRDING table and display the table in dgvBLPRDING
            FillDataGridView(".csv", "\\Cc-fs1\ss\AMIReadings", "SELECT * FROM [FromBLPRDING.csv]", tblBLPRDING, dgvBLPRDING, , "F1, F2, F3")
            'FillDataGridView(".csv", "\\carteret.ncemcs.com\dfs\users\jim\CC400", "SELECT * FROM [FromBLPRDING.csv]", tblBLPRDING, dgvBLPRDING, , "F1, F2, F3")

            'Pull the BLPAXMTR CSV file and put in the tblBLPAXMTR table and display the table in dgvBLPAXMTR
            FillDataGridView(".csv", "\\Cc-fs1\ss\AMIReadings", "SELECT * FROM [FromBLPAXMTR.csv]", tblBLPAXMTR, dgvBLPAXMTR, , "F1, F2, F3")
            'FillDataGridView(".csv", "\\carteret.ncemcs.com\dfs\users\jim\CC400", "SELECT * FROM [FromBLPAXMTR.csv]", tblBLPAXMTR, dgvBLPAXMTR, , "F1, F2, F3")

            'Clean up files
            '****** This now happens with the CleanUp() procedure ******
            'File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\FromBLPRDING.csv")
            'File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\FromBLPAXMTR.csv")

            'Compare BLPRHIST to BLPAXMTR based on Meter Number and make the change to the reading if there is a match.
            EditBLPAXMTR()

            'Export the editted dgvBLPAXMTR to a csv, ready to upload.
            ExportDGV_BLPAXMTRtoCSV("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPAXMTR.csv", dgvBLPAXMTR)
            'ExportDGV_BLPAXMTRtoCSV("\\carteret.ncemcs.com\dfs\users\jim\CC400\ToBLPAXMTR.csv", dgvBLPAXMTR)

            'Fix BLPRHIST by changing demands for Rate 101, 102, 103, 199, and 405 to 0, and removing the Rate Code and Meter Columns
            EditBLPRHIST()

            'Compare BLPRHIST to BLPRDING based on Acct#, Sub, and Service and make changes to the reading if there is a match.
            EditBLPRDING()

            'Export the editted dgvBLPRDING_NEW to a csv, ready to upload.
            ExportDGV_BLPRDING_NEWToCSV("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPRDING.csv", dgvBLPRDING_New)
            'ExportDGV_BLPRDING_NEWToCSV("\\carteret.ncemcs.com\dfs\users\jim\CC400\ToBLPRDING.csv", dgvBLPRDING_New)

            'Upload the new BLPAXMTR
            LoadProcess("\\Cc-fs1\ss\AMIReadings\ToBLPAXMTR.dtt")
            'LoadProcess("\\carteret.ncemcs.com\dfs\users\jim\CC400\ToBLPAXMTR.dttx")

            'Upload the new BLPRDING
            LoadProcess("\\Cc-fs1\ss\AMIReadings\ToBLPRDING.dtt")
            'LoadProcess("\\carteret.ncemcs.com\dfs\users\jim\CC400\ToBLRDING.dttx")

            'Clean up new files
            '****** This now happens with the CleanUp() procedure ******
            'File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPRDING.csv")
            'File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPAXMTR.csv")
            CleanUp()
            MsgBox("Readings Imported Successfully.")

        Catch ex As Exception
            MsgBox(ex.Message)      'If there's an error, display the error message.  ** POSSIBLE FUTURE UPDATE: Automatically email Jim a copy of the error message? **

        Finally
            btnOFDExcel.Enabled = True  'Re-enable the Excel OpenFileDialog button.

            ' This is to make sure the temp files are wiped so they don't get appended if the program needs to run again.
            CleanUp()

        End Try

    End Sub

#End Region

#Region "Procedures"

    Private Sub FillDataGridView(ByVal Type As String, ByVal File As String, ByVal Query As String, ByVal Datatable As DataTable, ByVal Datagridview As DataGridView, Optional ByVal HeaderRow As String = "No", Optional ByVal SortColumn As String = "")

        Dim odcCN As OleDbConnection
        Dim odcDA As OleDbDataAdapter

        Try

            'Use the correct connection string based on the type, whether its .xlsx (Excel 2010+), .csv, etc.  Go to http://www.connectionstrings.com/ for a full list.
            Select Case Type
                Case ".xlsx"
                    odcCN = New OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + File + ";Extended Properties=""Excel 12.0;HDR=" + HeaderRow + ";""")
                Case ".xls"
                    odcCN = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + File + ";Extended Properties=""Excel 8.0;HDR=" + HeaderRow + ";""")
                Case ".csv"
                    odcCN = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + File + ";Extended Properties=""text;HDR=" + HeaderRow + ";FMT=Delimited;""")
            End Select

            odcCN.Open()                                'Open the connection
            odcDA = New OleDbDataAdapter(Query, odcCN)  'Use the dataAdapter to get the data from the selected Connection
            Datatable = New DataTable                   'Create a dataTable to put all the data from the dataAdapter into
            odcDA.Fill(Datatable)                       'Fill the dataTable with the pulled info from the dataAdapter
            odcCN.Close()                               'Close the connection once the data is imported
            Datatable.DefaultView.Sort = SortColumn     'Sort by SortColumn(s), if applicable
            With Datagridview
                .DataSource = Datatable                 'Make the dataGridView display the data from the dataTable
            End With

        Catch ex As Exception
            MsgBox(ex.Message)      'If there's an error, display the error message.  ** POSSIBLE FUTURE UPDATE: Automatically email Jim a copy of the error message? **

        End Try

    End Sub

    Private Sub EditBLPAXMTR()

        Dim intCount As Integer = 0         'Holds the count of number of AUX meters with readings added

        For intBLPAXMTRRow As Integer = 0 To dgvBLPAXMTR.Rows.Count - 1             'Define intBLPAXMTRRow as tracker for row in BLPAXMTR and go from top to bottom.
            For intBLPRHISTRow As Integer = dgvBLPRHIST.Rows.Count - 1 To 0 Step -1 'Define intBLPRHISTRow as tracker for row in BLPRHIST and go from bottom to top by 1 row at a time
                If IsDBNull(dgvBLPRHIST.Rows(intBLPRHISTRow).Cells(0).Value) Then
                    'Do Nothing.  It'll get deleted when it exports.
                Else
                    If dgvBLPRHIST.Item(16, intBLPRHISTRow).Value = dgvBLPAXMTR.Item(3, intBLPAXMTRRow).Value Then 'If the meter number column in BLPRHIST and BLPAXMTR match...
                        dgvBLPAXMTR.Item(18, intBLPAXMTRRow).Value = dgvBLPRHIST.Item(4, intBLPRHISTRow).Value       'Replace the reading with the current reading in BLPRHIST
                        dgvBLPAXMTR.Item(19, intBLPAXMTRRow).Value = dgvBLPRHIST.Item(3, intBLPRHISTRow).Value       'Replace the date with the read date in BLPRHIST
                        MsgBox("Meter Number " + dgvBLPAXMTR.Item(3, intBLPAXMTRRow).Value.ToString + " has been updated.")
                        dgvBLPRHIST.Rows.RemoveAt(intBLPRHISTRow)       'Remove the Aux Meter reading row from BLPRHIST so BLPRDING doesn't accidentally get the reading
                    End If
                End If
            Next    'Move to next record in dgvBLPRHIST
        Next        'Move to next record in dgvBLPAXMTR

    End Sub

    Private Sub EditBLPRHIST()

        'Fix dgvBLPRHIST by changing demands for Rate 101, 102, 103, 199, and 405 to 0, and removing the Rate Code and Meter Columns
        ' 1/12/18 - change KW Reading by dividing reading by Multiplier (index 17).  Remove Multiplier value afterwards.
        With dgvBLPRHIST

            For dgvBLPRHISTRow As Integer = .RowCount - 1 To 0 Step -1
                If IsDBNull(.Item(15, dgvBLPRHISTRow).Value) Then
                    'Do nothing.  This is an empty record.  Will be removed upon export.
                    dgvBLPRHIST.Rows.RemoveAt(dgvBLPRHISTRow)
                Else
                    If .Item(15, dgvBLPRHISTRow).Value Like "W0*" Then
                        'Do Nothing.  This is to keep from throwing a "W0*" isn't a 'double' error.
                        '****** This is now used to denote a Harker's Island Water account ********
                        .Item(15, dgvBLPRHISTRow).Value = 3
                    ElseIf .Item(15, dgvBLPRHISTRow).Value Like "P*" Then
                        '****** This is now used to denote a Pine Knoll Shores Water account ********
                        .Item(15, dgvBLPRHISTRow).Value = 2
                    ElseIf .Item(15, dgvBLPRHISTRow).Value = 101 Or .Item(15, dgvBLPRHISTRow).Value = 102 Or .Item(15, dgvBLPRHISTRow).Value = 103 Or .Item(15, dgvBLPRHISTRow).Value = 199 Or .Item(15, dgvBLPRHISTRow).Value = 405 Then
                        .Item(5, dgvBLPRHISTRow).Value = 0
                    Else ' 1/12/18 ******
                        .Item(5, dgvBLPRHISTRow).Value = .Item(5, dgvBLPRHISTRow).Value / .Item(17, dgvBLPRHISTRow).Value
                    End If
                End If
            Next    'Move to next record in dgvBLPRHIST

            'Remove the three unneeded columns
            .Columns.Remove("Rate Code")
            .Columns.Remove("Meter")
            .Columns.Remove("MULTIPLIER")
        End With

    End Sub

    Private Sub EditBLPRDING()

        Dim dgvBLPRDINGRow As Integer = 0       'For keeping track of which row in dgvBLPRDING we left off at
        Dim dgvBLPRHISTRow As Integer = 0       'For keeping track of which row in dgvBLPRHIST we left off at
        Dim intRemovedReadings As Integer = 0   'For keeping track of how many already have readings and thus were discarded
        Dim strRemovedReadings As String = Nothing  'The string holding the removed readings
        Dim strRemovedReadingsFileName As String = Path.GetDirectoryName(txtExcelFile.Text) + "\RemovedReadings.txt"  'For the filename for the removed readings

        'Start at the top of both dgvBLPRHIST and dgvBLPRDING
        For dgvBLPRHISTRow = dgvBLPRHISTRow To dgvBLPRHIST.RowCount - 1
            For dgvBLPRDINGRow = dgvBLPRDINGRow To dgvBLPRDING.RowCount - 1

                If dgvBLPRHIST.Item(0, dgvBLPRHISTRow).Value > dgvBLPRDING.Item(0, dgvBLPRDINGRow).Value Then
                    'If the acct number in BLPRHIST is higher than BLPRDING, move to the next record in BLPRDING

                ElseIf dgvBLPRHIST.Item(0, dgvBLPRHISTRow).Value = dgvBLPRDING.Item(0, dgvBLPRDINGRow).Value And dgvBLPRHIST.Item(1, dgvBLPRHISTRow).Value > dgvBLPRDING.Item(1, dgvBLPRDINGRow).Value Then
                    'If the sub number in BLPRHIST is higher than BLPRDING, move to the next record in BLPRDING

                ElseIf dgvBLPRHIST.Item(0, dgvBLPRHISTRow).Value = dgvBLPRDING.Item(0, dgvBLPRDINGRow).Value And dgvBLPRHIST.Item(1, dgvBLPRHISTRow).Value = dgvBLPRDING.Item(1, dgvBLPRDINGRow).Value And dgvBLPRHIST.Item(2, dgvBLPRHISTRow).Value > dgvBLPRDING.Item(2, dgvBLPRDINGRow).Value Then
                    'If the service number in BLPRHIST is higher than BLPRDING, move to the next record in BLPRDING

                ElseIf dgvBLPRHIST.Item(0, dgvBLPRHISTRow).Value = dgvBLPRDING.Item(0, dgvBLPRDINGRow).Value And dgvBLPRHIST.Item(1, dgvBLPRHISTRow).Value = dgvBLPRDING.Item(1, dgvBLPRDINGRow).Value And dgvBLPRHIST.Item(2, dgvBLPRHISTRow).Value = dgvBLPRDING.Item(2, dgvBLPRDINGRow).Value Then
                    'If the acct, sub, and serv numbers in BLPRHIST are equal to a record in BLPRDING, add it to the Removed Readings file and move to the next record in BLPRHIST and BLPRDING since a record already exists
                    strRemovedReadings += "Removed Acct: " + dgvBLPRHIST.Item(0, dgvBLPRHISTRow).Value.ToString + " Sub: " + dgvBLPRHIST.Item(1, dgvBLPRHISTRow).Value.ToString + " Date: " + dgvBLPRHIST.Item(3, dgvBLPRHISTRow).Value.ToString + " Readings: " + dgvBLPRHIST.Item(4, dgvBLPRHISTRow).Value.ToString + vbCrLf
                    intRemovedReadings += 1
                    'Add the current row from BLPRDING to the BLPRDING_New
                    CopyData(dgvBLPRDING, dgvBLPRDINGRow, dgvBLPRDING_New)

                    'Increase dgvBLPRDINGRow by 1 to go to the next row since the appropriate 'Next' gets skipped.
                    dgvBLPRDINGRow += 1
                    Exit For

                Else
                    'If the Acct#, Sub, and Serv in BLPRHIST are lower than those in BLPRDING, then the reading isn't in BLPRDING and has to be added.  Move to next record in BLPRHIST
                    CopyData(dgvBLPRHIST, dgvBLPRHISTRow, dgvBLPRDING_New)
                    Exit For
                End If
                'Add the current row from BLPRDING to the BLPRDING_New
                CopyData(dgvBLPRDING, dgvBLPRDINGRow, dgvBLPRDING_New)
            Next
            'If BLPRDING has finished, but there are still rows in BLPRHIST, exit the loop.  The pickup at the end will grab the rest.
            If dgvBLPRDINGRow = dgvBLPRDING.RowCount And dgvBLPRHISTRow <= dgvBLPRHIST.RowCount - 1 Then
                dgvBLPRHISTRow += 1
                Exit For
            End If
        Next

        If dgvBLPRDINGRow < dgvBLPRDING.RowCount - 1 Then
            Do Until dgvBLPRDINGRow > dgvBLPRDING.RowCount - 1
                'Add the rest of the rows from BLPRDING to the BLPRDING_New
                CopyData(dgvBLPRDING, dgvBLPRDINGRow, dgvBLPRDING_New)
                dgvBLPRDINGRow += 1
            Loop

        ElseIf dgvBLPRHISTRow < dgvBLPRHIST.RowCount - 1 Then
            Do Until dgvBLPRHISTRow > dgvBLPRHIST.RowCount - 1
                'Add the rest of the rows from BLPRHIST to the BLPRDING_New
                CopyData(dgvBLPRHIST, dgvBLPRHISTRow, dgvBLPRDING_New)
                dgvBLPRHISTRow += 1
            Loop
        End If

            'Create the removed readings file populated with the meter reads that were already in BLPRDING
            My.Computer.FileSystem.WriteAllText(strRemovedReadingsFileName, strRemovedReadings, False)
            MsgBox(intRemovedReadings.ToString + " duplicates have been removed.  A list of removed readings can be found here:" + vbCrLf + strRemovedReadingsFileName + ".")

    End Sub

    Private Sub CopyData(ByVal source As DataGridView, ByVal sourceIndex As Integer, ByVal dest As DataGridView)

        'This copies the data from one DGV to another.  In this case, dgvRHIST to dgvRDING_NEW

        'Create the string array with the necessary fields
        Dim row As String() = New String() {source.Item(0, sourceIndex).Value, _
                                            source.Item(1, sourceIndex).Value, _
                                            source.Item(2, sourceIndex).Value, _
                                            source.Item(3, sourceIndex).Value, _
                                            source.Item(4, sourceIndex).Value, _
                                            source.Item(5, sourceIndex).Value, _
                                            source.Item(6, sourceIndex).Value, _
                                            source.Item(7, sourceIndex).Value.ToString, _
                                            source.Item(8, sourceIndex).Value, _
                                            source.Item(9, sourceIndex).Value, _
                                            source.Item(10, sourceIndex).Value.ToString, _
                                            source.Item(11, sourceIndex).Value, _
                                            source.Item(12, sourceIndex).Value, _
                                            source.Item(13, sourceIndex).Value.ToString, _
                                            source.Item(14, sourceIndex).Value.ToString}

        'Add the data to the destination datagridview
        dest.Rows.Add(row)

    End Sub

    Private Sub CleanUp()

        ' This is to make sure the temp files are wiped so they don't get appended if the program needs to run again.
        If File.Exists("\\\Cc-fs1\\ss\\AMIReadings\\FromBLPRDING.csv") Then
            File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\FromBLPRDING.csv")
        End If

        If File.Exists("\\\Cc-fs1\\ss\\AMIReadings\\FromBLPAXMTR.csv") Then
            File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\FromBLPAXMTR.csv")
        End If

        If File.Exists("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPRDING.csv") Then
            File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPRDING.csv")
        End If

        If File.Exists("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPAXMTR.csv") Then
            File.Delete("\\\Cc-fs1\\ss\\AMIReadings\\ToBLPAXMTR.csv")
        End If

    End Sub

    Private Sub ExportDGV_BLPAXMTRtoCSV(ByVal strExportFileName As String, ByVal DataGridView As DataGridView, Optional ByVal blnWriteColumnHeaderNames As Boolean = False, Optional ByVal strDelimiterType As String = ",")

        'I totally Ctrl+C and Ctrl+V'd this.  You should be able to follow along.  That Select Case is where there are string fields that need "".

        Dim sw As StreamWriter = File.CreateText(strExportFileName)
        Dim strDelimiter As String = strDelimiterType
        Dim intColumnCount As Integer = DataGridView.Columns.Count - 1
        Dim strRowData As String = ""

        Dim blnWriteline As Boolean = True

        If blnWriteColumnHeaderNames Then
            For intX As Integer = 0 To intColumnCount
                strRowData += Replace(DataGridView.Columns(intX).Name, strDelimiter, "") & IIf(intX < intColumnCount, strDelimiter, "")
            Next intX
            sw.WriteLine(strRowData)
        End If

        For intX As Integer = 0 To DataGridView.Rows.Count - 1
            strRowData = ""
            For intRowData As Integer = 0 To intColumnCount
                Select Case intRowData
                    Case 4, 6, 8, 15, 19, 20, 24, 28, 32
                        strRowData += Replace(Quote + DataGridView.Rows(intX).Cells(intRowData).Value + Quote, strDelimiter, "") & IIf(intRowData < intColumnCount, strDelimiter, "")
                    Case Else
                        strRowData += Replace(DataGridView.Rows(intX).Cells(intRowData).Value, strDelimiter, "") & IIf(intRowData < intColumnCount, strDelimiter, "")
                End Select
                blnWriteline = True

            Next intRowData
            If blnWriteline Then
                sw.WriteLine(strRowData)
            End If
        Next intX
        sw.Close()

    End Sub

    Private Sub ExportDGV_BLPRDING_NEWToCSV(ByVal strExportFileName As String, ByVal DataGridView As DataGridView, Optional ByVal blnWriteColumnHeaderNames As Boolean = False, Optional ByVal strDelimiterType As String = ",")

        'I totally Ctrl+C and Ctrl+V'd this.  You should be able to follow along.  That Select Case is where there are string fields that need "".

        Dim sw As StreamWriter = File.CreateText(strExportFileName)
        Dim strDelimiter As String = strDelimiterType
        Dim intColumnCount As Integer = DataGridView.Columns.Count - 1
        Dim strRowData As String = ""

        Dim blnWriteline As Boolean = True

        If blnWriteColumnHeaderNames Then
            For intX As Integer = 0 To intColumnCount
                strRowData += Replace(DataGridView.Columns(intX).Name, strDelimiter, "") & IIf(intX < intColumnCount, strDelimiter, "")
            Next intX
            sw.WriteLine(strRowData)
        End If

        For intX As Integer = 0 To DataGridView.Rows.Count - 1
            strRowData = ""
            For intRowData As Integer = 0 To intColumnCount
                If IsDBNull(DataGridView.Rows(intX).Cells(intRowData).Value) Then
                    blnWriteline = False
                Else
                    Select Case intRowData
                        Case 3, 7, 10 To 14
                            strRowData += Replace(Quote + DataGridView.Rows(intX).Cells(intRowData).Value + Quote, strDelimiter, "") & IIf(intRowData < intColumnCount, strDelimiter, "")
                        Case Else
                            strRowData += Replace(DataGridView.Rows(intX).Cells(intRowData).Value, strDelimiter, "") & IIf(intRowData < intColumnCount, strDelimiter, "")
                    End Select
                    blnWriteline = True
                End If
            Next intRowData
            If blnWriteline Then
                sw.WriteLine(strRowData + "," + Quote + Quote + "," + Quote + Quote + "," + Quote + "01/01/0001" + Quote)
            End If
        Next intX
        sw.Close()

    End Sub

    Private Sub LoadProcess(ByVal File As String)

        'This process runs the file specified in File.
        Dim LoadProcess = New Process
        With LoadProcess
            .StartInfo.FileName = File      'Specifies the file to run
            .Start()                        'Runs the file with default associations
            .EnableRaisingEvents = True     'Sets whether the HasExited event is raised when it's exited. 
        End With

        'Wait for the process to complete
        Do While LoadProcess.HasExited = False  'While the program is still open, continue an empty loop.
        Loop

    End Sub

#End Region

End Class
