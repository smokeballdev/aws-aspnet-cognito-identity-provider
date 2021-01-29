using System;
using System.Reflection;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;

namespace Amazon.AspNetCore.Identity.Cognito.Extensions
{
    public static class CognitoUserExtensions
    {
        private static readonly MethodInfo _secretPropertyGetter;
        private static readonly MethodInfo _updateSessionMethod;
        private static readonly MethodInfo _createForgotPasswordRequestMethod;

        static CognitoUserExtensions()
        {
            var userType = typeof(CognitoUser);

            var secretProperty = userType.GetProperty("SecretHash", BindingFlags.Instance | BindingFlags.NonPublic);
            _secretPropertyGetter = secretProperty?.GetGetMethod(nonPublic: true);
            _updateSessionMethod = userType.GetMethod("UpdateSessionIfAuthenticationComplete", BindingFlags.NonPublic | BindingFlags.Instance);
            _createForgotPasswordRequestMethod = userType.GetMethod("CreateForgotPasswordRequest", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        internal static string GetSecretHash(this CognitoUser user)
        {
            if (_secretPropertyGetter == null)
            {
                return null;
            }

            return _secretPropertyGetter.Invoke(user, null) as string;
        }

        internal static void UpdateSessionIfAuthenticationComplete(this CognitoUser user, ChallengeNameType challengeName,
            AuthenticationResultType authResult)
        {
            _updateSessionMethod.Invoke(user, new object[] { challengeName, authResult });
        }

        internal static ForgotPasswordRequest CreateForgotPasswordRequest(this CognitoUser user)
        {
            return _createForgotPasswordRequestMethod.Invoke(user, Array.Empty<object>()) as ForgotPasswordRequest;
        }
    }
}
