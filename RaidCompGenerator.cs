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

        public bool FindFullPositionForClassSpec(string classSpecKey, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch, out int out_groupIndex, out int out_partyMemberIndex)
        {
            out_groupIndex = -1;
            out_partyMemberIndex = -1;

            // Try to match exact class/spec.
            for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
            {
                for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                {
                    string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                    if (classSpecKey == desiredSpecialisation && characters[groupIndex, partyMemberIndex] != null)
                    {
                        out_groupIndex = groupIndex;
                        out_partyMemberIndex = partyMemberIndex;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool FindEmptyPositionForClassSpec(string classSpecKey, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch, out int out_groupIndex, out int out_partyMemberIndex, out float out_matchWeight)
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
                        out_matchWeight = 50;
                        return true;
                    }
                }
            }

            // Try to match role (dps, tank, healer)
            if (allowGenericRoleMatch)
            {
                for (int groupIndex = 0; groupIndex < characters.GetLength(0); groupIndex++)
                {
                    for (int partyMemberIndex = 0; partyMemberIndex < characters.GetLength(1); partyMemberIndex++)
                    {
                        string desiredSpecialisation = desiredRaidComposition.GetRaidPositionSpecialisation(groupIndex, partyMemberIndex);
                        if (Helper.MatchClassSpecToRole(desiredSpecialisation, classSpecKey) && characters[groupIndex, partyMemberIndex] == null)
                        {
                            out_groupIndex = groupIndex;
                            out_partyMemberIndex = partyMemberIndex;
                            out_matchWeight = 200;
                            return true;
                        }
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

        internal PlayerCharacter GetPlayerCharacterAt(int groupIndex, int partyMemberIndex)
        {
            return characters[groupIndex, partyMemberIndex];
        }
    }
    public class RaidGroupGenerator
    {
        public List<RaidGroup> raidGroups = new List<RaidGroup>();
        private List<PlayerCharacter> playerCharacters = new List<PlayerCharacter>();

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
            public bool exactRoleMatch;
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

        public bool AnyRaidHasRoomForClassSpec(PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition, bool allowGenericRoleMatch)
        {
            foreach (RaidGroup raidGroup in raidGroups)
            {
                float out_matchWeight;
                int out_groupIndex, out_partyMemberIndex;
                if (raidGroup.FindEmptyPositionForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, allowGenericRoleMatch, out out_groupIndex, out out_partyMemberIndex, out out_matchWeight))
                {
                    return true;
                }
            }
            return false;
        }

        public void GeneratePlayerRaidGroupWeights(PlayerCharacter playerCharacter, int raidGroupCount, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights)
        {
            int roleIndex = Helper.GetRoleIndex(playerCharacter.classSpecKey);
            if (roleIndex == -1)
            {
                throw new ApplicationException(String.Format("Player {0}, character {1} has invalid class/specialisation: {2}.", playerCharacter.player, playerCharacter.character, playerCharacter.classSpecKey));
            }

            int gearTypeIndex = Helper.GetGearTypeIndex(playerCharacter.classSpecKey);

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
                if (playerCharacter.raid >= 0 && raidIndex != playerCharacter.raid)
                {
                    continue;
                }

                RaidGroup raidGroup = raidGroups[raidIndex];

                PlayerRaidGroupWeight playerRaidGroupWeight = new PlayerRaidGroupWeight();
                playerRaidGroupWeight.raidIndex = raidIndex;

                float matchWeight = 10000;
                playerRaidGroupWeight.exactRoleMatch = raidGroup.FindEmptyPositionForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, false, out playerRaidGroupWeight.groupIndex, out playerRaidGroupWeight.partyMemberIndex, out matchWeight);
                if (!playerRaidGroupWeight.exactRoleMatch)
                {
                    raidGroup.FindEmptyPositionForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, true, out playerRaidGroupWeight.groupIndex, out playerRaidGroupWeight.partyMemberIndex, out matchWeight);
                }

                float raidMatchWeight = playerCharacter.raid != raidIndex ? 1000 : 0;
                float roleWeight = raidGroup.roleCounts[roleIndex] * 10;
                float gearTypeWeight = raidGroup.gearTypeCounts[gearTypeIndex];
                playerRaidGroupWeight.weight = raidMatchWeight + matchWeight + roleWeight + gearTypeWeight;
                playerCharacterRaidGroupWeights.Add(playerRaidGroupWeight);
            }
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

            float bestRaidGroupWeight = 10000;
            foreach (PlayerRaidGroupWeight playerRaidGroupWeight in playerCharacterRaidGroupWeights)
            {
                if (!allowGenericRoleMatch && !playerRaidGroupWeight.exactRoleMatch)
                {
                    continue;
                }

                if (raidGroups[playerRaidGroupWeight.raidIndex].ContainsPlayer(playerCharacter.player))
                {
                    continue;
                }

                if (playerRaidGroupWeight.weight < bestRaidGroupWeight)
                {
                    bestRaidGroupWeight = playerRaidGroupWeight.weight;
                }
            }

            // Second, filter out the ones that don't fit as well.
            List<PlayerRaidGroupWeight> suitableRaidGroups = new List<PlayerRaidGroupWeight>();
            foreach (PlayerRaidGroupWeight playerRaidGroupWeight in playerCharacterRaidGroupWeights)
            {
                if (!allowGenericRoleMatch && !playerRaidGroupWeight.exactRoleMatch)
                {
                    continue;
                }

                if (raidGroups[playerRaidGroupWeight.raidIndex].ContainsPlayer(playerCharacter.player))
                {
                    continue;
                }

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

                return true;
            }

            return false;
        }

        private bool AttemptToRedistributeOtherPlayerCharacterOfSameClass(PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights)
        {
            List<PlayerRaidGroupWeight> playerRaidGroupWeights;
            if (!playersRaidGroupWeights.TryGetValue(playerCharacter, out playerRaidGroupWeights))
            {
                return false;
            }

            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < playerRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = playerRaidGroupWeights[raidGroupWeightIndex];

                // First, make sure this character isn't absent for this raid.
                if (playerCharacter.absentRaids.Contains(playerRaidGroupWeight.raidIndex))
                {
                    continue;
                }

                // Second, make sure this player does not have yet another character already in this raid.
                RaidGroup raidGroup = raidGroups[playerRaidGroupWeight.raidIndex];
                if (raidGroup.ContainsPlayer(playerCharacter.player))
                {
                    continue;
                }

                // Check whether another character can be redistributed.
                int groupIndex = 0, partyMemberIndex = 0;
                if (raidGroup.FindFullPositionForClassSpec(playerCharacter.classSpecKey, desiredRaidComposition, false, out groupIndex, out partyMemberIndex))
                {
                    PlayerCharacter existingPlayerCharacter = raidGroup.GetPlayerCharacterAt(groupIndex, partyMemberIndex);

                    int ctr = 0, priorityThreshold = playerCharacter.priority + 1;
                    if (existingPlayerCharacter.priority <= priorityThreshold && AttemptToRedistributePlayer(ctr, existingPlayerCharacter, desiredRaidComposition, playersRaidGroupWeights))
                    {
                        // Successfully moved someone else's character, assign this character.
                        raidGroup.SetPlayerCharacter(groupIndex, partyMemberIndex, playerCharacter);
                        playerRaidGroupWeight.groupIndex = groupIndex;
                        playerRaidGroupWeight.partyMemberIndex = partyMemberIndex;
                        return true;
                    }
                }
            }

            return false;
        }

        static readonly int MAX_RECURSIONS = 5;
        private bool AttemptToRedistributePlayer(int ctr, PlayerCharacter playerCharacter, RaidComposition desiredRaidComposition, Dictionary<PlayerCharacter, List<PlayerRaidGroupWeight>> playersRaidGroupWeights)
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
            int raidIndexContainingPlayer;
            PlayerCharacter alternatePlayerCharacter;
            if (!GetRaidGroupContainingPlayer(playerCharacter.player, out raidIndexContainingPlayer, out alternatePlayerCharacter))
            {
                return false;
            }

            // Secondly, check whether this character successfully weighted any raids.
            List<PlayerRaidGroupWeight> playerRaidGroupWeights;
            if (!playersRaidGroupWeights.TryGetValue(alternatePlayerCharacter, out playerRaidGroupWeights))
            {
                return false;
            }

            // Find any other raids that have a matching weight.
            List<PlayerRaidGroupWeight> validRaidGroupWeights = new List<PlayerRaidGroupWeight>();
            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < playerRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = playerRaidGroupWeights[raidGroupWeightIndex];
                if (playerRaidGroupWeight.raidIndex != raidIndexContainingPlayer)
                {
                    // First, make sure this character isn't absent for this raid.
                    if (alternatePlayerCharacter.absentRaids.Contains(playerRaidGroupWeight.raidIndex))
                    {
                        continue;
                    }    

                    // Second, make sure this player does not have yet another character already in this raid.
                    RaidGroup raidGroup = raidGroups[playerRaidGroupWeight.raidIndex];
                    if (raidGroup.ContainsPlayer(alternatePlayerCharacter.player))
                    {
                        continue;
                    }

                    validRaidGroupWeights.Add(playerRaidGroupWeight);
                }
            }

            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < validRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = validRaidGroupWeights[raidGroupWeightIndex];
                RaidGroup raidGroup = raidGroups[playerRaidGroupWeight.raidIndex];

                // Check whether there is room in this raid for this character.
                float matchWeight = 100;
                int groupIndex = 0, partyMemberIndex = 0;
                if (raidGroup.FindEmptyPositionForClassSpec(alternatePlayerCharacter.classSpecKey, desiredRaidComposition, false, out groupIndex, out partyMemberIndex, out matchWeight))
                {
                    raidGroups[raidIndexContainingPlayer].RemovePlayerCharacter(alternatePlayerCharacter);

                    raidGroup.SetPlayerCharacter(groupIndex, partyMemberIndex, alternatePlayerCharacter);
                    playerRaidGroupWeight.groupIndex = groupIndex;
                    playerRaidGroupWeight.partyMemberIndex = partyMemberIndex;
                    return true;
                }
            }

            // There's no room in any other raid for this player's character that has already been assigned, try to move another player's character.
            for (int raidGroupWeightIndex = 0; raidGroupWeightIndex < validRaidGroupWeights.Count; raidGroupWeightIndex++)
            {
                PlayerRaidGroupWeight playerRaidGroupWeight = validRaidGroupWeights[raidGroupWeightIndex];
                RaidGroup raidGroup = raidGroups[playerRaidGroupWeight.raidIndex];

                // Check whether there is room in this raid for this character.
                int groupIndex = 0, partyMemberIndex = 0;
                if (raidGroup.FindFullPositionForClassSpec(alternatePlayerCharacter.classSpecKey, desiredRaidComposition, false, out groupIndex, out partyMemberIndex))
                {
                    PlayerCharacter existingPlayerCharacter = raidGroup.GetPlayerCharacterAt(groupIndex, partyMemberIndex);
                    if (AttemptToRedistributePlayer(ctr, existingPlayerCharacter, desiredRaidComposition, playersRaidGroupWeights))
                    {
                        // Successfully moved someone else's character, assign this character.
                        raidGroup.SetPlayerCharacter(groupIndex, partyMemberIndex, alternatePlayerCharacter);
                        playerRaidGroupWeight.groupIndex = groupIndex;
                        playerRaidGroupWeight.partyMemberIndex = partyMemberIndex;
                    }
                }
            }

            return false;
        }

        public void GenerateRaidGroups(int raidGroupCount, RaidComposition desiredRaidComposition, int randomSeed)
        {
            Random random = new Random(randomSeed);

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
                GeneratePlayerRaidGroupWeights(playerCharacter, raidGroupCount, desiredRaidComposition, playersRaidGroupWeights);

                bool allowGenericRoleMatch = false;
                bool successfulDistribution = AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random);
                if (!successfulDistribution)
                {
                    successfulDistribution = AttemptToRedistributeOtherPlayerCharacterOfSameClass(playerCharacter, desiredRaidComposition, playersRaidGroupWeights);
                }

                if (!successfulDistribution)
                {
                    // If we failed to distribute this character, check whether there actually is an empty spot they can fill that we could potentially move another character into to make space for this one.
                    int ctr = 0;
                    if (AttemptToRedistributePlayer(ctr, playerCharacter, desiredRaidComposition, playersRaidGroupWeights))
                    {
                        // Try once more now that we succesfully moved one of this player's characters to a different raid group.
                        successfulDistribution = AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random);
                    }
                }

                allowGenericRoleMatch = true;
                
                    // If we still haven't successfully found this character a position, try moving a their other characters via generic role (tank, healer, dps).
                if (!successfulDistribution)
                {
                    successfulDistribution = AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random);
                }

                if (!successfulDistribution)
                {
                    int ctr = 0;
                    if (AnyRaidHasRoomForClassSpec(playerCharacter, desiredRaidComposition, allowGenericRoleMatch) && AttemptToRedistributePlayer(ctr, playerCharacter, desiredRaidComposition, playersRaidGroupWeights))
                    {
                        AttemptToDistributePlayerCharacter(playerCharacter, raidGroupCount, desiredRaidComposition, allowGenericRoleMatch, playersRaidGroupWeights, random);
                    }
                }
            }
        }
    }
}