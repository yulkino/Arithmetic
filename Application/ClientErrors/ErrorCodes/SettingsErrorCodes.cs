﻿namespace Application.ClientErrors.ErrorCodes;

public static class SettingsErrorCodes
{
    public const string NotFound = "Settings.NotFound";
    public const string Conflict = "Settings.Conflict";

    public static class OperationsErrorCodes
    {
        public const string NotFound = "Operations.NotFound";
    }

    public static class DifficultyErrorCodes
    {
        public const string NotFound = "Difficulty.NotFound";
    }
}