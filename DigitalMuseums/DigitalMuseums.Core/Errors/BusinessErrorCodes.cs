namespace DigitalMuseums.Core.Errors
{
    /// <summary>
    /// The business logic error codes.
    /// </summary>
    public static class BusinessErrorCodes
    {
        public const string InvalidCredentialsCode = "account.login.errors.invalid-credentials";
        
        public const string MuseumNotFoundCode = "museum.errors.not-found";
        
        public const string UserNotFoundCode = "user.errors.not-found";
    }
}