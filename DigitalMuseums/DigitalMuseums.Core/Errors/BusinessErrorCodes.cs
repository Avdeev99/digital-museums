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
        
        public const string ExhibitionNotFoundCode = "exhibition.errors.not-found";
        
        public const string UserNotFoundCode = "user.errors.not-found";

        public const string RoleNotFoundCode = "role.errors.not-found";

        public const string SouvenirAlreadyAddedToCartCode = "cart.errors.souvenir-already-added";

        public const string InvalidPassword = "account.errors.invalid-password";

        public const string UserWithoutMuseum = "user.errors.no-museum";
    }
}