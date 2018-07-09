##### Model
Model objects retrieve and store the model state in a database
##### View
Displays and enables modification of the model data
##### Controller
Handles the user request. Typically the user interacts with the View, which raises a URL request, which is then handled by the controller. The controller renders the appropriate view with the model data as a response.

##### Default Route Config
```CSharp
"{controller}/{action}/{id}"
```

### Project Folders
#### App_Data
Can contain application data files like LocalDB, .mdf, xml, etc. IIS will never serve files from the App_Data folder
#### App_Start
Contains class files which will be executed when the application starts. Typically these are config files like AuthConfig.cs, BundleConfig.cs, FilterConfig.cs, RouteConfig.cs, etc.
#### Content
Static files like css, images, and icons
#### Controllers
Class files for controllers, which handle users' request and returns a response. MVC requires the name of all controller files to end with "Controller". 
#### Fonts
Custom fonts files
#### Models
Model class files, typically includes public properties, which are used to hold and manipulate application data
#### Scripts
Scripts folder contains JavaScript or VBScript, like bootstrap, jQuery, and modernizer. 
#### Views
Contains html files, typically .cshtml files. Views folder includes separate folders for each controller. The Shared folder contains views which will be shared among different controllers, like layout files.
#### Global.asax
Allows you to write code that runs in response to application level events, such as the Application_BeginRequest, application_start, application_error, session_start, session_end, etc.
#### Packages.config
Packages managed by NuGet
#### Web.config
Contains application-level configurations. 

### Routing In MVC
```CSharp
public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        routes.MapRoute(
            name: "Student",
            url: "students/{id}",
            defaults: new { controller = "Student", action = "Index"},
            constraints: new { id = @"\d+" }

        );

        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );
    }
} 
```
After configuring all the routes in RouteConfig class, you need to register them in the Application_Start() event in teh Global.asax, so that it includes all the routes into RouteTable.
```CSharp
public class MvcApplication : System.Web.HttpApplication
{
    protected void Application_Start()
    {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
    }
}
```

#### Action Methods
All public methods of a Controller class are called Action methods, and they have the following restrictions:
1. Action methods must be public.
2. Action methods cannot be overloaded
3. Action methods cannot be static methods
##### Result Class & Description
1. ViewResult -> represents HTML & markup
2. EmptyResult -> represents No Response
3. ContentResult -> string literal
4. FileContentResult/ FilePathResult/ FileStreamResult -> the content of a file
5. JavaScriptResult -> JavaScript script
6. JsonResult -> JSON that can be used in AJAX
7. RedirectResult -> A redirection to a new URL
8. RedirectToRouteResult -> Anotehr action of same or other controller
9. PartialViewResult -> Returns HTML from Partial view
10. HttpUnauthorizedResult -> Returns HTTP 403 status
##### Result Class & Description & Base Controller Method
1. ViewResult -> View()
2. EmptyResult -> 
3. ContentResult -> Content()
4. FileContentResult, FilePathResult, FileStreamResult -> File()
5. JavaScriptResult -> JavaScript()
6. JsonResult -> Json()
7. RedirectResult -> Redirect()
8. RedirectToRouteResult -> RedirectToRoute()
9. PartialViewResult -> PartialView()

#### Action Method Parameters can be simple type or complex type or Nullable Type
```CSharp
public ActionResult Edit(Student std)
{
    // update student to the database
    
    return RedirectToAction("Index");
}

[HttpDelete]
public ActionResult Delete(int id)
{
    // delete student from the database whose id matches with specified id

    return RedirectToAction("Index");
}
```

#### Action Selector Attributes
1. ActionName -> Allow us to specify a different action name than the method name.
```CSharp
public class StudentController : Controller
{
    public StudentController()
    {

    }
       
    [ActionName("find")]
    public ActionResult GetById(int id)
    {
        // get student from the database 
        return View();
    }
}
```

MVC Folder Structure
Routing
Controller
Action method
Action Selectors
ActionVerbs
Model
View
Integrate Model, View & Controller
Razor Syntax
Html Helpers
TextBox
TextArea
CheckBox
RadioButton
DropDownList
Hidden Field
Password
Display
Label
Editor
Model Binding
Create Edit View
Validation
ValidationMessage
ValidationMessageFor
ValidationSummary
Layout View
Create Layout View
Partial View
ViewBag
ViewData
TempData
Filters
ActionFilters
Bundling
ScriptBundle
StyleBundle
Area
Useful Resources
Action Selectors
Action selector is the attribute that can be applied to the action methods. It helps routing engine to select the correct action method to handle a particular request. MVC 5 includes the following action selector attributes:

