using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelLibrary.SpreadSheet;

namespace RaidCompGenerator
{
    public class Helper
    {
        public static readonly List<String> Roles = new List<String>
        {
            "Tank",
            "PhysicalMelee",
            "PhysicalRanged",
            "Caster",
            "Healer",
        };

        public static readonly List<String> GearTypes = new List<String>
        {
            "Cloth",
            "Leather",
            "Mail",
            "Plate",
        };

        public static readonly List<String> Specialisations = new List<String>
        {
            // Death Knight
            "Blood Death Knight",
            "Frost Death Knight",
            "Unholy Death Knight",

            // Druid
            "Balance Druid",
            "Feral Druid",
            "Restoration Druid",

            // Hunter
            "Beast Mastery Hunter",
            "Marksmanship Hunter",
            "Survival Hunter",

            // Mage
            "Arcane Mage",
            "Fire Mage",
            "Frost Mage",

            // Paladin
            "Holy Paladin",
            "Protection Paladin",
            "Retribution Paladin",

            // Priest
            "Discipline Priest",
            "Holy Priest",
            "Shadow Priest",

            // Rogue
            "Assassination Rogue",
            "Combat Rogue",
            "Subtlety Rogue",

            // Shaman
            "Elemental Shaman",
            "Enhancement Shaman",
            "Restoration Shaman",

            // Warlock
            "Affliction Warlock",
            "Demonology Warlock",
            "Destruction Warlock",

            // Warrior
            "Arms Warrior",
            "Fury Warrior",
            "Protection Warrior"
        };

        public static readonly List<String> Classes = new List<string>
        {
            "Death Knight",
            "Druid",
            "Hunter",
            "Mage",
            "Paladin",
            "Priest",
            "Rogue",
            "Shaman",
            "Warlock",
            "Warrior"
        };

        public static readonly List<Color> ClassColors = new List<Color>
        {
             Color.FromArgb ( 196, 30, 58 ), // Death Knight
             Color.FromArgb ( 255, 124, 10 ), // Druid
             Color.FromArgb ( 170, 211, 114 ), // Hunter
             Color.FromArgb ( 63, 199, 235 ), // Mage
             Color.FromArgb ( 244, 140, 186 ), // Paladin
             Color.FromArgb ( 255, 255, 255 ), // Priest
             Color.FromArgb ( 255, 244, 104 ), // Rogue
             Color.FromArgb ( 0, 112, 221 ), // Shaman
             Color.FromArgb ( 135, 136, 238 ), // Warlock
             Color.FromArgb ( 198, 155, 109 ), // Warrior
        };

        public static int getIndexOfString(List<string> list, string value)
        {
            int index = list.IndexOf(value);
            if (index < 0)
            {
                for (int entryIndex = 0; entryIndex < list.Count; entryIndex++)
                {
                    if (list[entryIndex].Contains(value))
                    {
                        return entryIndex;
                    }
                }
            }
            return index;
        }

        public static Color GetSpecColour(string specialisation)
        {
            int specIndex = getIndexOfString(Specialisations, specialisation);
            return ClassColors[specIndex / 3];
        }

        public static int GetGearTypeIndex(string specialisation)
        {
            switch (specialisation)
            {
                case "Arcane Mage":
                case "Fire Mage":
                case "Frost Mage":
                case "Discipline Priest":
                case "Holy Priest":
                case "Shadow Priest":
                case "Affliction Warlock":
                case "Demonology Warlock":
                case "Destruction Warlock":
                    return getIndexOfString(GearTypes, "Cloth");

                case "Balance Druid":
                case "Feral Druid":
                case "Restoration Druid":
                case "Assassination Rogue":
                case "Combat Rogue":
                case "Subtlety Rogue":
                    return getIndexOfString(GearTypes, "Leather");

                case "Beast Mastery Hunter":
                case "Marksmanship Hunter":
                case "Survival Hunter":
                case "Elemental Shaman":
                case "Enhancement Shaman":
                case "Restoration Shaman":
                    return getIndexOfString(GearTypes, "Mail");

                case "Blood Death Knight":
                case "Frost Death Knight":
                case "Unholy Death Knight":
                case "Holy Paladin":
                case "Protection Paladin":
                case "Retribution Paladin":
                case "Arms Warrior":
                case "Fury Warrior":
                case "Protection Warrior":
                    return getIndexOfString(GearTypes, "Plate");
            }

            return -1;
        }
        public static int GetRoleIndex(string specialisation)
        {
            switch (specialisation)
            {
                case "Protection Warrior":
                case "Blood Death Knight":
                case "Protection Paladin":
                case "Feral Druid":
                    return getIndexOfString(Roles, "Tank");

                case "Rogue":
                case "Assassination Rogue":
                case "Combat Rogue":
                case "Subtlety Rogue":
                case "Enhancement Shaman":
                case "Frost Death Knight":
                case "Unholy Death Knight":
                case "Retribution Paladin":
                case "Warrior":
                case "Arms Warrior":
                case "Fury Warrior":
                    return getIndexOfString(Roles, "PhysicalMelee");

                case "Beast Mastery Hunter":
                case "Marksmanship Hunter":
                case "Survival Hunter":
                    return getIndexOfString(Roles, "PhysicalRanged");

                case "Mage":
                case "Arcane Mage":
                case "Fire Mage":
                case "Frost Mage":
                case "Shadow Priest":
                case "Warlock":
                case "Affliction Warlock":
                case "Demonology Warlock":
                case "Destruction Warlock":
                case "Balance Druid":
                case "Elemental Shaman":
                    return getIndexOfString(Roles, "Caster");

                case "Discipline Priest":
                case "Holy Priest":
                case "Restoration Druid":
                case "Restoration Shaman":
                case "Holy Paladin":
                    return getIndexOfString(Roles, "Healer");
            }

            return -1;
        }
    }

