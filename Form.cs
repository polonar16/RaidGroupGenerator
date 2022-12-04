using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;

namespace RaidCompGenerator
{
    public partial class Form : System.Windows.Forms.Form
    {
        RaidComposition desiredRaidComposition;
        RaidGroupGenerator raidGroupGenerator;
        RaidGroupCollection raidGroupCollection;
        List<RaidGroupCollection> raidGroupCollections;

        int randomSeed = 0;
        bool regenerating = false;
        string filePath = "";
        Process importURLProcess;

        public Form()
        {
            InitializeComponent();

            desiredRaidComposition = new RaidComposition();
            raidGroupGenerator = new RaidGroupGenerator();
            raidGroupCollections = new List<RaidGroupCollection>();

            textBoxRandomSeed.Text = randomSeed.ToString();

            backgroundWorkerGenerate.WorkerReportsProgress = true;

#if !DEBUG
            buttonRegenerate.Enabled = false;
            buttonRegenerate.Visible = false;
#endif
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Microsoft XLS Files|*.xls";
            openFileDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(filePath) + " Output";
            saveFileDialog.Filter = "Microsoft XLS Files|*.xls";
            saveFileDialog.ShowDialog();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            string[] blankRow = new string[] { "", "", "", "", "" };

            raidCompDataGridView.Rows.Add(blankRow);
            raidCompDataGridView.Rows.Add(blankRow);
            raidCompDataGridView.Rows.Add(blankRow);
            raidCompDataGridView.Rows.Add(blankRow);
            raidCompDataGridView.Rows.Add(blankRow);
            raidCompDataGridView.ClearSelection();

            raidDataGridView.Rows.Add(blankRow);
            raidDataGridView.Rows.Add(blankRow);
            raidDataGridView.Rows.Add(blankRow);
            raidDataGridView.Rows.Add(blankRow);
            raidDataGridView.Rows.Add(blankRow);
            raidDataGridView.ClearSelection();

            LoadConfig();
        }

        private void ImportDesiredRaidComposition(Workbook workbook)
        {
            Worksheet worksheet = workbook.GetWorksheet("Output");
            CellCollection worksheetCells = worksheet.Cells;

            int partyMemberIndex = 0, groupIndex = 0;
            int rowIndex = 3, colIndex = 0;
            while (!worksheetCells[rowIndex, colIndex].IsEmpty)
            {
                while (!worksheetCells[rowIndex, colIndex].IsEmpty)
                {
                    string dataStringValue = worksheetCells[rowIndex++, colIndex].StringValue;

                    DataGridViewCell dataGridViewCell = raidCompDataGridView.Rows[partyMemberIndex].Cells[groupIndex];
                    dataGridViewCell.Value = dataStringValue;
                    dataGridViewCell.Style.BackColor = Helper.GetSpecColour(dataStringValue);

                    desiredRaidComposition.SetRaidPositionSpecialisation(groupIndex, partyMemberIndex, dataStringValue);

                    partyMemberIndex++;
                }

                partyMemberIndex = 0;
                groupIndex++;

                rowIndex = 3;
                colIndex++;
            }
        }

