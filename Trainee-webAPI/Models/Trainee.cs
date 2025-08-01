

namespace Trainee_webAPI.Models

{
    public class Trainee
    {
        public int TraineeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public DateTime JoiningDate { get; set; }
        public string Gender { get; set; }
        public byte[] Photo { get; set; }
        public string Password { get; set; }
    }


}
