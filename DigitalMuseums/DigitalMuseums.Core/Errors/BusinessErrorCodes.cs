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

        public const string UserWithoutMuseumCode = "user.errors.no-museum";

        public const string GenreNotFoundCode = "genre.errors.not-found";

        public const string GenreUsedByMuseumCode = "genre.errors.used-by-museum";

        public const string MuseumLinkedToUserCode = "museum.errors.linked-to-user";

        public const string ExhibitRelatedToExhibitionCode = "exhibit.errors.related-to-exhibition";

        public const string GenreWithSameNameExistCode = "genre.errors.same-name-exist";

        public const string ExhibitWithSameNameExistCode = "exhibit.errors.same-name-exist";

        public const string ExhibitionWithSameNameExistCode = "exhibition.errors.same-name-exist";

        public const string SouvenirWithSameNameExistCode = "souvenir.errors.same-name-exist";
    }
}