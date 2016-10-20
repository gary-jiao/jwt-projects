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

using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Tests;
using Xunit;

namespace System.IdentityModel.Tokens.Jwt.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtHeaderTests
    {
        [Fact(DisplayName = "JwtHeaderTests: Constructors")]
        public void Constructors()
        {
            var header1 = new JwtHeader();
            var header2 = new JwtHeader(null);

            var context = new CompareContext();
            IdentityComparer.AreEqual(header1, header2, context);
            TestUtilities.AssertFailIfErrors("JwtHeaderTests.Constructors", context.Diffs);
        }

        [Fact(DisplayName = "JwtHeaderTests: Defaults")]
        public void Defaults()
        {
            JwtHeader jwtHeader = new JwtHeader();
            Assert.True(jwtHeader.Typ == JwtConstants.HeaderType, "jwtHeader.ContainsValue( JwtConstants.HeaderType )");
            Assert.True(jwtHeader.Alg == SecurityAlgorithms.None, "jwtHeader.SignatureAlgorithm == null");
            Assert.True(jwtHeader.SigningCredentials == null, "jwtHeader.SigningCredentials != null");
            Assert.True(jwtHeader.Kid == null, "jwtHeader.Kid == null");
            Assert.True(jwtHeader.Comparer.GetType() == StringComparer.Ordinal.GetType(), "jwtHeader.Comparer.GetType() != StringComparer.Ordinal.GetType()");
        }

        [Fact(DisplayName = "JwtHeaderTests: GetSets")]
        public void GetSets()
        {
        }

        [Fact(DisplayName = "JwtHeaderTests: Publics")]
        public void Publics()
        {
        }
    }
}
