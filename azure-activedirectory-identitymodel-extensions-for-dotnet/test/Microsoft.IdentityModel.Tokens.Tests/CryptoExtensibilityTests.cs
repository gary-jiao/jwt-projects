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
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Xunit;

namespace Microsoft.IdentityModel.Tokens.Tests
{
    /// <summary>
    /// Crypto extensibility scenarios
    /// </summary>
    public class CryptoExtensibilityTests
    {
        /// <summary>
        /// SecurityTokenDescriptor.CryptoProviderFactory has priority over SecurityKey.CryptoProviderFactory
        /// </summary>
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory, MemberData("SecurityTokenDescriptorDataSet")]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void CryptoProviderOrderingWhenSigning(SecurityTokenDescriptor tokenDescriptor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            if (tokenDescriptor.SigningCredentials.CryptoProviderFactory == null)
            {
                Assert.True((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).CreateForSigningCalled);
                Assert.True((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).ReleaseSignatureProviderCalled);
                Assert.True(((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).SignCalled);
                Assert.True(((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).DisposeCalled);
            }
            else
            {
                Assert.True((tokenDescriptor.SigningCredentials.CryptoProviderFactory as CustomCryptoProviderFactory).CreateForSigningCalled);
                Assert.True((tokenDescriptor.SigningCredentials.CryptoProviderFactory as CustomCryptoProviderFactory).ReleaseSignatureProviderCalled);
                Assert.True(((tokenDescriptor.SigningCredentials.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).SignCalled);
                Assert.True(((tokenDescriptor.SigningCredentials.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).DisposeCalled);
                Assert.False((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).CreateForSigningCalled);
                Assert.False((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).ReleaseSignatureProviderCalled);
                Assert.False(((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).SignCalled);
                Assert.False(((tokenDescriptor.SigningCredentials.Key.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).DisposeCalled);
            }
        }

        public static TheoryData<SecurityTokenDescriptor> SecurityTokenDescriptorDataSet
        {
            get
            {
                var dataset = new TheoryData<SecurityTokenDescriptor>();

                var key = new SymmetricSecurityKey(new byte[256]);
                key.CryptoProviderFactory = new CustomCryptoProviderFactory()
                {
                    SignatureProvider = new CustomSignatureProvider(key, "alg")
                };

                var tokenDescriptor = IdentityUtilities.DefaultSecurityTokenDescriptor(new SigningCredentials(key, "alg"));

                dataset.Add(tokenDescriptor);

                key = new SymmetricSecurityKey(new byte[256]);
                key.CryptoProviderFactory = new CustomCryptoProviderFactory()
                {
                    SignatureProvider = new CustomSignatureProvider(key, "alg")
                };

                tokenDescriptor = IdentityUtilities.DefaultSecurityTokenDescriptor(new SigningCredentials(key, "alg"));
                tokenDescriptor.SigningCredentials.CryptoProviderFactory = new CustomCryptoProviderFactory()
                {
                    SignatureProvider = new CustomSignatureProvider(key, "alg")
                };

                dataset.Add(tokenDescriptor);
                return dataset;
            }
        }

        /// <summary>
        /// TokenValidationParameters.CryptoProviderFactory has priority over SecurityKey.CryptoProviderFactory
        /// </summary>
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory, MemberData("SigningCredentialsDataSet")]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void CryptoProviderOrderingWhenVerifying(TokenValidationParameters validationParameters, string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = null;
            tokenHandler.ValidateToken(jwt, validationParameters, out token);

            if (validationParameters.CryptoProviderFactory == null)
            {
                Assert.True((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).CreateForVerifyingCalled, "IssuerSigningKey.CustomCryptoProviderFactory.CreateForVerifyingCalled was NOT called");
                Assert.True((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).ReleaseSignatureProviderCalled, "IssuerSigningKey.CustomCryptoProviderFactory.ReleaseSignatureProviderCalled was NOT called");
                Assert.True(((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).VerifyCalled, "IssuerSigningKey.CustomCryptoProviderFactory.VerifyCalled was NOT called");
                Assert.True(((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).DisposeCalled, "IssuerSigningKey.CustomCryptoProviderFactory.DisposeCalled was NOT called");
            }
            else
            {
                Assert.True((validationParameters.CryptoProviderFactory as CustomCryptoProviderFactory).CreateForVerifyingCalled, "validationParameters.CustomCryptoProviderFactory.CreateForVerifyingCalled was NOT called");
                Assert.True((validationParameters.CryptoProviderFactory as CustomCryptoProviderFactory).ReleaseSignatureProviderCalled, "validationParameters.CustomCryptoProviderFactory.ReleaseSignatureProviderCalled was NOT called");
                Assert.True(((validationParameters.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).VerifyCalled, "validationParameters.CustomSignatureProvider.VerifyCalled was NOT called");
                Assert.True(((validationParameters.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).DisposeCalled, "validationParameters.CustomSignatureProvider.DisposeCalled was NOT called");
                Assert.False((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).CreateForVerifyingCalled, "IssuerSigningKey.CustomCryptoProviderFactory.CreateForVerifyingCalled WAS called");
                Assert.False((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).ReleaseSignatureProviderCalled, "IssuerSigningKey.CustomCryptoProviderFactory.ReleaseSignatureProviderCalled was WAS called");
                Assert.False(((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).VerifyCalled, "IssuerSigningKey.CustomSignatureProvider.VerifyCalled was WAS called");
                Assert.False(((validationParameters.IssuerSigningKey.CryptoProviderFactory as CustomCryptoProviderFactory).SignatureProvider as CustomSignatureProvider).DisposeCalled, "IssuerSigningKey.CustomSignatureProvider.DisposeCalled was WAS called");
            }
        }

        public static TheoryData<TokenValidationParameters, string> SigningCredentialsDataSet
        {
            get
            {
                var dataset = new TheoryData<TokenValidationParameters, string>();
                var jwt = IdentityUtilities.DefaultAsymmetricJwt;

                var validationParameters = IdentityUtilities.DefaultAsymmetricTokenValidationParameters;
                validationParameters.IssuerSigningKey.CryptoProviderFactory = new CustomCryptoProviderFactory()
                {
                    SignatureProvider = new CustomSignatureProvider(validationParameters.IssuerSigningKey, "alg")
                };

                dataset.Add(validationParameters, jwt);

                validationParameters = IdentityUtilities.DefaultAsymmetricTokenValidationParameters;
                validationParameters.IssuerSigningKey.CryptoProviderFactory = new CustomCryptoProviderFactory()
                {
                    SignatureProvider = new CustomSignatureProvider(validationParameters.IssuerSigningKey, "alg")
                };

                validationParameters.CryptoProviderFactory = new CustomCryptoProviderFactory()
                {
                    SignatureProvider = new CustomSignatureProvider(validationParameters.IssuerSigningKey, "alg")
                };

                dataset.Add(validationParameters, jwt);

                return dataset;
            }
        }

        /// <summary>
        /// Tests that Default behaviors
        /// </summary>
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory, MemberData("DefaultCryptoProviderDataSet")]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void DefaultCryptoProviderFactory(SecurityKey key, string algorithm, bool isSupported, bool supportsSigning)
        {
            Assert.True(CryptoProviderFactory.Default.IsSupportedAlgorithm(algorithm, key) == isSupported, string.Format(CultureInfo.InvariantCulture, "SecurityKey: '{0}', algorithm: '{1}', isSupported: '{2}'", key, algorithm, isSupported));
            if (supportsSigning)
            {
                var signatureProvider = CryptoProviderFactory.Default.CreateForSigning(key, algorithm);
                var signatureProviderVerify = CryptoProviderFactory.Default.CreateForVerifying(key, algorithm);
                var bytes = Encoding.UTF8.GetBytes("GenerateASignature");
                var signature = signatureProvider.Sign(bytes);
                var signatureCheck = signatureProviderVerify.Verify(bytes, signature);
                Assert.True(signatureCheck);
                CryptoProviderFactory.Default.ReleaseSignatureProvider(signatureProvider);
                CryptoProviderFactory.Default.ReleaseSignatureProvider(signatureProviderVerify);
            }
        }

        public static TheoryData<SecurityKey, string, bool, bool> DefaultCryptoProviderDataSet
        {
            get
            {
                return new TheoryData<SecurityKey, string, bool, bool>
                {
                    {KeyingMaterial.ECDsa256Key, SecurityAlgorithms.EcdsaSha256, true, true},
                    {KeyingMaterial.ECDsa256Key, SecurityAlgorithms.EcdsaSha384, true, true},
                    {KeyingMaterial.ECDsa256Key, SecurityAlgorithms.EcdsaSha512, true, true},
                    {KeyingMaterial.ECDsa256Key, SecurityAlgorithms.EcdsaSha256Signature, true, true},
                    {KeyingMaterial.ECDsa256Key, SecurityAlgorithms.EcdsaSha384Signature, true, true},
                    {KeyingMaterial.ECDsa256Key, SecurityAlgorithms.EcdsaSha512Signature, true, true},
                    {KeyingMaterial.ECDsa256Key, SecurityAlgorithms.Aes128Encryption, false, false},

                    {KeyingMaterial.JsonWebKeyEcdsa256, SecurityAlgorithms.EcdsaSha256, true, true},
                    {KeyingMaterial.JsonWebKeyEcdsa256Public, SecurityAlgorithms.EcdsaSha256, true, false},
                    {KeyingMaterial.JsonWebKeyEcdsa256, SecurityAlgorithms.EcdsaSha256Signature, true, true},
                    {KeyingMaterial.JsonWebKeyEcdsa256Public, SecurityAlgorithms.EcdsaSha256Signature, true, false},
                    {KeyingMaterial.JsonWebKeyEcdsa256, SecurityAlgorithms.Aes256KeyWrap, false, false},
                    {KeyingMaterial.JsonWebKeyEcdsa256Public, SecurityAlgorithms.DesEncryption, false, false},

                    {KeyingMaterial.JsonWebKeyRsa256, SecurityAlgorithms.RsaSha256, true, true},
                    {KeyingMaterial.JsonWebKeyRsa256, SecurityAlgorithms.RsaSha256Signature, true, true},
                    {KeyingMaterial.JsonWebKeyRsa256Public, SecurityAlgorithms.RsaSha256, true, false},
                    {KeyingMaterial.JsonWebKeyRsa256Public, SecurityAlgorithms.RsaSha256Signature, true, false},
                    {KeyingMaterial.JsonWebKeyRsa256, SecurityAlgorithms.Aes192KeyWrap, false, false},
                    {KeyingMaterial.JsonWebKeyRsa256Public, SecurityAlgorithms.Aes192KeyWrap, false, false},

                    {KeyingMaterial.JsonWebKeySymmetric256, SecurityAlgorithms.HmacSha256, true, true},
                    {KeyingMaterial.JsonWebKeySymmetric256, SecurityAlgorithms.HmacSha256Signature, true, true},
                    {KeyingMaterial.JsonWebKeySymmetric256, SecurityAlgorithms.EcdsaSha512Signature, false, false},
                    {KeyingMaterial.JsonWebKeySymmetric256, SecurityAlgorithms.RsaSha256Signature, false, false},

                    {KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha256, true, true},
                    {KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha256Signature, true, true},
                    {KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha384, true, true},
                    {KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha384Signature, true, true},
                    {KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha512, true, true},
                    {KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha512Signature, true, true},
                    {KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.Aes128Encryption, false, false},

                    {KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.RsaSha256, true, true},
                    {KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.RsaSha256Signature, true, true},
                    {KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.RsaSha384, true, true},
                    {KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.RsaSha384Signature, true, true},
                    {KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.RsaSha512, true, true},
                    {KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.RsaSha512Signature, true, true},
                    {KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.Aes128Encryption, false, false},

                    {KeyingMaterial.SymmetricSecurityKey2_256, SecurityAlgorithms.HmacSha256, true, true},
                    {KeyingMaterial.SymmetricSecurityKey2_256, SecurityAlgorithms.HmacSha256Signature, true, true},
                    {KeyingMaterial.SymmetricSecurityKey2_256, SecurityAlgorithms.RsaSha256Signature, false, false}
                };
            }
        }

#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory]
        [InlineData(SecurityAlgorithms.Sha256, true)]
        [InlineData(SecurityAlgorithms.Sha256Digest, true)]
        [InlineData(SecurityAlgorithms.Sha384, true)]
        [InlineData(SecurityAlgorithms.Sha384Digest, true)]
        [InlineData(SecurityAlgorithms.Sha512, true)]
        [InlineData(SecurityAlgorithms.Sha512Digest, true)]
        [InlineData(SecurityAlgorithms.Aes128Encryption, false)]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void DefaultCryptoProviderFactoryGetHashAlgorithm(string algorithm, bool isSupported)
        {
            var ee = isSupported ? ExpectedException.NoExceptionExpected : ExpectedException.InvalidOperationException("IDX10640:");
            try
            {
                CryptoProviderFactory.Default.CreateHashAlgorithm(algorithm);
                ee.ProcessNoException();
            }
            catch(Exception ex)
            {
                ee.ProcessException(ex);
            }
        }

        /// <summary>
        /// Tests that setting a <see cref="ICryptoProvider"/> does not colide with defaults.
        /// </summary>
        [Fact]
        public void CustomCryptoProvider()
        {
            var cryptoProviderFactoryDefault = CryptoProviderFactory.Default;
            var cryptoProviderFactoryWithCustomProvider = new CustomCryptoProviderFactory();
            var customCryptoProvider = new CustomCryptoProvider
            {
                HashAlgorithm = new CustomHashAlgorithm(),
                SignatureProvider = new CustomSignatureProvider(KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha256),
                IsSupportedResult = true,
            };

            cryptoProviderFactoryWithCustomProvider.CustomCryptoProvider = customCryptoProvider;
            var cryptoProviderFactoryDefault2 = CryptoProviderFactory.Default;

            Assert.Null(cryptoProviderFactoryDefault.CustomCryptoProvider);
            Assert.Null(cryptoProviderFactoryDefault2.CustomCryptoProvider);
            Assert.NotNull(cryptoProviderFactoryWithCustomProvider.CustomCryptoProvider);

            cryptoProviderFactoryDefault.CreateForSigning(KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha256);
            var customSignatureProvider = cryptoProviderFactoryWithCustomProvider.CreateForSigning(KeyingMaterial.RsaSecurityKey_2048, SecurityAlgorithms.RsaSha256) as CustomSignatureProvider;
            var customHashAlgorithm = cryptoProviderFactoryWithCustomProvider.CreateHashAlgorithm(SecurityAlgorithms.Sha256) as CustomHashAlgorithm;

            cryptoProviderFactoryWithCustomProvider.ReleaseSignatureProvider(customSignatureProvider);
            cryptoProviderFactoryWithCustomProvider.ReleaseHashAlgorithm(customHashAlgorithm);

            Assert.NotNull(customSignatureProvider);
            Assert.NotNull(customHashAlgorithm);
            Assert.True(cryptoProviderFactoryWithCustomProvider.ReleaseSignatureProviderCalled, "cryptoProviderFactoryWithCustomProvider.ReleaseSignatureProviderCalled");
            Assert.True(cryptoProviderFactoryWithCustomProvider.ReleaseAlgorithmCalled, "cryptoProviderFactoryWithCustomProvider.ReleaseAlgorithmCalled");
            Assert.True(customCryptoProvider.IsSupportedAlgorithmCalled, "customCryptoProvider.IsSupportedAlgorithmCalled");
            Assert.True(customCryptoProvider.ReleaseCalled, "customCryptoProvider.ReleaseCalled");
            Assert.True(customCryptoProvider.CreateCalled, "customCryptoProvider.CreateCalled");
            Assert.True(customSignatureProvider.DisposeCalled, "customSignatureProvider.DisposeCalled");
            Assert.False(customHashAlgorithm.DisposeCalled, "customHashAlgorithm.DisposeCalled");
        }
    }
}
