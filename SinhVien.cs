namespace Lab03_QuanLySinhVienOOP;

/// <summary>
/// Sinh viên kế thừa từ Nguoi.
/// </summary>
public class SinhVien : Nguoi
{
    private string _maSinhVien = string.Empty;
    private string _maLop = string.Empty;
    private double _diemTrungBinh;

    /// <summary>Mã sinh viên, không được để trống.</summary>
    public string MaSinhVien
    {
        get => _maSinhVien;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Mã sinh viên không được để trống.");
            }
            _maSinhVien = value.Trim().ToUpper();
        }
    }

    /// <summary>Mã lớp, không được để trống.</summary>
    public string MaLop
    {
        get => _maLop;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Mã lớp không được để trống.");
            }
            _maLop = value.Trim().ToUpper();
        }
    }

    /// <summary>Điểm trung bình, chỉ nhận giá trị từ 0 đến 10.</summary>
    public double DiemTrungBinh
    {
        get => _diemTrungBinh;
        set
        {
            if (double.IsNaN(value) || value < 0 || value > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(DiemTrungBinh), "Điểm phải nằm trong khoảng 0 - 10.");
            }
            _diemTrungBinh = value;
        }
    }

    public SinhVien(string maSinhVien, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
        : base(hoTen, ngaySinh)
    {
        MaSinhVien = maSinhVien;
        MaLop = maLop;
        DiemTrungBinh = diemTrungBinh;
    }

    /// <summary>Xếp loại theo điểm trung bình.</summary>
    public string XepLoai()
    {
        if (DiemTrungBinh >= 9) return "Xuất sắc";
        if (DiemTrungBinh >= 8) return "Giỏi";
        if (DiemTrungBinh >= 6.5) return "Khá";
        if (DiemTrungBinh >= 5) return "Trung bình";
        return "Yếu";
    }

    /// <summary>Override: bổ sung thông tin sinh viên vào thông tin của Nguoi.</summary>
    public override string LayThongTin()
    {
        return $"Mã SV: {MaSinhVien}, {base.LayThongTin()}, Lớp: {MaLop}, " +
               $"Điểm TB: {DiemTrungBinh:0.00}, Xếp loại: {XepLoai()}";
    }
}