ActionName
NonAction
ActionVerbs
ActionName
ActionName attribute allows us to specify a different action name than the method name. Consider the following example.

Example: ActionName
public class StudentController : Controller
{
    public StudentController()
    {

    }
       
    [ActionName("find")]
    public ActionResult GetById(int id)
    {
        // get student from the database 
        return View();
    }
}

```
In the above example, we have applied ActioName("find") attribute to GetById action method. So now, action name is "find" instead of "GetById". This action method will be invoked on http://localhost/student/find/1 request instead of http://localhost/student/getbyid/1 request.


2. NonAction -> Indicates that a public method of a Controller is not an action method. Use NonAction attribute when you want public methods in a controlelr but do not want to treat it as an action method. 
```CSharp
public  class StudentController : Controller
{
    public StudentController()
    {

    }
   
    [NonAction]
    public Student GetStudnet(int id)
    {
        return studentList.Where(s => s.StudentId == id).FirstOrDefault();
    }
}
```

3. ActionVerbs -> Used when you want to control teh selection of an action method based on a Http request method. You can define two different methods with the same name but one action method responds to an HTTP Get request and another responds to an HTTP Post request.
You can apply multiple Http verbs using AcceptVerbs attribute. 
```CSharp
[AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
public ActionResult GetAndPostAction()
{
    return RedirectToAction("Index");
}
```

#### View
```CSharp
@model IEnumerable<MVC_BasicTutorials.Models.Student>

@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.StudentName)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Age)
        </th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.StudentName)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Age)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { id=item.StudentId }) |
            @Html.ActionLink("Details", "Details", new { id=item.StudentId  }) |
            @Html.ActionLink("Delete", "Delete", new { id = item.StudentId })
        </td>
    </tr>
}

</table>
```

### MVC
```CSharp
//StudentController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_BasicTutorials.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
    }
}

//Student.cs (model)
namespace MVC_BasicTutorials.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
    }
}

//Index.cshtml
@model IEnumerable<MVC_BasicTutorials.Models.Student>

@{
    ViewBag.Title = "Index";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(model => model.StudentName)
        </th>
        <th>
            @Html.DisplayNameFor(model => model.Age)
        </th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.StudentName)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Age)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { id=item.StudentId }) |
            @Html.ActionLink("Details", "Details", new { id=item.StudentId  }) |
            @Html.ActionLink("Delete", "Delete", new { id = item.StudentId })
        </td>
    </tr>
}

</table>
```

### Controllers
The Controller class contains public methods called Action methods, which handle incoming broswers requests, retrieves the necessary model data, and returns appropriate responses.
Every controller class must end with "Controller"


### Razor Syntax
Use @Variable_Name to display the value of a server side variable
```CSharp
<h1>Razor INLINE Syntax Demo</h1>
<h2> @DateTime.Now.ToShortDateString()</h2>

<h1> Razor Mutli-Line Block</h1>
@{
    var date = DateTime.Now.ToShortDateString();
    var message = "Hello Razor!";
}
<h2>Today's date is: @date</h2>
<h3>@message</h3>


<h1>Display Text inside Code Block with @: or <text></text> </h1>
@{
    var date = DateTime.Now.ToShortDateString();
    string message = "Yo World!";
    @: Today's date is: @date <br/>
    @message
}

<h1>If-Else Statements</h1>
@if(DateTime.IsLeapYear(DateTime.Now.Year) )
{
    @DateTime.Now.Year @:is a leap year.
}
else { 
    @DateTime.Now.Year @:is not a leap year.
}

<h1>For Loop in Razor</h1>
@for (int i = 0; i < 5; i++) { 
    @i.ToString() <br />
}

<h1>Use @model to use Model Object anywhere in the view</h1>

@model Student
<h2>Student Detail:</h2>
<ul>
    <li>Student Id: @Model.StudentId</li>
    <li>Student Name: @Model.StudentName</li>
    <li>Age: @Model.Age</li>