        private void ImportPlayerCharacters(Workbook workbook)
        {
            raidGroupGenerator.ClearPlayerCharacters();

            Worksheet worksheet = workbook.GetWorksheet("Output");
            CellCollection worksheetCells = worksheet.Cells;

#if !DEBUG
            try
#endif
            {
                int rowIndex = 11, colIndex = 0;
                int playerColIndex = colIndex;
                while (!worksheetCells[rowIndex, 0].IsEmpty)
                {
                    String player = worksheetCells[rowIndex, colIndex++].StringValue;
                    if (String.IsNullOrEmpty(player))
                    {
                        break;
                    }

                    PlayerCharacter playerCharacter = new PlayerCharacter();
                    playerCharacter.index = raidGroupGenerator.GetPlayerCharacterCount();
                    playerCharacter.player = player;
                    playerCharacter.character = worksheetCells[rowIndex, colIndex++].StringValue;
                    playerCharacter.characterClass = worksheetCells[rowIndex, colIndex++].StringValue;
                    playerCharacter.specialisation = worksheetCells[rowIndex, colIndex++].StringValue;
                    playerCharacter.classSpecKey = String.Format("{0} {1}", playerCharacter.specialisation, playerCharacter.characterClass);
                    playerCharacter.priority = Convert.ToInt32(worksheetCells[rowIndex, colIndex++].Value);


                    if (!worksheetCells[rowIndex, colIndex].IsEmpty)
                    {
                        int raidIndex = Convert.ToInt32(worksheetCells[rowIndex, colIndex++].Value);
                        if (raidIndex > 0)
                        {
                            playerCharacter.staticRaid = raidIndex - 1;
                        }
                    }

                    if (!worksheetCells[rowIndex, colIndex].IsEmpty)
                    {
                        int raidPositionIndex = Convert.ToInt32(worksheetCells[rowIndex, colIndex++].Value);
                        if (raidPositionIndex > 0)
                        {
                            playerCharacter.staticRaidPosition = raidPositionIndex - 1;
                        }
                    }

                    int absentRaidIndex = 0;
                    while (!worksheetCells[rowIndex, colIndex].IsEmpty)
                    {
                        bool absent = Convert.ToBoolean(worksheetCells[rowIndex, colIndex++].Value);
                        if (absent)
                        {
                            playerCharacter.absentRaids.Add(absentRaidIndex);
                        }
                        absentRaidIndex++;
                    }

                    raidGroupGenerator.AddPlayerCharacter(playerCharacter);

                    rowIndex++;
                    colIndex = playerColIndex;
                }
            }
#if !DEBUG
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }

        private void WriteConfig()
        {
            using (StreamWriter writetext = new StreamWriter("config.ini"))
            {
                writetext.WriteLine(filePath);
                writetext.WriteLine(textBoxRandomSeed.Text);
                writetext.WriteLine(textBoxIterations.Text);
                writetext.WriteLine(textBoxURL.Text);
            }
        }

        private void LoadConfig()
        {
            if (File.Exists("config.ini"))
            {
                using (StreamReader readtext = new StreamReader("config.ini"))
                {
                    filePath = readtext.ReadLine();
                    if (File.Exists(filePath))
                    {
                        LoadWorkbook(filePath);
                        openFileDialog.FileName = Path.GetFileName(filePath);
                    }

                    if (readtext.EndOfStream)
                    {
                        return;
                    }

                    textBoxRandomSeed.Text = readtext.ReadLine();
                    textBoxIterations.Text = readtext.ReadLine();

                    if (readtext.EndOfStream)
                    {
                        return;
                    }

                    textBoxURL.Text = readtext.ReadLine();
                }
            }
        }

        private void LoadWorkbook(string fileName)
        {
#if !DEBUG
            try
#endif
            {
                Workbook workbook = Workbook.Load(fileName);

                ImportDesiredRaidComposition(workbook);
                ImportPlayerCharacters(workbook);
            }
#if !DEBUG
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            filePath = openFileDialog.FileName;
            LoadWorkbook(filePath);
            WriteConfig();
        }

        Cell CreateCell(Worksheet worksheet, int rowIndex, int columnIndex, string value)
        {
            Cell cell = new Cell(value);

            worksheet.Cells[rowIndex, columnIndex] = cell;
            return cell;
        }

