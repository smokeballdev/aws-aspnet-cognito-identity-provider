namespace Amazon.AspNetCore.Identity.Cognito.Extensions
{
    public class AWSCognitoTokenOptions
    {
        /// <summary>
        /// If true, will store the refresh token and use it to refresh the
        /// access token when it expires.
        /// </summary>
        public bool AllowTokenRefresh { get; set; } = true;
    }
}
