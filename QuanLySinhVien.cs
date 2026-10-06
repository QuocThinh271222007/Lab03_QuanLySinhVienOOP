using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVienOOP;

/// <summary>
/// Service quản lý danh sách sinh viên (lưu trong bộ nhớ bằng List&lt;SinhVien&gt;).
/// Program chỉ gọi các method của class này, không thao tác trực tiếp lên danh sách.
/// </summary>
public class QuanLySinhVien
{
    private readonly List<SinhVien> _danhSach = new();

    /// <summary>Thêm sinh viên. Trả về false nếu mã đã tồn tại.</summary>
    public bool Them(SinhVien sinhVien)
    {
        if (TimTheoMa(sinhVien.MaSinhVien) != null)
        {
            return false;
        }
        _danhSach.Add(sinhVien);
        return true;
    }

    /// <summary>Cập nhật điểm trung bình. Trả về false nếu không tìm thấy mã.</summary>
    public bool SuaDiem(string maSinhVien, double diemMoi)
    {
        SinhVien? sinhVien = TimTheoMa(maSinhVien);
        if (sinhVien == null)
        {
            return false;
        }
        sinhVien.DiemTrungBinh = diemMoi;
        return true;
    }

    /// <summary>Xóa sinh viên theo mã. Trả về false nếu không tìm thấy.</summary>
    public bool Xoa(string maSinhVien)
    {
        SinhVien? sinhVien = TimTheoMa(maSinhVien);
        if (sinhVien == null)
        {
            return false;
        }
        return _danhSach.Remove(sinhVien);
    }

    /// <summary>Tìm theo mã (không phân biệt hoa thường). Không thấy thì trả về null.</summary>
    public SinhVien? TimTheoMa(string maSinhVien)
    {
        string ma = (maSinhVien ?? string.Empty).Trim();
        return _danhSach.FirstOrDefault(sv =>
            string.Equals(sv.MaSinhVien, ma, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>Tìm các sinh viên có họ tên chứa từ khóa (không phân biệt hoa thường và dấu).</summary>
    public List<SinhVien> TimTheoTen(string tuKhoa)
    {
        string khoa = BoDau(tuKhoa ?? string.Empty).Trim();
        return _danhSach
            .Where(sv => BoDau(sv.HoTen).Contains(khoa, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>Trả về danh sách sắp xếp theo điểm giảm dần (không làm thay đổi danh sách gốc).</summary>
    public List<SinhVien> SapXepTheoDiem()
    {
        return _danhSach
            .OrderByDescending(sv => sv.DiemTrungBinh)
            .ThenBy(sv => sv.HoTen)
            .ToList();
    }

    /// <summary>Lọc các sinh viên đạt (điểm trung bình từ 5 trở lên).</summary>
    public List<SinhVien> LocSinhVienDat()
    {
        return _danhSach.Where(sv => sv.DiemTrungBinh >= 5).ToList();
    }

    /// <summary>Trả về bản sao danh sách để bên ngoài không sửa trực tiếp được.</summary>
    public List<SinhVien> LayDanhSach()
    {
        return new List<SinhVien>(_danhSach);
    }

    /// <summary>Bỏ dấu tiếng Việt để tìm kiếm thuận tiện hơn (vd: "Nguyen" khớp "Nguyễn").</summary>
    private static string BoDau(string text)
    {
        string normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }
        return sb.ToString()
                 .Normalize(NormalizationForm.FormC)
                 .Replace('đ', 'd')
                 .Replace('Đ', 'D');
    }
}
