using LinkDev.Wasel.Core.Base;
using LinkDev.Wasel.Core.Contracts;


namespace Core.DominModels.UserAggregate
{
    public class UserProfile : Entity<Guid>, IAggregate
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Mobile { get; private set; }
        public string? Email { get; private set; }
        public DateOnly? DateOfBirth { get; private set; }
        public Gender? Gender { get; private set; }
        public string? Address { get; private set; }
        public bool? IsDeleted { get; private set; }
        private UserProfile() { }
        public UserProfile(
            Guid id,
            string firstName,
            string lastName,
            string mobile,
            string email,
            DateOnly dateOfBirth,
            string address
          )
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;
            Email = email;
            DateOfBirth = dateOfBirth;
            Address = address;
        }

        public void DeleteUser()
        {
            IsDeleted = true;
        }





    }

}
