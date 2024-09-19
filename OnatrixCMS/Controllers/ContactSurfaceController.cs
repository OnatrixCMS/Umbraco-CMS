using Microsoft.AspNetCore.Mvc;
using OnatrixCMS.Models;
using OnatrixCMS.Services;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace OnatrixCMS.Controllers;

public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, EmailService emailService) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    private readonly EmailService _emailService = emailService;
    //public async Task<IActionResult> SubmitForm(ContactForm form, bool isPhoneRequired)
    //{
    //    if (!ModelState.IsValid || isPhoneRequired && form.Phone is null)
    //    {
    //        ViewData["name"] = form.Name;
    //        ViewData["email"] = form.Email;
    //        ViewData["phone"] = form.Phone;

    //        ViewData["error_name"] = string.IsNullOrEmpty(form.Name);
    //        ViewData["error_email"] = string.IsNullOrEmpty(form.Email);
    //        ViewData["error_phone"] = string.IsNullOrEmpty(form.Phone);

    //        return CurrentUmbracoPage();
    //    }

    //    var emailSent = await _emailService.SendConfirmationEmailAsync(form);

    //    if (emailSent)
    //    {
    //        ViewData["success"] = "Thank you! Form submitted successfully, a confirmation mail has been sent.";
    //    }
    //    else
    //    {
    //        ViewData["error"] = "Form submitted, but there was an error sending confirmation email.";
    //    }

    //    return CurrentUmbracoPage();
    //}

    public async Task<IActionResult> SubmitForm(ContactForm form, bool isPhoneRequired)
    {
        if (!ModelState.IsValid || isPhoneRequired && form.Phone is null)
        {
            return Json(new
            {
                success = false,
                error = "Please fill out all required fields.",
                errors = new
                {
                    name = string.IsNullOrEmpty(form.Name),
                    email = string.IsNullOrEmpty(form.Email),
                    phone = string.IsNullOrEmpty(form.Phone)
                }
            });
        }

        var emailSent = await _emailService.SendConfirmationEmailAsync(form);

        if (emailSent)
        {
            return Json(new
            {
                success = true,
                message = "Thank you! Form submitted successfully, a confirmation mail has been sent."
            });
        }
        else
        {
            return Json(new
            {
                success = false,
                message = "Form submitted, but there was an error sending confirmation email."
            });
        }
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
