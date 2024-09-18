using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnatrixCMS.Models;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace OnatrixCMS.Controllers;

public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    public IActionResult SubmitForm(ContactForm form, bool isPhoneRequired)
    {
        if (!ModelState.IsValid || isPhoneRequired && form.Phone is null)
        {
            ViewData["name"] = form.Name;
            ViewData["email"] = form.Email;
            ViewData["phone"] = form.Phone;

            ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
            ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
            ViewData["error_phone"] = string.IsNullOrEmpty(form.Phone);

            return CurrentUmbracoPage();
        }

        ViewData["success"] = "Thank you! Form submitted successfully.";
        return CurrentUmbracoPage();
    }

    public IActionResult SubmitServiceForm(ContactForm form, bool isMessageRequired)
    {
        if (!ModelState.IsValid || isMessageRequired && form.Question is null)
        {
            ViewData["name"] = form.Name;
            ViewData["email"] = form.Email;
            ViewData["question"] = form.Question;

            ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
            ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
            ViewData["error_question"] = string.IsNullOrEmpty(form.Question);

            return CurrentUmbracoPage();
        }

        ViewData["success"] = "Thank you! Form submitted successfully.";
        return CurrentUmbracoPage();
    }







    public IActionResult SubmitSmallForm(ContactForm form, bool isMessageRequired)
    {
        if (isMessageRequired && form.Question is null)
        {
            ViewData["email"] = form.Email;

            ViewData["error_question"] = string.IsNullOrEmpty(form.Question);

            return CurrentUmbracoPage();
        }

        ViewData["success"] = "Thank you! Form submitted successfully.";
        return CurrentUmbracoPage();
    }
}
