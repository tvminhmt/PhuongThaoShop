using System.Linq;

namespace PTS.Shared.Helper
{
    public class CacheTimeConfig
    {
        //use for timer refresh cache
        public static int CacheLongTime = 30 * 60;// 30 minute
        public static int CacheShortTime = 5 * 60;// 5 minute
        public static int RefreshLongTime = 5 * 60;// 5 minute
        public static int RefreshShortTime = 1 * 60;// 1 minute
    }
}
