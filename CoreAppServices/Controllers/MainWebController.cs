using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CoreApp.Business.Interfaces;
using CoreApp.Models.Shared;
using CoreAppServices.Models;
using System.Web.Helpers;
using Microsoft.Practices.ServiceLocation;
using System.Net;
using CoreApp.Models.CoreModels.SharedEntities;
using Newtonsoft.Json;

namespace CoreAppServices.Controllers
{

   // [EnableCorsAttribute("http://localhost:4200","*", "GET, POST")]
    public class MainWebController : ApiController
    {

        public dynamic res;
          [System.Web.Http.Route("mainweb/login")]
          [System.Web.Http.AcceptVerbs("GET", "POST")]

        public string Login( [FromBody] string x)
        {
            return "Hi Its working";

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        public JsonResult CreateProfile([FromBody] UserModel userprofile)
        {
           

            var response = new ResponseModel<UserModel>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userdata = new UserModel
                    {
                        FirstName = userprofile.FirstName,
                        LastName = userprofile.LastName,
                        Email = userprofile.Email,
                        UserName = userprofile.UserName,
                        Address = userprofile.Address,
                        UniqueNumber = userprofile.UniqueNumber,
                        UserType = userprofile.UserType,
                        Gender = userprofile.Gender,
                        Age = userprofile.Age,
                        MobileNumber =userprofile.MobileNumber,
                        SecondaryMobileNumber =userprofile.SecondaryMobileNumber,
                    };
                    response.Item= ServiceLocator.Current.GetInstance<IUserProcessor>().CreateUser(userdata);
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessages.Add(ex.Message);
                response.ResponseCode = Constant.ResponseCode.Error;
            }
           
            //JsonConvert.SerializeObject(response.Item);
            return   new JsonResult { Data = response.Item };
           // return Request.CreateResponse(HttpStatusCode.OK);
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.Route("mainweb/LoginAuthoninication")]
        public JsonResult LoginAuthoninication([FromBody] Credentials credentilas)
        {
            var responses = new ResponseModel<AuthenticationInfo>();

            if (ModelState.IsValid && credentilas.Password != null & credentilas.UserName != null)
            {
                try
                {
                    var userdata = new Credentials
                    {
                        UserName = credentilas.UserName,
                        Password = credentilas.Password
                    };
                    responses.Item = ServiceLocator.Current.GetInstance<IUserProcessor>().GetUserAuth(credentilas, true);
                }
                catch (Exception ex)
                {
                    responses.ErrorMessages.Add(ex.Message);
                    responses.ResponseCode = Constant.ResponseCode.Error;
                }
            }
            else
            {
                if(credentilas.UserName==null || credentilas.Password == null)
                {
                    responses.ErrorMessages.Add("Enter Valid Credentilas");
                }
            }
            // var my = JsonConvert.SerializeObject(responses);
            var x = new JsonResult { Data = responses, ContentType = "JSON", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return x;
            // return new JsonDataResult { Data = responses, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
    }



    public class JsonDataResult : JsonResult
    {
        // Summary:
        //     Enables processing of the result of an action method by a custom type that inherits
        //     from the System.Web.Mvc.ActionResult class.
        //
        // Parameters:
        //   context:
        //     The context within which the result is executed.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The context parameter is null.
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {
                var writer = new JsonTextWriter(response.Output) { Formatting = Formatting.Indented };
                var serializer = JsonSerializer.Create(new JsonSerializerSettings());
                serializer.Serialize(writer, Data);
                writer.Flush();
            }
        }
    }
}