        void ExportGeneratedRaids(Workbook workbook)
        {
            Worksheet worksheet = new Worksheet("Raid Comps");
            CellCollection cells = worksheet.Cells;

            Cell cell = CreateCell(worksheet, 0, 0, "Random seed:");
            cell = CreateCell(worksheet, 0, 1, String.Format("{0}", randomSeed));

            int startRowIndex = 2, startColIndex = 0;
            for (int raidGroupIndex = 0; raidGroupIndex < raidGroupCollection.Count; raidGroupIndex++)
            {
                startRowIndex++;

                RaidGroup raidGroup = raidGroupCollection.At(raidGroupIndex);
                int rowIndex = startRowIndex, colIndex = startColIndex;
                for (int groupIndex = 0; groupIndex < raidGroup.characters.GetLength(0); groupIndex++)
                {
                    rowIndex = startRowIndex;

                    cell = CreateCell(worksheet, rowIndex++, colIndex, String.Format("Group {0}", groupIndex + 1));
                    for (int partyMemberIndex = 0; partyMemberIndex < raidGroup.characters.GetLength(1); partyMemberIndex++)
                    {
                        PlayerCharacter playerCharacter = raidGroup.characters[groupIndex, partyMemberIndex];
                        if (playerCharacter != null)
                        {
                            cell = CreateCell(worksheet, rowIndex, colIndex, String.Format("{0} ({1})", playerCharacter.character, playerCharacter.specialisation));
                        }

                        rowIndex++;
                    }
                    colIndex++;
                }

                startRowIndex = rowIndex;
            }

            // Write out characters that couldn't be assigned to a raid:
            {
                int rowIndex = startRowIndex + 2, colIndex = startColIndex;
                cell = CreateCell(worksheet, rowIndex++, colIndex, "Characters who could not be assigned:");

                cell = CreateCell(worksheet, rowIndex, colIndex++, "Character");
                cell = CreateCell(worksheet, rowIndex, colIndex++, "Absent raids");

                colIndex = startColIndex;
                rowIndex++;

                for (int playerCharacterIndex = 0; playerCharacterIndex < raidGroupGenerator.GetPlayerCharacterCount(); playerCharacterIndex++)
                {
                    PlayerCharacter playerCharacter = raidGroupGenerator.GetPlayerCharacterAt(playerCharacterIndex);
                    if (!raidGroupCollection.AnyRaidContainsCharacter(playerCharacter) && playerCharacter.absentRaids.Count != raidGroupCollection.Count)
                    {
                        cell = CreateCell(worksheet, rowIndex, colIndex++, String.Format("{0} ({1})", playerCharacter.character, playerCharacter.specialisation));

                        String absentRaidsString = "";
                        if (playerCharacter.absentRaids.Count != 0)
                        {
                            for (int raidIndex = 0; raidIndex < playerCharacter.absentRaids.Count; raidIndex++)
                            {
                                absentRaidsString += raidIndex == 0 ? (playerCharacter.absentRaids[raidIndex] + 1).ToString() : String.Format(", {0}", playerCharacter.absentRaids[raidIndex] + 1);
                            }
                        }
                        cell = CreateCell(worksheet, rowIndex, colIndex++, absentRaidsString);
                        colIndex = startColIndex;
                        rowIndex++;
                    }
                }

                colIndex = startColIndex;
                rowIndex++;

                cell = CreateCell(worksheet, rowIndex++, colIndex, "Inactive Characters:");
                colIndex = startColIndex;

                for (int playerCharacterIndex = 0; playerCharacterIndex < raidGroupGenerator.GetPlayerCharacterCount(); playerCharacterIndex++)
                {
                    PlayerCharacter playerCharacter = raidGroupGenerator.GetPlayerCharacterAt(playerCharacterIndex);
                    if (!raidGroupCollection.AnyRaidContainsCharacter(playerCharacter) && playerCharacter.absentRaids.Count == raidGroupCollection.Count)
                    {
                        cell = CreateCell(worksheet, rowIndex, colIndex++, String.Format("{0} ({1})", playerCharacter.character, playerCharacter.specialisation));

                        String absentRaidsString = "";
                        if (playerCharacter.absentRaids.Count != 0)
                        {
                            for (int raidIndex = 0; raidIndex < playerCharacter.absentRaids.Count; raidIndex++)
                            {
                                absentRaidsString += raidIndex == 0 ? (playerCharacter.absentRaids[raidIndex] + 1).ToString() : String.Format(", {0}", playerCharacter.absentRaids[raidIndex] + 1);
                            }
                        }
                        cell = CreateCell(worksheet, rowIndex, colIndex++, absentRaidsString);
                        colIndex = startColIndex;
                        rowIndex++;
                    }
                }
            }

            workbook.Worksheets.Add(worksheet);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Workbook workbook = new Workbook();
            ExportGeneratedRaids(workbook);
            workbook.Save(saveFileDialog.FileName);
        }

