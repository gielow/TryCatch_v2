using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using TryCatch.Data;
using TryCatch.Interfaces;
using TryCatch.Models;
using TryCatch.Providers;

namespace TryCatch.Controllers
{
    [Authorize]
    public class CustomerController : System.Web.Mvc.Controller
    {
        [HttpPost]
        [AllowAnonymous]
        //[System.Web.Mvc.ValidateAntiForgeryToken]
        public System.Web.Mvc.ActionResult Login(CustomerLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                /*if (ValidateLogin(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }*/
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
    }

    [Authorize]
    public class CustomerApiController : ApiController
    {
        IRepository _repository;

        public CustomerApiController(IRepository repository)
        {
            _repository = repository;
        }

        public void Login()
        {
            //Authentication.SignIn()
        }

        //private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Customer
        public IQueryable<Customer> GetCustomers()
        {
            //return db.Customers;
            return _repository.Customers.AsQueryable();
        }

        // GET: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            /*Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);*/
            return Ok(new Customer());
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customer
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();
            */
            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            /*Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);*/
            return Ok(new Customer());
        }

        protected override void Dispose(bool disposing)
        {
            /*if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);*/
        }

        private bool CustomerExists(int id)
        {
            //return db.Customers.Count(e => e.Id == id) > 0;
            return false;
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> Login(CustomerLoginModel model)
        {
            /*if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }*/

            var customer = _repository.Customers.FirstOrDefault(c => c.Email == model.Email && c.Password == model.Password);

            /*ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));*/

            //_repository.cus

            //bool hasRegistered = user != null;

            //if (hasRegistered)
            //{

            Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookieIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);

                //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager, OAuthDefaults.AuthenticationType);
                //ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager, CookieAuthenticationDefaults.AuthenticationType);

                //AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                var credentials = new OAuthGrantResourceOwnerCredentialsContext(OAuthDefaults.AuthenticationType, new OAuthAuthorizationServerOptions() { AuthenticationType = OAuthDefaults.AuthenticationType }, string.Empty,
                    model.Email, model.Password, new List<string>());
                AuthenticationProperties properties = SimpleAuthorizationServerProvider.GrantResourceOwnerCredentials(user.UserName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            //}
            //else
            //{
            //    IEnumerable<Claim> claims = externalLogin.GetClaims();
            //    ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            //    Authentication.SignIn(identity);
            //}

            FormsAuthentication.SetAuthCookie(model.Email, false);

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        #region Helpers
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
        #endregion
    }
}