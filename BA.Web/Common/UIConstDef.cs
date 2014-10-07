
namespace BA.Web.Common
{
    public class UIConstDef
    {

        public const string EmptyKey = "-1";
        public const string EmptyText = "";

        public const string EmptyValue = "-1";

        public const int NewObjectID = -1;
    }

    public static class UILabelDef
    {
        public const string InstanceNewLabel = "New";
        public const string InstanceListManagementLabel = "Management";
    }

    public enum WarningMessageType
    {
        Unknown = 0,
        SessionTimeout = 1, 
        NotAuthorized = 2,
        NullObjectID = 3,
    }

}