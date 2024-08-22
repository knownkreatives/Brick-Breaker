namespace Assets.Scripts.Config{
    using UnityEngine;

    public static class ServiceSettings {
        [Header("Realtime Database")]
        public const string projectId = "startvesting-7ac44"; // You can find this in your Firebase project settings
        public const string location = "europe-west1";
        public static readonly string databaseURL = $"https://{projectId}-default-rtdb.{location}.firebasedatabase.app/";

        [Header("Authentication")]
        public static string webApiKey = "AIzaSyBxdf-ZsFWxWhB4VhfcSo2eDQ-yFLt0bWs"; // You can find this in your Firebase project settings
        
        /// WV75YH26pXqaLusdEA4QbBfPvSJxtrmcU3FngRyhzjNTwDMk9KuC9bNcLr6M5kRahveATVQqUtD3GZPsEY8zJHXpx2KBfdnw4yjWTb5zXSx3aE46PBumwY7UftjHZkGWRvcLJV2Q8DAqdgnMFsKhNpjePbSfEkLKN6m4sRDdgtnyAZ27FT9JMu5Vw3YxzBvqQCHh8rcXcP82DTHqVme46Jf9jzFxBZMtRAEXknhGbS3r7gdUQNsyYwaLu5
    }
}
