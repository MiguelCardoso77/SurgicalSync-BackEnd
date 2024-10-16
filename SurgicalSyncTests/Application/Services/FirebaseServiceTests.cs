using System.Threading.Tasks;
using DDDNetCore.Application.Services;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSyncTests.Application.Services
{
    public class FirebaseServiceTests
    {
        [SetUp]
        public void Setup()
        {
            // Configuração do Firebase Admin SDK
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("../../../surgicalsync-d5bd5-firebase-adminsdk-7v461-12fb9fe637.json")
            });
        }

        [Test]
        public async Task TestCreateUserInFirebase()
        {
            // Arrange
            var email = "testuser@example.com";
            var password = "TestPassword123";
            var args = new UserRecordArgs
            {
                Email = email,
                Password = password,
            };

            // Act
            UserRecord userRecord = null;
            try
            {
                userRecord = await FirebaseService.CreateUserRecordAsync(email, password);
            }
            catch (FirebaseAuthException ex)
            {
                Assert.Fail($"Failed to create user in Firebase: {ex.Message}");
            }

            // Assert
            Assert.IsNotNull(userRecord, "User creation failed.");
            Assert.AreEqual(email, userRecord.Email, "The created user's email does not match the expected email.");
        }

        [TearDown]
        public async Task TearDown()
        {
            // Clean up the created user after the test runs
            var user = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync("testuser@example.com");
            if (user != null)
            {
                await FirebaseAuth.DefaultInstance.DeleteUserAsync(user.Uid);
            }
        }
    }
}