</ul>
```

### HTML Helper Classes
HtmlHelper class generates html elements. For example, @Html.ActionLink("Create New", "Create") would generate anchor tag <a href="/Student/Create">Create New</a>.
HtmlHelper methods are designed to make it easy to bind view data or model data
#### HtmlHelper & Strongly-Typed HtmlHelpers & Html Control
1. Html.ActionLink -> xxx -> anchor tag
2. Html.TextBox -> Html.TextBoxFor -> Textbox
3. Html.TextArea -> Html.TextAreaFor -> TextArea
4. Html.CheckBox -> ...
5. Html.RadioButton -> ...
6. Html.DropDownList
7. Html.ListBox -> ... -> mutli-select list box
8. Html.Hidden -> ... -> Hidden Field
9. Password -> ... -> Password textbox
10. Html.Display -> Html text
11. Html.Label -> ...
12. Html.Editor -> ... -> Generates Html controls based on data type of specified model property (e.g. textbox for string property, numeric field for int, etc. )

```CSharp
@model Student

@Html.TextBox("StudentName", null, new { @class = "form-control" }) 
```

#### Editor()
Editor() method requires a string expression parameter to specify the property name. It creats a html element based on the datatype of the specified property.
```CSharp
StudentId:      @Html.Editor("StudentId")
Student Name:   @Html.Editor("StudentName")
Age:            @Html.Editor("Age")
Password:       @Html.Editor("Password")
isNewlyEnrolled: @Html.Editor("isNewlyEnrolled")
Gender:         @Html.Editor("Gender")
DoB:            @Html.Editor("DoB")
```

#### Exclude Properties in Binding
```CSharp
[HttpPost]
public ActionResult Edit([Bind(Exclude = "Age")] Student std)
{
    var name = std.StudentName;
           
    //write code to update student 
            
    return RedirectToAction("Index");
}
```
The Bind attribute will improve the performance by only bind properties which you needed.

 Model binding is a two step process. First, it collects values from the incoming http request and second, populates primitive type or complex type with these values.


Default value provider collection evaluates values from the following sources:

1. Previously bound action parameters, when the action is a child action
2. Form fields (Request.Form)
3. The property values in the JSON Request body (Request.InputStream), but only when the request is an AJAX request
4. Route data (RouteData.Values)
5. Querystring parameters (Request.QueryString)
6. Posted files (Request.Files)

### DataAnnotations - Validation
DataAnnotation attrubutes implement validations. 
1. Required
2. StringLength
3. Range
4. RegularExpression
5. CreditCard
6. CustomValidation
7. EmailAddress
8. FileExtension
9. MaxLength
10. MinLength
11. Phone

```CSharp
public class Student
{
    public int StudentId { get; set; }
     
    [Required]
    public string StudentName { get; set; }
       
    [Range(5,50)]
    public int Age { get; set; }
}
```

Use ValidationSummary to display all the error messages in the view.
Use ValidationMessageFor or ValidationMessage helper methods to display filed level error messages in the view
Check wheter the model is valid before updating in the action method using ModelState.IsValid


#### _ViewStart.cshtml 
included inteh Views folder by default. It sets up the default layout page for all the views in the folder and its subfolders using the Layout property.

#### Rendering Methods
RenderBody() -> Renders the portion of the child view that is not within a named section. Layout view must include RenderBody() method, and it can only be used once.
RenderSection(string name) -> Renders content of named section and specifies whether the section is required. RenderSection() is option in a Layout view. This can be called multiple times.

##### ViewBag
Model object is used to send data in a razor view. However, if you only want to send a small amount of temporary data to the view, you can use ViewBag.
Viewbag is useful if you want to transfer temporary data (which is not included in a model) from the controller to the view. The viewBag is a dynamic type property of the ControllerBase class (which is the base class of all controllers).
Ex: 
```CSharp
//Controller
ViewBag.Name = "Kali"; //this attaches the Name property to ViewBag with dot notation and assigns a string value. 
//View
@ViewBag.Name //the value set in the controller can be accessed in the view using the @ symbol, which is the razor syntax to access the server-side variable.
```
ViewBag only transfers data from the controller to the view, not visa-versa. If attempted, ViewBag values will be null.
##### Partial Views
Partial views are a reusable view which can be used as a child view in other views. Can be used in the Layout view, as well as other content views. 

#### ViewData
ViewData transfers data from the Controller to the View, and it only lasts for the current Http request. Values will be cleared if redirection occurs. 
ViewData value must be type cast before used
ViewBag internally inserts data into ViewData dictionary, so teh key of ViewData and property of ViewBag CANNOT match


#### TempData
TempData can be used to store data between two consecutive requests, and will be retained during redirection.
TempData internally use Session to store data. 
TempData must be typecast before use, and checked for null values to avoid runtime errors.
TempData can be used to store only one time messages like error messages or validation messages
Calling TempData.Keep() will keep all the values in TempData for a third request.

#### Filters
A user request is routed to the controller and action method. However, sometimes you may want to execute some logic before or after an action method executes. 
Filter is a custom class where you can write custom logic ot execute before/after an action method executes.
Different types of filters:
1. Authorization filters -> performs authentication and authorizes before executing action method -> [Authorize], [RequireHttps]
2. Action filters -> performs some operation before and after an action method executes
3. Result filters -> Performs some operatoin before/after the execution of view result -> [OutputCache]
4. Exception filters -> performs some operation if there is an unhandled exception thrown during the execution of the pipeline -> [HandleError]

```Csharp
rror]
public class HomeController : Controller
{
    public ActionResult Index()
    {
        //throw exception for demo
        throw new Exception("This is unhandled exception");
            
        return View();
    }

