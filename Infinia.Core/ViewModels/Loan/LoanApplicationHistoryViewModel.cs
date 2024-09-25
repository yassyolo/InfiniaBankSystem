namespace Infinia.Core.ViewModels
{
    public class LoanApplicationHistoryViewModel
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

            public string NetMonthlyIncome { get; set; } = string.Empty;
           
            public string FixedMonthlyExpenses { get; set; } = string.Empty;

            public string PermanentContractIncome { get; set; } = string.Empty;

            public string TemporaryContractIncome { get; set; } = string.Empty;

            public string CivilContractIncome { get; set; } = string.Empty;
            public string BusinessIncome { get; set; } = string.Empty;

            public string PensionIncome { get; set; } = string.Empty;

            public string FreelanceIncome { get; set; } = string.Empty;

            public string OtherIncome { get; set; } = string.Empty;

            public string HasOtherCredits { get; set; } = string.Empty;

            public string HasApartmentOrHouse { get; set; } = string.Empty;

            public string HasCommercialProperty { get; set; } = string.Empty;

            public string HasLand { get; set; } = string.Empty;

            public string HasMultipleProperties { get; set; } = string.Empty;

            public string HasPartialOwnership { get; set; } = string.Empty;

            public string NoProperty { get; set; } = string.Empty;
    
            public int VehicleCount { get; set; }
            
            public string MaritalStatus { get; set; } = string.Empty;

            public string LoanAmount { get; set; } = string.Empty;

            public int LoanTermMonths { get; set; }

            public string InterestRate { get; set; } = string.Empty;

            public string Type { get; set; } = string.Empty;

            public int LoanRepaymentNumber { get; set; }
            public string Status { get; set; } = string.Empty;
        public double ProbabilityOfApproval { get; set; }

        public double CreditScore { get; set; }

        public string RiskGroup { get; set; } = string.Empty;
    }
}

