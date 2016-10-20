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

using Microsoft.IdentityModel.Logging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System;

namespace Microsoft.IdentityModel.Tokens
{
    /// <summary>
    /// Security key that allows access to cert
    /// </summary>
    public class X509SecurityKey : AsymmetricSecurityKey
    {
        X509Certificate2 _certificate;
        AsymmetricAlgorithm _privateKey;
        bool _privateKeyAvailabilityDetermined;
        AsymmetricAlgorithm _publicKey;
        object _thisLock = new Object();

        /// <summary>
        /// Instantiates a <see cref="SecurityKey"/> using a <see cref="X509Certificate2"/>
        /// </summary>
        /// <param name="certificate">The cert to use.</param>
        public X509SecurityKey(X509Certificate2 certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException("certificate");

            _certificate = certificate;
            KeyId = certificate.Thumbprint;
        }

        /// <summary>
        /// Gets the key size.
        /// </summary>
        public override int KeySize
        {
            get
            {
                return PublicKey.KeySize;
            }
        }

        /// <summary>
        /// Returns the private key from the <see cref="X509SecurityKey"/>.
        /// </summary>
        public AsymmetricAlgorithm PrivateKey
        {
            get
            {
                if (!_privateKeyAvailabilityDetermined)
                {
                    lock (ThisLock)
                    {
                        if (!_privateKeyAvailabilityDetermined)
                        {
#if NETSTANDARD1_4
                            _privateKey = RSACertificateExtensions.GetRSAPrivateKey(_certificate);
#else
                            _privateKey = _certificate.PrivateKey;
#endif
                            _privateKeyAvailabilityDetermined = true;
                        }
                    }
                }

                return _privateKey;
            }
        }

        /// <summary>
        /// Gets the public key from the <see cref="X509SecurityKey"/>.
        /// </summary>
        public AsymmetricAlgorithm PublicKey
        {
            get
            {
                if (_publicKey == null)
                {
                    lock (ThisLock)
                    {
                        if (_publicKey == null)
                        {
#if NETSTANDARD1_4
                            _publicKey = RSACertificateExtensions.GetRSAPublicKey(_certificate);
#else
                            _publicKey = _certificate.PublicKey.Key;
#endif
                        }
                    }
                }

                return _publicKey;
            }
        }

        object ThisLock
        {
            get { return _thisLock; }
        }

        /// <summary>
        /// Gets a bool indicating if a private key exists.
        /// </summary>
        /// <return>true if it has a private key; otherwise, false.</return>
        public override bool HasPrivateKey
        {
            get { return (PrivateKey != null); }
        }

        /// <summary>
        /// Gets the <see cref="X509Certificate2"/>.
        /// </summary>
        public X509Certificate2 Certificate
        {
            get { return _certificate; }
        }
    }
}
