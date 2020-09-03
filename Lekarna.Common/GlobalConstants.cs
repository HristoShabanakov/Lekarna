namespace Lekarna.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "Lekarna";

        public const string AdministratorRoleName = "Administrator";

        public const string Administrator = "Admin";

        public const string AdminPassword = "admin123";

        public const string AdminEmail = "admin@lekarna.com";

        public static class Cloudinary
        {
            public const string Prefix = "https://res.cloudinary.com/{0}/image/upload/";

            public const string CloudName = "Cloudinary:CloudName";
        }

        public static class Images
        {
            public const string LogoPath = "/images/logo.png";
        }

        public static class Notifications
        {
            public const string Key = "Notification";

            public const string SuccessfullyCreatedPharmacy = "Pharmacy was successfully created!";

            public const string SuccessfullyEditedPharmacy = "Pharmacy was successfully edited!";

            public const string SuccessfullyDeletedPharmacy = "Pharmacy was successfully deleted!";
        }
    }
}
