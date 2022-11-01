using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;

namespace RaidCompGenerator
{
    public partial class Form : System.Windows.Forms.Form
    {
        RaidComposition desiredRaidComposition;
        RaidGroupGenerator raidGroupGenerator;

        int randomSeed = 0;

        public Form()
        {
            InitializeComponent();

            desiredRaidComposition = new RaidComposition();
            raidGroupGenerator = new RaidGroupGenerator();

            textBoxRandomSeed.Text = randomSeed.ToString();

#if !DEBUG
            buttonRegenerate.Enabled = false;
            buttonRegenerate.Visible = false;
#endif
        }

        private void LoadDefaultWorkbook()
        {
            if (File.Exists("config.ini"))
            {
                using (StreamReader readtext = new StreamReader("config.ini"))
                {
                    string defaultWorkbook = readtext.ReadLine();
                    if (File.Exists(defaultWorkbook))
                    {
                        LoadWorkbook(defaultWorkbook);
                    }
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Microsoft XLS Files|*.xls";
            openFileDialog.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = openFileDialog.FileName;
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

            LoadDefaultWorkbook();
        }

        private void ImportDesiredRaidComposition(Workbook workbook)
        {
            Worksheet worksheet = workbook.GetWorksheet("Output Values");
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

            Worksheet worksheet = workbook.GetWorksheet("Output Values");
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

                    for (int absentRaidIndex = 0; absentRaidIndex < int.Parse(textBoxRaidGroupCount.Text); absentRaidIndex++)
                    {
                        bool absent = Convert.ToBoolean(worksheetCells[rowIndex, colIndex++].Value);
                        if (absent)
                        {
                            playerCharacter.absentRaids.Add(absentRaidIndex);
                        }
                    }

                    if (!worksheetCells[rowIndex, colIndex].IsEmpty)
                    {
                        int raidIndex = Convert.ToInt32(worksheetCells[rowIndex, colIndex++].Value);
                        if (raidIndex > 0)
                        {
                            playerCharacter.raid = raidIndex - 1;
                        }
                    }

                    if (!worksheetCells[rowIndex, colIndex].IsEmpty)
                    {
                        int raidPositionIndex = Convert.ToInt32(worksheetCells[rowIndex, colIndex++].Value);
                        if (raidPositionIndex > 0)
                        {
                            playerCharacter.raidPosition = raidPositionIndex - 1;
                        }
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

        private void WriteIniFile(string fileName)
        {
            using (StreamWriter writetext = new StreamWriter("config.ini"))
            {
                writetext.WriteLine(fileName);
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
            LoadWorkbook(openFileDialog.FileName);
            WriteIniFile(openFileDialog.FileName);
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
            for (int raidGroupIndex = 0; raidGroupIndex < raidGroupGenerator.raidGroups.Count; raidGroupIndex++)
            {
                startRowIndex++;

                RaidGroup raidGroup = raidGroupGenerator.raidGroups.At(raidGroupIndex);                
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
                cell = CreateCell(worksheet, rowIndex++, colIndex, "Characters who couild not be assigned:");

                cell = CreateCell(worksheet, rowIndex, colIndex++, "Character");
                cell = CreateCell(worksheet, rowIndex, colIndex++, "Spec");
                cell = CreateCell(worksheet, rowIndex, colIndex++, "Prio");
                cell = CreateCell(worksheet, rowIndex, colIndex++, "Absent raids");

                colIndex = startColIndex;
                rowIndex++;

                for (int playerCharacterIndex = 0; playerCharacterIndex < raidGroupGenerator.GetPlayerCharacterCount(); playerCharacterIndex++)
                {
                    PlayerCharacter playerCharacter = raidGroupGenerator.GetPlayerCharacterAt(playerCharacterIndex);
                    if (!raidGroupGenerator.AnyRaidContainsCharacter(playerCharacter))
                    {
                        cell = CreateCell(worksheet, rowIndex, colIndex++, playerCharacter.character);
                        cell = CreateCell(worksheet, rowIndex, colIndex++, playerCharacter.specialisation);
                        cell = CreateCell(worksheet, rowIndex, colIndex++, playerCharacter.priority.ToString());

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

        private void SetupRaidGroup()
        {
            RaidGroup raidGroup = raidGroupGenerator.raidGroups.At(comboBoxRaidGroups.SelectedIndex);
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
                if (generateRandomSeed)
                {
                    randomSeed = Guid.NewGuid().GetHashCode();
                    textBoxRandomSeed.Text = randomSeed.ToString();
                }
                else
                {
                    randomSeed = Convert.ToInt32(textBoxRandomSeed.Text);
                }

                raidGroupGenerator.GenerateRaidGroups(int.Parse(textBoxRaidGroupCount.Text), desiredRaidComposition, randomSeed);

                int comboBoxPreviousIndex = comboBoxRaidGroups.SelectedIndex;
                comboBoxRaidGroups.Items.Clear();
                for (int raidGroupIndex = 0; raidGroupIndex < raidGroupGenerator.raidGroups.Count; raidGroupIndex++)
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

                SetupRaidGroup();
            }
#if !DEBUG
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            SetupRaidGroup();
        }
    }
}
