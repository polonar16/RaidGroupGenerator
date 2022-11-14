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

        public static readonly List<int> UniqueRoleScores = new List<int>
        {
            100000, // Tank
            1000,   // PhysicalMelee
            1000,   // PhysicalRanged
            1000,   // Caster
            25000,  // Healer
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
            if (specIndex < 0)
            {
                return Color.LightGray;
            }

            int classIndex = specIndex / 3;
            if (classIndex < ClassColors.Count)
            {
                return ClassColors[classIndex];
            }

            return Color.LightGray;
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

        internal static bool MatchClassSpecToRole(string desiredRole, string classSpecKey)
        {
            int roleIndex = GetRoleIndex(classSpecKey);
            if (String.Compare(desiredRole, "Any Tank") == 0)
            {
                return roleIndex == getIndexOfString(Roles, "Tank");
            }
            else if (String.Compare(desiredRole, "Any DPS") == 0)
            {
                return roleIndex == getIndexOfString(Roles, "PhysicalMelee")
                    || roleIndex == getIndexOfString(Roles, "PhysicalRanged")
                    || roleIndex == getIndexOfString(Roles, "Caster");
            }
            else if (String.Compare(desiredRole, "Any Healer") == 0)
            {
                return roleIndex == getIndexOfString(Roles, "Healer");
            }

            return false;
        }

        internal static int GetSpecIndex(string classSpecKey)
        {
            return getIndexOfString(Specialisations, classSpecKey);
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
        public List<int> absentRaids = new List<int>();
        public int priority = 10;
        public int staticRaid = -1;
        public int staticRaidPosition = -1;
        public int CompareTo(object that)
        {
            if (that == null)
            {
                return 1;
            }

            PlayerCharacter thatPlayerCharacter = that as PlayerCharacter;
            if (staticRaid != -1 || thatPlayerCharacter.staticRaid != -1)
            {
                bool isGreaterPriority = staticRaid > thatPlayerCharacter.staticRaid;
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
        public static int RAID_GROUP_COUNT = 5;
        public static int PARTY_MEMBER_COUNT = 5;

        private string[,] raidPositions;

        public RaidComposition()
        {
            raidPositions = new string[RAID_GROUP_COUNT, PARTY_MEMBER_COUNT];
        }

        public void SetRaidPositionSpecialisation(int groupIndex, int partyMemberIndex, string specialisation)
        {
            raidPositions[groupIndex, partyMemberIndex] = specialisation;
        }

        public string GetRaidPositionSpecialisation(int groupIndex, int partyMemberIndex)
        {
            return raidPositions[groupIndex, partyMemberIndex];
        }

        internal bool IsExactRoleMatch(int groupIndex, int partyMemberIndex, string specialisation)
        {
            string desiredClassSpec = raidPositions[groupIndex, partyMemberIndex];
            if (desiredClassSpec == specialisation)
            {
                return true;
            }

            else if(specialisation.Contains(desiredClassSpec))
            {
                return true;
            }

            return false;
        }
    }

    public struct RaidPosition
    {
        public int groupIndex;
        public int partyMemberIndex;
        public float weight;
    }

    public class RaidGroup : ICloneable
    {
        public PlayerCharacter[,] characters;
        public int[] roleCounts;
        public int[] gearTypeCounts;
        public int[] classSpecCounts;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public RaidGroup()
        {
            characters = new PlayerCharacter[5, 5];
            roleCounts = new int[Helper.Roles.Count];
            gearTypeCounts = new int[Helper.GearTypes.Count];
            classSpecCounts = new int[Helper.Specialisations.Count];
        }

        public RaidGroup(RaidGroup that)
        {
            characters = new PlayerCharacter[5, 5];
            roleCounts = new int[Helper.Roles.Count];
            gearTypeCounts = new int[Helper.GearTypes.Count];
            classSpecCounts = new int[Helper.Specialisations.Count];

            Set(that);
        }

        public void Set(RaidGroup that)
        {
            for (int groupIndex = 0; groupIndex < that.characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < that.characters.GetLength(1); partyMemberIndex++)
                {
                    characters[groupIndex, partyMemberIndex] = that.characters[groupIndex, partyMemberIndex];
                }
            }

            for (int roleIndex = 0; roleIndex < that.roleCounts.GetLength(0); roleIndex++)
            {
                roleCounts[roleIndex] = that.roleCounts[roleIndex];
            }

            for (int gearTypeIndex = 0; gearTypeIndex < that.gearTypeCounts.GetLength(0); gearTypeIndex++)
            {
                gearTypeCounts[gearTypeIndex] = that.gearTypeCounts[gearTypeIndex];
            }

            for (int classSpecIndex = 0; classSpecIndex < that.classSpecCounts.GetLength(0); classSpecIndex++)
            {
                classSpecCounts[classSpecIndex] = that.classSpecCounts[classSpecIndex];
            }
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

        internal bool ContainsPlayerCharacter(PlayerCharacter playerCharacter)
        {
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    if (characters[groupIndex, partyMemberIndex] == playerCharacter)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public PlayerCharacter GetCharacterForPlayer(string player)
        {
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    PlayerCharacter playerCharacter = characters[groupIndex, partyMemberIndex];
                    if (playerCharacter != null && playerCharacter.player == player)
                    {
                        return playerCharacter;
                    }
                }
            }
            return null;
        }

        public List<RaidPosition> FindFullPositionsForClassSpec(string classSpecKey, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch)
        {
            List<RaidPosition> raidPositions = new List<RaidPosition>();
            if (allowGenericRoleMatch)
            {
                // Try to match role (dps, tank, healer)
                for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
                {
                    for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                    {
                        string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                        if (Helper.MatchClassSpecToRole(desiredSpecialisation, classSpecKey) && characters[groupIndex, partyMemberIndex] != null)
                        {
                            RaidPosition raidPosition = new RaidPosition();
                            raidPosition.groupIndex = groupIndex;
                            raidPosition.partyMemberIndex = partyMemberIndex;
                            raidPosition.weight = 10000;
                            raidPositions.Add(raidPosition);
                        }
                    }
                }
            }
            else
            {
                // Try to match exact class/spec.
                for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
                {
                    for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                    {
                        string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                        if (classSpecKey == desiredSpecialisation && characters[groupIndex, partyMemberIndex] != null)
                        {
                            RaidPosition raidPosition = new RaidPosition();
                            raidPosition.groupIndex = groupIndex;
                            raidPosition.partyMemberIndex = partyMemberIndex;
                            raidPosition.weight = 0;
                            raidPositions.Add(raidPosition);
                        }
                    }
                }

                if (raidPositions.Count > 0)
                {
                    return raidPositions;
                }

                // Try to match class.
                for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
                {
                    for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                    {
                        string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                        if (classSpecKey.Contains(desiredSpecialisation) && characters[groupIndex, partyMemberIndex] != null)
                        {
                            RaidPosition raidPosition = new RaidPosition();
                            raidPosition.groupIndex = groupIndex;
                            raidPosition.partyMemberIndex = partyMemberIndex;
                            raidPosition.weight = 50;
                            raidPositions.Add(raidPosition);
                        }
                    }
                }
            }

            return raidPositions;
        }

        public List<RaidPosition> FindEmptyPositionsForClassSpec(string classSpecKey, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch)
        {
            List<RaidPosition> raidPositions = new List<RaidPosition>();

            if (allowGenericRoleMatch)
            {
                // Try to match role (dps, tank, healer)
                for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
                {
                    for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                    {
                        string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                        if (Helper.MatchClassSpecToRole(desiredSpecialisation, classSpecKey) && characters[groupIndex, partyMemberIndex] == null)
                        {
                            RaidPosition raidPosition = new RaidPosition();
                            raidPosition.groupIndex = groupIndex;
                            raidPosition.partyMemberIndex = partyMemberIndex;
                            raidPosition.weight = 200;
                            raidPositions.Add(raidPosition);
                        }
                    }
                }
            }
            else
            {
                // Try to match exact class/spec.
                for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
                {
                    for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                    {
                        string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                        if (classSpecKey == desiredSpecialisation && characters[groupIndex, partyMemberIndex] == null)
                        {
                            RaidPosition raidPosition = new RaidPosition();
                            raidPosition.groupIndex = groupIndex;
                            raidPosition.partyMemberIndex = partyMemberIndex;
                            raidPosition.weight = 0;
                            raidPositions.Add(raidPosition);
                        }
                    }
                }

                if (raidPositions.Count > 0)
                {
                    return raidPositions;
                }

                // Try to match class.
                for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
                {
                    for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                    {
                        string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                        if (classSpecKey.Contains(desiredSpecialisation) && characters[groupIndex, partyMemberIndex] == null)
                        {
                            RaidPosition raidPosition = new RaidPosition();
                            raidPosition.groupIndex = groupIndex;
                            raidPosition.partyMemberIndex = partyMemberIndex;
                            raidPosition.weight = 50;

                            raidPositions.Add(raidPosition);
                        }
                    }
                }
            }

            return raidPositions;
        }

        public void SetPlayerCharacter(int groupIndex, int partyMemberIndex, PlayerCharacter playerCharacter)
        {
            characters[groupIndex, partyMemberIndex] = playerCharacter;

            CalculateRoleCounts();
        }

        public bool GetPlayerCharacterLocation(PlayerCharacter playerCharacter, out int out_groupIndex, out int out_partyMemberIndex)
        {
            out_groupIndex = -1;
            out_partyMemberIndex = -1;

            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    if (characters[groupIndex, partyMemberIndex] == playerCharacter)
                    {
                        out_groupIndex = groupIndex;
                        out_partyMemberIndex = partyMemberIndex;
                        return true;
                    }
                }
            }

            return false;
        }

        internal void ReplacePlayerCharacter(PlayerCharacter existingPlayerCharacter, PlayerCharacter playerCharacter)
        {
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    if (characters[groupIndex, partyMemberIndex] == existingPlayerCharacter)
                    {
                        SetPlayerCharacter(groupIndex, partyMemberIndex, playerCharacter);
                        CalculateRoleCounts();
                        return;
                    }
                }
            }
        }

        internal void RemovePlayerCharacter(PlayerCharacter existingPlayerCharacter)
        {
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    PlayerCharacter playerCharacter = characters[groupIndex, partyMemberIndex];
                    if (playerCharacter != null && playerCharacter == existingPlayerCharacter)
                    {
                        SetPlayerCharacter(groupIndex, partyMemberIndex, null);
                        CalculateRoleCounts();
                        return;
                    }
                }
            }
        }

        public void CalculateRoleCounts()
        {
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
                        classSpecCounts[Helper.GetSpecIndex(raidPlayerCharacter.classSpecKey)]++;
                    }
                }
            }
        }

        internal PlayerCharacter GetPlayerCharacterAt(int groupIndex, int partyMemberIndex)
        {
            return characters[groupIndex, partyMemberIndex];
        }

        internal bool PositionIsEmpty(int groupIndex, int partyMemberIndex)
        {
            return characters[groupIndex, partyMemberIndex] == null;
        }

        public int GetAssignedCharacterCount()
        {
            int assignedPlayerCount = 0;
            for (int raidGroupIndex = 0; raidGroupIndex < characters.GetLength(0); raidGroupIndex++)
            {
                for (int raidPartyMemberIndex = 0; raidPartyMemberIndex < characters.GetLength(1); raidPartyMemberIndex++)
                {
                    if (characters[raidGroupIndex, raidPartyMemberIndex] != null)
                    {
                        assignedPlayerCount++;
                    }
                }
            }
            return assignedPlayerCount;
        }

        public int GetClassSpecCount(string classSpecKey)
        {
            int classSpecCount = 0;
            for (int raidGroupIndex = 0; raidGroupIndex < characters.GetLength(0); raidGroupIndex++)
            {
                for (int raidPartyMemberIndex = 0; raidPartyMemberIndex < characters.GetLength(1); raidPartyMemberIndex++)
                {
                    PlayerCharacter playerCharacter = characters[raidGroupIndex, raidPartyMemberIndex];
                    if (characters[raidGroupIndex, raidPartyMemberIndex] != null && playerCharacter.classSpecKey == classSpecKey)
                    {
                        classSpecCount++;
                    }
                }
            }
            return classSpecCount;
        }

        public int ScoreUniqueness()
        {
            int score = 0;

            for (int raidGroupIndex = 0; raidGroupIndex < characters.GetLength(0); raidGroupIndex++)
            {
                for (int raidPartyMemberIndex = 0; raidPartyMemberIndex < characters.GetLength(1); raidPartyMemberIndex++)
                {
                    PlayerCharacter playerCharacter = characters[raidGroupIndex, raidPartyMemberIndex];
                    if (playerCharacter != null)
                    {
                        int classSpecCount = GetClassSpecCount(playerCharacter.classSpecKey);
                        int roleIndex = Helper.GetRoleIndex(playerCharacter.classSpecKey);
                        score += (classSpecCount - 1) * Helper.UniqueRoleScores[roleIndex];
                    }
                }
            }

            return score;
        }
    }

    public class RaidGroupCollection : IComparable
    {
        List<RaidGroup> raidGroups = new List<RaidGroup>();

        public int Count
        {
            get {  return raidGroups.Count; }
        }

        public int ID
        {
            get;
            set;
        }

        public int RandomSeed
        {
            get;
            set;
        }

        public int UniquenessScore
        {
            get 
            {
                int score = 0;

                for (int raidGroupIndex = 0; raidGroupIndex < raidGroups.Count; raidGroupIndex++)
                {
                    score += raidGroups[raidGroupIndex].ScoreUniqueness();
                }

                return score;
            }
        }

        public int AssignedCharacterCount
        {
            get
            {
                int assignedCharacterCount = 0;
                foreach (RaidGroup raidGroup in raidGroups)
                {
                    assignedCharacterCount += raidGroup.GetAssignedCharacterCount();
                }
                return assignedCharacterCount;
            }
        }

        public int CompareTo(object that)
        {
            if (that == null)
            {
                return 1;
            }

            RaidGroupCollection thatRaidGroupCollection = that as RaidGroupCollection;
            if (this.AssignedCharacterCount != thatRaidGroupCollection.AssignedCharacterCount)
            {
                return this.AssignedCharacterCount > thatRaidGroupCollection.AssignedCharacterCount ? -1 : 1;
            }

            int thisUniquenessScore = this.UniquenessScore;
            int thatUniquenessScore = thatRaidGroupCollection.UniquenessScore;
            if (thisUniquenessScore != thatUniquenessScore)
            {
                return thisUniquenessScore < thatUniquenessScore ? -1 : 1;
            }

            return this.ID > thatRaidGroupCollection.ID ? 1 : -1;
        }

        public RaidGroup At(int index)
        {
            return raidGroups[index];
        }
        public void Add(RaidGroup raidGroup)
        {
            raidGroups.Add(raidGroup);
        }

        public void Clear()
        {
            raidGroups.Clear();
        }

        public RaidGroupCollection Clone()
        {
            RaidGroupCollection raidGroupsClone = new RaidGroupCollection();
            foreach (RaidGroup raidGroupTemp in raidGroups)
            {
                raidGroupsClone.Add(new RaidGroup(raidGroupTemp));
            }
            return raidGroupsClone;
        }

        public void Set(RaidGroupCollection that)
        {
            for (int raidGroupIndex = 0; raidGroupIndex < that.Count; raidGroupIndex++)
            {
                raidGroups[raidGroupIndex].Set(that.At(raidGroupIndex));
            }
        }

        public bool AnyRaidContainsCharacter(PlayerCharacter playerCharacter)
        {
            for (int raidIndex = 0; raidIndex < Count; raidIndex++)
            {
                RaidGroup raidGroup = At(raidIndex);
                if (raidGroup.ContainsPlayerCharacter(playerCharacter))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class RaidGroupGenerator
    {
        static readonly int MAX_RECURSIONS = 6;

        private RaidGroupCollection raidGroupCollection;
        private List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();

        public void ClearPlayerCharacters()
        {
            playerCharacters.Clear();
        }

        public void AddPlayerCharacter(PlayerCharacter playerCharacter)
        {
            playerCharacters.Add(playerCharacter);
        }

        public PlayerCharacter GetPlayerCharacterAt(int index)
        {
            return playerCharacters[index];
        }

        public int GetPlayerCharacterCount()
        {
            return playerCharacters.Count;
        }

        public class PlayerRaidGroupWeight : IComparable
        {
            public int raidIndex;
            public int groupIndex;
            public int partyMemberIndex;
            public bool exactRoleMatch;
            public float weight;

            public int CompareTo(object that)
            {
                if (that == null)
                {
                    return 1;
                }

                PlayerRaidGroupWeight thatPlayerRaidGroupWeight = that as PlayerRaidGroupWeight;
                if (thatPlayerRaidGroupWeight.weight != weight)
                {
                    return this.weight > thatPlayerRaidGroupWeight.weight ? 1 : -1;
                }

                if (thatPlayerRaidGroupWeight.raidIndex != raidIndex)
                {
                    return this.raidIndex > thatPlayerRaidGroupWeight.raidIndex ? 1 : -1;
                }

                if (thatPlayerRaidGroupWeight.groupIndex != groupIndex)
                {
                    return this.groupIndex > thatPlayerRaidGroupWeight.groupIndex ? 1 : -1;
                }

                return this.partyMemberIndex > thatPlayerRaidGroupWeight.partyMemberIndex ? 1 : -1;
            }
        }

        public bool GetRaidGroupContainingPlayer(string player, out int out_raidIndex, out PlayerCharacter out_foundPlayerCharacter)
        {
            out_raidIndex = -1;
            out_foundPlayerCharacter = null;

            for (int raidIndex = 0; raidIndex < raidGroupCollection.Count; raidIndex++)
            {
                RaidGroup raidGroup = raidGroupCollection.At(raidIndex);
                if (raidGroup.ContainsPlayer(player))
                {
                    out_raidIndex = raidIndex;
                    out_foundPlayerCharacter = raidGroup.GetCharacterForPlayer(player);
                    return true;
                }
            }

            return false;
        }

        private bool GetRaidGroupContainingAlternatePlayerCharacter(PlayerCharacter playerCharacter, out int out_raidIndex, out PlayerCharacter out_foundPlayerCharacter)
        {
            out_raidIndex = -1;
            out_foundPlayerCharacter = null;

            for (int raidIndex = 0; raidIndex < raidGroupCollection.Count; raidIndex++)
            {
                RaidGroup raidGroup = raidGroupCollection.At(raidIndex);
                for (int raidGroupIndex = 0; raidGroupIndex < raidGroup.characters.GetLength(0); raidGroupIndex++)
                {
                    for (int raidPartyMemberIndex = 0; raidPartyMemberIndex < raidGroup.characters.GetLength(1); raidPartyMemberIndex++)
                    {
                        if (raidGroup.characters[raidGroupIndex, raidPartyMemberIndex] != null)
                        {
                            PlayerCharacter raidPlayerCharacter = raidGroup.characters[raidGroupIndex, raidPartyMemberIndex];
                            if (raidPlayerCharacter.player == playerCharacter.player && raidPlayerCharacter != playerCharacter)
                            {
                                out_raidIndex = raidIndex;
                                out_foundPlayerCharacter = raidPlayerCharacter;
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool GetRaidGroupContainingPlayerCharacter(PlayerCharacter playerCharacter, out int out_raidIndex)
        {
            out_raidIndex = -1;

            for (int raidIndex = 0; raidIndex < raidGroupCollection.Count; raidIndex++)
            {
                RaidGroup raidGroup = raidGroupCollection.At(raidIndex);
                for (int raidGroupIndex = 0; raidGroupIndex < raidGroup.characters.GetLength(0); raidGroupIndex++)
                {
                    for (int raidPartyMemberIndex = 0; raidPartyMemberIndex < raidGroup.characters.GetLength(1); raidPartyMemberIndex++)
                    {
                        if (raidGroup.characters[raidGroupIndex, raidPartyMemberIndex] != null)
                        {
                            PlayerCharacter raidPlayerCharacter = raidGroup.characters[raidGroupIndex, raidPartyMemberIndex];
                            if (raidPlayerCharacter == playerCharacter)
                            {
                                out_raidIndex = raidIndex;
                                return true;
                            }
                        }
                    }
                }

            }

            return false;
        }

        public bool AnyRaidHasRoomForClassSpec(PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights, out int out_raidIndex, out int out_groupIndex, out int out_partyMemberIndex)
        {
            out_raidIndex = -1;
            out_groupIndex = -1;
            out_partyMemberIndex = -1;

            for (int raidIndex = 0; raidIndex < raidGroupCollection.Count; raidIndex++)
            {
                RaidGroup raidGroup = raidGroupCollection.At(raidIndex);   
                List<RaidPosition> emptyRaidPositions = raidGroup.FindEmptyPositionsForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, allowGenericRoleMatch);
                if (emptyRaidPositions.Count > 0)
                {
                    out_raidIndex = raidIndex;
                    return true;
                }
            }
            return false;
        }

        private void AddPlayerRaidGroupWeight(PlayerCharacter playerCharacter, RaidGroup raidGroup, int raidIndex, int roleIndex, int gearTypeIndex, int specIndex, float matchWeight, int groupIndex, int partyMemberIndex, bool exactRoleMatch, List<PlayerRaidGroupWeight> playerCharacterRaidGroupWeights)
        {
            PlayerRaidGroupWeight playerRaidGroupWeight = new PlayerRaidGroupWeight();
            playerRaidGroupWeight.raidIndex = raidIndex;
            playerRaidGroupWeight.groupIndex = groupIndex;
            playerRaidGroupWeight.partyMemberIndex = partyMemberIndex;
            playerRaidGroupWeight.exactRoleMatch = exactRoleMatch;

            float raidMatchWeight = playerCharacter.staticRaid != raidIndex ? 1000 : 0;
            float roleWeight = raidGroup.roleCounts[roleIndex] * 10;
            float gearTypeWeight = raidGroup.gearTypeCounts[gearTypeIndex];
            float classSpecWeight = raidGroup.classSpecCounts[specIndex] * 10000;
            playerRaidGroupWeight.weight = raidMatchWeight + matchWeight + roleWeight + gearTypeWeight + classSpecWeight;

            playerCharacterRaidGroupWeights.Add(playerRaidGroupWeight);
        }

        public void GeneratePlayerRaidGroupWeights(PlayerCharacter playerCharacter, int raidGroupCount, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights)
        {
            int roleIndex = Helper.GetRoleIndex(playerCharacter.classSpecKey);
            if (roleIndex == -1)
            {
                throw new ApplicationException(String.Format("Player {0}, character {1} has invalid class/specialisation: {2}.", playerCharacter.player, playerCharacter.character, playerCharacter.classSpecKey));
            }

            int gearTypeIndex = Helper.GetGearTypeIndex(playerCharacter.classSpecKey);
            int specIndex = Helper.GetSpecIndex(playerCharacter.classSpecKey);

            // First, calculate weights for each raid group and which ones are the best fit.
            List<PlayerRaidGroupWeight> playerCharacterRaidGroupWeights = new List<PlayerRaidGroupWeight>();
            for (int raidIndex = 0; raidIndex < raidGroupCount; raidIndex++)
            {
                // Check if the character is absent for this raid.
                if (playerCharacter.absentRaids.Contains(raidIndex))
                {
                    continue;
                }

                // Check whether the character is fixed to this raid.
                if (playerCharacter.staticRaid >= 0 && raidIndex != playerCharacter.staticRaid)
                {
                    continue;
                }

                RaidGroup raidGroup = raidGroupCollection.At(raidIndex);

                PlayerRaidGroupWeight playerRaidGroupWeight = new PlayerRaidGroupWeight();
                playerRaidGroupWeight.raidIndex = raidIndex;

                if (raidIndex == playerCharacter.staticRaid)
                {
                    int staticRaidGroup = playerCharacter.staticRaidPosition / 5;
                    int staticPartyMemberPosition = playerCharacter.staticRaidPosition % 5;
                    AddPlayerRaidGroupWeight(playerCharacter, raidGroup, raidIndex, roleIndex, gearTypeIndex, specIndex, 0, staticRaidGroup, staticPartyMemberPosition, true, playerCharacterRaidGroupWeights);
                    continue;
                }

                // Try to find an exact role match.
                List<RaidPosition> exactRoleMatchEmptyRaidPositions = raidGroup.FindEmptyPositionsForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, false);
                foreach (RaidPosition raidPosition in exactRoleMatchEmptyRaidPositions)
                {
                    AddPlayerRaidGroupWeight(playerCharacter, raidGroup, raidIndex, roleIndex, gearTypeIndex, specIndex, raidPosition.weight, raidPosition.groupIndex, raidPosition.partyMemberIndex, true, playerCharacterRaidGroupWeights);
                }

                List<RaidPosition> exactRoleMatchFullRaidPositions = raidGroup.FindFullPositionsForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, false);
                foreach (RaidPosition raidPosition in exactRoleMatchFullRaidPositions)
                {
                    AddPlayerRaidGroupWeight(playerCharacter, raidGroup, raidIndex, roleIndex, gearTypeIndex, specIndex, raidPosition.weight, raidPosition.groupIndex, raidPosition.partyMemberIndex, true, playerCharacterRaidGroupWeights);
                }

                List<RaidPosition> genericRoleMatchEmptyRaidPositions = raidGroup.FindEmptyPositionsForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, true);
                foreach (RaidPosition raidPosition in genericRoleMatchEmptyRaidPositions)
                {
                    AddPlayerRaidGroupWeight(playerCharacter, raidGroup, raidIndex, roleIndex, gearTypeIndex, specIndex, raidPosition.weight, raidPosition.groupIndex, raidPosition.partyMemberIndex, false, playerCharacterRaidGroupWeights);
                }

                List<RaidPosition> genericRoleMatchFullRaidPositions = raidGroup.FindFullPositionsForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, true);
                foreach (RaidPosition raidPosition in genericRoleMatchFullRaidPositions)
                {
                    AddPlayerRaidGroupWeight(playerCharacter, raidGroup, raidIndex, roleIndex, gearTypeIndex, specIndex, raidPosition.weight, raidPosition.groupIndex, raidPosition.partyMemberIndex, false, playerCharacterRaidGroupWeights);
                }
            }

            playerCharacterRaidGroupWeights.Sort();

            playersRaidGroupWeights.Add(playerCharacter, playerCharacterRaidGroupWeights);
        }

        public bool AttemptToDistributePlayerCharacter(PlayerCharacter playerCharacter, int raidGroupCount, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights, Random random)
        {
            int roleIndex = Helper.GetRoleIndex(playerCharacter.classSpecKey);
            if (roleIndex == -1)
            {
                throw new ApplicationException(String.Format("Player {0}, character {1} has invalid class/specialisation: {2}.", playerCharacter.player, playerCharacter.character, playerCharacter.classSpecKey));
            }

            List<PlayerRaidGroupWeight> playerCharacterRaidGroupWeights;
            if (!playersRaidGroupWeights.TryGetValue(playerCharacter, out playerCharacterRaidGroupWeights))
            {
                return false;
            }

            List<PlayerRaidGroupWeight> suitableRaidGroupWeights = new List<PlayerRaidGroupWeight>();
            foreach (PlayerRaidGroupWeight playerRaidGroupWeight in playerCharacterRaidGroupWeights)
            {
                if (!allowGenericRoleMatch && !playerRaidGroupWeight.exactRoleMatch)
                {
                    continue;
                }

                if (raidGroupCollection.At(playerRaidGroupWeight.raidIndex).ContainsPlayer(playerCharacter.player))
                {
                    continue;
                }

                suitableRaidGroupWeights.Add(playerRaidGroupWeight);
            }

            // Randomly pick from one of the suitable raid groups to assign this player.
            while (suitableRaidGroupWeights.Count > 0 )
            {
                int weightIndex = random.Next(suitableRaidGroupWeights.Count);
                PlayerRaidGroupWeight playerRaidGroupWeight = suitableRaidGroupWeights[weightIndex];
                RaidGroup raidGroup = raidGroupCollection.At(playerRaidGroupWeight.raidIndex);
                if (raidGroup.PositionIsEmpty(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex))
                {
                    raidGroup.SetPlayerCharacter(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, playerCharacter);
                    return true;
                }
                suitableRaidGroupWeights.Remove(playerRaidGroupWeight);
            }

            return false;
        }

        private bool AttemptToRedistributePlayerCharacter(int ctr, List<PlayerCharacter> in_playerCharacterStack, PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights, Random random)
        {
            if (ctr == MAX_RECURSIONS)
            {
                return false;
            }
            else
            {
                ctr++;
            }

            // First, check whether this player already has a character in any raid.
            int raidIndexContainingPlayerCharacter;
            if (!GetRaidGroupContainingPlayerCharacter(playerCharacter, out raidIndexContainingPlayerCharacter))
            {
                return false;
            }

            // Secondly, check whether this character successfully weighted any raids.
            List<PlayerRaidGroupWeight> playerRaidGroupWeights;
            if (!playersRaidGroupWeights.TryGetValue(playerCharacter, out playerRaidGroupWeights))
            {
                return false;
            }

            int existingGroupIndex, existingPartyMemberIndex;
            RaidGroup raidGroupContainingPlayer = raidGroupCollection.At(raidIndexContainingPlayerCharacter);
            raidGroupContainingPlayer.GetPlayerCharacterLocation(playerCharacter, out existingGroupIndex, out existingPartyMemberIndex);
            bool playerIsInGenericRole = !desiredRaidComposition.IsExactRoleMatch(existingGroupIndex, existingPartyMemberIndex, playerCharacter.classSpecKey);

            RaidGroupCollection tempRaidGroups = raidGroupCollection.Clone();

            // Find any other raids that have a matching weight.
            List<PlayerRaidGroupWeight> validRaidGroupWeights = new List<PlayerRaidGroupWeight>();
            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < playerRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = playerRaidGroupWeights[raidGroupWeightIndex];
                if (playerRaidGroupWeight.raidIndex != raidIndexContainingPlayerCharacter && !playerCharacter.absentRaids.Contains(playerRaidGroupWeight.raidIndex))
                {
                    validRaidGroupWeights.Add(playerRaidGroupWeight);
                }
            }

            validRaidGroupWeights.Sort();

            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < validRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = validRaidGroupWeights[raidGroupWeightIndex];
                if (!playerRaidGroupWeight.exactRoleMatch)
                {
                    continue;
                }

                RaidGroup raidGroup = raidGroupCollection.At(playerRaidGroupWeight.raidIndex);
                RaidGroup tempRaidGroup = new RaidGroup(raidGroup);

                // Check whether there is room in this raid for this character.
                bool hasAvailablePosition = raidGroup.PositionIsEmpty(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex);

                // Check whether this player does not have yet another character already in this raid.
                if (raidGroup.ContainsPlayer(playerCharacter.player))
                {
                    if (raidGroup.PositionIsEmpty(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex))
                    {
                        // If we do, try to move this player's alt to fit this character.
                        PlayerCharacter alternatePlayerCharacter = raidGroup.GetCharacterForPlayer(playerCharacter.player);
                        if (in_playerCharacterStack.Contains(alternatePlayerCharacter))
                        {
                            continue;
                        }

                        // Remove the character from their old raid group and try to move the alt.
                        raidGroupContainingPlayer.RemovePlayerCharacter(playerCharacter);

                        List<PlayerCharacter> playerCharacterStack = new List<PlayerCharacter>(in_playerCharacterStack);
                        playerCharacterStack.Add(playerCharacter);

                        if (AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, alternatePlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random) && !raidGroup.ContainsPlayer(playerCharacter.player))
                        {
                            raidGroup.SetPlayerCharacter(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, playerCharacter);
                            return true;
                        }
                        else
                        {
                            // Failed to move character, reset the raid group back to how it was.
                            break;
                        }
                    }

                    {
                        List<PlayerCharacter> playerCharacterStack = new List<PlayerCharacter>(in_playerCharacterStack);
                        playerCharacterStack.Add(playerCharacter);

                        // Try to move the character in this position.
                        PlayerCharacter existingPlayerCharacter = raidGroup.GetPlayerCharacterAt(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex);
                        if (existingPlayerCharacter.staticRaid == playerRaidGroupWeight.raidIndex)
                        {
                            continue;
                        }

                        // Don't move a character if they're much higher priority than this one.
                        bool existingPlayerIsInExactRoleMatch = desiredRaidComposition.IsExactRoleMatch(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, existingPlayerCharacter.classSpecKey);
                        int priorityThreshold = existingPlayerIsInExactRoleMatch ? existingPlayerCharacter.priority + 2 : 10;
                        if (playerCharacter.priority > priorityThreshold)
                        {
                            continue;
                        }

                        // Don't move the existing character if it's in an exact role match and the player we're looking to slot in isn't.
                        if (playerIsInGenericRole && existingPlayerIsInExactRoleMatch)
                        {
                            continue;
                        }

                        // If his is the character we're trying to redistribute, don't try to redistribute.
                        bool swappingInThisCharacter = in_playerCharacterStack.Count > 0 && in_playerCharacterStack.Last() == existingPlayerCharacter;

                        // If this character already exists up the stack, don't continue further;
                        if (!swappingInThisCharacter)
                        {
                            if (in_playerCharacterStack.Contains(existingPlayerCharacter))
                            {
                                continue;
                            }

                            if (AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, existingPlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random))
                            {
                                return true;
                            }
                        }

                        PlayerCharacter alternatePlayerCharacter = raidGroup.GetCharacterForPlayer(playerCharacter.player);
                        if (in_playerCharacterStack.Contains(alternatePlayerCharacter))
                        {
                            continue;
                        }

                        if (!AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, alternatePlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random))
                        {
                            continue;
                        }

                        if (raidGroup.PositionIsEmpty(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex))
                        {
                            raidGroup.SetPlayerCharacter(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, playerCharacter);
                            return true;
                        }
                    }
                }
                else
                {
                    if (hasAvailablePosition)
                    {
                        // There is a space in this raid, remove this character from the old raid and add them to the new one.
                        RaidGroup raidGroupContainingPlayerCharacter = raidGroupCollection.At(raidIndexContainingPlayerCharacter);
                        raidGroupContainingPlayerCharacter.RemovePlayerCharacter(playerCharacter);
                        raidGroup.SetPlayerCharacter(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, playerCharacter);

                        return true;
                    }

                    // Check whether any of the raid positions we found contain the player we're trying to swap in.
                    PlayerCharacter existingPlayerCharacter = raidGroup.GetPlayerCharacterAt(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex);
                    if (existingPlayerCharacter.staticRaid == playerRaidGroupWeight.raidIndex)
                    {
                        continue;
                    }

                    // Don't move a character if they're much higher priority than this one.
                    bool existingPlayerIsInExactRoleMatch = desiredRaidComposition.IsExactRoleMatch(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, existingPlayerCharacter.classSpecKey);
                    int priorityThreshold = existingPlayerIsInExactRoleMatch ? existingPlayerCharacter.priority + 2 : 10;
                    if (playerCharacter.priority > priorityThreshold)
                    {
                        continue;
                    }

                    if (in_playerCharacterStack.Contains(existingPlayerCharacter))
                    {
                        PlayerCharacter stackPlayerCharacter = in_playerCharacterStack.Count > 0 ? in_playerCharacterStack.Last() : null;
                        if (stackPlayerCharacter == existingPlayerCharacter && GetRaidGroupContainingPlayerCharacter(playerCharacter, out raidIndexContainingPlayerCharacter))
                        {
                            RaidGroup raidGroupContainingPlayerCharacter = raidGroupCollection.At(raidIndexContainingPlayerCharacter);
                            if (!raidGroupContainingPlayerCharacter.ContainsPlayer(existingPlayerCharacter.player) && !raidGroup.ContainsPlayer(playerCharacter.player))
                            {
                                raidGroupContainingPlayerCharacter.ReplacePlayerCharacter(playerCharacter, existingPlayerCharacter);
                                raidGroup.ReplacePlayerCharacter(existingPlayerCharacter, playerCharacter);
                                return true;
                            }
                        }
                        else if(stackPlayerCharacter != existingPlayerCharacter)
                        {
                            continue;
                        }
                    }

                    // Try to move the character in this position.
                    List<PlayerCharacter> playerCharacterStack = new List<PlayerCharacter>(in_playerCharacterStack);
                    playerCharacterStack.Add(playerCharacter);

                    if (AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, existingPlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random))
                    {
                        return true;
                    }
                }

                // We failed to do anything productive, set the raid group back to how it was.
                raidGroup.Set(tempRaidGroup);
            }

            // We failed to do anything productive, set the raid groups back to how they were.
            raidGroupCollection.Set(tempRaidGroups);

            return false;
        }

        private bool AttemptToRedistributeOtherPlayerCharacterOfSameClass(int ctr, List<PlayerCharacter> in_playerCharacterStack, PlayerCharacter playerCharacter, PlayerCharacter swappingPlayerCharacter, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights, Random random)
        {
            if (ctr == MAX_RECURSIONS)
            {
                return false;
            }
            else
            {
                ctr++;
            }

            List<PlayerRaidGroupWeight> playerRaidGroupWeights;
            if (!playersRaidGroupWeights.TryGetValue(playerCharacter, out playerRaidGroupWeights))
            {
                return false;
            }

            RaidGroupCollection tempRaidGroupCollection = raidGroupCollection.Clone();

            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < playerRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = playerRaidGroupWeights[raidGroupWeightIndex];

                // First, make sure this character isn't absent for this raid.
                if (playerCharacter.absentRaids.Contains(playerRaidGroupWeight.raidIndex))
                {
                    continue;
                }

                if (!allowGenericRoleMatch && !playerRaidGroupWeight.exactRoleMatch)
                {
                    continue;
                }

                // Second, make sure this player does not have yet another character already in this raid.
                RaidGroup raidGroup = raidGroupCollection.At(playerRaidGroupWeight.raidIndex);
                if (raidGroup.ContainsPlayer(playerCharacter.player))
                {
                    // Get the alternate character out.
                    PlayerCharacter alternatePlayerCharacter = raidGroup.GetCharacterForPlayer(playerCharacter.player);
                    if (in_playerCharacterStack.Contains(alternatePlayerCharacter))
                    {
                        continue;
                    }

                    List<PlayerCharacter> playerCharacterStack = new List<PlayerCharacter>(in_playerCharacterStack);
                    playerCharacterStack.Add(playerCharacter);

                    // Try to redistribute the alt.
                    if (!AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, alternatePlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random))
                    {
                        continue;
                    }
                }

                // Check whether another character can be redistributed.
                //List<RaidPosition> fullRaidPositions = raidGroup.FindFullPositionsForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, allowGenericRoleMatch);
                //foreach (RaidPosition raidPosition in fullRaidPositions)
                {
                    PlayerCharacter existingPlayerCharacter = raidGroup.GetPlayerCharacterAt(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex);
                    if (existingPlayerCharacter == null)
                    {
                        continue;
                    }

                    if (existingPlayerCharacter.staticRaid == playerRaidGroupWeight.raidIndex)
                    {
                        continue;
                    }

                    if (in_playerCharacterStack.Contains(existingPlayerCharacter))
                    {
                        int raidIndexContainingStackCharacter;
                        PlayerCharacter stackPlayerCharacter = in_playerCharacterStack.Count > 0 ? in_playerCharacterStack.Last() : null;
                        if (stackPlayerCharacter == existingPlayerCharacter && GetRaidGroupContainingPlayerCharacter(stackPlayerCharacter, out raidIndexContainingStackCharacter))
                        {
                            RaidGroup raidGroupContainingStackCharacter = raidGroupCollection.At(raidIndexContainingStackCharacter);
                            if (!raidGroupContainingStackCharacter.ContainsPlayer(playerCharacter.player))
                            {
                                raidGroupContainingStackCharacter.ReplacePlayerCharacter(stackPlayerCharacter, existingPlayerCharacter);
                                raidGroup.ReplacePlayerCharacter(existingPlayerCharacter, stackPlayerCharacter);
                                return true;
                            }
                        }
                        continue;
                    }
                    
                    // Don't move a character if they're much higher priority than this one.
                    bool existingPlayerIsInExactRoleMatch = desiredRaidComposition.IsExactRoleMatch(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, existingPlayerCharacter.classSpecKey);
                    int priorityThreshold = existingPlayerIsInExactRoleMatch ? existingPlayerCharacter.priority + 2 : 10;
                    if (playerCharacter.priority > priorityThreshold)
                    {
                        continue;
                    }

                    // Check whether we're trying to swap directly with the player we're trying to re-distribute for, but not the same character.
                    bool swappingWithDesiredPlayer = swappingPlayerCharacter != null && swappingPlayerCharacter.player == existingPlayerCharacter.player && swappingPlayerCharacter.character != existingPlayerCharacter.character;
                    if (swappingWithDesiredPlayer)
                    {
                        raidGroup.ReplacePlayerCharacter(existingPlayerCharacter, playerCharacter);
                        return true;
                    }

                    List<PlayerCharacter> playerCharacterStack = new List<PlayerCharacter>(in_playerCharacterStack);
                    playerCharacterStack.Add(playerCharacter);

                    if (AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, existingPlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random))
                    {
                        if (raidGroup.PositionIsEmpty(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex) && !raidGroup.ContainsPlayer(playerCharacter.player))
                        {
                            raidGroup.SetPlayerCharacter(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, playerCharacter);
                            return true;
                        }
                    }
                }
            }

            // We failed to do anything productive, set the raid groups back to how they were.
            raidGroupCollection.Set(tempRaidGroupCollection);

            return false;
        }

        private bool AttemptToDistributePlayerCharacterWithRoleMatch(bool allowGenericRoleMatch, PlayerCharacter playerCharacter, int raidGroupCount, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights, Random random)
        {
            if (AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random))
            {
                return true;
            }

            int ctr = 0;
            List<PlayerCharacter> playerCharacterStack = new List<PlayerCharacter>();

            List<PlayerRaidGroupWeight> playerRaidGroupWeights;
            if (!playersRaidGroupWeights.TryGetValue(playerCharacter, out playerRaidGroupWeights))
            {
                return false;
            }

            playerRaidGroupWeights.Sort();

            RaidGroupCollection tempRaidGroupCollection = raidGroupCollection.Clone();

            // Check whether any raid that we have an alternate character in has emtpy spots for this character specific role.
            foreach (PlayerRaidGroupWeight playerRaidGroupWeight in playerRaidGroupWeights)
            {
                RaidGroup raidGroup = raidGroupCollection.At(playerRaidGroupWeight.raidIndex);
                if (raidGroup.PositionIsEmpty(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex))
                {
                    // If we get here there is room for this character but an alternate is in that raid, try to move the alternate.
                    PlayerCharacter alternatePlayerCharacter = raidGroup.GetCharacterForPlayer(playerCharacter.player);
                    if (alternatePlayerCharacter != null && AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, alternatePlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random) && !raidGroup.ContainsPlayer(playerCharacter.player))
                    {
                        raidGroup.SetPlayerCharacter(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, playerCharacter);
                        return true;
                    }
                }
            }

            // We failed to do anything productive, set the raid groups back to how they were.
            raidGroupCollection.Set(tempRaidGroupCollection);

            // Now check whether any raid that we have an alternate character in has full spots that we could redistribute to fit this character.
            foreach (PlayerRaidGroupWeight playerRaidGroupWeight in playerRaidGroupWeights)
            {
                RaidGroup raidGroup = raidGroupCollection.At(playerRaidGroupWeight.raidIndex);
                if (!allowGenericRoleMatch && !playerRaidGroupWeight.exactRoleMatch)
                {
                    continue;
                }

                if (!raidGroup.PositionIsEmpty(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex))
                {
                    // If we get here and there is an alt in the raid, try to move the alternate. If there was no alternate.
                    PlayerCharacter alternatePlayerCharacter = raidGroup.GetCharacterForPlayer(playerCharacter.player);
                    if (alternatePlayerCharacter != null && !AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, alternatePlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random))
                    {
                        continue;
                    }

                    // Grab the player in this spot and try to redistribute them.
                    PlayerCharacter existingPlayerCharacter = raidGroup.GetPlayerCharacterAt(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex);
                    if (existingPlayerCharacter == null)
                    {
                        // We succesfully moved the player in this position, try to assign this player.
                        if (AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random))
                        {
                            return true;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    bool existingPlayerIsInExactRoleMatch = desiredRaidComposition.IsExactRoleMatch(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, existingPlayerCharacter.classSpecKey);

                    // Don't move a character if they're much higher priority than this one.
                    int priorityThreshold = existingPlayerIsInExactRoleMatch ? existingPlayerCharacter.priority + 2 : 10;
                    if (playerCharacter.priority > priorityThreshold)
                    {
                        continue;
                    }

                    if (existingPlayerCharacter.staticRaid == playerRaidGroupWeight.raidIndex)
                    {
                        continue;
                    }

                    if (AttemptToRedistributePlayerCharacter(ctr, playerCharacterStack, existingPlayerCharacter, desiredRaidComposition, playersRaidGroupWeights, random))
                    {
                        // We succesfully moved the player in this position, try to assign this player.
                        if (AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random))
                        {
                            return true;
                        }
                    }
                }
            }

            // We failed to do anything productive again, set the raid groups back to how they were.
            raidGroupCollection.Set(tempRaidGroupCollection);

            // We tried to move the alternate of this character but failed, try to move another character.
            if (AttemptToRedistributeOtherPlayerCharacterOfSameClass(ctr, playerCharacterStack, playerCharacter, null, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random))
            {
                return true;
            }

            // We failed to do anything productive again, set the raid groups back to how they were.
            raidGroupCollection.Set(tempRaidGroupCollection);

            return false;
        }

        public RaidGroupCollection GenerateRaidGroups(int raidGroupCount, RaidComposition desiredRaidComposition, int randomSeed)
        {
            raidGroupCollection = new RaidGroupCollection();
            raidGroupCollection.RandomSeed = randomSeed;

            Random random = new Random(randomSeed);

            for (int raidIndex = 0; raidIndex < raidGroupCount; raidIndex++)
            {
                RaidGroup raidGroup = new RaidGroup();
                raidGroupCollection.Add(raidGroup);
            }

            playerCharacters.Sort();

            // Generate the raids.
            Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights = new Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>>();
            foreach (PlayerCharacter playerCharacter in playerCharacters)
            {
                GeneratePlayerRaidGroupWeights(playerCharacter, raidGroupCount, desiredRaidComposition, playersRaidGroupWeights);

                // Attempt to distribute this character in a specific role.
                if (AttemptToDistributePlayerCharacterWithRoleMatch(false, playerCharacter, raidGroupCount, desiredRaidComposition, playersRaidGroupWeights, random))
                {
                    continue;
                }

                // Attempt to distribute this character in a generic role.
                AttemptToDistributePlayerCharacterWithRoleMatch(true, playerCharacter, raidGroupCount, desiredRaidComposition, playersRaidGroupWeights, random);
            }

            // Once we've done a first pass, try again with players who missed out now that raids may have been shifted around.
            foreach (PlayerCharacter playerCharacter in playerCharacters)
            {
                if (!raidGroupCollection.AnyRaidContainsCharacter(playerCharacter))
                {
                    // Attempt to distribute this character in a generic role.
                    AttemptToDistributePlayerCharacterWithRoleMatch(true, playerCharacter, raidGroupCount, desiredRaidComposition, playersRaidGroupWeights, random);
                }
            }

            return raidGroupCollection;
        }
    }
}