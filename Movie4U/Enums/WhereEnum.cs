namespace Movie4U.Enums
{
    public enum WhereEnum
    {
        None = 0,
        InWatchLater = 1<<0,
        NotInWatchLater = 1<<1,
        PrefferenceIsMore = 1<<2,
        PrefferenceIsLess = 1<<3,
        PrefferenceIsNull = 1<<4,
        WatcherCountryOnly = 1<<5,

    }
}
