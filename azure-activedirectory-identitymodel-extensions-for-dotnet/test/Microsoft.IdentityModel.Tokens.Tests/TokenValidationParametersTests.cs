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
using System.Reflection;
using System.Security.Claims;
using Xunit;

namespace Microsoft.IdentityModel.Tokens.Tests
{
    public class TokenValidationParametersTests
    {
        [Fact]
        public void Publics()
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            Type type = typeof(TokenValidationParameters);
            PropertyInfo[] properties = type.GetProperties();
            if (properties.Length != 30)
                Assert.True(false, "Number of properties has changed from 30 to: " + properties.Length + ", adjust tests");

            TokenValidationParameters actorValidationParameters = new TokenValidationParameters();
            SecurityKey issuerSigningKey = KeyingMaterial.DefaultX509Key_Public_2048;
            SecurityKey issuerSigningKey2 = KeyingMaterial.RsaSecurityKey_2048;

            List<SecurityKey> issuerSigningKeys =
                new List<SecurityKey>
                {
                    KeyingMaterial.DefaultX509Key_Public_2048,
                    KeyingMaterial.RsaSecurityKey_2048
                };

            List<SecurityKey> issuerSigningKeysDup =
                new List<SecurityKey>
                {
                    KeyingMaterial.DefaultX509Key_Public_2048,
                    KeyingMaterial.RsaSecurityKey_2048
                };

            string validAudience = "ValidAudience";
            List<string> validAudiences = new List<string> { validAudience };
            string validIssuer = "ValidIssuer";
            List<string> validIssuers = new List<string> { validIssuer };


            TokenValidationParameters validationParametersInline = new TokenValidationParameters()
            {
                ActorValidationParameters = actorValidationParameters,
                AudienceValidator = IdentityUtilities.AudienceValidatorReturnsTrue,
                IssuerSigningKey = issuerSigningKey,
                IssuerSigningKeyResolver = (token, securityToken, keyIdentifier, tvp) => { return new List<SecurityKey> { issuerSigningKey }; },
                IssuerSigningKeys = issuerSigningKeys,
                IssuerValidator = IdentityUtilities.IssuerValidatorEcho,
                LifetimeValidator = IdentityUtilities.LifetimeValidatorReturnsTrue,
                SignatureValidator = IdentityUtilities.SignatureValidatorReturnsTokenAsIs,
                SaveSigninToken = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidAudience = validAudience,
                ValidAudiences = validAudiences,
                ValidIssuer = validIssuer,
                ValidIssuers = validIssuers,
            };

            Assert.True(object.ReferenceEquals(actorValidationParameters, validationParametersInline.ActorValidationParameters));
            Assert.True(object.ReferenceEquals(validationParametersInline.IssuerSigningKey, issuerSigningKey));
            Assert.True(validationParametersInline.SaveSigninToken);
            Assert.False(validationParametersInline.ValidateAudience);
            Assert.False(validationParametersInline.ValidateIssuer);
            Assert.True(object.ReferenceEquals(validationParametersInline.ValidAudience, validAudience));
            Assert.True(object.ReferenceEquals(validationParametersInline.ValidAudiences, validAudiences));
            Assert.True(object.ReferenceEquals(validationParametersInline.ValidIssuer, validIssuer));

            TokenValidationParameters validationParametersSets = new TokenValidationParameters();
            validationParametersSets.ActorValidationParameters = actorValidationParameters;
            validationParametersSets.AudienceValidator = IdentityUtilities.AudienceValidatorReturnsTrue;
            validationParametersSets.IssuerSigningKey = KeyingMaterial.DefaultX509Key_Public_2048;
            validationParametersSets.IssuerSigningKeyResolver = (token, securityToken, keyIdentifier, tvp) => { return new List<SecurityKey> { issuerSigningKey2 }; };
            validationParametersSets.IssuerSigningKeys = issuerSigningKeysDup;
            validationParametersSets.IssuerValidator = IdentityUtilities.IssuerValidatorEcho;
            validationParametersSets.LifetimeValidator = IdentityUtilities.LifetimeValidatorReturnsTrue;
            validationParametersSets.SignatureValidator = IdentityUtilities.SignatureValidatorReturnsTokenAsIs;
            validationParametersSets.SaveSigninToken = true;
            validationParametersSets.ValidateAudience = false;
            validationParametersSets.ValidateIssuer = false;
            validationParametersSets.ValidAudience = validAudience;
            validationParametersSets.ValidAudiences = validAudiences;
            validationParametersSets.ValidIssuer = validIssuer;
            validationParametersSets.ValidIssuers = validIssuers;

            var compareContext = new CompareContext();
            IdentityComparer.AreEqual(validationParametersInline, validationParametersSets, compareContext);

