using Infinia.Core.MessageConstants;
using System.ComponentModel.DataAnnotations;

namespace Infinia.Core.ViewModels
{
        public class LoanApplicationViewModel1
        {
            public int Id { get; set; }
           
            public string EducationLevel { get; set; } = string.Empty;

            public bool IsRetired { get; set; }

            public string EmployerName { get; set; } = string.Empty;

            public string Position { get; set; } = string.Empty;

            public int YearsAtJob { get; set; }

            public int MonthsAtJob { get; set; }

            public int TotalWorkExperienceYears { get; set; }

            public int TotalWorkExperienceMonths { get; set; }
   
            public int NumberOfHouseholdMembers { get; set; }
           
            public int MembersWithProvenIncome { get; set; }
        
            public int Dependents { get; set; }

            public decimal NetMonthlyIncome { get; set; }
           
            public decimal FixedMonthlyExpenses { get; set; }

            public decimal PermanentContractIncome { get; set; }

            public decimal TemporaryContractIncome { get; set; }

            public decimal CivilContractIncome { get; set; }
            public decimal BusinessIncome { get; set; }

            public decimal PensionIncome { get; set; }

            public decimal FreelanceIncome { get; set; }

            public decimal OtherIncome { get; set; }

            public bool HasOtherCredits { get; set; }

            public bool HasApartmentOrHouse { get; set; }

            public bool HasCommercialProperty { get; set; }

            public bool HasLand { get; set; }

            public bool HasMultipleProperties { get; set; }

            public bool HasPartialOwnership { get; set; }

            public bool NoProperty { get; set; }
    
            public int VehicleCount { get; set; }
            
            public string MaritalStatus { get; set; } = string.Empty;

            public decimal LoanAmount { get; set; }

            public int LoanTermMonths { get; set; }

            public double InterestRate { get; set; }

            public string Type { get; set; } = string.Empty;

            public int LoanRepaymentNumber { get; set; }
        }
    
}