    public ActionResult About()
    {
        return View();
    }

    public ActionResult Contact()
    {
        return View();
    }        
}
```
In the above example, we have applied [HandleError] attribute to HomeController. So now it will display Error page if any action method of HomeController would throw unhandled exception. 
Filters can be applied globally in FilterConfig class, at Controller-level, or at action method level.



## Strongly Typed MVC View
A view that is associated with a specific type, which exposes the model instance through it's model property
To make our model view strongly typed, we just need to add a model view directive to the top of the view. 
(in View)
```CSharp
@model ComicBookViewer.Models.ComicBook
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
    ViewBag.Title = Model.DisplayText;

}

<h2>@Model.DisplayText</h2>
<div class="row">
    <div class="col-md-6">
        <div class="well">
            <h5><label>Series Title:</label> @Model.SeriesTitle</h5>
            <h5><label>Issue #:</label> @Model.IssueNumber</h5>
            <h5><label>Favorite:</label> @(Model.Favorite ? "Yes" : "No") </h5>
            @if (Model.Artists.Length > 0)
            {
                <h5>Artist:</h5>
                <div>
                    <ul>
                        @foreach (var artist in Model.Artists)
                        {
                            <li>@artist.Role: @artist.Name</li>
                        }
                    </ul>
                </div>
            }
        </div>
        <h5>Description:</h5>
        <div>@Html.Raw(Model.DescriptionHtml)</div>
    </div>
    <div class="col-md-6">
        <img src="/Images/@Model.CoverImageFileName"
             alt="@Model.DisplayText" class="img-responsive" />
    </div>
</div>
```

### Repository Design Pattern
Separation of concerns design principal
This will give us a central location fro storing and managing our comicbook model instances (Adding Data Folder)

#### Private Fields
Naming Convention -> _myPrivateField

### Debugging
Step Into and Step Over both tell the debugger to execute the next line of code, but they handle method calls differently 
Step Into will execute the method call and suspend execution on the first line of code in the calling method
Step Over will execute the entire method and suspend execution on the next line of code after the method call 
Step Out will finish the Step Into's method lines

### Static Method
Declaring a method as static means it can be called directly on the class itself, without having to create an object instance first. 

```CSharp
@Html.ActionLink("Return to List", "Index", null, new { @class = "btn btn-default" }) //here we escape the csharp keyword class with an @ symbol so we can use it in our html
```

# Form Notes

## Form Element Action Attribute
If an action attribute is omitted, the form will post back to the same URL that was used to request the form. 
Ex. A request from “http://teamtreehouse.com/issues/report” would submit back to the same URL when the form contains no action attribute, as in the following: 
```HTML
<form method="post">
    <input type="text" name="Name" />
    <input type="text" name="Email" />
    <input type="text" name="DescriptionOfProblem" />
    <button type="submit">Report Issue</button>
</form>
```

## Attributes
Attributes provide a way of associating information with our C# code. Instead of writing programming instructions using code statements (like if(action=='Post'){...}else{...}), we can label or decorate our code with attributes. Attributes can be associated with classes, properties, or methods. This can be done by adding a pair of brackets above the class/prop/method and type the name of the attribute, followed by a set of parentheses if the attribute requires any parameter values. Multiple attributes can be stacked or can be separated by a comma.
```Csharp
public ActionResult Add()
{
    return View();
}

[ActionName("Add"), HttpPost]
public ActionResult AddPost()
{
    return View()
}
```