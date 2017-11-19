namespace OcenUczelnie.Core.Domain.Exceptions
{
    public static class ErrorCodes
    {
        public static string InvalidReview => "invalid_review";
        public static string InvalidAction => "invalid_action";
        public static string EmailOccupied => "email_occupied";
        public static string NameOccupied => "name_occupied";
        public static string InvalidUser => "invalid_user";
        public static string InvalidCredentials => "invalid_credentials";
        public static string NotActivated => "not_activated";
        public static string TokenExpired => "token_expired";
        public static string InvalidToken => "invalid_token";
        public static string InvalidPassword => "invalid_password";
    }
}