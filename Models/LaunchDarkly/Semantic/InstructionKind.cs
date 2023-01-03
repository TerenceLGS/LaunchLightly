namespace LaunchLightly.Models.LaunchDarkly.Semantic;
public class InstructionKind
    {
        public static readonly string TurnFlagOn = "turnFlagOn";
        public static readonly string AddIncludedUsers = "addIncludedUsers";
        public static readonly string AddExcludedUsers = "addExcludedUsers";
        public static readonly string RemoveIncludedUsers = "removeIncludedUsers";
        public static readonly string RemoveExcludedUsers = "removeExcludedUsers";
        public static readonly string UpdateName = "updateName";
        public static readonly string AddValuesToClause = "addValuesToClause";
        public static readonly string RemoveValuesFromClause = "removeValuesFromClause";
    }