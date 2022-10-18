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
                    return getIndexOfString(Roles, "PhysicalMelee");
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
            else if (String.Compare(desiredRole, "Any healer") == 0)
            {
                return roleIndex == getIndexOfString(Roles, "Healer");
            }

            return false;
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

        public bool FindEmptyPositionForClassSpec(string classSpecKey, RaidComposition desiredRaidComposition, out int out_groupIndex, out int out_partyMemberIndex, out float out_matchWeight)
        {
            out_groupIndex = -1;
            out_partyMemberIndex = -1;
            out_matchWeight = 10000;

            // Try to match exact class/spec.
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                    if (classSpecKey == desiredSpecialisation && characters[groupIndex, partyMemberIndex] == null)
                    {
                        out_groupIndex = groupIndex;
                        out_partyMemberIndex = partyMemberIndex;
                        out_matchWeight = 0;
                        return true;
                    }
                }
            }

            // Try to match class.
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                    if (classSpecKey.Contains(desiredSpecialisation) && characters[groupIndex, partyMemberIndex] == null)
                    {
                        out_groupIndex = groupIndex;
                        out_partyMemberIndex = partyMemberIndex;
                        out_matchWeight = 75;
                        return true;
                    }
                }
            }

            // Try to match role (dps, tank, healer)
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                    if (Helper.MatchClassSpecToRole(desiredSpecialisation, classSpecKey) && characters[groupIndex, partyMemberIndex] == null)
                    {
                        out_groupIndex = groupIndex;
                        out_partyMemberIndex = partyMemberIndex;
                        out_matchWeight = 150;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool FindEmptyPositionForCharacter(PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition, out int out_groupIndex, out int out_partyMemberIndex, out float out_matchWeight)
        {
            out_groupIndex = -1;
            out_partyMemberIndex = -1;
            out_matchWeight = 0;

            if (ContainsPlayer(playerCharacter.player))
            {
                return false;
            }

            return FindEmptyPositionForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, out out_groupIndex, out out_partyMemberIndex, out out_matchWeight);
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

        internal void RemovePlayerCharacter(PlayerCharacter existingPlayerCharacter)
        {
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    PlayerCharacter playerCharacter = characters[groupIndex, partyMemberIndex];
                    if (playerCharacter != null && playerCharacter == existingPlayerCharacter)
                    {
                        characters[groupIndex, partyMemberIndex] = null;
                    }
                }
            }
        }
    }
    public class RaidGroupGenerator
    {
        public List<RaidGroup> raidGroups = new List<RaidGroup>();
        private List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();
        private int m_randomSeed = 0;

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

        public struct PlayerRaidGroupWeight
        {
            public int raidIndex;
            public int groupIndex;
            public int partyMemberIndex;
            public float weight;
        }

        public bool GetRaidGroupContainingPlayer(string player, out int out_raidIndex, out PlayerCharacter out_foundPlayerCharacter)
        {
            out_raidIndex = -1;
            out_foundPlayerCharacter = null;

            for (int raidIndex = 0; raidIndex < raidGroups.Count; raidIndex++)
            {
                RaidGroup raidGroup = raidGroups[raidIndex];
                if (raidGroup.ContainsPlayer(player))
                {
                    out_raidIndex = raidIndex;
                    out_foundPlayerCharacter = raidGroup.GetCharacterForPlayer(player);
                    return true;
                }
            }

            return false;
        }

        public bool AnyRaidHasRoomForClassSpec(PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition)
        {
            foreach (RaidGroup raidGroup in raidGroups)
            {
                float out_matchWeight;
                int out_groupIndex, out_partyMemberIndex;
                if (raidGroup.FindEmptyPositionForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, out out_groupIndex, out out_partyMemberIndex, out out_matchWeight))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AttemptToDistributePlayerCharacter(PlayerCharacter playerCharacter, int raidGroupCount, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights, Random random)
        {
            int roleIndex = Helper.GetRoleIndex(playerCharacter.classSpecKey);
            if (roleIndex == -1)
            {
                throw new ApplicationException(String.Format("Player {0}, character {1} has invalid class/specialisation: {2}.", playerCharacter.player, playerCharacter.character, playerCharacter.classSpecKey));
            }

            int gearTypeIndex = Helper.GetGearTypeIndex(playerCharacter.classSpecKey);

            float bestRaidGroupWeight = 10000;

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
                if (playerCharacter.raid > 0 && raidIndex != playerCharacter.raid)
                {
                    continue;
                }


                RaidGroup raidGroup = raidGroups[raidIndex];

                PlayerRaidGroupWeight playerRaidGroupWeight = new PlayerRaidGroupWeight();
                playerRaidGroupWeight.raidIndex = raidIndex;

                float matchWeight = 100;
                if (!raidGroup.FindEmptyPositionForCharacter(playerCharacter, desiredRaidComposition, out playerRaidGroupWeight.groupIndex, out playerRaidGroupWeight.partyMemberIndex, out matchWeight))
                {
                    continue;
                }

                float raidMatchWeight = playerCharacter.raid != raidIndex ? 1000 : 0;
                float roleWeight = raidGroup.roleCounts[roleIndex] * 10;
                float gearTypeWeight = raidGroup.gearTypeCounts[gearTypeIndex];
                playerRaidGroupWeight.weight = raidMatchWeight + matchWeight + roleWeight + gearTypeWeight;
                playerCharacterRaidGroupWeights.Add(playerRaidGroupWeight);

                if (playerRaidGroupWeight.weight < bestRaidGroupWeight)
                {
                    bestRaidGroupWeight = playerRaidGroupWeight.weight;
                }
            }

            // Second, filter out the ones that don't fit as well.
            List<PlayerRaidGroupWeight> suitableRaidGroups = new List<PlayerRaidGroupWeight>();
            foreach (PlayerRaidGroupWeight playerRaidGroupWeight in playerCharacterRaidGroupWeights)
            {
                if (playerRaidGroupWeight.weight == bestRaidGroupWeight)
                {
                    suitableRaidGroups.Add(playerRaidGroupWeight);
                }
            }

            // Lastly, randomly pick from one of the suitable raid groups to assign this player.
            if (suitableRaidGroups.Count > 0)
            {
                int weightIndex = random.Next(suitableRaidGroups.Count);
                PlayerRaidGroupWeight playerRaidGroupWeight = suitableRaidGroups[weightIndex];
                RaidGroup raidGroup = raidGroups[playerRaidGroupWeight.raidIndex];
                raidGroup.SetPlayerCharacter(playerRaidGroupWeight.groupIndex, playerRaidGroupWeight.partyMemberIndex, playerCharacter);

                playersRaidGroupWeights.Add(playerCharacter, playerCharacterRaidGroupWeights);

                return true;
            }

            return false;
        }

        private bool AttemptToRedistributePlayer(PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights)
        {
            // First, check whether this player already has a character in any raid.
            int raidIndexContainingPlayer;
            PlayerCharacter existingPlayerCharacter;
            if (!GetRaidGroupContainingPlayer(playerCharacter.player, out raidIndexContainingPlayer, out existingPlayerCharacter))
            {
                return false;
            }

            // Secondly, check whether this character successfully weighted any raids.
            List<PlayerRaidGroupWeight> playerRaidGroupWeights;
            if (!playersRaidGroupWeights.TryGetValue(existingPlayerCharacter, out playerRaidGroupWeights))
            {
                return false;
            }

            // Find any other raids that have a matching weight.
            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < playerRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = playerRaidGroupWeights[raidGroupWeightIndex];
                if (playerRaidGroupWeight.raidIndex != raidIndexContainingPlayer)
                {
                    // First, make sure this character isn't absent for this raid.
                    if (existingPlayerCharacter.absentRaids.Contains(playerRaidGroupWeight.raidIndex))
                    {
                        continue;
                    }    

                    // Second, make sure this player does not have yet another character already in this raid.
                    RaidGroup raidGroup = raidGroups[playerRaidGroupWeight.raidIndex];
                    if (raidGroup.ContainsPlayer(existingPlayerCharacter.player))
                    {
                        continue;
                    }

                    // Check whether there is room in this raid for this character.
                    float matchWeight = 100;
                    int groupIndex = 0, partyMemberIndex = 0;
                    if (raidGroup.FindEmptyPositionForClassSpec(existingPlayerCharacter.classSpecKey, desiredRaidComposition, out groupIndex, out partyMemberIndex, out matchWeight))
                    {
                        raidGroups[raidIndexContainingPlayer].RemovePlayerCharacter(existingPlayerCharacter);

                        raidGroup.SetPlayerCharacter(groupIndex, partyMemberIndex, existingPlayerCharacter);
                        playerRaidGroupWeight.groupIndex = groupIndex;
                        playerRaidGroupWeight.partyMemberIndex = partyMemberIndex;
                        return true;
                    }
                }
            }

            return false;
        }

        public void GenerateRaidGroups(int raidGroupCount, RaidComposition desiredRaidComposition, bool generateRandomSeed)
        {
            if (m_randomSeed == 0 || generateRandomSeed)
            {
                TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
                m_randomSeed = (int)span.TotalSeconds;
            }
            Random random = new Random(m_randomSeed);

            raidGroups.Clear();
            for (int raidIndex = 0; raidIndex < raidGroupCount; raidIndex++)
            {
                RaidGroup raidGroup = new RaidGroup();
                raidGroups.Add(raidGroup);
            }

            playerCharacters.Sort();


            // Generate the raids.
            Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights = new Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>>();
            foreach (PlayerCharacter playerCharacter in playerCharacters)
            {
                if (!AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, playersRaidGroupWeights, random))
                {
                    // If we failed to distribute this character, check whether there actually is an empty spot they can fill that we could potentially move another character into to make space for this one.
                    if (AnyRaidHasRoomForClassSpec(playerCharacter, desiredRaidComposition) && AttemptToRedistributePlayer(playerCharacter, desiredRaidComposition, playersRaidGroupWeights))
                    {
                        // Try once more now that we succesfully moved one of this player's characters to a different raid group.
                        AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, playersRaidGroupWeights, random);
                    }
                }
            }
        }
    }
}