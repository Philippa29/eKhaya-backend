using eKhaya.Debugging;

namespace eKhaya
{
    public class eKhayaConsts
    {
        public const string LocalizationSourceName = "eKhaya";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "a4f11f6ffc4441659eb2294b591db380";
    }
}
