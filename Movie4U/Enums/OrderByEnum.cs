namespace Movie4U.Enums
{
    public enum OrderByEnum
    {
        None = 0,
        // Use flag system (1<<choiceNo) for the next values
        Score = 1<<0,
    }
}
