using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using movies_ecommerce.Controllers;
using movies_ecommerce.Models;
using NuGet.ContentModel;


namespace movies_ecommerce.Controllers
{
    //iam didnt check this code 
    // in mvc day9 p3 -1:21:34
    // Manager is Salama Salama111@000
    //Producer is Abdallah 
    //actor is Ali
    //Admin is ahmed
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
   
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel ModelUser)
        {
            if (ModelState.IsValid)
            {
                AppUser NEWUser = new AppUser()
                {
                    UserName = ModelUser.UserName,
                    PasswordHash = ModelUser.Password,
                    Email = ModelUser.email
                };
                IdentityResult r = await _userManager.CreateAsync(NEWUser, ModelUser.Password);
                if (r.Succeeded)
                {
                    await _userManager.AddToRoleAsync(NEWUser, ModelUser.num);
                    await _signInManager.SignInAsync(NEWUser, isPersistent: false);
                    if(User.IsInRole("Admin"))
                    {
                        return View("index", "Movies");
                    }
                    
                    else
                    {
                        return Ok();
                    }
                      
                }
                else
                {
                    foreach (var item in r.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(ModelUser);
        }
        public IActionResult Login()
        {
            var mv = new LoginViewModel();
            return View(mv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    bool isValidPassword = await _userManager.CheckPasswordAsync(user, model.password);

                    if (isValidPassword)
                    {
                        await _signInManager.SignInAsync(user, true);
                        if (User.IsInRole("Actor"))
                            return RedirectToAction("Index", "Actors");
                        else if (User.IsInRole("Producer")) 
                            return RedirectToAction("Index", "Producers");
                        else if(User.IsInRole("Manager"))
                            return RedirectToAction("Index", "Cinemas");
                        else 
                            return RedirectToAction("Index", "Movies");

                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }

}
//public class LoginTests
//{
//    [Fact]
//    public async Task ValidModel_RedirectsToCorrectController()
//    {
//        // Arrange
//        var userManagerMock = new Mock<UserManager<AppUser>>(MockBehavior.Strict);
//        var signInManagerMock = new Mock<SignInManager<AppUser>>(userManagerMock.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(), null, null, null, null);
//        var controller = new YourController(userManagerMock.Object, signInManagerMock.Object);
//        var model = new LoginViewModel { UserName = "validUser", Password = "validPassword" };

//        // Act
//        var result = await controller.Login(model) as RedirectToActionResult;

//        // Assert
//        Assert.NotNull(result);
//        Assert.Equal("Actors", result.ControllerName); // Assuming the validUser has the "Actor" role.
//    }

//    // Write more tests for different scenarios like invalid model, invalid user, invalid password, etc.
//}

//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using NUnit.Framework;
//using YourNamespace.Controllers;
//using YourNamespace.Models;

//namespace YourNamespace.Tests.Controllers
//{
//    [TestFixture]
//    public class AccountControllerTests
//    {
//        private AccountController _controller;
//        private Mock<SignInManager<AppUser>> _signInManagerMock;
//        private Mock<UserManager<AppUser>> _userManagerMock;

//        [SetUp]
//        public void Setup()
//        {
//            var userStoreMock = new Mock<IUserStore<AppUser>>();
//            _userManagerMock = new Mock<UserManager<AppUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
//            _signInManagerMock = new Mock<SignInManager<AppUser>>(_userManagerMock.Object, null, null, null, null, null, null);

//            _controller = new AccountController(_signInManagerMock.Object, _userManagerMock.Object);
//        }

//        [Test]
//        public void Index_ReturnsView()
//        {
//            // Act
//            var result = _controller.Index() as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//        }

//        [Test]
//        public void Register_Get_ReturnsView()
//        {
//            // Act
//            var result = _controller.Register() as ViewResult;

//            // Assert
//            Assert.IsNotNull(result);
//        }

//        [Test]
//        public async Task Register_Post_ValidModel_ReturnsRedirectToAction()
//        {
//            // Arrange
//            var model = new RegisterViewModel
//            {
//                UserName = "TestUser",
//                Password = "TestPassword",
//                Email = "test@example.com",
//                num = "UserRole"
//            };

//            _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
//                            .ReturnsAsync(IdentityResult.Success);

//            // Act
//            var result = await _controller.Register(model) as RedirectToActionResult;

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("Index", result.ActionName);
//            Assert.AreEqual("Movies", result.ControllerName);
//        }

//        [Test]
//        public async Task Login_ValidCredentials_RedirectsToCorrectPage()
//        {
//            // Arrange
//            var model = new LoginViewModel
//            {
//                UserName = "validUsername",
//                Password = "validPassword"
//            };

//            var user = new AppUser { UserName = "validUsername" };
//            _userManagerMock.Setup(m => m.FindByNameAsync("validUsername"))
//                            .ReturnsAsync(user);
//            _userManagerMock.Setup(m => m.CheckPasswordAsync(user, "validPassword"))
//                            .ReturnsAsync(true);

//            // Act
//            var result = await _controller.Login(model) as RedirectToActionResult;

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("Index", result.ActionName);
//            Assert.AreEqual("Movies", result.ControllerName);
//        }

//        [Test]
//        public async Task Logout_SignsOutAndRedirectsToLogin()
//        {
//            // Act
//            var result = await _controller.Logout() as RedirectToActionResult;

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual("Login", result.ActionName);
//        }
//    }
//}


