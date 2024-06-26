﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Display(Name = "Street Address")]
            public string StreetAddress { get; set; }

            [Display(Name = "State")]
            public string State { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }

            [Display(Name = "Post Code")]
            public string PostalCode { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //var name = await _userManager.GetNameAsync(user);
            //var streetAddress = await _userManager.GetStreetAddressAsync(user);
            //var state = await _userManager.GetStateAsync(user);
            //var city = await _userManager.GetCityAsync(user);
            //var postalCode = await _userManager.GetPostalCodeAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            //var name = await _userManager.GetNameAsync(user);
            //if (Input.Name != name)
            //{
            //    var setNameResult = await _userManager.SetNameAsync(user, Input.Name);
            //    if (!setNameResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set Name.";
            //        return RedirectToPage();
            //    }
            //}

            //var streetAddress = await _userManager.GetStreetAddressAsync(user);
            //if (Input.StreetAddress != streetAddress)
            //{
            //    var setStreetAddressResult = await _userManager.setStreetAddressAsync(user, Input.StreetAddress);
            //    if (!setStreetAddress.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set street address.";
            //        return RedirectToPage();
            //    }
            //}

            //var state = await _userManager.GetStateAsync(user);
            //if (Input.State != state)
            //{
            //    var setStateResult = await _userManager.SetStateAsync(user, Input.State);
            //    if (!setStateResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set State.";
            //        return RedirectToPage();
            //    }
            //}

            //var city = await _userManager.GetCityAsync(user);
            //if (Input.City != city)
            //{
            //    var setCityResult = await _userManager.SetCityAsync(user, Input.City);
            //    if (!setCityResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set City.";
            //        return RedirectToPage();
            //    }
            //}

            //var postalCode = await _userManager.GetPostalCodeAsync(user);
            //if (Input.PostalCode != postalCode)
            //{
            //    var setPostalCodeResult = await _userManager.SetPostalCodeAsync(user, Input.PostalCode);
            //    if (!setPostalCodeResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set Postal Code.";
            //        return RedirectToPage();
            //    }
            //}

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
