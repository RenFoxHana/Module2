namespace Module2.Models;

public partial class Partner
{
    public int IdPartner { get; set; }

    public string TypePartner { get; set; } = null!;

    public string NamePartner { get; set; } = null!;

    public string LastNameDirectorPartner { get; set; } = null!;

    public string FirstNameDirectorPartner { get; set; } = null!;

    public string? PatronymicDirectorPartner { get; set; }

    public string EmailPartner { get; set; } = null!;

    public string PhonePartner { get; set; } = null!;

    public int IndexPartner { get; set; }

    public string RegionPartner { get; set; } = null!;

    public string CityPartner { get; set; } = null!;

    public string StreetPartner { get; set; } = null!;

    public string HousePartner { get; set; } = null!;

    public long InnPartner { get; set; }

    public int RatingPartner { get; set; }

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
    public string FullNamePartner => $"{TypePartner} | {NamePartner}";
    public string NameDirectorPartner => $"{LastNameDirectorPartner} {FirstNameDirectorPartner} {PatronymicDirectorPartner}";
    public string FullPhonePartner => $"+7 {PhonePartner}";
    public string Rating => $"Рейтинг: {RatingPartner}";
    public string Discount => $"{FindDiscount()} %";
    public int FindDiscount()
    {
        using (var context = new BochagovaDemExamContext())
        {
            long countProduct = context.PartnerProducts.Where(a => a.IdPartner == IdPartner).Sum(a => a.CountProduct);

            if (countProduct < 10000)
            {
                return 0;
            }
            else if (countProduct >= 10000 && countProduct < 50000)
            {
                return 5;
            }
            else if (countProduct >= 50000 && countProduct < 300000)
            {
                return 10;
            }
            else
            {
                return 15;
            }
        }
    }
}