            TokenValidationParameters tokenValidationParametersCloned = validationParametersInline.Clone() as TokenValidationParameters;
            IdentityComparer.AreEqual(tokenValidationParametersCloned, validationParametersInline, compareContext);
            //tokenValidationParametersCloned.AudienceValidator(new string[]{"bob"}, JwtTestTokens.Simple();

            string id = Guid.NewGuid().ToString();
            DerivedTokenValidationParameters derivedValidationParameters = new DerivedTokenValidationParameters(id, validationParametersInline);
            DerivedTokenValidationParameters derivedValidationParametersCloned = derivedValidationParameters.Clone() as DerivedTokenValidationParameters;
            IdentityComparer.AreEqual(derivedValidationParameters, derivedValidationParametersCloned, compareContext);
            IdentityComparer.AreEqual(derivedValidationParameters.InternalString, derivedValidationParametersCloned.InternalString, compareContext);

            TestUtilities.AssertFailIfErrors("TokenValidationParameters", compareContext.Diffs);
        }

        [Fact( DisplayName = "TokenValidationParametersTests: GetSets, covers defaults")]
        public void GetSets()
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            Type type = typeof(TokenValidationParameters);
            PropertyInfo[] properties = type.GetProperties();
            if (properties.Length != 30)
                Assert.True(false, "Number of public fields has changed from 30 to: " + properties.Length + ", adjust tests");

            GetSetContext context =
                new GetSetContext
                {
                    PropertyNamesAndSetGetValue = new List<KeyValuePair<string, List<object>>>
                    {
                        new KeyValuePair<string, List<object>>("ActorValidationParameters", new List<object>{(TokenValidationParameters)null, new TokenValidationParameters(), new TokenValidationParameters()}),
                        new KeyValuePair<string, List<object>>("AuthenticationType", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                        //new KeyValuePair<string, List<object>>("CertificateValidator", new List<object>{(string)null, X509CertificateValidator.None, X509CertificateValidatorEx.None}),
                        new KeyValuePair<string, List<object>>("ClockSkew", new List<object>{TokenValidationParameters.DefaultClockSkew, TimeSpan.FromHours(2), TimeSpan.FromMinutes(1)}),
                        new KeyValuePair<string, List<object>>("IssuerSigningKey", new List<object>{(SecurityKey)null, KeyingMaterial.DefaultX509Key_2048, KeyingMaterial.RsaSecurityKey_2048}),
                        new KeyValuePair<string, List<object>>("IssuerSigningKeys", new List<object>{(IEnumerable<SecurityKey>)null, new List<SecurityKey>{KeyingMaterial.DefaultX509Key_2048, KeyingMaterial.RsaSecurityKey_1024}, new List<SecurityKey>()}),
                        new KeyValuePair<string, List<object>>("NameClaimType", new List<object>{ClaimsIdentity.DefaultNameClaimType, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                        new KeyValuePair<string, List<object>>("RoleClaimType", new List<object>{ClaimsIdentity.DefaultRoleClaimType, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                        new KeyValuePair<string, List<object>>("RequireExpirationTime", new List<object>{true, false, true}),
                        new KeyValuePair<string, List<object>>("RequireSignedTokens", new List<object>{true, false, true}),
                        new KeyValuePair<string, List<object>>("SaveSigninToken", new List<object>{false, true, false}),
                        new KeyValuePair<string, List<object>>("ValidateActor", new List<object>{false, true, false}),
                        new KeyValuePair<string, List<object>>("ValidateAudience", new List<object>{true, false, true}),
                        new KeyValuePair<string, List<object>>("ValidateIssuer", new List<object>{true, false, true}),
                        new KeyValuePair<string, List<object>>("ValidateLifetime", new List<object>{true, false, true}),
                        new KeyValuePair<string, List<object>>("ValidIssuer", new List<object>{(string)null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()}),
                    },
                    Object = validationParameters,
                };
            TestUtilities.GetSet(context);
            TestUtilities.AssertFailIfErrors("TokenValidationParametersTests: GetSets", context.Errors);
            Assert.Null(validationParameters.AudienceValidator);
            Assert.Null(validationParameters.LifetimeValidator);
            Assert.Null(validationParameters.IssuerSigningKeyResolver);
            Assert.Null(validationParameters.IssuerValidator);
            Assert.Null(validationParameters.ValidAudiences);
            Assert.Null(validationParameters.ValidIssuers);
            Assert.Null(validationParameters.SignatureValidator);
        }

        class DerivedTokenValidationParameters : TokenValidationParameters
        {
            string _internalString;
            public DerivedTokenValidationParameters(string internalString, TokenValidationParameters validationParameters)
                : base(validationParameters)
            {
                _internalString = internalString;
            }

            protected DerivedTokenValidationParameters(DerivedTokenValidationParameters other)
                : base(other)
            {
                _internalString = other._internalString;
            }

            public string InternalString { get { return _internalString; } }

            public override TokenValidationParameters Clone()
            {
                return new DerivedTokenValidationParameters(this);
            }
        }
    }
}
