//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Tests;
using Newtonsoft.Json;
using Xunit;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenIdConnectConfigurationTests
    {
        [Fact]
        public void Constructors()
        {
            var context = new CompareContext { Title = "OpenIdConnectConfigurationTests.Constructors" };
            RunOpenIdConnectConfigurationTest((string)null, new OpenIdConnectConfiguration(), ExpectedException.ArgumentNullException(), context);
            RunOpenIdConnectConfigurationTest(OpenIdConfigData.JsonAllValues, OpenIdConfigData.FullyPopulated, ExpectedException.NoExceptionExpected, context);
            RunOpenIdConnectConfigurationTest(OpenIdConfigData.OpenIdConnectMetatadataBadJson, null, ExpectedException.ArgumentException(substringExpected: "IDX10815:", inner: typeof(JsonReaderException)), context);
            TestUtilities.AssertFailIfErrors(context);
        }

        private void RunOpenIdConnectConfigurationTest(object obj, OpenIdConnectConfiguration compareTo, ExpectedException expectedException, CompareContext context, bool asString = true)
        {
            bool exceptionHit = false;

            OpenIdConnectConfiguration openIdConnectConfiguration = null;
            try
            {
                if (obj is string || asString)
                {
                    openIdConnectConfiguration = new OpenIdConnectConfiguration(obj as string);
                }

                expectedException.ProcessNoException(context.Diffs);
            }
            catch (Exception ex)
            {
                exceptionHit = true;
                expectedException.ProcessException(ex, context.Diffs);
            }

            if (!exceptionHit && compareTo != null)
            {
                IdentityComparer.AreEqual(openIdConnectConfiguration, compareTo, context);
            }
        }

        [Fact]
        public void Defaults()
        {
            OpenIdConnectConfiguration configuration = new OpenIdConnectConfiguration();
            Assert.NotNull(configuration.AcrValuesSupported);
            Assert.NotNull(configuration.ClaimsSupported);
            Assert.NotNull(configuration.ClaimsLocalesSupported);
            Assert.NotNull(configuration.ClaimTypesSupported);
            Assert.NotNull(configuration.DisplayValuesSupported);
            Assert.NotNull(configuration.GrantTypesSupported);
            Assert.NotNull(configuration.IdTokenEncryptionAlgValuesSupported);
            Assert.NotNull(configuration.IdTokenEncryptionEncValuesSupported);
            Assert.NotNull(configuration.IdTokenSigningAlgValuesSupported);
            Assert.NotNull(configuration.RequestObjectEncryptionAlgValuesSupported);
            Assert.NotNull(configuration.RequestObjectEncryptionEncValuesSupported);
            Assert.NotNull(configuration.RequestObjectSigningAlgValuesSupported);
            Assert.NotNull(configuration.ResponseModesSupported);
            Assert.NotNull(configuration.ResponseTypesSupported);
            Assert.NotNull(configuration.ScopesSupported);
            Assert.NotNull(configuration.SigningKeys);
            Assert.NotNull(configuration.SubjectTypesSupported);
            Assert.NotNull(configuration.TokenEndpointAuthMethodsSupported);
            Assert.NotNull(configuration.TokenEndpointAuthSigningAlgValuesSupported);
            Assert.NotNull(configuration.UILocalesSupported);
            Assert.NotNull(configuration.UserInfoEndpointEncryptionAlgValuesSupported);
            Assert.NotNull(configuration.UserInfoEndpointEncryptionEncValuesSupported);
            Assert.NotNull(configuration.UserInfoEndpointSigningAlgValuesSupported);
        }

        [Fact]
        public void GetSets()
        {
            OpenIdConnectConfiguration configuration = new OpenIdConnectConfiguration();
            Type type = typeof(OpenIdConnectConfiguration);
            PropertyInfo[] properties = type.GetProperties();
            if (properties.Length != 42)
                Assert.True(false, "Number of properties has changed from 42 to: " + properties.Length + ", adjust tests");

            TestUtilities.CallAllPublicInstanceAndStaticPropertyGets(configuration, "OpenIdConnectConfiguration_GetSets");

            GetSetContext context =
                new GetSetContext
                {
                    PropertyNamesAndSetGetValue = new List<KeyValuePair<string, List<object>>>
                        {
                            new KeyValuePair<string, List<object>>("AuthorizationEndpoint", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("CheckSessionIframe", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("ClaimsParameterSupported", new List<object>{false, true, false}),
                            new KeyValuePair<string, List<object>>("EndSessionEndpoint", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("HttpLogoutSupported", new List<object>{false, true, true}),
                            new KeyValuePair<string, List<object>>("Issuer",  new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("JwksUri",  new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("JsonWebKeySet",  new List<object>{null, new JsonWebKeySet()}),
                            new KeyValuePair<string, List<object>>("LogoutSessionSupported", new List<object>{false, true, true}),
                            new KeyValuePair<string, List<object>>("OpPolicyUri", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("OpTosUri", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("RegistrationEndpoint", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("RequireRequestUriRegistration", new List<object>{false, true, true}),
                            new KeyValuePair<string, List<object>>("RequestParameterSupported", new List<object>{false, true, false}),
                            new KeyValuePair<string, List<object>>("RequestUriParameterSupported", new List<object>{false, true, true}),
                            new KeyValuePair<string, List<object>>("ServiceDocumentation", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("TokenEndpoint", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                            new KeyValuePair<string, List<object>>("UserInfoEndpoint", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                        },

                    Object = configuration,
                };

            TestUtilities.GetSet(context);
            TestUtilities.AssertFailIfErrors("OpenIdConnectConfiguration_GetSets", context.Errors);

            string authorization_Endpoint = Guid.NewGuid().ToString();
            string end_Session_Endpoint = Guid.NewGuid().ToString();
            string issuer = Guid.NewGuid().ToString();
            string jwks_Uri = Guid.NewGuid().ToString();
            string token_Endpoint = Guid.NewGuid().ToString();

            configuration = new OpenIdConnectConfiguration()
            {
                AuthorizationEndpoint = authorization_Endpoint,
                EndSessionEndpoint = end_Session_Endpoint,
                Issuer = issuer,
                JwksUri = jwks_Uri,
                TokenEndpoint = token_Endpoint,
            };

            List<SecurityKey> securityKeys = new List<SecurityKey> { new X509SecurityKey(KeyingMaterial.Cert_1024), new X509SecurityKey(KeyingMaterial.DefaultCert_2048) };
            configuration.SigningKeys.Add(new X509SecurityKey(KeyingMaterial.Cert_1024));
            configuration.SigningKeys.Add(new X509SecurityKey(KeyingMaterial.DefaultCert_2048));

            List<string> errors = new List<string>();

            if (!string.Equals(configuration.AuthorizationEndpoint, authorization_Endpoint))
                errors.Add(string.Format(CultureInfo.InvariantCulture, "configuration.AuthorizationEndpoint != authorization_Endpoint. '{0}', '{1}'.", configuration.AuthorizationEndpoint, authorization_Endpoint));

            if (!string.Equals(configuration.EndSessionEndpoint, end_Session_Endpoint))
                errors.Add(string.Format(CultureInfo.InvariantCulture, "configuration.EndSessionEndpoint != end_Session_Endpoint. '{0}', '{1}'.", configuration.EndSessionEndpoint, end_Session_Endpoint));

            if (!string.Equals(configuration.Issuer, issuer))
                errors.Add(string.Format(CultureInfo.InvariantCulture, "configuration.Issuer != issuer. '{0}', '{1}'.", configuration.Issuer, issuer));

            if (!string.Equals(configuration.JwksUri, jwks_Uri))
                errors.Add(string.Format(CultureInfo.InvariantCulture, "configuration.JwksUri != jwks_Uri. '{0}', '{1}'.", configuration.JwksUri, jwks_Uri));

            if (!string.Equals(configuration.TokenEndpoint, token_Endpoint))
                errors.Add(string.Format(CultureInfo.InvariantCulture, "configuration.TokenEndpoint != token_Endpoint. '{0}', '{1}'.", configuration.TokenEndpoint, token_Endpoint));

            CompareContext compareContext = new CompareContext();
            if (!IdentityComparer.AreEqual(configuration.SigningKeys, securityKeys, compareContext))
                errors.AddRange(compareContext.Diffs);

            TestUtilities.AssertFailIfErrors("OpenIdConnectConfiguration_GetSets", errors);
        }

        [Fact]
        public void RoundTripFromJson()
        {
            var context = new CompareContext { Title = "RoundTripFromJson" };
            var oidcConfig1 = OpenIdConnectConfiguration.Create(OpenIdConfigData.JsonAllValues);
            var oidcConfig2 = new OpenIdConnectConfiguration(OpenIdConfigData.JsonAllValues);
            var oidcJson1 = OpenIdConnectConfiguration.Write(oidcConfig1);
            var oidcJson2 = OpenIdConnectConfiguration.Write(oidcConfig2);
            var oidcConfig3 = OpenIdConnectConfiguration.Create(oidcJson1);
            var oidcConfig4 = new OpenIdConnectConfiguration(oidcJson2);

            IdentityComparer.AreEqual(oidcConfig1, oidcConfig2, context);
            IdentityComparer.AreEqual(oidcConfig1, oidcConfig3, context);
            IdentityComparer.AreEqual(oidcConfig1, oidcConfig4, context);
            IdentityComparer.AreEqual(oidcJson1, oidcJson2, context);

            TestUtilities.AssertFailIfErrors(context);
        }
    }
}
