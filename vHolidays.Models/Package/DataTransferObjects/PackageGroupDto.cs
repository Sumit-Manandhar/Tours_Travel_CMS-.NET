namespace vHolidays.Models.Package.DataTransferObjects
{
    public class PackageGroupDto
    {
        public int Id { get; set; }
        public int Adult { get; set; }
        public int FreeOfCost { get; set; }
        public bool SingleSupliment { get; set; }
        public int PackageDetailId { get; set; }
        public int Order { get; set; }
        public string Name
        {
            get
            {
                var _name = Adult.ToString();
                if (SingleSupliment)
                    return "S-S";
                if (FreeOfCost > 0)
                    _name += $" + {FreeOfCost} FOC";
                return _name;
            }
        }
    }
}
