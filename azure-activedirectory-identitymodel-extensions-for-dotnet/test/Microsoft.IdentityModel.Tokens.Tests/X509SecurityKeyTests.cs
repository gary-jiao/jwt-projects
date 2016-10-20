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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace Microsoft.IdentityModel.Tokens.Tests
{
    public class X509SecurityKeyTests
    {
        [Fact]
        public void Constructor()
        {
            X509SecurityKey x509SecurityKey;
            ExpectedException expectedException = new ExpectedException(typeExpected: typeof(ArgumentNullException), substringExpected: "certificate");
            try
            {
                x509SecurityKey = new X509SecurityKey(null);
                expectedException.ProcessNoException();
            }
            catch (Exception exception)
            {
                expectedException.ProcessException(exception);
            }

            X509Certificate2 x509Certificate2 = KeyingMaterial.DefaultCert_2048;
            expectedException = ExpectedException.NoExceptionExpected;
            try
            {
                x509SecurityKey = new X509SecurityKey(x509Certificate2);
                Assert.Same(x509Certificate2, x509SecurityKey.Certificate);
            }
            catch (Exception exception)
            {
                expectedException.ProcessException(exception);
            }
        }

#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory, MemberData("IsSupportedAlgDataSet")]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void IsSupportedAlgorithm(X509SecurityKey key, string alg, bool expectedResult)
        {
            if (key.CryptoProviderFactory.IsSupportedAlgorithm(alg, key) != expectedResult)
                Assert.True(false, string.Format("{0} failed with alg: {1}. ExpectedResult: {2}", key, alg, expectedResult));
        }

        public static TheoryData<X509SecurityKey, string, bool> IsSupportedAlgDataSet
        {
            get
            {
                var dataset = new TheoryData<X509SecurityKey, string, bool>();
                dataset.Add(KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256, SecurityAlgorithms.RsaSha256Signature, true);
                dataset.Add(KeyingMaterial.X509SecurityKeySelfSigned2048_SHA256_Public, SecurityAlgorithms.RsaSha256, true);
                dataset.Add(KeyingMaterial.X509SecurityKeySelfSigned2048_SHA512, SecurityAlgorithms.Aes128Encryption, false);
                dataset.Add(KeyingMaterial.X509SecurityKeySelfSigned1024_SHA256, SecurityAlgorithms.RsaSha384, true);
                X509SecurityKey testKey = new X509SecurityKey(KeyingMaterial.CertSelfSigned2048_SHA256);
                testKey.CryptoProviderFactory = new CustomCryptoProviderFactory(new string[] { SecurityAlgorithms.RsaSsaPssSha256Signature });
                dataset.Add(testKey, SecurityAlgorithms.RsaSsaPssSha256Signature, true);
                return dataset;
            }
        }
    }
}