﻿using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Address;
using System.ComponentModel.DataAnnotations;
using static Infinia.Core.MessageConstants.ErrorMessages;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.IdentityCard;
using static Infinia.Infrastructure.Data.DataConstants.DataConstants.Customer;
using System.ComponentModel;

namespace Infinia.Core.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [EmailAddress(ErrorMessage = InvalidEmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = LengthErrorMessage)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(PasswordMaxLength,MinimumLength = PasswordMinLength, ErrorMessage = PasswordLengthErrorMessage)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.])[A-Za-z\d@$!%*?&.]{8,}$",
        ErrorMessage = PasswordCharactersErrorMessage)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [Compare("Password", ErrorMessage = PasswordsDoNotMatchErrorMessage)]
        [PasswordPropertyText]       
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength, ErrorMessage = LengthErrorMessage)]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength, ErrorMessage = LengthErrorMessage)]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(StreetMaxLength, MinimumLength = StreetMinLength, ErrorMessage = LengthErrorMessage)]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(PostalCodeMaxLength, ErrorMessage = LengthErrorMessage)]
        public string PostalCode { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(CardNumberMaxLength, MinimumLength = CardNumberMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(IssuerMaxLength, MinimumLength = IssuerMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardIssuer { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardNationality { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(SexMaxLength, MinimumLength = SexMinLength, ErrorMessage = LengthErrorMessage)]
        public string IdentityCardSex { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredFieldErrorMessage)]
        [StringLength(SSNMaxLength, MinimumLength = SSNMinLength, ErrorMessage = LengthErrorMessage)]
        public string SSN { get; set; } = string.Empty;
    }
}
