using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using RestApiEnvironment;
using RestApiEnvironment.Models;
using RestSharp;

namespace ApiTestsLessons
{
    public class Tests
    {
        private const string EmptyJson = "{}";
        private ApiUtils _api;
        [SetUp]
        public void Setup()
        {
            _api = new ApiUtils();
        }

        [Test]
        public void VerifyGetPostTest()
        {
            var response = _api.ExecuteRequest(Endpoints.Posts, Method.Get);
            
            // Verify status code is 200
            Assert.True(response.IsSuccessful);
            
            // Verify content is Json
            Assert.AreEqual("application/json",response.ContentType);

            //Verify sorting by asc
            var allUsers = ApiUtils.GetJsonContent<List<UserModel>>(response.Content);
            Helper.AssertOrderIsAscendingById(allUsers);
        }
        
        [Test]
        public void VerifyGetPostWithIdTest()
        {
            var response = _api.ExecuteRequest(Endpoints.Posts, Method.Get, modifier: "99");
                    
            // Verify status code is 200
            Assert.True(response.IsSuccessful);
            
            // Verify user model values
            var user = ApiUtils.GetJsonContent<UserModel>(response.Content);
            Assert.AreEqual(user.Id, "99");
            Assert.AreEqual(user.UserId, "10");
            Assert.IsNotEmpty(user.Title);
            Assert.IsNotEmpty(user.Body);
        }

        [Test]
        public void VerifyGetPostReturnErrorIfNotFound()
        {
            var response = _api.ExecuteRequest(Endpoints.Posts, Method.Get, "150");

            // Verify status code is 404
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
            
            // Verify response body is empty
            Assert.IsTrue(response.Content == EmptyJson);
        }

        [Test]
        public void VerifyPostCreatesUser()
        {
            var userForRequest = new UserModel()
            {
                UserId = "1",
                Body = Helper.GenerateRandomString(10),
                Title = Helper.GenerateRandomString(15),
            };
            
            // Verify status code is 201
            var responce = _api.ExecuteRequest(Endpoints.Posts, Method.Post, user:userForRequest);
            Assert.IsTrue(responce.StatusCode == HttpStatusCode.Created);
            
            // Verify Post information is correct: title, body, userId match data from request, id is present in response.
            var createdUser = ApiUtils.GetJsonContent<UserModel>(responce.Content);
            Assert.AreEqual(userForRequest.UserId,createdUser.UserId);
            Assert.AreEqual(userForRequest.Title,createdUser.Title);
            Assert.AreEqual(userForRequest.Body,createdUser.Body);
            Assert.IsNotEmpty(createdUser.Id);
        }

        [Test]
        public void VerifyGetUsers()
        {
            var response = _api.ExecuteRequest(Endpoints.Users, Method.Get);
            
            // Verify status code is 200
            Assert.True(response.IsSuccessful);
            
            // Verify content is Json
            Assert.AreEqual("application/json",response.ContentType);

            //User (id=5) data equals to expected Data
            var expectedUser = ApiUtils.GetJsonContent<FullUserModel>(Helper.LoadJson("TestDataJson.json"));
            var createdUser = ApiUtils.GetJsonContent<List<FullUserModel>>(response.Content).First(user => user.Id == "5");
            
            // Verify Actual User matches Expected user
            Assert.IsTrue(expectedUser.Equals(createdUser));
        }
        
        [Test]
        public void VerifyGetParticularUser()
        {
            var response = _api.ExecuteRequest(Endpoints.Users, Method.Get, modifier:"5");
            
            // Verify status code is 200
            Assert.True(response.IsSuccessful);
            
            //User (id=5) data equals to expected Data
            var expectedUser = ApiUtils.GetJsonContent<FullUserModel>(Helper.LoadJson("TestDataJson.json"));
            var createdUser = ApiUtils.GetJsonContent<FullUserModel>(response.Content);
            
            // Verify Actual User matches Expected user
            Assert.IsTrue(expectedUser.Equals(createdUser));
        }
    }
}