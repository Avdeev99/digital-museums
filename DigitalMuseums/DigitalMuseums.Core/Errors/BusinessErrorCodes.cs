namespace DigitalMuseums.Core.Errors
{
    /// <summary>
    /// The business logic error codes.
    /// </summary>
    public static class BusinessErrorCodes
    {
        public const string InvalidCredentialsCode = "account.login.errors.invalid-credentials";
        
        public const string MuseumNotFoundCode = "museum.errors.not-found";
        
        public const string ExhibitNotFoundCode = "exhibit.errors.not-found";
        
        public const string SouvenirNotFoundCode = "souvenir.errors.not-found";
        
        public const string ExceededAvailableSouvenirCountCode = "souvenir.errors.limit-exceeded";
        
        public const string OrderNotFoundCode = "order.errors.not-found";
        
        public const string UserNotFoundCode = "user.errors.not-found";
    }
}