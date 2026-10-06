namespace Lab03_QuanLySinhVienOOP;

/// <summary>
/// Class cha: mô tả thông tin chung của một con người.
/// </summary>
public class Nguoi
{
    private string _hoTen = string.Empty;

    /// <summary>Họ tên, không được để trống.</summary>
    public string HoTen
    {
        get => _hoTen;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Họ tên không được để trống.");
            }
            _hoTen = value.Trim();
        }
    }

    /// <summary>Ngày sinh.</summary>
    public DateTime NgaySinh { get; set; }

    public Nguoi(string hoTen, DateTime ngaySinh)
    {
        HoTen = hoTen;
        NgaySinh = ngaySinh;
    }

    /// <summary>Trả về chuỗi thông tin; class con có thể override.</summary>
    public virtual string LayThongTin()
    {
        return $"Họ tên: {HoTen}, Ngày sinh: {NgaySinh:dd/MM/yyyy}";
    }
}