    public class PlayerCharacter : IComparable
    {
        public int index = 0;
        public string player;
        public string character;
        public string characterClass;
        public string specialisation;
        public string classSpecKey;
        public bool absent = false;
        public int priority = 10;
        public int raid = -1;
        public int CompareTo(object that)
        {
            if (that == null)
            {
                return 1;
            }

            PlayerCharacter thatPlayerCharacter = that as PlayerCharacter;
            if (raid != -1 || thatPlayerCharacter.raid != -1)
            {
                bool isGreaterPriority = raid > thatPlayerCharacter.raid;
                return isGreaterPriority ? -1 : 1;
            }

            if (priority != thatPlayerCharacter.priority)
            {
                bool isGreaterPriority = priority < thatPlayerCharacter.priority;
                return isGreaterPriority ? -1 : 1;
            }

            return this.index > thatPlayerCharacter.index ? 1 : -1;
        }
    }

    public class RaidComposition
    {
        private string[,] raidPositions;

        public RaidComposition()
        {
            raidPositions = new string[5, 5];
        }

        public void SetRaidPositionSpecialisation(int group, int partyMemberIndex, string specialisation)
        {
            raidPositions[group, partyMemberIndex] = specialisation;
        }

        public string GetRaidPositionSpecialisation(int group, int partyMemberIndex)
        {
            return raidPositions[group, partyMemberIndex];
        }
    };

    public class RaidGroup
    {
        public PlayerCharacter[,] characters;
        public int[] roleCounts;
        public int[] gearTypeCounts;

        public RaidGroup()
        {
            characters = new PlayerCharacter[5, 5];
            roleCounts = new int[Helper.Roles.Count];
            gearTypeCounts = new int[Helper.GearTypes.Count];
        }

