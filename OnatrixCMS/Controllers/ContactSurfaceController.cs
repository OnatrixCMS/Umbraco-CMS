using Microsoft.AspNetCore.Mvc;
using OnatrixCMS.Models;
using OnatrixCMS.Services;
using System.Text.RegularExpressions;
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

    public async Task<IActionResult> SubmitForm(ContactForm form, bool isPhoneRequired)
    {
        if (!ModelState.IsValid || isPhoneRequired && form.Phone is null || !IsValidEmail(form.Email))
        {
            return Json(new
            {
                success = false,
                error = "Please fill out all required fields.",
                errors = new
                {
                    name = string.IsNullOrEmpty(form.Name),
                    email = string.IsNullOrEmpty(form.Email),
                    phone = isPhoneRequired && string.IsNullOrEmpty(form.Phone)
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

    public async Task<IActionResult> SubmitServiceForm(ContactForm form, bool isMessageRequired)
    {
        if (!ModelState.IsValid || isMessageRequired && string.IsNullOrEmpty(form.Question) || !IsValidEmail(form.Email))
        {
            return Json(new
            {
                success = false,
                error = "",
                errors = new
                {
                    name = string.IsNullOrEmpty(form.Name),
                    email = string.IsNullOrEmpty(form.Email),
                    question = isMessageRequired && string.IsNullOrEmpty(form.Phone)
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

    public async Task<IActionResult> SubmitSmallForm(ContactForm form, bool isMessageRequired)
    {
        if (isMessageRequired || form.Email is null || !IsValidEmail(form.Email))
        {
            return Json(new
            {
                success = false,
                error = "Enter a valid e-mail",
                errors = new
                {
                    message = isMessageRequired && string.IsNullOrEmpty(form.Email)
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

    private static bool IsValidEmail(string email)
    {
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@s]+$";
        return Regex.IsMatch(email, emailRegex);
    }
}
