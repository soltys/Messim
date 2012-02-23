using System.Text;
using NUnit.Framework;

namespace Messim.Util.Test
{
    [TestFixture]
    class SHATest
    {
        [Test]
        public void SHA1_hash_is_equal_to_other_generator_using_default_encoding()
        {
            string wordToHash = "milk";
            string milkHash = "cf5dbf0ec57dff5da56d86c45b3bdd11849a065a".ToUpperInvariant();

            Assert.AreEqual(milkHash, SHA.CreateSHA1Hash(wordToHash));
        }

        [Test]
        public void SHA512_hash_is_equal_to_other_generator_using_default_encoding()
        {
            string wordToHash = "milk";
            string milkHash = "7586A2A3DA04B9C2610906E936D0365EAA601AD8CDF26D30501AFA2A27A3D42AB0023912B488F408D457BD958EBB24140C2BA029CE93A9702D9197A6E30C331D";

            Assert.AreEqual(milkHash, SHA.CreateSHA512Hash(wordToHash));
        }


        [Test]
        public void SHA512_hash_coverted_to_base64_still_represent_same_data()
        {
            string wordToHash = "milk";
            string milkHash = "dYaio9oEucJhCQbpNtA2XqpgGtjN8m0wUBr6Kiej1CqwAjkStIj0CNRXvZWOuyQUDCugKc6TqXAtkZem4wwzHQ==";

            string generatedHash = SHA.CreateSHA512HashToBase64String(wordToHash, Encoding.UTF8);


            Assert.AreEqual(milkHash, generatedHash);
        }
    }
}