        private void DisplayRaidGroup()
        {
            if (raidGroupCollection == null)
            {
                return;
            }

            textBoxRandomSeed.Text = raidGroupCollection.RandomSeed.ToString();

            if (comboBoxRaidGroups.SelectedIndex < 0 || comboBoxRaidGroups.SelectedIndex > raidGroupCollection.Count)
            {
                comboBoxRaidGroups.SelectedIndex = 0;
            }

            RaidGroup raidGroup = raidGroupCollection.At(comboBoxRaidGroups.SelectedIndex);
            for (int groupIndex = 0; groupIndex < raidGroup.characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < raidGroup.characters.GetLength(1); partyMemberIndex++)
                {
                    PlayerCharacter playerCharacter = raidGroup.characters[groupIndex, partyMemberIndex];
                    DataGridViewCell raidDataGridViewCell = raidDataGridView.Rows[partyMemberIndex].Cells[groupIndex];
                    if (playerCharacter == null)
                    {
                        raidDataGridViewCell.Style.BackColor = Color.White;
                        raidDataGridViewCell.Value = String.Empty;
                    }
                    else
                    {
                        raidDataGridViewCell.Style.BackColor = Helper.GetSpecColour(playerCharacter.classSpecKey);
                        raidDataGridViewCell.Value = String.Format("{0} ({1})", playerCharacter.character, playerCharacter.specialisation);
                    }
                }
            }
        }

        private void ExecuteGenerate(bool generateRandomSeed)
        {
#if !DEBUG
            try
#endif
            {
                LockControls();

                if (generateRandomSeed)
                {
                    randomSeed = Guid.NewGuid().GetHashCode();
                    regenerating = false;
                }
                else
                {
                    randomSeed = Convert.ToInt32(textBoxRandomSeed.Text);
                    regenerating = true;
                }

                backgroundWorkerGenerate.RunWorkerAsync();
            }

#if !DEBUG
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
#endif
        }

        private void LockControls()
        {
            textBoxIterations.Enabled = false;
            textBoxRaidGroupCount.Enabled = false;
            textBoxRandomSeed.Enabled = false;
            textBoxURL.Enabled = false;

            buttonImportURL.Enabled = false;
            buttonGenerate.Enabled = false;
            buttonRegenerate.Enabled = false;

            comboBoxRaidGroups.Enabled = false;
            comboBoxRaidCollections.Enabled = false;
        }

        private void UnlockControls()
        {
            textBoxIterations.Enabled = true;
            textBoxRaidGroupCount.Enabled = true;
            textBoxRandomSeed.Enabled = true;
            textBoxURL.Enabled = true;

            buttonImportURL.Enabled = true;
            buttonGenerate.Enabled = true;
            buttonRegenerate.Enabled = true;

            comboBoxRaidGroups.Enabled = true;
            comboBoxRaidCollections.Enabled = true;
        }

