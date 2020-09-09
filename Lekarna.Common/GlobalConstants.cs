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

            public const string ApiSecret = "Cloudinary:ApiSecret";

            public const string AppKey = "Cloudinary:AppKey";

        }

        public static class Images
        {
            public const string LogoPath = "/images/logo.png";
        }

        public static class Notifications
        {
            public const string Key = "Notification";

            public const string Error = "Error";

            public const string SuplierAlreadyExists = "Supplier with that name alredy exists!";

            public const string SuccessfullyCreatedPharmacy = "Pharmacy was successfully created!";

            public const string SuccessfullyEditedPharmacy = "Pharmacy was successfully edited!";

            public const string SuccessfullyDeletedPharmacy = "Pharmacy was successfully deleted!";

            public const string SuccessfullyCreatedCategory = "Category was successfully created!";

            public const string SuccessfullyDeletedCategory = "Category was successfully deleted!";

            public const string SuccessfullyEditedCategory = "Category was successfully edited!";

            public const string SuccessfullyCreatedSupplier = "Supplier was successfully created!";

            public const string SuccessfullyEditedSupplier = "Supplier was successfully edited!";

            public const string SuccessfullyDeletedSupplier = "Supplier was successfully deleted!";

            public const string SuccessfullyEditedOffer = "Offer was successfully edited!";

            public const string SuccessfullyCreatedOffer = "Offer was successfully created!";

            public const string SuccessfullyDeletedOffer = "Offer was successfully deleted!";
        }

        public static class Offer
        {
            public const string Formula = "FORMULA";

            public const string Total = "Total";
        }
    }
}