        public bool ContainsPlayer(string player)
        {
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    PlayerCharacter playerCharacter = characters[groupIndex, partyMemberIndex];
                    if (playerCharacter != null && playerCharacter.player == player)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool FindEmptyPositionForCharacter(RaidComposition desiredComposition, PlayerCharacter playerCharacter, out int out_groupIndex, out int out_partyMemberIndex, out bool out_exactMatch)
        {
            out_groupIndex = -1;
            out_partyMemberIndex = -1;
            out_exactMatch = false;

            if (ContainsPlayer(playerCharacter.player))
            {
                return false;
            }

            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    string desiredSpecialisation = desiredComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                    if ((playerCharacter.character == desiredSpecialisation || playerCharacter.classSpecKey == desiredSpecialisation) && characters[groupIndex, partyMemberIndex] == null)
                    {
                        out_groupIndex = groupIndex;
                        out_partyMemberIndex = partyMemberIndex;
                        out_exactMatch = true;
                        return true;
                    }
                }
            }

            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    string desiredSpecialisation = desiredComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                    if (playerCharacter.classSpecKey.Contains(desiredSpecialisation) && characters[groupIndex, partyMemberIndex] == null)
                    {
                        out_groupIndex = groupIndex;
                        out_partyMemberIndex = partyMemberIndex;
                        return true;
                    }
                }
            }

            return false;
        }

        public void SetPlayerCharacter(int groupIndex, int partyMemberIndex, PlayerCharacter playerCharacter)
        {
            characters[groupIndex, partyMemberIndex] = playerCharacter;

            for (int roleIndex = 0; roleIndex < Helper.Roles.Count; roleIndex++)
            {
                roleCounts[roleIndex] = 0;
            }

            for (int gearTypeIndex = 0; gearTypeIndex < Helper.GearTypes.Count; gearTypeIndex++)
            {
                gearTypeCounts[gearTypeIndex] = 0;
            }

            for (int raidGroupIndex = 0; raidGroupIndex < characters.GetLength(0); raidGroupIndex++)
            {
                for (int raidPartyMemberIndex = 0; raidPartyMemberIndex < characters.GetLength(1); raidPartyMemberIndex++)
                {
                    if (characters[raidGroupIndex, raidPartyMemberIndex] != null)
                    {
                        PlayerCharacter raidPlayerCharacter = characters[raidGroupIndex, raidPartyMemberIndex];
                        roleCounts[Helper.GetRoleIndex(raidPlayerCharacter.classSpecKey)]++;
                        gearTypeCounts[Helper.GetGearTypeIndex(raidPlayerCharacter.classSpecKey)]++;
                    }
                }
            }
        }
    };

    public class RaidGroupGenerator
    {
        public List<RaidGroup> raidGroups;
        private List<PlayerCharacter> playerCharacters;

        public RaidGroupGenerator()
        {
            playerCharacters = new List<PlayerCharacter>();

            raidGroups = new List<RaidGroup>();
        }

        public void ClearPlayerCharacters()
        {
            playerCharacters.Clear();
        }

        public void AddPlayerCharacter(PlayerCharacter playerCharacter)
        {
            playerCharacters.Add(playerCharacter);
        }

        public int GetPlayerCharacterCount()
        {
            return playerCharacters.Count;
        }

        public void GenerateRaidGroups(int raidGroupCount, RaidComposition desiredRaidComposition)
        {
            raidGroups.Clear();
            for (int raidIndex = 0; raidIndex < raidGroupCount; raidIndex++)
            {
                RaidGroup raidGroup = new RaidGroup();
                raidGroups.Add(raidGroup);
            }

            playerCharacters.Sort();

            // Generate the raids.
            foreach (PlayerCharacter playerCharacter in playerCharacters)
            {
                if (playerCharacter.absent)
                {
                    continue;
                }

                int roleIndex = Helper.GetRoleIndex(playerCharacter.classSpecKey);
                if (roleIndex == -1)
                {
                    throw new ApplicationException(String.Format("Player {0}, character {1} has invalid class/specialisation: {2}.", playerCharacter.player, playerCharacter.character, playerCharacter.classSpecKey));
                }

                int gearTypeIndex = Helper.GetGearTypeIndex(playerCharacter.classSpecKey);

                // Calculate weights.
                float bestRaidGroupWeight = 10000;
                int bestRaidGroupIndex = -1, bestRaidGroupGroupIndex = -1, bestRaidGroupPartyMemberIndex = -1;

                for (int raidIndex = 0; raidIndex < raidGroupCount; raidIndex++)
                {
                    if (playerCharacter.raid > 0 && raidIndex != playerCharacter.raid)
                    {
                        continue;
                    }

                    int groupIndex = 0, partyMemberIndex = 0;
                    bool exactMatch = false;

                    RaidGroup raidGroup = raidGroups[raidIndex];
                    if (!raidGroup.FindEmptyPositionForCharacter(desiredRaidComposition, playerCharacter, out groupIndex, out partyMemberIndex, out exactMatch))
                    {
                        continue;
                    }

                    float raidMatchWeight = playerCharacter.raid != raidIndex ? 1000 : 0;
                    float exactMatchWeight = exactMatch ? 0 : 100;
                    float roleWeight = raidGroup.roleCounts[roleIndex] * 10;
                    float gearTypeWeight = raidGroup.gearTypeCounts[gearTypeIndex];
                    float raidGroupWeight = raidMatchWeight + exactMatchWeight + roleWeight + gearTypeWeight;

                    if (raidGroupWeight < bestRaidGroupWeight)
                    {
                        bestRaidGroupWeight = raidGroupWeight;
                        bestRaidGroupIndex = raidIndex;
                        bestRaidGroupGroupIndex = groupIndex;
                        bestRaidGroupPartyMemberIndex = partyMemberIndex;
                    }
                }

                if (bestRaidGroupIndex >= 0)
                {
                    RaidGroup raidGroup = raidGroups[bestRaidGroupIndex];
                    raidGroup.SetPlayerCharacter(bestRaidGroupGroupIndex, bestRaidGroupPartyMemberIndex, playerCharacter);
                }
            }
        }
    }
}