        private void buttonImportURL_Click(object sender, EventArgs e)
        {
#if !DEBUG
            try
#endif
            {
                LockControls();

                ProcessStartInfo startInfo = new ProcessStartInfo("get_xls.exe");
                filePath = Directory.GetCurrentDirectory() + "\\Raid Comp Generator.xls";

                //Regex

                startInfo.Arguments = textBoxURL.Text + "/export?format=xlsx \"Raid Comp Generator.xls\"";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                importURLProcess = System.Diagnostics.Process.Start(startInfo);

                backgroundWorkerImportURL.RunWorkerAsync();
            }
#if !DEBUG
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                UnlockControls();
            }
#endif
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            ExecuteGenerate(true);
        }

        private void buttonRegenerate_Click(object sender, EventArgs e)
        {
            ExecuteGenerate(false);
        }

        private void comboBoxRaidGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            DisplayRaidGroup();
        }

        private void comboBoxRaidCollections_SelectedValueChanged(object sender, EventArgs e)
        {
            raidGroupCollection = raidGroupCollections.ElementAt(comboBoxRaidCollections.SelectedIndex);
            DisplayRaidGroup();
        }

        private void backgroundWorkerGenerate_DoWork(object sender, DoWorkEventArgs e)
        {
            Random random = new Random(randomSeed);

            raidGroupCollections.Clear();

            int iterations = regenerating ? 1 : int.Parse(textBoxIterations.Text);
            for (int raidGroupCollectionIndex = 0; raidGroupCollectionIndex < iterations; raidGroupCollectionIndex++)
            {
                RaidGroupCollection raidGroupCollection = raidGroupGenerator.GenerateRaidGroups(int.Parse(textBoxRaidGroupCount.Text), desiredRaidComposition, randomSeed + raidGroupCollectionIndex);
                raidGroupCollection.ID = raidGroupCollectionIndex;

                raidGroupCollections.Add(raidGroupCollection);

                int progress = (int)((float)raidGroupCollectionIndex / (float)iterations * 100);
                backgroundWorkerGenerate.ReportProgress(progress);
            }

            raidGroupCollections.Sort();

            raidGroupCollection = raidGroupCollections.First();
        }

        private void backgroundWorkerGenerate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = (e.ProgressPercentage);
        }

        private void backgroundWorkerGenerate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Value = 100;
            textBoxRandomSeed.Text = raidGroupCollection.RandomSeed.ToString();

            int comboBoxPreviousIndex = comboBoxRaidGroups.SelectedIndex;
            comboBoxRaidGroups.Items.Clear();
            for (int raidGroupIndex = 0; raidGroupIndex < raidGroupCollection.Count; raidGroupIndex++)
            {
                comboBoxRaidGroups.Items.Add(String.Format("Raid Group {0}", raidGroupIndex + 1));
            }

            if (comboBoxRaidGroups.Items.Count > 0 && comboBoxPreviousIndex == -1)
            {
                comboBoxRaidGroups.SelectedIndex = 0;
            }
            else if (comboBoxPreviousIndex < comboBoxRaidGroups.Items.Count)
            {
                comboBoxRaidGroups.SelectedIndex = comboBoxPreviousIndex;
            }

            comboBoxRaidCollections.Items.Clear();
            for (int raidGroupCollectionIndex = 0; raidGroupCollectionIndex < raidGroupCollections.Count; raidGroupCollectionIndex++)
            {
                RaidGroupCollection raidGroupCollection = raidGroupCollections[raidGroupCollectionIndex];
                comboBoxRaidCollections.Items.Add(String.Format("{0} (score: {1}, players: {2})", raidGroupCollection.ID + 1, raidGroupCollection.ScoreUniqueness(), raidGroupCollection.AssignedCharacterCount));
            }
            comboBoxRaidCollections.SelectedIndex = 0;

            WriteConfig();

            UnlockControls();
        }

        private void backgroundWorkerImportURL_DoWork(object sender, DoWorkEventArgs e)
        {
            importURLProcess.WaitForExit();
        }

        private void backgroundWorkerImportURL_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorkerImportURL_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (importURLProcess.ExitCode == 0)
            {
                LoadWorkbook(filePath);
            }
            else
            {
                MessageBox.Show(String.Format("Failed to import {0}, ensure that the permissions for your spreadsheet are set to publically viewable and the URL does not end with '/', contain '/edit' or anything beyond that point.\n\nError Message:\n\n{1}", textBoxURL.Text, importURLProcess.StandardError.ReadToEnd()));
            }

            UnlockControls();
        }
    }
}
