using NUnit.Framework;
using SafeVault.Security;
using System;
using System.Text.Encodings.Web;

namespace Tests
{
    [TestFixture]
    public class TestInputValidation
    {
        [Test]
        public void TestForSQLInjection()
        {
            var payload = "admin' OR 1=1 --";
            Assert.Throws<ArgumentException>(() => InputValidator.ValidateUsername(payload));

            var emailPayload = "a@b.com' OR '1'='1";
            Assert.Throws<ArgumentException>(() => InputValidator.ValidateEmail(emailPayload));
        }

        [Test]
        public void TestForXSS()
        {
            var payload = "<script>alert('xss')</script>";

            Assert.Throws<ArgumentException>(() => InputValidator.ValidateUsername(payload));


            var encoded = HtmlEncoder.Default.Encode(payload);
            Assert.That(encoded, Does.Not.Contain("<script>"));
            Assert.That(encoded, Does.Contain("&lt;script&gt;"));
        }

        [Test]
        public void AcceptsValidInputs()
        {
            var u = InputValidator.ValidateUsername("john_doe-12");
            var e = InputValidator.ValidateEmail("john@example.com");

            Assert.That(u, Is.EqualTo("john_doe-12"));
            Assert.That(e, Is.EqualTo("john@example.com"));
        }
    }
}
